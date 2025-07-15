# LottoApp – "6 aus 49"

Eine einfache **WPF-Desktop-Anwendung**, die das klassische deutsche Lotto **„6 aus 49“** simuliert. Entwickelt in **C# und WPF**. Wähle 6 Zahlen aus, starte die Ziehung und finde heraus, wie viel du gewonnen hättest.

---

## Funktionen

- Auswahl von bis zu 6 Zahlen (1–49)
- Zufällige Ziehung von 6 Gewinnzahlen
- Farbige Hervorhebung:
  - Grün: Ausgewählte Zahl
  - Gelb: Gezogene Gewinnzahl
- Gewinnberechnung basierend auf der Anzahl der Treffer
- Möglichkeit, erneut zu spielen

---

## Funktionsweise

### Zahlenauswahl

- Klick auf eine Zahl (1–49) → Zahl wird grün markiert
- Zweiter Klick → Auswahl wird aufgehoben
- Maximal 6 Zahlen können ausgewählt werden; bei mehr erscheint eine Warnung

### Ziehung

- Klick auf **"Spielen"**:
  - 6 zufällige Gewinnzahlen werden generiert
  - Treffer werden gezählt
  - Die Anzahl der richtigen Zahlen wird angezeigt
  - Gewinn wird berechnet:

| Richtige Zahlen | Gewinn (€) |
|-----------------|------------|
| 3               | 50         |
| 4               | 300        |
| 5               | 1.000      |
| 6               | 10.000     |
| < 3             | 0          |

### Buttons

- **Spielen** – Gewinnzahlen ziehen, vergleichen, Ergebnis anzeigen
- Auswahl kann durch erneutes Klicken auf eine Zahl zurückgenommen werden

---

## Technischer Aufbau

- Programmiersprache: C#
- UI: WPF (.NET Framework)
- Nutzung von Arrays und Zufallszahlen für Logik und Ziehung
- Farbige Buttons zur Anzeige von Status (Ausgewählt, Gewinnzahl)


