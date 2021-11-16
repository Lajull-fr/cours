# 3. Créer une application dans Visual Studio

## 3.1 Solution, projet, assembly

**La solution** est le conteneur de plus haut niveau d’une application.  
Elle peut contenir un ou plusieurs projets.  
Elle est décrite par un fichier .sln, au format texte.

**Un projet** est un ensemble de fichiers sources destiné à être compilé
pour produire un fichier binaire dll ou exe, que l’on nomme
**assembly**.  
Tous les fichiers sources d’un même projet doivent être écrits dans le même langage.  
Un projet est décrit par un fichier .csproj au format xml.

Les propriétés d’un projet permettent de définir entre autres:  
- le nom de l’assembly généré
- la version du .net framework ciblé
- l’emplacement du fichier binaire à générer
- le chemin du répertoire où aller chercher les assemblies référencées

## 3.2 Références

Un projet peut faire référence à :

-  Des assemblies externes
-  D’autres projets de la même solution

Le code source du projet peut alors utiliser les objets décrits dans ces
assemblies ou projets.

Les assemblies référencées peuvent être celles fournies par Microsoft
avec Visual Studio, ou des assemblies créées par soi-même ou par
d’autres éditeurs.

Pour les grosses solutions contenant de nombreux projets, une bonne
pratique est de spécifier le chemin du répertoire contenant les
assemblies externes dans un fichier .targets centralisé (placé à côté du
fichier .sln par exemple), et de le référencer dans les fichiers csproj.
De cette façon, on peut très facilement modifier le chemin de recherche
des assemblies pour l’ensemble des projets de la solution.

Exemple de fichier targets :
```xml
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <ReferencePath>$(SolutionDir)\References\$(Configuration)\;</ReferencePath>
  </PropertyGroup>
</Project>
```

On utilise ici la variable `$(SolutionDir)` pour désigner le chemin du
répertoire de la solution, ce qui est plus souple que de définir un
chemin en dur si on est amené à déplacer la solution.

`$(Configuration)` peut prendre la valeur « debug » ou « release ». On
peut ainsi selon le mode d’exécution de l’application, référencer des
assemblies compilées en mode debug ou release.

Exemple de fichier csproj :

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="12.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(SolutionDir)PR.Targets" />
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <Prefer32Bit>false</Prefer32Bit>
    <UseVSHostingProcess>true</UseVSHostingProcess>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <Prefer32Bit>false</Prefer32Bit>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core"/>
    <Reference Include="System.Runtime.Serialization" />
    <Reference Include="System.Xml.Linq"/>
    <Reference Include="System.Xml" />
    <Reference Include="WindowsBase" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Program.cs" />
    <Compile Include="Animal.cs" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\Tools\Tools.csproj">
      <Project>{BBCFE307-45C6-4A3A-830F-7BD22C218275}</Project>
      <Name>Tools</Name>
    </ProjectReference>
  </ItemGroup>
  <ItemGroup />
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
```

On voit entre autres :

-  La référence au fichier targets
-  Les références aux assemblies Microsoft (System, Sytem.Core…)
-  Une référence à un autre projet de la solution (Tools.csproj)

## 3.3 Compilation, exécution, débogage

### 3.3.1 Compilation

La compilation d’un projet consiste à générer un fichier binaire
appelé **assembly**, à l'extension .dll ou .exe.

La compilation peut se faire dans différents modes : debug, release ou
autre mode personnalisé.

Lorsqu’on compile un projet P1 qui référence un autre projet P2 de la
solution, P2 est compilé en premier si ce n’est pas déjà fait.

Visual Studio est assez intelligent pour compiler les projets dans le
bon ordre et uniquement ceux qui ont été modifiés depuis la dernière
compilation.

### 3.3.2 Exécution et débogage

La solution contient toujours au moins un projet maître, qui est celui
dont l’assembly (fichier exe) est exécuté en premier. Cet exe ne peut
néanmoins pas fonctionner sans les assemblies qu’il référence.

L’exécution en mode debug lance l’exe du projet maître, et arrête
l’exécution au premier point d’arrêt rencontré, s’il y en a un. Le mode
debug permet d’exécuter le code pas à pas et d’examiner en détails les
valeurs des variables.

L’exécution en mode release lance l’exe et ne redonne pas la main au
développeur ; elle ne s’arrête pas aux points d’arrêt. L’application
peut être lancée soit depuis Visual Studio, soit en double-cliquant sur
le fichier exe situé dans le répertoire de génération de l’appli.

Il est possible de lancer manuellement l’application compilée en debug,
en double-cliquant sur l’exe, puis d’attacher ensuite Visual Studio au
processus de l’application pour pouvoir la déboguer. Ceci peut faire
gagner du temps lorsqu’il s’agit de déboguer un scénario un peu long à
reproduire.
