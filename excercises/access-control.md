# 🧠 Zadanie problemowe: Kontrolowany dostęp do pomieszczenia z automatycznym generowaniem raportu


## Kontekst:
System IoT został zamontowany przy wejściu do pomieszczenia o ograniczonym dostępie (np. laboratorium lub serwerowni). Przy wejściu znajduje się kamera wyposażona w funkcję skanowania twarzy oraz urządzenie do generowania i zapisywania dokumentów potwierdzających wejście (np. raport PDF z datą, czasem, identyfikatorem osoby).

## Założenia:
- Jednocześnie w pomieszczeniu mogą przebywać maksymalnie 3 osoby.
- Każda osoba musi przejść proces skanowania twarzy (symulowany opóźnieniem czasowym).
- Po pozytywnej identyfikacji należy wygenerować raport wejścia.
- System musi być odporny na błędy oraz umożliwiać obsługę wielu osób próbujących wejść w tym samym czasie.
- Należy prowadzić historię dostępu (np. w pamięci lub pliku).
- Czasem skan twarzy może się nie powieść (symuluj np. 10% błędu).

## Cel zadania:
- Zaimplementuj system obsługujący wejścia do pomieszczenia zgodnie z powyższymi warunkami. 

## Zadbaj o:
- **Współbieżność** — wiele osób może próbować wejść równocześnie.
- **Synchronizację** — dostęp musi być ograniczony do maksymalnej liczby osób.
- **Bezpieczeństwo danych** — nie może dojść do błędów w raportach ani przekroczenia liczby osób.