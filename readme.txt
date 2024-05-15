2024-05-13
As far as now the application has few unit tests (to do soon).
And it may be extended with a database and Entity Framework.

20240-04-11 
AKTUALNE
Decyzja, aby trzymać 2 tabele (TYLKO ŻE NA RAZIE SAMA APKA BEZ EF I DB):
a) z urlopami: 
Id, EmployeeId, od, do, czy NŻ
b) z pracownikami: 
Id, imię itp, rok zatrudnienia, ile lat można wykorzystać urlop, liczba dni urlopowych ustawiona na aktualny rok, NŻ na aktualny rok oraz lista limitów na poszczególne lata (2021/limit itd).

2024-04-04
AKTUALNE
Urlopy zakładają ciągłośc dat w ramach danego roku i w ramach NŻ / nieNŻ. Czyli urlop od grudnia do stycznia oznacza 2 urlopy w systemie. Urlop, w którym pierwsze kilka dni jest NŻ a reszta zwykła, oznacza 2 urlopy w systemie. NŻ / nieNŻ - każdy trzeba będzie osobno wyklikać. Nie wiem, jak z przełomem roku (wydaje się, że trzeba podzielić dla większej prostoty).
Dla uproszczenia nie biorę pod uwagę dni wolnych od pracy :D. Obsługa tego byłaby osobnym dużym wyzwaniem.
