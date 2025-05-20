# 🧪 Zadanie: Asynchroniczne rozliczenie podatku małżeństwa


## Kontekst:
Wyobraź sobie system do rozliczania podatków, który umożliwia wspólne rozliczenie podatkowe dla małżeństw. Każda osoba posiada własne źródła dochodu, które muszą zostać przetworzone osobno, a następnie wynik zostaje połączony w jedno wspólne rozliczenie.

## 🎯 Cel:
Zaimplementuj asynchroniczne obliczanie podatku dla dwóch osób (małżeństwa), a następnie połącz wyniki w jeden wspólny wynik.

---

## 📋 Założenia:
- Każda osoba posiada listę dochodów (np. z umowy o pracę, najmu, itp.).
- Każdy dochód może być przeliczany asynchronicznie (np. symulując zewnętrzne źródło danych).
- Oblicz podatek indywidualny osobno dla każdego małżonka.
- Na końcu połącz wyniki i oblicz wspólny podatek (np. uśredniając dochód, stosując ulgę itd.).

## ✅ Wymagania:
- Zastosuj Task i async/await.
- Zrównoleglij obliczenia dla dwóch osób.
- Obsłuż potencjalne wyjątki (np. brak dochodów).

## 🔧 Przykładowa sygnatura klasy (dla inspiracji):
```cs
public class IncomeSource
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
}

public class TaxCalculator
{
    public async Task<decimal> CalculateTaxAsync(List<IncomeSource> incomes)
    {
        // Symuluj zewnętrzne źródła danych
    }
}

public class JointTaxService
{
    public async Task<decimal> CalculateJointTaxAsync(
        List<IncomeSource> spouse1,
        List<IncomeSource> spouse2)
    {
        // Oblicz podatek dla obu osób, a potem wspólny
    }
}
```

## 💡 Wskazówki:
- Możesz zasymulować losowe opóźnienie `await Task.Delay(...)` w obliczeniach pojedynczego dochodu za pomocą metody `Random.Shared.Next`
- Zdefiniuj własną logikę wspólnego rozliczenia (np. suma dochodów, wspólna stawka podatkowa, ulga itd.).



## 🚀 Rozszerzenia (dla chętnych)
- Dodaj możliwość przerwania obliczeń
- Dodaj raportowanie postępu dla każdego dochodu po zakończeniu jego przetwarzania.