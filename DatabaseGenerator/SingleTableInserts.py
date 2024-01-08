import datetime

import random

from Randoms import randomNameOfSomething, randomTitle, randomImage, randomPassword, randomUsername,\
                    randomEmail, HigherDate, Data, Name, Surname


payment_methods = ["Gotówka", "Blik", "Karta Visa"]

def Gatunek(how_many,cursor,conn):

    gatunek = []
    MaxId = 0

    cursor.execute("SELECT MAX(id_gatunku) FROM GATUNEK")
    row = cursor.fetchone()

    if row[0] is not None:
        MaxId = row[0]

    for i in range(MaxId + 1, how_many + MaxId + 1):
        gatunek += [(i, randomNameOfSomething())]

    cursor.executemany("INSERT INTO Gatunek (id_gatunku,Nazwa) values(:1, :2)", gatunek)
    conn.commit()
    print("Poprawnie wstawiono wiersze do tabeli Gatunek")

def Autor(how_many,cursor,conn):

    autor = []
    MaxId = 0

    cursor.execute("SELECT MAX(id_autora) FROM AUTOR")
    row = cursor.fetchone()

    if row[0] is not None:
        MaxId = row[0]

    for i in range(MaxId + 1, how_many + MaxId + 1):
        autor += [(i, Name(), Surname())]

    cursor.executemany("INSERT INTO Autor (id_autora,Imie,Nazwisko) values(:1, :2, :3)", autor)
    conn.commit()
    print("Poprawnie wstawiono wiersze do tabeli Autor")

def Ksiazka(how_many,cursor,conn):

    ksiazka = []
    MaxId = 0

    cursor.execute("SELECT MAX(id_ksiazki) FROM KSIAZKA")
    row = cursor.fetchone()

    if row[0] is not None:
        MaxId = row[0]

    for i in range(MaxId + 1, how_many + MaxId + 1):
        ksiazka += [(i, randomNameOfSomething(), randomTitle(), random.randint(1990, 2023), random.uniform(15.50, 40.20), randomImage())]

    cursor.executemany(
        "INSERT INTO Ksiazka (id_ksiazki,Wydawnictwo,Tytul,Rok_wydania,Cena,Zdjecie) values(:1, :2, :3, :4, :5, :6)",
        ksiazka)
    conn.commit()
    print("Poprawnie wstawiono wiersze do tabeli Ksiażka")

def gatunek_ksiazka(how_many,cursor,conn):

    #do poprawy tak jak jest w autorze
    gatunek_ksiazka = []
    rowGatunek = []
    rowKsiazka = []
    baseGatunek = []
    baseKsiazka = []

    cursor.execute("SELECT id_gatunku FROM GATUNEK")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowGatunek += row
        if row is None:
            break

    cursor.execute("SELECT id_ksiazki FROM KSIAZKA")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowKsiazka += row
        if row is None:
            break

    if(len(rowKsiazka) == 0 or len(rowGatunek) == 0):
        print("Nie można wykonać zapytania ponieważ nie istnieją rekordy nadrzędne (Gatunek lub Ksiazka)")

    #Wybranie danych w celu sprawdzenia czy kombinacja wylosowanych danych nie istnieje już w bazie
    else:
        Max = max(rowGatunek) * max(rowKsiazka)
        canGO = True
        cursor.execute("SELECT id_gatunku FROM GATUNEK_KSIAZKA ORDER BY id_ksiazki")
        while True:
            row = cursor.fetchone()
            if row is not None:
                baseGatunek += (row)
            if row is None:
                break

        cursor.execute("SELECT id_ksiazki FROM GATUNEK_KSIAZKA ORDER BY id_ksiazki")
        while True:
            row = cursor.fetchone()
            if row is not None:
                baseKsiazka += (row)
            if row is None:
                break

        if (len(baseGatunek) != 0 and len(baseKsiazka) != 0):
            IsMax = len(baseGatunek) + how_many
        else:
            IsMax = how_many

        if(IsMax > Max):
            print("Brak możliwych kombinacji do wygenerowania dodaj wartości do tablic nadrzędnych")
            canGO = False

        # Jeżeli są dostępne kombinacje
        if(canGO):
            for i in range(1, how_many + 1):
                isOK = True
                Insert1 = random.choice(rowGatunek)
                Insert2 = random.choice(rowKsiazka)
                OneInsert = False
                # Jeżeli są już dane w tabeli
                if (len(baseGatunek) > 0):
                    # Sprawdzenie kombinacji

                    for j in range(0, len(baseGatunek)):
                        if (Insert1 == baseGatunek[j] and Insert2 == baseKsiazka[j]):
                            Insert1 = random.choice(rowGatunek)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    for k in range(0, len(gatunek_ksiazka)):
                        x = [(Insert1, Insert2)]
                        if (x[0] == gatunek_ksiazka[k]):
                            Insert1 = random.choice(rowGatunek)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    while (isOK == False):
                        isOK = True
                        for j in range(0, len(baseGatunek)):
                            if (Insert1 == baseGatunek[j] and Insert2 == baseKsiazka[j]):
                                Insert1 = random.choice(rowGatunek)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                        # Sprawdzenie czy nie istnieje już w tablicy do przydzielenia
                        for k in range(0, len(gatunek_ksiazka)):
                            x = [(Insert1, Insert2)]
                            if (x[0] == gatunek_ksiazka[k]):
                                Insert1 = random.choice(rowGatunek)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                    gatunek_ksiazka += [(Insert1, Insert2)]
                # Jeżeli brak danych w tabeli (więc nie ma powtórzeń)
                else:
                    for k in range(0, len(gatunek_ksiazka)):
                        x = [(Insert1, Insert2)]
                        if (x[0] == gatunek_ksiazka[k]):
                            Insert1 = random.choice(rowGatunek)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    while (isOK == False):
                        isOK = True
                        # Sprawdzenie czy nie istnieje już w tablicy do przydzielenia
                        for k in range(0, len(gatunek_ksiazka)):
                            x = [(Insert1, Insert2)]
                            if (x[0] == gatunek_ksiazka[k]):
                                Insert1 = random.choice(rowGatunek)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                    gatunek_ksiazka += [(Insert1, Insert2)]
        #Jeżeli można wykonać
        if (canGO):
            cursor.executemany("INSERT INTO gatunek_ksiazka (id_gatunku, id_ksiazki) values(:1, :2)", gatunek_ksiazka)
            conn.commit()
            print("Poprawnie wstawiono wiersze do tabeli Gatunek_Ksiazka")

