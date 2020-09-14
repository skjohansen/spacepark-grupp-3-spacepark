### SpacePort 
Bygga modeller i API och sedan göra en migration (Code First) 

Välja Azure SQL relationsdatabas

Slutmål är att få en Docker Image byggd av varje applikation (API och Presentation) till en Azure Container Registry och sedan Azure Container Instance

### Hur ska parkering gå till
Vi tänker oss vårat program som en mobile parking app. Man kan lämna först när man har betalat för sig, baserat på längden av vistelsen.

Vid start kan man få ett val att betala eller parkera. För att betala får man ange sitt användarid.

### För att parkera
Man ska vara medlem (Ha ett namn som finns med i Star Wars). 
Man anger storlek på sitt skepp.
Man väljer sedan Parklinglot
Om det finns lediga platser så kan man fortsätta, annars så får man gå tillbaka och göra ett nytt val

## Komponenter

### API
Vi behöver 3 Models.

Vi behöver en DTO för varje Model.

Vi behöver en kontroller för varje Model.

Vi behöver ett repository för varje Model.

I Repository för Person bör en API request göras. Ifall personen inte är med i Starwars ska en HTTP Status Code 401 returneras.

Varje repository ska ha ett IRepository.

### Metoder i Controllers
Get: Parkinglots 

Get: Parkingspot

Update: Parkingspot

Post: Receipt

### Metoder i Repositories

## Presentation
Under konstruktion.

### Vilka modeller vi behöver
```csharp
class Receipt {
 	DateTime Registrationtime;
	DateTime Endtime;
	int Price;
	Parkingspot Parkingspot;
}

class Parkinglot {
	string Name;
	List<Parkingspot> Parkingspots;
}

class Parkingspot {
	Parkinglot Parkinglot;
	bool Occupied;
	enum Size;
}
```
