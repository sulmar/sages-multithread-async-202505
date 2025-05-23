# ✅ Checklist: Przegląd kodu asynchronicznego i wielowątkowego w aplikacjach webowych (.NET)

## 🔄 Asynchroniczność i Taski

| ✅ | Pytanie | Przykład |
|----|--------|----------|
| ✔️ | Czy metoda nie używa `.Result` lub `.Wait()`? | ❌ `var r = DoAsync().Result;` <br> ✅ `var r = await DoAsync();` |
| ✔️ | Czy metoda ma sygnaturę `async Task` / `async Task<T>`? | Unikaj `async void` poza eventami |
| ✔️ | Czy asynchroniczne wywołania są `awaitowane`? | `await service.SendEmailAsync()` |
| ✔️ | Czy `ConfigureAwait(false)` jest stosowane tylko w bibliotekach? | ✅ `await FooAsync().ConfigureAwait(false)` |
| ✔️ | Czy `Task.WhenAll(...)` lub `Parallel.ForEachAsync(...)` są używane do zadań równoległych? | ✅ zamiast sekwencyjnych `await` |

---

## 📦 BackgroundService i przetwarzanie w tle

| ✅ | Pytanie | Przykład |
|----|--------|----------|
| ✔️ | Czy `BackgroundService` honoruje `CancellationToken`? | `while (!ct.IsCancellationRequested)` |
| ✔️ | Czy używasz `Channel<T>` lub `BlockingCollection` jako bufora danych? | `await channel.Writer.WriteAsync(...)` |
| ✔️ | Czy logika przetwarzania obsługuje wyjątki (try-catch)? | wewnątrz `ExecuteAsync()` |
| ✔️ | Czy są limity równoległości (np. `SemaphoreSlim`)? | przy przetwarzaniu wielu zadań jednocześnie |

---

## 🧵 Bezpieczeństwo współbieżności

| ✅ | Pytanie | Przykład |
|----|--------|----------|
| ✔️ | Czy Singleton nie przechowuje stanu zależnego od żądania? | ❌ `public string LastUser;` |
| ✔️ | Czy dostęp do wspólnych danych jest zabezpieczony (lock, `SemaphoreSlim`, `Channel`)? | |
| ✔️ | Czy dane żądania są przechowywane lokalnie (`HttpContext.Items`)? | |

---

## ⚙️ Integracje i zależności

| ✅ | Pytanie | Przykład |
|----|--------|----------|
| ✔️ | Czy `HttpClient` jest wstrzykiwany przez `IHttpClientFactory`? | ✅ `services.AddHttpClient(...)` |
| ✔️ | Czy retry, timeout i limity są ustawione przy wywołaniach zewnętrznych? | ❌ Brak `TimeoutPolicy` |

---

## 🔐 Stabilność i odporność

| ✅ | Pytanie | Przykład |
|----|--------|----------|
| ✔️ | Czy wyjątki są logowane i nie są „połykane”? | `try { ... } catch (Exception ex) { _logger.LogError(...) }` |
| ✔️ | Czy długo trwające operacje są delegowane do `BackgroundService` lub `Task.Run(...)`? | |

---

## 🛠️ Testowalność i struktura

| ✅ | Pytanie | Przykład |
|----|--------|----------|
| ✔️ | Czy kod jest podzielony na serwisy/interfejsy (np. `IEmailService`)? | |
| ✔️ | Czy metody nie robią zbyt wiele (SRP)? | |
| ✔️ | Czy można łatwo zasymulować async metodę w testach (np. z Moq)? | `Task.FromResult(...)` |

---

📌 Stosuj tę listę podczas code review, refaktoryzacji lub tworzenia nowych komponentów backendowych.
