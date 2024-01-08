# ProjectBookstore

## PL 
```
ProjectBookstore jest to projekt korzystająca z plików XML oraz bazy danych
Oracle w celu przechowywania danych.
Projekt został stworzony w procesie nauki i składa się z dwóch podprojektów:
Generatora oraz aplikacji Księgarni
```
### Generator dla bazy danych
```
Generator dla bazy danych jest to skrypt napisany w
języku python w celu generowania danych dla bazy, pozwala on na:

--Usuwanie istniejących danych
--Dodanie nowych rekordów do wybranej tabeli
--Dodanie nowych rekordów do wszystkich tabeli
```
### Ksiegarnia
```
Księgarnia jest to aplikacja WPF mająca być aplikacją magazynową
pozwalającą na operacje CRUD dla jednego użytkownika zalogowanego za pomoca
imienia oraz nazwiska pracownika znajdującego się w bazie danych Oracle.
Aplikacja działa głównie na pliku XML, który jest zrzutem bazy
danych wykonanym podczas uruchomienia się aplikacji.
Księgarnia pozwala na aktualizowanie danych w bazie poprzez funkcje
zapisu do bazy podpiętej pod odpowiedni przycisk.
Wykonanie zapisu powoduje napisanie wysztkich rekordów w bazie
```
### Cel
```
Celem tego projektu było nauczenie się jak korzystać z plików XML w środowisku C#.
Zaowocowało to stworzeniem aplikacji WPF, która wykonuje wszystkie
akcje na plikach XML a następnie pozwala na zapis do bazy.
```
## EN 
```
ProjectBookstore is an project that uses XML files and
Oracle Database for storing data. The project was created for selftaugth
purposes andconsists of two sub-projects: Generator and Bookstore
```
### DatabaseGenerator
```
DatabaseGenerator is a python script that is used for creating mock data i allows:

--Deleting existing data
--Adding new data to one specified tabel
--Adding new data to all tabels
```
### Bookstore
```
Bookstore is a WPF warehouse managment application that allows CRUD operations
for single user, loged in using name and surename of employee that is present
in Oracle DB. Aplication work mostly on XML file that is a snapshot
of a database, which is done during lunching the aplication. 
Bookstore allows to update database using a function that is binded to an button. 
Saving local changes to database will result in updating all records.
 ```
### Purpose
```
The purpose of this project was to learn how to use XML files in a C# environment.
This resulted in the creation of a WPF application that operates on
XML files and allows writing changes to the database.
```
