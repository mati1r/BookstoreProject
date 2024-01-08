import random


mail = ["@wp.pl", "@onet.pl", "@gmail.com", "@cos.pl"]
images = ["image.png", "image2.png", "image3.png", "image4.png"]

def Name():

    names = []
    with open("imiona.txt") as file:
        names = [line.rstrip() for line in file]

    randomNames = random.choice(names)
    if (randomNames == ""):
        randomNames = "Pawel"
        return randomNames
    else:
        return randomNames

def Surname():

    surnames = []
    with open("nazwiska.txt") as file:
        surnames = [line.rstrip() for line in file]

    randomSurname = random.choice(surnames)
    if(randomSurname == ""):
        randomSurname = "Sawicki"
        return randomSurname
    else:
        return randomSurname

def IncreaseByOne(partOfDate):
    increasedPartOfDate = ""
    if (partOfDate < 10):
        increasedPartOfDate = "0" + str(partOfDate)
    else:
        increasedPartOfDate = str(partOfDate)

    return increasedPartOfDate

def HigherDate(year, month, day):
    date = ""
    increaseYear = ""
    increaseMonth = ""
    increaseDay = ""

    if(int(month) in (1, 3, 5, 7, 8, 10, 12)):
        if(int(day) < 31):
            increaseYear = year
            increaseMonth = month
            increaseDay = str(int(day)+1)
        else:
            if(int(month) != 12):
                increaseYear = year
                increaseMonth = str(int(month)+1)
                increaseDay = "01"
            else:
                increaseYear = str(int(year)+1)
                increaseMonth = "01"
                increaseDay = "01"
    else:
        if(int(year) % 4 == 0):
            if(int(month) == 2):
                if(int(day) == 29):
                    increaseYear = year
                    increaseMonth = str(int(month)+1)
                    increaseDay = "01"
                else:
                    increaseYear = year
                    increaseMonth = month
                    increaseDay = str(int(day)+1)
            else:
                if(int(day) == 30):
                    if(int(month) != 12):
                        increaseYear = year
                        increaseMonth = str(int(month)+1)
                        increaseDay = "01"
                    else:
                        increaseYear = str(int(year)+1)
                        increaseMonth = "01"
                        increaseDay = "01"
                else:
                    increaseYear = year
                    increaseMonth = month
                    increaseDay = str(int(day)+1)
        else:
            if (int(month) == 2):
                if (int(day) == 28):
                    increaseYear = year
                    increaseMonth = str(int(month) + 1)
                    increaseDay = "01"
                else:
                    increaseYear = year
                    increaseMonth = month
                    increaseDay = str(int(day) + 1)
            else:
                if (int(day) == 30):
                    if (int(month) != 12):
                        increaseYear = year
                        increaseMonth = str(int(month) + 1)
                        increaseDay = "01"
                    else:
                        increaseYear = str(int(year) + 1)
                        increaseMonth = "01"
                        increaseDay = "01"
                else:
                    increaseYear = year
                    increaseMonth = month
                    increaseDay = str(int(day) + 1)

    date = str(increaseYear)+"/"+str(increaseMonth)+"/"+str(increaseDay)
    return date


def Data():
    date = ""
    year = random.randint(2020, 2023)
    date += str(year) + "/"
    month = random.randint(1, 12)

    date += IncreaseByOne(month) + "/"
    day = ""
    if (month in (1, 3, 5, 7, 8, 10, 12)):
        day = random.randint(1, 31)
        date += IncreaseByOne(day)
    else:
        if (year == 2020):
            if (month == 2):
                day = random.randint(1, 29)
                date += IncreaseByOne(day)
            else:
                day = random.randint(1, 30)
                date += IncreaseByOne(day)
        else:
            if (month == 2):
                day = random.randint(1, 28)
                date += IncreaseByOne(day)
            else:
                day = random.randint(1, 30)
                date += IncreaseByOne(day)

    return date


def randomPassword():

    word = ""
    howMany = random.randint(6, 20)
    for i in range(howMany):
        mod = random.randint(1, 10)
        if (mod <= 5):
            word += chr(random.randint(97, 122))
        elif (mod > 5 and mod <= 8):
            word += chr(random.randint(65, 90))
        else:
            word += str(random.randint(1, 9))

    return word


def randomUsername():

    word = chr(random.randint(65, 90))
    howMany = random.randint(3, 10)
    for i in range(howMany):
        mod = random.randint(1, 10)
        if (mod <= 5):
            word += chr(random.randint(97, 122))
        elif (mod > 5 and mod <= 8):
            word += chr(random.randint(65, 90))
        else:
            word += str(random.randint(1, 9))

    return word


def randomEmail():

    word = ""
    howMany = random.randint(3, 10)
    for i in range(howMany):
        mod = random.randint(1, 10)
        if (mod <= 5):
            word += chr(random.randint(97, 122))
        elif (mod > 5 and mod <= 8):
            word += chr(random.randint(65, 90))
        else:
            word += str(random.randint(1, 9))
    word += mail[random.randint(0, 3)]

    return word


def randomTitle():

    word = chr(random.randint(65, 90))
    howMany = random.randint(3, 10)
    for i in range(howMany):
        word += chr(random.randint(97, 122))

    return word


def randomNameOfSomething():

    word = chr(random.randint(65, 90))
    howMany = random.randint(3, 15)
    for i in range(howMany):
        word += chr(random.randint(97, 122))

    return word


def randomImage():
    with open(images[random.randint(0, len(images) - 1)], 'rb') as f:
        img_data = f.read()
        return img_data