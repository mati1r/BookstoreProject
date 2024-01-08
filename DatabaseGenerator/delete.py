
def Delete(cursor,conn):
    cursor.execute("DELETE FROM gatunek_ksiazka")
    cursor.execute("DELETE FROM autor_ksiazka")
    cursor.execute("DELETE FROM gatunek")
    cursor.execute("DELETE FROM autor")
    cursor.execute("DELETE FROM magazyn")
    cursor.execute("DELETE FROM ksiazka_historia")
    cursor.execute("DELETE FROM ksiazka")
    cursor.execute("DELETE FROM historia_zamowien")
    cursor.execute("DELETE FROM pracownik")
    cursor.execute("DELETE FROM stanowisko")
    cursor.execute("DELETE FROM klient")
    cursor.execute("DELETE FROM platnosc")

    conn.commit()

