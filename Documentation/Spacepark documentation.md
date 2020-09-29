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
  * Azure Portal
  * Azure DevOps 
    * Boards
    * Build och Test pipeline
    * Release pipeline
- [CI/CD](#ci/cd)
  * [Repositories](#repositories)
  * [Build Pipeline](#build-pipeline)
    * Automatiserade tester
    * [Pipeline Presentation](#pipeline-presentation)
    * [Pipeline API](#pipeline-api)
- Resultat

# Lista över förkortningar och begrepp
- **CI:** Continuous Integration
- **CD:** Continuous Development/Deployment

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
## Arbetssätt
Vi börjar med att gemensamt sätta upp issues och eventuella tidsramar och tider för uppsamling. Vi jobbar enskilt med issues i separata branches som vi sedan, ofta gemensamt, mergar till master.

Varje vardag då vi inte har lektion jobbar vi på projektet från 9 till 16. Behöver man komma in senare, gå tidigare eller rent av missa  en dag ska man kommunicera det i god tid.


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

## Azure DevOps 
### Boards
### Build och Test pipeline
### Release pipeline

## CI/CD
### Repositories
För vårat projekt använder vi ett GitHub repository. Detta repository kopplar vi till ett projekt i Azure DevOps där vi tidigt i projektets gång sätter upp våra build pipelines.

### Build Pipeline
Vi använder vi oss av 2 st build pipelines, en för API:et och en för vår Web App. Dessa yaml-filer(pipelines) ska genomföra testning och konstruktion (build) av hela applikationen. Slutligen ska dessa pipelines skapa Docker Images som sedan skickas upp till Azure Container Registry (ACR).

#### Pipeline Presentation
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

#### Pipeline API
> ***Uppdatera här!***
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