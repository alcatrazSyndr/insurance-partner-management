# Insurance Partner Management

Web aplikacija za upravljanje partnerima osiguravajućeg društva, razvijena kao dio tehničkog zadatka.

## Tehnologije

- **ASP.NET Core MVC** (.NET 8)
- **Dapper** Micro ORM
- **SQLite** baza podataka
- **Bootstrap 4**

## Funkcionalnosti

- Pregled svih partnera sortiranih od najnovijeg prema najstarijem
- Unos novog partnera s validacijom svih polja
- Pregled detalja partnera u modalnom prozoru
- Unos polica za pojedinog partnera
- Automatsko označavanje partnera s `*` ako imaju više od 5 polica ili ukupni iznos polica prelazi 5000 €
- Validacija OIB-a prema ISO 7064 MOD 11,10 algoritmu

## Pokretanje projekta

1. Kloniraj repozitorij
2. Otvori `InsurancePartnerManagement.sln` u Visual Studio 2022
3. Pokreni aplikaciju (**F5**)
4. Baza podataka se automatski kreira pri prvom pokretanju

## Testni podaci

Za testiranje OIB validacije koristi jedan od sljedećih ispravnih OIB-ova:
- `38383838383`
- `12345678903`
- `11111111110`
- `22222222228`

## Struktura projekta

InsurancePartnerManagement/

├── Controllers/       # MVC controlleri

├── Database/          # SQL schema i DatabaseInitializer

├── Helpers/           # OIB validator i Decimal type handler

├── Models/            # C# modeli (Partner, Policy, PartnerPolicies)

├── Repositories/      # Dapper repository sloj

└── Views/             # Razor Views (Index, Create)

## Napomena

Aplikacija koristi SQLite bazu podataka koja se sprema kao `partners.db` datoteka u root direktoriju projekta.