# Projekt Semestralny: Przetwarzanie danych typu CLOB

## Wstęp Teoretyczny

W dzisiejszych czasach gromadzenie i przetwarzanie dużych ilości danych jest nieodłączną częścią pracy w wielu dziedzinach, w tym w biznesie, nauce i technologii. Jednym z rodzajów danych, które mogą być wykorzystywane, są dane typu CLOB (Character Large Object), które pozwalają na przechowywanie dużych bloków tekstu w bazach danych.

CLOB przechowuje różne rodzaje danych, takie jak artykuły, raporty czy dokumenty. W celu efektywnego zarządzania i wykorzystania tych danych, konieczne jest API, które umożliwią manipulację tymi danymi, w tym zapis, usuwanie i wyszukiwanie.

## Zakres Funkcjonalności

Celem projektu jest opracowanie API oraz jego implementacja do przetwarzania danych typu CLOB. Główne funkcjonalności, które będą dostępne poprzez to API, obejmują:

1. **Zapis dokumentu:** Umożliwienie użytkownikowi zapisu nowego dokumentu typu CLOB do repozytorium danych. API powinno przyjmować jako parametr dane tekstowe dokumentu.

2. **Usunięcie dokumentu:** Zapewnienie możliwości usunięcia wybranego dokumentu z repozytorium na podstawie jego identyfikatora ID.

3. **Wyszukiwanie informacji:** Implementacja mechanizmu wyszukiwania pełnotekstowego (FTS - Full Text Search), który umożliwi wyszukiwanie określonej informacji w dokumentach znajdujących się w repozytorium. Użytkownik powinien mieć możliwość wyszukiwania na podstawie frazy lub słów kluczowych.

4. **Edycja dokumentu:** Opcjonalnie, jeśli projekt będzie rozwijany, można rozważyć dodanie funkcjonalności edycji istniejących dokumentów.

## Opis API

### Endpoints:

1. **POST /documents**
   - Opis: Endpoint do zapisywania nowego dokumentu.
   - Parametry żądania:
     - `content`: Tekst dokumentu (wymagane)
   - Odpowiedź: Status Created w przypadku powodzenia, zawierający identyfikator nowo utworzonego dokumentu.

2. **DELETE /documents/{document_id}**
   - Opis: Endpoint do usuwania dokumentu na podstawie jego identyfikatora.
   - Parametry żądania:
     - `document_id`: Identyfikator dokumentu (wymagane)
   - Odpowiedź: Status No Content w przypadku powodzenia.

3. **GET /search**
   - Opis: Endpoint do wyszukiwania dokumentów na podstawie zadanej frazy.
   - Parametry żądania:
     - `query`: Fraza lub słowa kluczowe do wyszukania (wymagane)
   - Odpowiedź: Lista dokumentów pasujących do zadanego zapytania.

4. **GET /getAll**
   - Opis: Endpoint do wyświetlenia wszystkich dokumentów w bazie.
   - Odpowiedź: Lista wszystkich dokumentów.

5. **PUT /documents/{document_id}**
    - Opis: Endpoint do aktualizowania dokumentu na podstawie jego identyfikatora.
    - Parametry żądania:
      - `document_id`: Identyfikator dokumentu (wymagane)
      - `content`: Nowy tekst dokumentu (wymagane)
    - Odpowiedź: Status OK w przypadku powodzenia, zawierający identyfikator zaktualizowanego dokumentu.

## Prezentacja Przeprowadzonych Testów Jednostkowych

Testy jednostkowe zostały przeprowadzone w C#. Testy sprawdzają, czy wszystkie metody API działają poprawnie i zwracają oczekiwane wyniki. Poniżej przedstawiamy krótki opis każdego z testów:

TestGetAll: Test sprawdza, czy metoda GetAll zwraca status HTTP 200 (OK). Metoda ta powinna zwrócić wszystkie dokumenty przechowywane w repozytorium.

TestPostDocument: Test sprawdza, czy metoda PostDocument zwraca status HTTP 201 (Created) po dodaniu nowego dokumentu do repozytorium.

TestUpdateDocument: Test sprawdza, czy metoda UpdateDocument zwraca status HTTP 200 (OK) po aktualizacji istniejącego dokumentu. Przed aktualizacją, test zapewnia, że dokument istnieje w repozytorium.

TestDeleteDocument: Test sprawdza, czy metoda DeleteDocument zwraca status HTTP 204 (No Content) po usunięciu dokumentu z repozytorium.

TestSearch: Test sprawdza, czy metoda Search zwraca status HTTP 200 (OK) po przeszukaniu repozytorium w poszukiwaniu dokumentów zawierających określone słowo kluczowe.

Wszystkie testy są asynchroniczne i korzystają z HttpClient do wysyłania żądań HTTP do API.

## Prezentacja Przykładowej Aplikacji

Aplikacja jest klientem API, który komunikuje się z serwerem do zarządzania dokumentami typu CLOB. Aplikacja została napisana w języku C# i korzysta z HttpClient do wysyłania żądań HTTP do serwera.

Aplikacja zawiera klasę DocumentLibrary, która zawiera metody do wykonywania operacji na dokumentach, takich jak:

GetAll: Ta metoda wysyła żądanie GET do endpointu /getAll serwera, który zwraca wszystkie dokumenty przechowywane w repozytorium.

PostDocument: Ta metoda wysyła żądanie POST do endpointu /documents serwera, który dodaje nowy dokument do repozytorium. Metoda ta przyjmuje jako parametr treść dokumentu.

UpdateDocument: Ta metoda wysyła żądanie PUT do endpointu /documents/{id} serwera, który aktualizuje istniejący dokument w repozytorium. Metoda ta przyjmuje jako parametry treść dokumentu i identyfikator dokumentu.

DeleteDocument: Ta metoda wysyła żądanie DELETE do endpointu /documents/{id} serwera, który usuwa dokument z repozytorium. Metoda ta przyjmuje jako parametr identyfikator dokumentu.

Search: Ta metoda wysyła żądanie GET do endpointu /search serwera, który przeszukuje repozytorium w poszukiwaniu dokumentów zawierających określone słowo kluczowe. Metoda ta przyjmuje jako parametr słowo kluczowe.

Wszystkie metody są asynchroniczne i zwracają odpowiedź serwera jako obiekt HttpResponseMessage.

## Podsumowanie i Wnioski

Podsumowanie:

Projekt polegał na opracowaniu i implementacji API do przetwarzania danych typu CLOB. API umożliwia zapis, usuwanie, wyszukiwanie i edycję dokumentów przechowywanych w repozytorium. Aplikacja kliencka napisana w C# korzysta z tego API do zarządzania dokumentami.

Wnioski:

Opracowanie API do przetwarzania danych typu CLOB pozwoliło na efektywne zarządzanie tymi danymi, umożliwiając użytkownikom łatwe zapisywanie, wyszukiwanie, edycję i usuwanie informacji. Wykorzystanie technologii wyszukiwania pełnotekstowego (FTS) pozwoliło na szybkie wyszukiwanie informacji w dużych zbiorach danych tekstowych. Projekt pokazał, jak ważne jest posiadanie dobrze zaprojektowanego i zaimplementowanego API do zarządzania danymi.

## Literatura

1. Flask Documentation. https://flask.palletsprojects.com/en/2.0.x/
2. C# Documentation. https://learn.microsoft.com/en-us/dotnet/csharp/

## Dołączone Kody Źródłowe

Wszystkie kody źródłowe dostępne są w repozytorium projektu