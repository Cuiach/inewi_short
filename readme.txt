2024.04.24 NIEAKTUALNE - jest check
W apce można dowolnie zmienić limity urlopowe za poprzednie lata i nie sprawdzam możliwości do tyłu (tak jak np. przy zmianie polityki wykorzystywania zaległych urlopów). Można by sprawdzać, ale zakładam, że nie trzeba na razie. W kazdym razie jest to potencjalny punkt zapalny, którym można sobie uniemożliwić wpisywanie nowych urlopów, bo sprawdzenie polityki urlopów za poprzednie lata wykaże błąd.

2024.04.17 NIEAKTUALNE - ZROBIONE
Nie wiem, jak zrobić aktualizację limitów urlopowych na kolejny rok w przypadku, gdy nastał styczeń nowego roku. Do przemyślenia.

2024.04.15 NIEAKTUALNE - JUŻ OBSŁUGUJĘ 
Na razie nie obsługuję sytuacji, gdy ktoś wpisuje urlop np. w 2022, bo wygląda na to, że może być dodany wtedy, lecz jednak dni urlopowe za 2022 przeszły na 2023 i zostały wykorzystane w 2023. Nie sprawdzam na razie takiego przypadku.
W SUMIE WARTO, BO TO NA LOGIKĘ JEST CHYBA BARDZIEJ.

2024.04.11 NIEAKTUALNE - SPRAWDZAM 
Decyzja, że nie sprawdzam, czy daty urlopów się pokrywają. Zresztą nie sprawdzam też gdy urlop zahacza o 2 lata. Można to zakodować później, ale chyba nie o to chodzi w zadaniu.
Zakładam happy path - wpisujący urlopy się nie myli.

AKTUALNE
Decyzja, aby trzymać 2 tabele (TYLKO ŻE NA RAZIE SAMA APKA BEZ EF I DB):
a) z urlopami: 
Id, EmployeeId, od, do, czy NŻ
b) z pracownikami: 
Id, imię itp, rok zatrudnienia, ile lat można wykorzystać urlop, liczba dni urlopowych ustawiona na aktualny rok, NŻ na aktualny rok oraz lista limitów na poszczególne lata (2021/limit itd).

2024.04.08 NIEAKTUALNE
Zakładam dla uproszczenia, że pracownik jest przez cały pierwszy rok i jest tylko jedna liczba określająca jego dni urlopowe dla każdego roku od pierwszego włącznie.
EDIT: Lista rok + limit

2024.04.04 NIEAKTUALNE
Decyzja, aby trzymać 2 tabele:
a) z urlopami: 
Id, EmployeeId, od, do, czy NŻ
b) z pracownikami: 
Id, imię itp, rok zatrudnienia, ile lat można wykorzystać urlop, liczba dni urlopowych ustawiona na aktualny rok, NŻ na aktualny rok,
urlopy niewykorzystane z poprzednich lat, urlopy wzięte w tym roku, NŻ wzięte w tym roku

AKTUALNE
Urlopy zakładają ciągłośc dat w ramach danego roku i w ramach NŻ / nieNŻ. Czyli urlop od grudnia do stycznia oznacza 2 urlopy w systemie. Urlop, w którym pierwsze kilka dni jest NŻ a reszta zwykła, oznacza 2 urlopy w systemie. NŻ / nieNŻ - każdy trzeba będzie osobno wyklikać. Nie wiem, jak z przełomem roku (wydaje się, że trzeba podzielić dla większej prostoty).
Dla uproszczenia nie biorę pod uwagę dni wolnych od pracy :D. Obsługa tego byłaby osobnym dużym wyzwaniem.

NIEAKTUALNE Dla uproszczenia podaje się limity dni urlopów i NŻ po prostu dla danego pracownika. Można je potem zmienić, ale nie odróżnia się między latami (np. pierwszy rok i następne).

2024.04.03 NIEAKTUALNE 
Decyzja, aby w bazie danych trzymać tabelę Employees oraz tabele dla każdego roku osobne Leaves_rok (np. Leaves_2024) z:
a) podsumowaniem roku dla danego pracownika (liczba dni z poprzedniego roku, liczba wykorzystanych już dni, liczba dni NŻ, liczba wykorzystanych NŻ)
b) urlopami
c) komentarzami read only (data wpisania, edycji lub usunięcia urlopu - przy czym nie zakoduję tego teraz, żeby nie przedłużać).
Tabela Leaves_2024: wiersz = jeden pracownik. (Dla uproszczenia nie będę brał pod uwagę usuwania pracowników i czyszczenia bazy danych dla kolejnego roku (można to dokodować później)).
