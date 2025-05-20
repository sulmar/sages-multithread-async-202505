# 🧪 Zadanie: Porównanie Thread vs ThreadPool

## 🎯 Cel
Zaimplementuj aplikację, która sprawdza dostępność kilku stron internetowych równolegle. Celem jest porównanie podejść: `Thread` vs `ThreadPool`, zrozumienie różnic i skutków wydajnościowych.

---

## 📋 Opis zadania

Napisz dwie wersje programu:

- **Wersja 1:** wykorzystuje `Thread` – każda operacja wykonywana w osobnym wątku.
- **Wersja 2:** wykorzystuje `ThreadPool` – operacje są przekazywane do puli wątków zarządzanej przez .NET.

### ✅ Dla każdej strony:
- Pobierz kod odpowiedzi HTTP (np. 200, 404, 500…).
- Zmierz czas wykonania (np. `Stopwatch`).
- Wypisz numer wątku (`Thread.CurrentThread.ManagedThreadId`).

---

## 🌐 Lista testowych URL-i

```text
https://example.com
https://google.com
https://microsoft.com
https://github.com
https://dotnet.microsoft.com
https://wikipedia.org
https://stackoverflow.com
https://www.lipsum.com/
https://bing.com
https://openai.com
```

## 💡 Wskazówki

- Możesz użyć klasy `WebClient` lub `HttpClient`.
- Pamiętaj, by nie używać `async/await` w tej wersji.
- Obsłuż wyjątki (np. brak połączenia).
- Użyj klasy `Stopwatch` do pomiaru czasu.

---

## 🧰 Przykładowy rezultat na konsoli
```bash
[ThreadPool] Thread #9 working on https://example.com
[ThreadPool] Thread #10 working on https://google.com
[ThreadPool] Thread #11 working on https://github.com
[ThreadPool] Thread #12 working on https://microsoft.com
[ThreadPool] Thread #13 working on https://wikipedia.org
[ThreadPool] Thread #9 completed https://example.com - Status: 200 - Time: 302ms
[ThreadPool] Thread #11 completed https://github.com - Status: 200 - Time: 513ms
[ThreadPool] Thread #10 completed https://google.com - Status: 200 - Time: 601ms
[ThreadPool] Thread #12 completed https://microsoft.com - Status: 200 - Time: 822ms
[ThreadPool] Thread #13 completed https://wikipedia.org - Status: 200 - Time: 1004ms
```


## 🚀 Rozszerzenia (dla chętnych)
- Zapisz wyniki do pliku CSV.