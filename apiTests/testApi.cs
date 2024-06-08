using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using appTests;
namespace appTests.Tests
{
    public class ApiTests
    {
        private DocumentLibrary library = new DocumentLibrary();
        [Fact]
        public async Task TestGetAll()
        {
            HttpResponseMessage response = await library.GetAll();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestPostDocument()
        {
            HttpResponseMessage response = await library.PostDocument("This is a test document.");
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact]
        public async Task TestUpdateDocument()
        {
            await library.PostDocument("This is a test document.");
            HttpResponseMessage response = await library.UpdateDocument("This is an updated test document.", "1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDeleteDocument()
        {
            HttpResponseMessage response = await library.DeleteDocument("1");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        [Fact]
        public async Task TestSearch()
        {
            HttpResponseMessage response = await library.Search("test");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}