import SingleTableInserts as STI

def AllInOne(how_many,cursor,conn):
    STI.Gatunek(how_many,cursor,conn)
    STI.Autor(how_many,cursor,conn)
    STI.Ksiazka(how_many,cursor,conn)
    STI.gatunek_ksiazka(how_many,cursor,conn)
    STI.autor_ksiazka(how_many,cursor,conn)
    STI.Magazyn(how_many,cursor,conn)
    STI.Stanowisko(how_many,cursor,conn)
    STI.Pracownik(how_many,cursor,conn)
    STI.Klient(how_many,cursor,conn)
    STI.Platnosc(how_many,cursor,conn)
    STI.Historia_Zamowien(how_many,cursor,conn)
    STI.ksiazka_historia(how_many,cursor,conn)
    '''END OF AIO'''