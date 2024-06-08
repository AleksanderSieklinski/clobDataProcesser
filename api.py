from flask import Flask, request, jsonify
from flask_sqlalchemy import SQLAlchemy
from sqlalchemy import text

# 3.1 If the declared type of the column contains any of the strings "CHAR", "CLOB", or "TEXT" then that column has TEXT affinity. 
# Notice that the type VARCHAR contains the string "CHAR" and is thus assigned TEXT affinity.
# https://www.sqlite.org/datatype3.html
# 3.1.1 CLOB == TEXT

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///test.db'
db = SQLAlchemy(app)

class Document(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    content = db.Column(db.Text) # This will be treated as CLOB
    def __init__(self, id=None, content=None):
        self.id = id
        self.content = content
class AvailableId(db.Model):
    id = db.Column(db.Integer, primary_key=True)
def get_available_id():
    available_id = AvailableId.query.order_by(AvailableId.id).first()
    while available_id and db.session.get(Document, available_id.id):
        db.session.delete(available_id)
        db.session.commit()
        available_id = AvailableId.query.order_by(AvailableId.id).first()
    return available_id
@app.route('/', methods=['GET'])
def home():
    return "Hello, World!"
@app.route('/documents', methods=['POST'])
def create_document():
    content = request.json.get('content')
    available_id = get_available_id()
    if available_id:
        document = Document(id=available_id.id, content=content)
        db.session.delete(available_id)
    else:
        document = Document(content=content)
    db.session.add(document)
    db.session.commit()
    with db.engine.connect() as connection:
        connection.execute(text("INSERT INTO document_fts (docid, content) VALUES (:id, :content)"), {'id': document.id, 'content': content})
        connection.commit()
    return jsonify({'id': document.id}), 201
@app.route('/documents/<int:document_id>', methods=['PUT'])
def update_document(document_id):
    new_content = request.json.get('content')
    document = db.session.get(Document, document_id)
    if document is None:
        return jsonify({'error': 'Document not found'}), 404
    document.content = new_content
    db.session.commit()
    with db.engine.connect() as connection:
        connection.execute(text("UPDATE document_fts SET content = :content WHERE docid = :id"), {'id': document_id, 'content': new_content})
        connection.commit()
    return jsonify({'id': document.id}), 200
@app.route('/documents/<int:document_id>', methods=['DELETE'])
def delete_document(document_id):
    Document.query.filter_by(id=document_id).delete()
    db.session.add(AvailableId(id=document_id))
    db.session.commit()
    with db.engine.connect() as connection:
        connection.execute(text("DELETE FROM document_fts WHERE docid = :id"), {'id': document_id})
        connection.commit()
    return '', 204
@app.route('/search', methods=['GET'])
def search_documents():
    query = request.args.get('query')
    with db.engine.connect() as connection:
        result = connection.execute(text("SELECT * FROM document_fts WHERE document_fts MATCH :query"), {'query': query})
        rows = result.fetchall()
        column_names = result.keys()
    return jsonify([dict(zip(column_names, row)) for row in rows])
@app.route('/getAll', methods=['GET'])
def get_all_documents():
    with db.engine.connect() as connection:
        result = connection.execute(text("SELECT * FROM document_fts"))
        rows = result.fetchall()
        column_names = result.keys()
    return jsonify([dict(zip(column_names, row)) for row in rows])
if __name__ == '__main__':
    with app.app_context():
        db.create_all()
        with db.engine.connect() as connection:
            connection.execute(text("CREATE VIRTUAL TABLE IF NOT EXISTS document_fts USING fts5(content, docid UNINDEXED);"))
    app.run()