def autor_ksiazka(how_many,cursor,conn):

    autor_ksiazka = []
    rowAutor = []
    rowKsiazka = []
    baseAutor = []
    baseKsiazka = []

    cursor.execute("SELECT id_autora FROM AUTOR")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowAutor += row
        if row is None:
            break

    cursor.execute("SELECT id_ksiazki FROM KSIAZKA")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowKsiazka += row
        if row is None:
            break

    if(len(rowKsiazka) == 0 or len(rowAutor) == 0):
        print("Nie można wykonać zapytania ponieważ nie istnieją rekordy nadrzędne (Autor lub Ksiazka)")

    #Wybranie danych w celu sprawdzenia czy kombinacja wylosowanych danych nie istnieje już w bazie
    else:
        Max = max(rowAutor) * max(rowKsiazka)
        canGO = True
        cursor.execute("SELECT id_autora FROM AUTOR_KSIAZKA ORDER BY id_ksiazki")
        while True:
            row = cursor.fetchone()
            if row is not None:
                baseAutor += (row)
            if row is None:
                break

        cursor.execute("SELECT id_ksiazki FROM AUTOR_KSIAZKA ORDER BY id_ksiazki")
        while True:
            row = cursor.fetchone()
            if row is not None:
                baseKsiazka += (row)
            if row is None:
                break

        if (len(baseAutor) != 0 and len(baseKsiazka) != 0):
            IsMax = len(baseAutor) + how_many
        else:
            IsMax = how_many

        if(IsMax > Max):
            print("Brak możliwych kombinacji do wygenerowania dodaj wartości do tablic nadrzędnych")
            canGO = False

        #Jeżeli są dostępne kombinacje
        if(canGO):
            for i in range(1, how_many + 1):
                isOK = True
                Insert1 = random.choice(rowAutor)
                Insert2 = random.choice(rowKsiazka)
                #Jeżeli są już dane w tabeli
                if(len(baseAutor) > 0):
                    #Sprawdzenie kombinacji

                    for j in range(0, len(baseAutor)):
                        if (Insert1 == baseAutor[j] and Insert2 == baseKsiazka[j]):
                            Insert1 = random.choice(rowAutor)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    for k in range(0, len(autor_ksiazka)):
                        x = [(Insert1, Insert2)]
                        if (x[0] == autor_ksiazka[k]):
                            Insert1 = random.choice(rowAutor)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    while(isOK == False):
                        isOK = True
                        for j in range(0,len(baseAutor)):
                            if(Insert1 == baseAutor[j] and Insert2 == baseKsiazka[j]):
                                Insert1 = random.choice(rowAutor)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                        #Sprawdzenie czy nie istnieje już w tablicy do przydzielenia
                        for k in range(0,len(autor_ksiazka)):
                            x = [(Insert1,Insert2)]
                            if(x[0] == autor_ksiazka[k]):
                                Insert1 = random.choice(rowAutor)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                    autor_ksiazka += [(Insert1, Insert2)]
                #Jeżeli brak danych w tabeli (więc nie ma powtórzeń)
                else:
                    for k in range(0, len(autor_ksiazka)):
                        x = [(Insert1, Insert2)]
                        if (x[0] == autor_ksiazka[k]):
                            Insert1 = random.choice(rowAutor)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    while (isOK == False):
                        isOK = True
                        # Sprawdzenie czy nie istnieje już w tablicy do przydzielenia
                        for k in range(0, len(autor_ksiazka)):
                            x = [(Insert1, Insert2)]
                            if (x[0] == autor_ksiazka[k]):
                                Insert1 = random.choice(rowAutor)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                    autor_ksiazka += [(Insert1, Insert2)]

        #Jeżeli można wykonać
        if(canGO):
            cursor.executemany("INSERT INTO autor_ksiazka (id_autora,id_ksiazki) values(:1, :2)", autor_ksiazka)
            conn.commit()
            print("Poprawnie wstawiono wiersze do tabeli Autor_Ksiazka")

