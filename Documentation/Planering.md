## SpacePort Projekt
Projektet  går ut på att bygga ett program för ett parkingsföretag där enbart folk från Star Warsfår lov att parkera. Viktigare än själva programmet var i denna övning att utforska Azure Tjänster, CI och CD. 

##### Innehållsförteckning 
1. [Arbetssätt](#arbetssätt)
        [Testkonvention](#testkonvention)
2. [Verktyg](#verktyg)
        [Azure Devops](#azure-devops)
        [app.diagrams.net](#app.diagrams.net)  
        [Övriga Verktyg](#övriga-verktyg)
3. [3 lagers-arkitektur](#3-lagers-arkitektur)
        [Presentationslager](#presentationslager)
        [Applikationslager](#applikationslager)
        [Datalager](#datalager)
4. [CI](#CI)
        [Repositories](#repositories)
        [Build Pipeline](#build-pipeline)
                [Pipeline Presentation](#pipeline-presentation)
                [Pipeline API](#pipeline-api)
        

## Arbetssätt
Vi börjar med att gemensamt sätta upp issues och eventuella tidsramar och tider för uppsamling. Vi jobbar enskilt med issues i separata branches som vi sedan, ofta gemensamt, mergar till master.

Varje vardag då vi inte har lektion jobbar vi på projektet från 9 till 16. Behöver man komma in senare, gå tidigare eller rent av missa  en dag ska man kommunicera det i god tid.
### Testkonvention
Så mycket som möjligt ska testas. Testnamn ska skrivas utförliga utifrån följande:
```
MetodensNamn_VadSomTestas_VadSomFörväntas
```

## Verktyg
### Azure DevOps
Vi valde att använda oss av Azure DevOps Boards för att sätta upp relevanta issues och strukturera vårat arbetssätt. Vi tyckte detta passade bra eftersom vi får så mycket som möjligt på samma ställe, till skillnad om vi hade använt t ex Jira.

### app.diagrams.net
Vid början så gjorde vi skisser för flödesscheman på webbplatsen [app.diagrams.net](https://app.diagrams.net/).

<img src="diagram-flowchart.png">

### Övriga verktyg
Vi använder oss av GitBash som verktyg för Git, Visual Studio för kodning, Discord för kommunikation.

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

## CI
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
```

