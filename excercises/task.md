# ⚙️ Zadanie: Równoległe sprawdzanie stron – `async/await` i `HttpClient`

## 🎯 Cel
Zaimplementuj aplikację, która sprawdza dostępność stron internetowych równolegle z użyciem `async/await` oraz `HttpClient`. Celem jest praktyczne poznanie i przetestowanie współbieżnego programowania asynchronicznego.

---

## 📋 Opis zadania

Napisz program, który:

- Dla każdej strony:
  - pobiera kod odpowiedzi HTTP (np. 200, 404),
  - mierzy czas wykonania żądania,
  - wypisuje numer wątku (`Thread.CurrentThread.ManagedThreadId`) oraz nazwę adresu.

- Wykonuje te operacje **współbieżnie** za pomocą `Task` i `async/await`.

- Wszystkie operacje uruchamiane są równolegle, a na końcu program czeka na ich zakończenie (`Task.WhenAll`).

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

- Możesz użyć klasy `HttpClient`.
- Pamiętaj, by używać `async/await` w tej wersji.
- Obsłuż wyjątki (np. brak połączenia).
- Użyj klasy `Stopwatch` do pomiaru czasu.

---


## 🧰 Przykładowy rezultat na konsoli
```bash
[Async] Thread #5 checking https://example.com...
[Async] Thread #6 checking https://github.com...
[Async] Thread #7 checking https://microsoft.com...
[Async] Thread #6 done https://github.com - Status: 200 - Time: 432ms
[Async] Thread #5 done https://example.com - Status: 200 - Time: 530ms
[Async] Thread #7 done https://microsoft.com - Status: 200 - Time: 740ms

```


## 🚀 Rozszerzenia (dla chętnych)
- Zapisz wyniki do pliku CSV.