def Magazyn(how_many,cursor,conn):

    magazyn = []
    MaxId = 0
    rowKsiazka = []

    cursor.execute("SELECT MAX(id_wpisu) FROM MAGAZYN")
    registry = cursor.fetchone()

    cursor.execute("SELECT id_ksiazki FROM KSIAZKA")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowKsiazka += row
        if row is None:
            break

    if(len(rowKsiazka) == 0):
        print("Nie można wykonać zapytania ponieważ brak rekordów w tabeli nadrzędnej (Ksiazki)")
    else:
        if registry[0] is not None:
            MaxId = registry[0]

        for i in range(MaxId + 1, how_many + MaxId + 1):
            magazyn += [(i, random.choice(rowKsiazka), random.randint(1, 3000))]

        cursor.executemany("INSERT INTO magazyn (id_wpisu,id_ksiazki,ilosc_sztuk) values(:1, :2, :3)", magazyn)
        conn.commit()
        print("Poprawnie wstawiono wiersze do tabeli Magazyn")

def Stanowisko(how_many,cursor,conn):

    stanowisko = []
    MaxId = 0

    cursor.execute("SELECT MAX(id_stanowiska) FROM STANOWISKO")
    row = cursor.fetchone()

    if row[0] is not None:
        MaxId = row[0]

    for i in range(MaxId + 1, how_many + MaxId + 1):
        stanowisko += [(i, randomNameOfSomething())]

    cursor.executemany("INSERT INTO stanowisko (id_stanowiska, nazwa) values(:1, :2)", stanowisko)
    conn.commit()
    print("Poprawnie wstawiono wiersze do tabeli Stanowisko")

def Pracownik(how_many,cursor,conn):

    pracownik = []
    MaxId = 0
    rowStanowiska = []

    cursor.execute("SELECT MAX(id_pracownika) FROM PRACOWNIK")
    worker = cursor.fetchone()

    cursor.execute("SELECT id_stanowiska FROM STANOWISKO")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowStanowiska += row
        if row is None:
            break

    if(len(rowStanowiska) == 0):
        print("Nie można wykonać zapytania ponieważ brak rekordów w tabeli nadrzędnej (Stanowisko)")
    else:
        if worker[0] is not None:
            MaxId = worker[0]

        for i in range(MaxId + 1, how_many + MaxId + 1):
            pracownik += [(i, Name(), Surname(), random.choice(rowStanowiska))]

        cursor.executemany("INSERT INTO pracownik (id_pracownika,imie,nazwisko,id_stanowiska) values(:1, :2, :3, :4)",
                           pracownik)
        conn.commit()
        print("Poprawnie wstawiono wiersze do tabeli Pracownik")

