## SpacePort Projekt

## Verktyg
### Azure DevOps
Vi valde att använda oss av Azure DevOps Boards för att sätta upp relevanta issues och strukturera vårat arbetssätt. Vi tyckte detta passade bra eftersom vi får så mycket som möjligt på samma ställe, till skillnad om vi hade använt t ex Jira.

### app.diagrams.net
Vid början så gjorde vi skisser för flödesscheman på webbplatsen [app.diagrams.net](https://app.diagrams.net/).
<img src="diagram-flowchart.png">

### Övriga verktyg
Vi använder oss av GitBash som verktyg för Git, Visual Studio för kodning, Discord för kommunikation.


## Arbetssätt
Vi börjar med att gemensamt sätta upp issues och eventuella tidsramar och tider för uppsamling. Vi jobbar enskilt med issues i separata branches som vi sedan, ofta gemensamt, mergar till master.

### Testkonvention
Så mycket som möjligt ska testas. Testnamn ska skrivas utförliga utifrån följande:
```
Metodensnamn_VadSomTestas_VadSomFörväntas
```


## Själva programmet

### Hur parkering går till
Man kan vara ny kund eller återkommande kunder kan ange sitt kundId. Först kollas det upp om man har rätt att parkera. Om man har en pågående parkering måste man först betala den.

Vi tänker oss vårat program som en mobile parking app. Man kan  inte parkera igen förrän man har betalat för sin tidigare parkering. 


## Komponenter
Programmet ska använda sig av 3 komponenter - ett backend API, en frontend applikation och en databas. 

### API
Använder sig av 4 Models: 
```csharp
public class Driver
{
    [Key] 
    public int DriverId { get; set; }

    public string Name { get; set; }
    public ICollection<Receipt> Receipts { get; set; }
}
class Receipt 
{
    [Key] 
    public int ReceiptId { get; set; }

    public int Price { get; set; }
    public DateTime RegistrationTime { get; set; }
    public DateTime EndTime { get; set; }

    public Parkingspot Parkingspot { get; set; }
}
class Parkinglot 
{
    [Key]
    public int ParkinglotId { get; set; }
    public string Name { get; set; }
    public ICollection<Parkingspot> Parkingspot { get; set; }
}
class Parkingspot 
{
    [Key]
    public int ParkingspotId { get; set; }
    public int Size { get; set; }
    public bool Occupied { get; set; }
    public Parkinglot Parkinglot { get; set; }
}
```
Det ska finnas en DTO för varje Model. Varje Model har en egen Controller. Det ska finnas ett Repository för varje Model. Varje repository ska ha ett IRepository.

I Repository för Person bör en API request göras. Ifall personen inte är med i Starwars ska en HTTP Status Code 401 returneras.


## Presentation
Under konstruktion.

### Vilka modeller vi behöver

