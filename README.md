# CrystalAura Webshop

**ASP.NET Core MVC és Web API alapú webshop alkalmazás**

A CrystalAura egy ASP.NET Core alapú webshop alkalmazás, amely MVC / Razor nézeteket, Web API végpontokat és Entity Framework Core alapú adatbázis-kezelést használ.

A projekt célja egy működő webáruház megvalósítása, ahol a felhasználók kristályokat, ásványokat, ékszereket és dekorációs termékeket tudnak böngészni, keresni, kategória szerint szűrni, kosárba helyezni és megrendelni.

---

## Fő funkciók

- Termékek listázása
- Termékek keresése név és leírás alapján
- Kategória szerinti szűrés
- Termékek kosárba helyezése
- Kosár megjelenítése
- Részösszeg és végösszeg számítása
- Checkout / rendelés leadása
- Vásárlói adatok mentése
- Rendelési tételek mentése adatbázisba
- Termékkészlet automatikus csökkentése rendelés után
- Rendelések megtekintése
- Regisztráció és bejelentkezés
- Session alapú felhasználói állapotkezelés
- Admin jogosultság kezelése
- Admin felhasználó által új termék feltöltése
- Swagger felület API teszteléshez
- Soft delete termékeknél

---

## Használt technológiák

- ASP.NET Core
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- C#
- Razor / cshtml
- Bootstrap
- HTML
- CSS
- JavaScript
- Swagger / OpenAPI
- GitHub verziókezelés

---

## Adatbázis

Az alkalmazás többtáblás adatbázist használ. A termékek kategóriához kapcsolódnak, a rendelésekhez pedig rendelési tételek tartoznak. A felhasználók adatai a `Users` táblában vannak eltárolva.

| Tábla | Feladat |
|---|---|
| `Products` | Termékek adatai, ár, készlet, kép URL, kategória |
| `Categories` | Termékkategóriák tárolása |
| `Orders` | Leadott rendelések fejadatai |
| `OrderItems` | Rendelési tételek, mennyiségek és egységárak |
| `Users` | Felhasználók, jelszó hash, szerepkör |

---

## Felhasználói funkciók

A vásárlók a főoldalon megtekinthetik a termékeket. A termékek között lehet keresni, illetve kategória alapján szűrni.

A felhasználó a kiválasztott terméket kosárba tudja helyezni. A kosár Session segítségével tárolódik, így a felhasználó több terméket is hozzáadhat vásárlás előtt.

A checkout oldalon a felhasználó megadja a rendeléshez szükséges adatokat, majd leadhatja a rendelést. A rendelés leadása után az alkalmazás elmenti a rendelést és annak tételeit az adatbázisba, valamint csökkenti a termékek készletét.

---

## Admin funkciók

Az admin jogosultságú felhasználó számára elérhető az új termék létrehozása funkció.

Az admin felhasználó:

- új terméket tud feltölteni,
- kategóriához tudja rendelni a terméket,
- meg tudja adni a termék árát, készletét, leírását és képének URL-jét,
- megtekintheti a leadott rendeléseket.

Az admin funkciók Session alapú szerepkör-ellenőrzéssel vannak védve. Ha a felhasználó nem admin, akkor az admin oldal nem érhető el számára.

---

## Bejelentkezés és regisztráció

Az alkalmazás tartalmaz regisztrációs és bejelentkezési oldalt. A felhasználó regisztrációkor felhasználónevet és jelszót ad meg. A jelszó hash-elve kerül eltárolásra az adatbázisban.

Bejelentkezés után az alkalmazás Session-ben tárolja a felhasználó azonosítóját, felhasználónevét és szerepkörét.

Tárolt Session adatok:

- `UserId`
- `UserName`
- `UserRole`

A kijelentkezés törli a Session adatokat.

---

## API végpontok

Az alkalmazás Web API végpontokat is tartalmaz, amelyek Swagger felületen keresztül tesztelhetők.

Fontosabb API funkciók:

- Felhasználó regisztráció
- Bejelentkezés
- Termékek lekérdezése
- Termék létrehozása
- Termék törlése / soft delete
- Kategóriák lekérdezése
- Kategória létrehozása
- Rendelések kezelése

A Swagger felület fejlesztés és tesztelés közben segíti az API végpontok kipróbálását.

---

## Fontosabb oldalak

| Útvonal | Funkció |
|---|---|
| `/Home/Index` | Főoldal, terméklista |
| `/Cart` | Kosár |
| `/Checkout` | Rendelés leadása |
| `/Shop/Orders` | Rendelések listázása |
| `/Account/Login` | Bejelentkezés |
| `/Account/Register` | Regisztráció |
| `/Account/Logout` | Kijelentkezés |
| `/Admin/Create` | Új termék létrehozása admin felhasználónak |
| `/swagger` | API tesztelés Swaggerrel |

---

## Telepítés és futtatás

A projekt futtatásához szükséges:

- Visual Studio
- .NET SDK
- SQL Server LocalDB

### Lépések

1. Projekt klónozása GitHubról.
2. Connection string ellenőrzése az `appsettings.json` fájlban.
3. Adatbázis létrehozása Package Manager Console segítségével.
4. Projekt indítása Visual Studio-ból.
5. Az alkalmazás böngészőből elérhető a localhost címen.

### Adatbázis frissítése

A Package Manager Console-ban futtasd:

```powershell
Update-Database
```

---

## Tesztelés

A projekt bemutatásakor az alábbi folyamat tesztelhető:

1. Főoldal megnyitása
2. Termékek listázása
3. Keresés kipróbálása
4. Kategória szerinti szűrés kipróbálása
5. Termék kosárba helyezése
6. Kosár megtekintése
7. Checkout kitöltése
8. Rendelés leadása
9. Rendelés megjelenése a rendelések oldalon
10. Készlet csökkenésének ellenőrzése
11. Admin bejelentkezés
12. Új termék létrehozása admin oldalon
13. API végpontok tesztelése Swaggerben

---

## Projekt összegzés

A CrystalAura webshop egy teljes alap webáruház működését mutatja be ASP.NET Core környezetben.

A projektben megtalálható a frontend megjelenítés, a backend logika, az adatbázis-kezelés, a felhasználói bejelentkezés, az admin jogosultság, a kosárkezelés és a rendelésleadás is.

Az alkalmazás jól szemlélteti az MVC felépítést, a Web API használatát, az Entity Framework Core adatkezelést, valamint a Session alapú állapotkezelést.
