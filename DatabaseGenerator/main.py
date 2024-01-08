import datetime
import getpass
from time import strftime
from AIO2 import AllInOne
import SingleTableInserts as STI
import delete

import oracledb
import os
import random
import array

params = oracledb.ConnectParams(host="", port=, service_name="")
conn = oracledb.connect(
    user="",
    password="",
    params=params)

print("1.Autor")
print("2.Autor_Ksiazka")
print("3.Gatunek")
print("4.Gatunek_Ksiazka")
print("5.Historia_Zamowien")
print("6.Klient")
print("7.Ksiazka")
print("8.Ksiazka_Historia")
print("9.Magazyn")
print("10.Platnosc")
print("11.Pracownik")
print("12.Stanowisko")
print("13.Wszystko na raz")
print("14.Usunięcie danych z bazy")
print("15.Wyjście")

ESC = False
while (ESC == False):
    Operation = 0
    input1 = True
    input2 = True

    while(input1 == True):
        try:
            Operation = int(input("Wybierz numer operacji: "))
            input1 = False
        except ValueError:
            input1 = True
            print("Nie podano liczby")

    if(Operation < 14):
        while (input2 == True):
            try:
                how_many = int(input("Podaj ile wierszy chcesz wstawić: "))
                input2 = False
            except ValueError:
                input2 = True
                print("Nie podano liczby")

    cursor = conn.cursor()

    if Operation == 1:
        STI.Autor(how_many,cursor,conn)
    elif Operation == 2:
        STI.autor_ksiazka(how_many,cursor,conn)
    elif Operation == 3:
        STI.Gatunek(how_many,cursor,conn)
    elif Operation == 4:
        STI.gatunek_ksiazka(how_many,cursor,conn)
    elif Operation == 5:
        STI.Historia_Zamowien(how_many,cursor,conn)
    elif Operation == 6:
        STI.Klient(how_many,cursor,conn)
    elif Operation == 7:
        STI.Ksiazka(how_many,cursor,conn)
    elif Operation == 8:
        STI.ksiazka_historia(how_many,cursor,conn)
    elif Operation == 9:
        STI.magazyn(how_many,cursor,conn)
    elif Operation == 10:
        STI.Platnosc(how_many,cursor,conn)
    elif Operation == 11:
        STI.Pracownik(how_many,cursor,conn)
    elif Operation == 12:
        STI.Stanowisko(how_many,cursor,conn)
    elif Operation == 13:
        AllInOne(how_many,cursor,conn)
    elif Operation == 14:
        delete.Delete(cursor,conn)
    elif Operation == 15:
        ESC = True
    else:
        print("Brak operacji o taki numerze")