def Klient(how_many,cursor,conn):

    klient = []
    MaxId = 0

    cursor.execute("SELECT MAX(id_klienta) FROM KLIENT")
    row = cursor.fetchone()

    if row[0] is not None:
        MaxId = row[0]

    for i in range(MaxId + 1, how_many + MaxId + 1):
        klient += [(i, randomUsername(), randomPassword(), randomEmail(), Name(), Surname())]

    cursor.executemany(
        "INSERT INTO klient (id_klienta,username,password,mail,imie,nazwisko) values(:1, :2, :3, :4, :5, :6)", klient)
    conn.commit()
    print("Poprawnie wstawiono wiersze do tabeli Klient")

def Platnosc(how_many,cursor,conn):

    platnosc = []
    MaxId = 0
    rowStanowiska = []

    cursor.execute("SELECT MAX(id_platnosci) FROM PLATNOSC")
    payment = cursor.fetchone()

    if payment[0] is not None:
        MaxId = payment[0]

    for i in range(MaxId + 1, how_many + MaxId + 1):
        platnosc += [(i, random.uniform(15.50, 1000.0), payment_methods[random.randint(0, 2)])]

    cursor.executemany("INSERT INTO platnosc (id_platnosci,kwota,rodzaj_platnosci) values(:1, :2, :3)", platnosc)
    conn.commit()
    print("Poprawnie wstawiono wiersze do tabeli Platnosc")

def Historia_Zamowien(how_many,cursor,conn):

    historia_zamowien = []
    MaxId = 0
    rowPracownik = []
    rowKlient = []
    rowPlatnosci = []

    data = datetime.datetime.now()
    rok = ""
    miesiac = ""
    dzien = ""
    isNull = False
    MaxId = 0

    cursor.execute("SELECT MAX(data_zamowienia) FROM historia_zamowien")
    order_date = cursor.fetchone()

    cursor.execute("SELECT MAX(id_historii) FROM historia_zamowien")
    order_id = cursor.fetchone()

    #DATA
    if order_date[0] is not None:
        data = list(order_date)
        rok = data[0].strftime("%Y")
        miesiac = data[0].strftime("%m")
        dzien = data[0].strftime("%d")
    else:
        isNull = True

    cursor.execute("SELECT id_pracownika FROM PRACOWNIK")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowPracownik += row
        if row is None:
            break

    cursor.execute("SELECT id_klienta FROM KLIENT")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowKlient += row
        if row is None:
            break

    cursor.execute("SELECT id_platnosci FROM PLATNOSC")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowPlatnosci += row
        if row is None:
            break

    if(len(rowPracownik) == 0 or len(rowPlatnosci) == 0):
        print("Nie można wykonać zapytania ponieważ brak rekordów w tabeli nadrzędnej (Pracownik lub Platnosci)")
    else:
        if order_id[0] is not None:
            MaxId = order_id[0]

        for i in range(MaxId + 1, how_many + MaxId + 1):
            klient = 0
            if (len(rowKlient) == 0):
                klient = None
            else:
                klient = random.choice(rowKlient)

            if (isNull):
                if(i == MaxId + 1):
                    DataN = Data()
                    rok = DataN.split("/")[0]
                    miesiac = DataN.split("/")[1]
                    dzien = DataN.split("/")[2]
                    historia_zamowien += [(i, random.choice(rowPlatnosci), klient, random.choice(rowPracownik), DataN)]
                else:
                    x = (HigherDate(rok, miesiac, dzien))
                    rok = x.split("/")[0]
                    miesiac = x.split("/")[1]
                    dzien = x.split("/")[2]
                    historia_zamowien += [
                        (i, random.choice(rowPlatnosci), klient, random.choice(rowPracownik),
                         HigherDate(rok, miesiac, dzien))]
            else:
                x = (HigherDate(rok, miesiac, dzien))
                rok = x.split("/")[0]
                miesiac = x.split("/")[1]
                dzien = x.split("/")[2]

                historia_zamowien += [
                    (i, random.choice(rowPlatnosci), klient, random.choice(rowPracownik), HigherDate(rok, miesiac, dzien))]
        cursor.executemany(
            "INSERT INTO historia_zamowien (id_historii,id_platnosci,id_klienta,id_pracownika,data_zamowienia) values(:1, :2, :3, :4, to_date(:5, 'yyyy/mm/dd'))",
            historia_zamowien)
        conn.commit()
        print("Poprawnie wstawiono wiersze do tabeli Historia_Zamowien")

