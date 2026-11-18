
# 🌦️ Stacja pogodowa

## [01] Informacje o projekcie

**Stacja pogodowa** to aplikacja desktopowa wykonana w technologii WPF (Windows Presentation Foundation) z wykorzystaniem języka C#.

Głównym zadaniem programu jest zarządzanie pomiarami temperatury pochodzącymi z różnych stacji pogodowych. Aplikacja umożliwia odczytywanie danych z plików tekstowych, dodawanie nowych pomiarów, zapisywanie ich do pliku oraz analizowanie temperatur.

Program posiada graficzny interfejs użytkownika, który pozwala na wygodne przeglądanie i filtrowanie zgromadzonych danych.

---

## [02] Wykorzystane technologie

- **C#** – język programowania wykorzystany do stworzenia logiki aplikacji.
- **WPF** – technologia służąca do budowania interfejsu graficznego.
- **XAML** – język znaczników wykorzystywany do definiowania wyglądu okna i jego elementów.
- **.NET** – platforma uruchomieniowa aplikacji.
- **LINQ** – narzędzie wykorzystywane do wyszukiwania, filtrowania i analizowania pomiarów.

---

## [03] Funkcjonalności aplikacji

### [03.1] Odczyt danych z pliku
Program umożliwia wczytywanie pomiarów pogodowych z plików tekstowych za pomocą okna wyboru pliku.

Po wybraniu pliku aplikacja odczytuje jego zawartość i przekształca poszczególne linie na obiekty pomiarów.

### [03.2] Obsługa separatora danych
Dane w pliku oddzielone są średnikiem (`;`).

Każdy wiersz zawiera trzy informacje:
- Nazwę stacji pogodowej.
- Datę wykonania pomiaru.
- Wartość temperatury.

### [03.3] Dodawanie nowych pomiarów
Użytkownik może samodzielnie dodać nowy pomiar za pomocą formularza.

Formularz zawiera:
- Pole tekstowe do wpisania nazwy stacji.
- Wybór daty pomiaru.
- Pole do wpisania temperatury.
- Przycisk dodawania pomiaru.

Przed dodaniem danych program sprawdza poprawność wprowadzonych informacji.

### [03.4] Zapisywanie pomiarów do pliku
Aplikacja umożliwia eksport zgromadzonych pomiarów do pliku tekstowego.

Dane zapisywane są w tym samym formacie, w którym są odczytywane. Dzięki temu można ponownie wczytać wcześniej zapisane pomiary.

### [03.5] Wyszukiwanie temperatury MAX i MIN
Program analizuje zgromadzone pomiary i wyszukuje:
- MAX – najwyższą zarejestrowaną temperaturę.
- MIN – najniższą zarejestrowaną temperaturę.

Wyniki są automatycznie oznaczane w tabeli.

### [03.6] Podświetlanie rekordów
W celu ułatwienia analizy danych zastosowano kolorowanie wierszy tabeli:
- Zielony – rekord z maksymalną temperaturą.
- Niebieski – rekord z minimalną temperaturą.

Oznaczenia są aktualizowane po odświeżeniu danych.

### [03.7] Filtrowanie według stacji pogodowej
Aplikacja posiada listę rozwijaną, która umożliwia wybór konkretnej stacji pogodowej.

Po wybraniu stacji tabela wyświetla wyłącznie pomiary przypisane do tej lokalizacji.

Dostępna jest również opcja wyświetlania wszystkich stacji jednocześnie.

### [03.8] Wyświetlanie pomiarów
Dane prezentowane są w tabeli DataGrid.

Tabela zawiera:
- Nazwę stacji.
- Datę pomiaru.
- Temperaturę w stopniach Celsjusza.

Lista umożliwia przewijanie, dzięki czemu można wygodnie przeglądać większą liczbę rekordów.

---

## [04] Format pliku danych

Dane przechowywane są w pliku tekstowym `dane.txt`.

### Struktura pojedynczego rekordu

```text
NazwaStacji;Data;Temperatura
```

### Przykładowa zawartość pliku

```text
Warszawa;2026-11-18;12
Kraków;2026-11-18;-4
Poznań;2026-11-18;8
Wrocław;2026-11-18;15
Gdańsk;2026-11-18;6
```

