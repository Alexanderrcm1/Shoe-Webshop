# G + VG

- [x] Använd mallen Blazor Web App.
- [x] Produkter
  - [x] Innehåller information som: ID, Namn, Beskrivning, Bild-URL, Pris.
  - [x] Minst 10 produkter.
  - [x] En REST-API ska användas för att få åtkomst till produkterna.
- [x] Startsida
  - [x] Startsida (page) med en lista av produkter (komponenter).
  - [x] Produkterna visas upp med hjälp av Razor-komponenter på startsidan.
- [x] Produktkomponent
  - [x] Komponenten ska inte visa all information om produkten, utan endast en överblick.
  - [x] Komponenten ska innehålla en knapp som lägger till produkten i en varukorg.
  - [x] Om man klickar på produkt-komponenten ska man komma till en produktsida (page) där all information om den specika produkten visas.
- [x] Produktsida
  - [x] På produktsidan ska det också vara möjligt att lägga till produkten i varukorgen.
  - [x] Man ska kunna navigera till produktsidan via sökfältet i webbläsaren (t.ex. `localhost/product/1`).
- [x] Varukorg
  - [x] Sidan ska visa vad som finns i varukorgen.
  - [x] Man ska kunna navigera till varukorgen (page) via en knapp/länk.
  - [x] Varukorgen ska innehålla ett formulär för att fylla i adressuppgifter.
  - [x] När formuläret skickas in ska användaren komma till en bekräftelsesida (page) där informationen om beställningen visas: Vilka produkter som köpts. Namn och adress från formuläret. När beställningen är klar ska varukorgen tömmas.
- HTML/CSS/Blazor
  - [x] Minst 2 komponenter (inte pages/layout) ska användas.
  - [x] HTML ska användas på rätt sätt och valideras.
  - [x] Semantiska element när möjligt.
  - [x] CSS ska vara tydligt strukturerad och bidra till UI/UX.
  - [x] Appen ska vara responsiv och anpassad till mobile och desktop.
  - [x] Inget CSS ramverk (bootstrap, tailwind etc.) får användas.

# G, men inte VG

- Räcker med ETT projekt.
- All data i G-delen _får_ vara statisk (hårdkodad).
- [x] Varukorgen ska lagras i `localStorage` (tillåtet även i VG, men ej krav).

# VG

- [x] ... G-kriterierna ska uppfyllas.
- Blazor
  - [x] Minst 4 komponenter (inte pages/layout) ska designas och användas.
- Produkter
  - [x] Lagras i databas (ej valutakursen).
  - [x] Produkter ska ha kvantiteter. De ska kunna bli slutsålda.
  - [x] Produkter ska kunna vara på rea.
- Produktkomponent
  - [x] Ändra utseende om...
    - [x] varan är slut.
    - [x] varan är på rea.
- På produktsidan ska produktens pris kunna visas i olika valutor med hjälp av API:et https://api-ninjas.com/api/exchangerate.
  - [x] API:et måste användas på ett sätt som gör att slutanvändaren inte kan komma åt API-nyckeln.
- Konton
  - [x] Kontoregistrering användarnamn och lösenord.
  - [x] Inloggning med användarnamn och lösenord.
  - [x] Sidan ska komma ihåg vad en användare har lagt i sin varukorg när den loggar in nästa gång.
  - [x] Köp kan endast slutföras som inloggad.
- Struktur
  - [x] Delade klasser ska kunna användas från både Frontend och Backend.
  - [x] Dela upp din app i 3 projekt, exempelvis:
    - Frontend
      - UI, sidor, komponenter.
    - Backend
      - API-endpoints och affärslogik.
    - Shared
      - Ett separat bibliotek för exempelvis delade modeller, DTO:er och gemensam valideringslogik.
- [ ] Dokumentera och demonstrera hur du använt .NET Cores felsökningsverktyg och loggningssystem för att identifiera, analysera och åtgärda minst en specifik bugg i applikationen, inklusive en kort reflektion över bugghanteringsprocessen.
- [] Dokumentera i filen `Analysis.md` i projektets rot (jämte `.sln`-filen).