def ksiazka_historia(how_many,cursor,conn):

    ksiazka_historia = []
    rowHistoria = []
    rowKsiazka = []
    baseHistoria = []
    baseKsiazka = []

    cursor.execute("SELECT id_historii FROM historia_zamowien")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowHistoria += row
        if row is None:
            break

    cursor.execute("SELECT id_ksiazki FROM KSIAZKA")
    while True:
        row = cursor.fetchone()
        if row is not None:
            rowKsiazka += row
        if row is None:
            break

    if(len(rowKsiazka) == 0 or len(rowHistoria) == 0):
        print("Nie można wykonać zapytania ponieważ nie istnieją rekordy nadrzędne (Ksiazka lub Historia zamowien)")

    #Wybranie danych w celu sprawdzenia czy kombinacja wylosowanych danych nie istnieje już w bazie
    else:
        Max = max(rowHistoria) * max(rowKsiazka)
        canGO = True
        cursor.execute("SELECT id_historii FROM KSIAZKA_HISTORIA ORDER BY id_ksiazki")
        while True:
            row = cursor.fetchone()
            if row is not None:
                baseHistoria += (row)
            if row is None:
                break

        cursor.execute("SELECT id_ksiazki FROM KSIAZKA_HISTORIA ORDER BY id_ksiazki")
        while True:
            row = cursor.fetchone()
            if row is not None:
                baseKsiazka += (row)
            if row is None:
                break

        if(len(baseHistoria) != 0 and len(baseKsiazka) != 0):

            IsMax = len(baseHistoria) + how_many
        else:
            IsMax = how_many

        if(IsMax > Max):
            print("Brak możliwych kombinacji do wygenerowania dodaj wartości do tablic nadrzędnych")
            canGO = False

        # Jeżeli są dostępne kombinacje
        if(canGO):
            for i in range(1, how_many + 1):
                isOK = True
                Insert1 = random.choice(rowHistoria)
                Insert2 = random.choice(rowKsiazka)
                # Jeżeli są już dane w tabeli
                if (len(baseHistoria) > 0):
                    # Sprawdzenie kombinacji

                    for j in range(0, len(baseHistoria)):
                        if (Insert1 == baseHistoria[j] and Insert2 == baseKsiazka[j]):
                            Insert1 = random.choice(rowHistoria)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    for k in range(0, len(ksiazka_historia)):
                        x = [(Insert2, Insert1)]
                        if (x[0] == ksiazka_historia[k]):
                            Insert1 = random.choice(rowHistoria)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    while (isOK == False):
                        isOK = True
                        for j in range(0, len(baseHistoria)):
                            if (Insert1 == baseHistoria[j] and Insert2 == baseKsiazka[j]):
                                Insert1 = random.choice(rowHistoria)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                        # Sprawdzenie czy nie istnieje już w tablicy do przydzielenia
                        for k in range(0, len(ksiazka_historia)):
                            x = [(Insert2, Insert1)]
                            if (x[0] == ksiazka_historia[k]):
                                Insert1 = random.choice(rowHistoria)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                    ksiazka_historia += [(Insert2, Insert1)]
                # Jeżeli brak danych w tabeli (więc nie ma powtórzeń)
                else:
                    for k in range(0, len(ksiazka_historia)):
                        x = [(Insert2, Insert1)]
                        if (x[0] == ksiazka_historia[k]):
                            print(str(x[0]) + " " + str(ksiazka_historia[k]))
                            Insert1 = random.choice(rowHistoria)
                            Insert2 = random.choice(rowKsiazka)
                            isOK = False

                    while (isOK == False):
                        isOK = True
                        # Sprawdzenie czy nie istnieje już w tablicy do przydzielenia
                        for k in range(0, len(ksiazka_historia)):
                            x = [(Insert2, Insert1)]
                            if (x[0] == ksiazka_historia[k]):
                                print(str(x[0]) + " " + str(ksiazka_historia[k]))
                                Insert1 = random.choice(rowHistoria)
                                Insert2 = random.choice(rowKsiazka)
                                isOK = False

                    ksiazka_historia += [(Insert2, Insert1)]
        #Jeżeli można wykonać
        if (canGO):
            ksiazka_historia2 =[]
            for j in range(0, len(ksiazka_historia)):
                ksiazka_historia2 += [(ksiazka_historia[j][0],ksiazka_historia[j][1],random.randint(1,100))]

            cursor.executemany("INSERT INTO ksiazka_historia (id_ksiazki,id_historii,ilosc) values(:1, :2, :3)", ksiazka_historia2)
            conn.commit()
            print("Poprawnie wstawiono wiersze do tabeli Ksiażka_Historia")