### Zasady przechowywania danych

- Każdy pomiar znajduje się w osobnym wierszu.
- Poszczególne wartości oddzielone są średnikiem.
- Data zapisywana jest w formacie `RRRR-MM-DD`.
- Temperatura może być dodatnia lub ujemna.
- Wartości temperatury przechowywane są jako liczby.

---

## [05] Struktura projektu

```text
WpfApp1/
│
├── App.xaml
├── App.xaml.cs
│
├── MainWindow.xaml
│   └── MainWindow.xaml.cs
│
├── dane.txt
│
├── AssemblyInfo.cs
│
└── README.md
```

### Opis najważniejszych plików

**MainWindow.xaml**
- Definiuje wygląd głównego okna aplikacji.
- Zawiera tabelę pomiarów.
- Zawiera formularz dodawania danych.
- Zawiera listę wyboru stacji pogodowej oraz przyciski obsługi plików.

**MainWindow.xaml.cs**
- Odpowiada za logikę działania programu.
- Przechowuje listę pomiarów.
- Obsługuje dodawanie nowych rekordów.
- Odpowiada za odczyt i zapis plików.
- Wykonuje filtrowanie i wyszukiwanie temperatur MAX/MIN.

**dane.txt**
- Przechowuje przykładowe pomiary pogodowe.
- Stanowi plik wejściowy dla aplikacji.

**App.xaml**
- Zawiera konfigurację aplikacji WPF.

**README.md**
- Zawiera dokumentację projektu i opis jego funkcjonalności.

---

## [06] Instrukcja uruchomienia

1. Otwórz projekt w programie Visual Studio.
2. Sprawdź, czy plik `dane.txt` znajduje się w katalogu projektu.
3. Ustaw właściwości pliku `dane.txt`:
   - Akcja kompilacji: `Content`.
   - Kopiuj do katalogu wyjściowego: `Copy if newer`.
4. Uruchom aplikację za pomocą klawisza `F5`.
5. Korzystaj z dostępnych funkcji programu.

---

## [07] Instrukcja obsługi

### Wczytywanie danych
1. Kliknij przycisk „Wczytaj plik”.
2. Wybierz plik tekstowy z pomiarami.
3. Poczekaj na załadowanie danych do tabeli.

### Dodawanie pomiaru
1. Wpisz nazwę stacji pogodowej.
2. Wybierz datę.
3. Wprowadź temperaturę.
4. Kliknij „Dodaj pomiar”.

### Filtrowanie danych
1. Rozwiń listę wyboru stacji.
2. Wybierz interesującą Cię stację.
3. Tabela wyświetli pomiary tylko z wybranej lokalizacji.

### Zapisywanie danych
1. Kliknij „Zapisz pomiary”.
2. Wybierz lokalizację zapisu.
3. Podaj nazwę pliku.
4. Zatwierdź zapis.

---

## [08] Obsługa błędów

Aplikacja zawiera podstawową walidację danych:
- Sprawdzanie, czy nazwa stacji została podana.
- Sprawdzanie, czy wybrano datę.
- Sprawdzanie poprawności temperatury.
- Pomijanie niepoprawnych rekordów podczas odczytu pliku.
- Obsługa wyjątków podczas odczytu i zapisu plików.

W przypadku wystąpienia błędu użytkownik otrzymuje odpowiedni komunikat.

---

## [09] Podsumowanie

Projekt przedstawia praktyczne wykorzystanie technologii WPF oraz języka C# do stworzenia aplikacji desktopowej.

Podczas tworzenia aplikacji wykorzystano:
- Obsługę plików tekstowych.
- Operacje na kolekcjach obiektów.
- Programowanie obiektowe.
- Filtrowanie i sortowanie danych za pomocą LINQ.
- Zdarzenia i obsługę interfejsu użytkownika.
- Wiązanie danych z kontrolką DataGrid.
- Definiowanie stylów i wyzwalaczy w XAML.

Aplikacja umożliwia podstawowe zarządzanie pomiarami temperatury oraz ich analizę w czytelnym interfejsie graficznym.

---

**Projekt edukacyjny – Stacja pogodowa**  
**Język:** C#  
**Technologia:** WPF  
**Platforma:** .NET
