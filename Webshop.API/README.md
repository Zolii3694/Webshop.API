# CrystalAura Webshop

A projekt egy ASP.NET Core alapú webshop alkalmazás, amely MVC / cshtml nézeteket, Web API végpontokat és Entity Framework Core alapú adatbázis-kezelést használ.

## Projekt leírása

A rendszer termékeket és kategóriákat kezel, az adatokat SQL adatbázisban tárolja Entity Framework Core segítségével.

A vásárlók termékeket listázhatnak, kereshetnek, kategória szerint szűrhetnek, kosárba helyezhetnek termékeket, majd rendelést adhatnak le.

A rendelés során a rendszer menti a vásárlói adatokat, a rendelési tételeket, valamint automatikusan csökkenti a termék készletét.

Az alkalmazás tartalmaz bejelentkezési és regisztrációs lehetőséget. A felhasználók adatai adatbázisban kerülnek tárolásra, a jelszavak hash-elve vannak mentve.

Az admin jogosultságú felhasználó termékeket tud feltölteni, kategóriákhoz rendelni, valamint megtekintheti a leadott rendeléseket.

## Fő funkciók

- Termékek listázása
- Termékek keresése
- Kategória szerinti szűrés
- Kosár funkció
- Rendelés leadása
- Rendelési tételek mentése
- Készlet automatikus csökkentése rendelés után
- Regisztráció és bejelentkezés
- Admin jogosultság kezelése
- Termékfeltöltés admin felületen
- Rendelések megtekintése
- Soft delete termékeknél

## Adatbázis

A projekt többtáblás adatbázist használ.

Főbb táblák:

- Products
- Categories
- Orders
- OrderItems
- Users

## Technológiák

- ASP.NET Core
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap
- C#
- Razor / cshtml

## Jogosultságok

A vásárlók a publikus oldalon termékeket böngészhetnek, kereshetnek, kosárba tehetnek és rendelést adhatnak le.

Az admin felhasználó termékeket tölthet fel, kategóriákat kezelhet, valamint megtekintheti a leadott rendeléseket.

## Tesztelés

Az API végpontok Swagger felületen is tesztelhetők.

Az MVC felület böngészőből érhető el, például:

- /Home/Index
- /Cart
- /Checkout
- /Shop/Orders
- /Account/Login
- /Account/Register
- /Admin/Create