<h2 align="center">Projekt SpacePort</h2>
<p align="center">Grupp 3</p>

##### Innehållsförteckning 

- [Lista över förkortningar och begrepp](#lista-över-förkortningar-och-begrepp)
- [Bakgrund](#bakgrund) 
  * [DevOps](#devops)
  * [Molntjänster](#molntjänster)
- [Beslut om priser och kostnader](#beslut-om-priser-och-kostnader)
- [Metod](#metod)
  * [Arbetssätt](#arbetssätt)
  * [Tester](#tester)
  	* [Typ av tester](#typ-av-tester)
    * [Testkonvention](#testkonvention)
  * [3 lagers-arkitektur](#3-lagers-arkitektur)
    * [Presentationslager](#presentationslager)
    * [Applikationslager](#applikationslager)
    * [Datalager](#datalager)
  * [CI/CD](#ci/cd)
  * [Code repository](#code-repository)
  * [Azure Portal](#azure-portal)
  * [Azure DevOps](#azure-devops) 
    * [Boards](#boards)
    * [Build och Test pipelines](#build-och-test-pipelines)
    * [Release pipelines](#release-pipelines)
- Resultat

# Lista över förkortningar och begrepp
- **CI:** Continuous Integration
- **CD:** Continuous Development/Deployment
- **Presentation:** Frontend

# Bakgrund
Projektet innefattar att använda oss utav **Molntjänster** och **Azure DevOps**  samt bygga en applikation. Applikationen som ska byggas är ett parkeringssystem för ett företag där enbart folk med namn ifrån Star Wars får lov att parkera. Särskilt intressant med detta projekt är att vi redan är bekanta med applikationen från en tidigare uppgift. Alltså kan vi i detta projekt dra nytta utav misstag och framsteg vi tidigare gjort och integrera det i en så kallad *DevOps*-utvecklingsmiljö. Vi ska i projektet bland annat utnyttja oss av Continuous Integration (CI) samt Continuous Development (CD).

Projektet går ut på att öka vår kompetens i *Molntjänster* och *Azure DevOps*. Det är 2 - för oss - nya teknologier som vi studerar i denna kurs och i detta projekt får lära oss att arbeta med och fördjupa oss inom. I denna rapport kommer vi gå igenom:

* Verktyg vi använt oss av
* Metoderna vi använt oss av
* Resultat av projektet

## DevOps
DevOps är en förening av begreppen **Developer** och **Operations** som traditionelt sett är 2 olika discipliner inom IT utveckling. Utvecklare skriver kod och bygger applikationer och Operations svarar för kvalitét, testning och kundbehov. **DevOps** är kombinationen av dessa 2 företagskulturella filosofier.
## Molntjänster
I projektet ska vi utnyttja oss av Molnteknologi, och mer specifikt Azure Molntjänster. Förutom att det är namnet på kursen vi läser så kan man säga att Molntjänster är datortjänster som tillhandahålls över nätet. Det täcker allt ifrån lagring av data, säkerhetskopiering, publicering, säkerhetsanordningar, virtuella maskiner och mycket mer. 
En annan egenskap med molntjänster är deras elastiska priser, dvs man betalar för vad man använder. Sällan har molntjänster fasta priser.
Molntjänster vi ämnar att använda i projektet är åtminstone:

-  **SQL Database**
-  **Container Registry**
-  **Container Instance**, alternativt **App Service**

# Beslut om priser och kostnader
> Fortsätt här

# Metod

I detta avsnitt förklarar du hur du gick tillväga för att kunna utföra ditt exjobb. Vilka metoder, arbetssätt, verktyg, mätinstrument, maskiner och system har du använt dig av och varför?

Beskriv så systematiskt och tydligt du kan vad du gjort och hur du gjort det. Inkludera all information som behövs för att läsaren ska förstå och få förtroende för det du har gjort, dvs att ditt arbete har gjorts på ett pålitligt sätt. 

Kanske kan det vara en fördel att beskriva ordningen på de olika momenten eller beskriva de olika arbetssätten du valt. Ibland kan det vara en fördel att använda bilder och figurer för att förklara på ett bra sätt. Se tidigare rubrik i detta dokument, Examensarbete och Figur 1, för hur du använder figurer i examensarbetet. 

## Arbetssätt
Vi började dagarna med att samlas på Discord och diskutera hur vi låg till. Vi satte sedan gemensamt upp **Issues** för att arbeta med i separata GitHub-branches. Varje branch fick lov att mergas till GitHub master när minst 2 kontrollanter gav godkännande.

Våra arbetstider var vardagar **9** till **16**. Kunde man inte komma in och arbeta skulle man ge förvarning om det.

## Tester
### Typ av tester
Vi valde att endast använda oss av unit tester och inte försöka oss på integrations eller funktionell testning. Detta för att vi inte använt oss av annat än unit tester innan och detta då skulle ta alldeles för stor del i projektet.

### Testkonvention
Vi valde xUnit som vårat test ramverk. Så mycket som möjligt ska testas helst att alla klasser åtminstone har ett test. Dessa tester ska köras automatiskt i våran pipeline.

Testklassnamn ska skrivas utifrån följande:

```
KlassensNamnTests
```

Testmetodnamn ska skrivas utifrån följande:

```
MetodensNamn_VadSomTestas_VadSomFörväntas
```

Däremot valde vi att förutom namnen på klasser och metoder inte ha någon mer invecklad standard på hur man skriver själva testet utan att detta är upp till var och en vad man föredrar.

## 3 lagers-arkitektur
Programmet använder sig av 3 komponenter i ett så kallat 3 lagers-arkitektur (eller *n*-tiered architecture).
### Presentationslager
Vi valde att bygga vårat Presentationslager som en anpassad webbsida. Denna är byggd på HTML, CSS och JavaScript (JQuery). Vi gjorde och såg detta valet som fördelaktigt för att slippa lära oss t ex Razorpages, och kunde hellre fokusera på CI och CD genom projektets gång.

### Applikationslager
Vårat Applikationslager är ett .NET Core API som fungerar som en mellanhand och arbetare mellan presentationen och datalagret. API:et använder sig av 4 modeller: 
```csharp
public class Driver
{
    [Key] 
    public int DriverId { get; set; }
    public string Name { get; set; }
}
class Receipt 
{
    [Key] 
    public int ReceiptId { get; set; }
    public int Price { get; set; }
    public DateTime RegistrationTime { get; set; }
    public DateTime EndTime { get; set; }
	public Driver Driver { get; set; }
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
Varje Model har en tillhörande Controller och ett tillhörande Repository. Interfaces ska skrivas för samtliga repositories för att behålla låg koppling. En fördel med att bygga på detta viset är att vi inte får för avancerade relationer i databasen, och API:et blir behändigt att arbeta med.

### Datalager
Vi använder en Azure SQL relationsdatabas. Vi valde sedan att bygga upp och populera denna med EntityFrameworkCore och Code first metoden. Vi var alla som mest bekanta med relationsdatabaser och detta var ett väldigt billigt alternativ.

<img src="datalayer.png">

## CI/CD
Continuous- Integration/Development var ett fokus för detta projekt. Dessa arbetsfilosofiska begrepp beskriver kontinuerligt integrerande av kod, byggnad, testning och slutligen publicering av projektet. I vårt projekt använder vi främst CI då vi testar och bygger upp Docker Images kontinuerligt. Denna pipeline är länkad till vår GitHub master branch, vilket vill säga att varje commit till master - samt pull request mot master - bygger upp vår applikation.

## Code Repository
För vårat projekt använder vi ett GitHub repository. Detta repository kopplar vi till ett projekt i Azure DevOps där vi tidigt i projektets gång satte upp våra build och test pipelines.

## Azure Portal
Vi valde använder Azure Portal för att skapa **App Service** och **Container Registry** eftersom vi finner alternativet enklare än Azure CLI. Man kan till exempel se vilken specifikation har en container registry har och vad det kostar per månad. Med CLI det är svårare att skapa saker eftersom man måste följa en viss ordning när man matar in kommandon och det är lätt att få fel på grund av felstavning. Om man får fel man är tvungen att skriva om allting från början vilket är besvärligt. 

## Azure DevOps 
### Boards
Vi valde att använda oss utav Azure DevOps Boards mestadels för att vi skulle ha ett bra sett att organisera oss på och för att ha ett bra sätt att dela upp vårat arbete på. När vi började projektet så diskuterade vi i gruppen om vi skulle använda oss utav Boards eller Jira. Vi valde i slutändan Boards eftersom ingen av oss hade använt sig utav det tidigare och vi tyckte det skulle vara intressant att lära oss ett till sätt att skapa sprints etc. Dessutom så var det en fördel med Boards eftersom vi redan använde oss utav Azure DevOps så det blev lite smidigare att ha så mycket samlat på samma plattform som möjligt.

###  Build och Test pipelines
Vi  separerar våra build pipelines i 2 st filer. Detta för att lättare hålla isär projektspecifika skillnader, och dela upp kod:
- **azure-pipelines-api.yml**
- **azure-pipelines-presentation.yml**

I vårat API körs våra unit tester, och ger felutskrift ifall versionen ej går igenom testprocessen. Annars så fortlöper processen, bygger samt publicerar en Docker Container.

### Release pipelines


# Resultat

> Fortsätt här