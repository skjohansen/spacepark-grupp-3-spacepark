<h2 align="center">Projekt SpacePort</h2>
<p align="center">Grupp 3</p>

##### Innehållsförteckning 

- [Lista över förkortningar och begrepp](#lista-over-förkortningar-och-begrepp)
- [Bakgrund](#bakgrund) 
  * [DevOps](#devops)
  * [Molntjänster](#molntjänster)
- [Metod](#metod)
  * [Arbetssätt](#arbetssätt)
  * [Unit Tester](#unit-tester)
    * [Testkonvention](#testkonvention)
  * [3 lagers-arkitektur](#3-lagers-arkitektur)
    * [Presentationslager](#presentationslager)
    * [Applikationslager](#applikationslager)
    * [Datalager](#datalager)
  * [Azure Portal](#azure-portal)
  * [Azure DevOps](#azure-devops) 
    * [Boards](#boards)
    * Build och Test pipeline
    * Release pipeline
- [CI/CD](#ci/cd)
  * [Code repository](#code-repository)
  * [Build pipeline](#build-pipeline)
    * [Build pipeline presentation](#build-pipeline-presentation)
    * [Build pipeline API](#build-pipeline-api)
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

# Metod

I detta avsnitt förklarar du hur du gick tillväga för att kunna utföra ditt exjobb. Vilka metoder, arbetssätt, verktyg, mätinstrument, maskiner och system har du använt dig av och varför?

Beskriv så systematiskt och tydligt du kan vad du gjort och hur du gjort det. Inkludera all information som behövs för att läsaren ska förstå och få förtroende för det du har gjort, dvs att ditt arbete har gjorts på ett pålitligt sätt. 

Kanske kan det vara en fördel att beskriva ordningen på de olika momenten eller beskriva de olika arbetssätten du valt. Ibland kan det vara en fördel att använda bilder och figurer för att förklara på ett bra sätt. Se tidigare rubrik i detta dokument, Examensarbete och Figur 1, för hur du använder figurer i examensarbetet. 

## Arbetssätt
Vi började dagarna med att samlas på Discord och diskutera hur vi låg till. Vi satte sedan gemensamt upp **Issues** för att arbeta med i separata GitHub-branches. Varje branch fick lov att mergas till GitHub master när minst 2 kontrollanter gav godkännande.

Våra arbetstider var vardagar **9** till **16**. Kunde man inte komma in och arbeta skulle man ge förvarning om det.


## Unit Tester
### Testkonvention
Så mycket som möjligt ska testas. Testnamn ska skrivas utförliga utifrån följande:
```
MetodensNamn_VadSomTestas_VadSomFörväntas
```
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

## Azure Portal
Vi valde använder Azure Portal för att skapa **App Service** och **Container Registry** eftersom vi finner alternativet enklare än Azure CLI. Man kan till exempel se vilken specifikation har en container registry har och vad det kostar per månad. Med CLI det är svårare att skapa saker eftersom man måste följa en viss ordning när man matar in kommandon och det är lätt att få fel på grund av felstavning. Om man får fel man är tvungen att skriva om allting från början vilket är besvärligt. 

## Azure DevOps 
### Boards
Vi valde att använda oss utav Azure DevOps Boards mestadels för att vi skulle ha ett bra sett att organisera oss på och för att ha ett bra sätt att dela upp vårat arbete på. När vi började projektet så diskuterade vi i gruppen om vi skulle använda oss utav Boards eller Jira. Vi valde i slutändan Boards eftersom ingen av oss hade använt sig utav det tidigare och vi tyckte det skulle vara intressant att lära oss ett till sätt att skapa sprints etc. Dessutom så var det en fördel med Boards eftersom vi redan använde oss utav Azure DevOps så det blev lite smidigare att ha så mycket samlat på samma ""verktyg"" som möjligt.

### Build och Test pipeline
### Release pipeline

Vi har skapat två release pipeline, en pipeline för API(backend) och en för presentation(frontend). 

#### Presentation Release Pipeline



#### API Release Pipeline





## CI/CD
Continuous- Integration/Development var ett fokus för detta projekt. Dessa arbetsfilosofiska begrepp beskriver kontinuerligt integrerande av kod, byggnad, testning och slutligen publicering av projektet. I vårt projekt använder vi främst CI då vi testar och bygger upp Docker Images kontinuerligt. Denna pipeline är länkad till vår GitHub master branch, vilket vill säga att varje committill - samt pull request mot - master bygger upp vår applikation.

### Code Repository
För vårat projekt använde vi ett GitHub repository. Detta repository kopplar vi till ett projekt i Azure DevOps där vi tidigt i projektets gång sätter upp våra build pipelines.

### Build Pipeline
Vi väljer att separera våra build pipelines i 2 st filer. Detta för att lättare hålla isär projektspecifika skillnader, och dela upp kod:
- **azure-pipelines-api.yml**
- **azure-pipelines-presentation.yml**

#### Build pipeline Presentation
```yaml
trigger:
- master

pool:
  vmImage: 'ubuntu-latest'

steps:
- task: CopyFiles@2
  inputs:
    SourceFolder: 'Presentation'
    Contents: '**'
    TargetFolder: '$(Build.ArtifactStagingDirectory)'
    OverWrite: true
- task: ArchiveFiles@2
  inputs:
    rootFolderOrFile: '$(Build.ArtifactStagingDirectory)'
    includeRootFolder: false
- task: PublishBuildArtifacts@1
- task: Docker@2
  inputs:
    containerRegistry: '<dolt>'
    repository: '<dolt>'
    command: 'buildAndPush'
    Dockerfile: '**/Dockerfile'
```

#### Build pipeline API
I vårat API körs våra unit tester, och ger felutskrift ifall versionen ej går igenom testprocessen. Annars så fortlöper processen, bygger samt publicerar en Docker Container.
```yaml
trigger:
- master

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'

steps:
- task: DotNetCoreCLI@2
  inputs: 
    command: test
    project: '/SpacePort.Tests/*.csproj'
    arguments: '--configuration $(buildConfiguration)'
- script: dotnet build --configuration $(buildConfiguration)
  displayName: 'dotnet build $(buildConfiguration)'
- task: Docker@2
  inputs:
    containerRegistry: 'spaceportConnection'
    repository: 'spaceportConnection'
    command: 'buildAndPush'
    Dockerfile: '**/Dockerfile'
```

# Resultat
