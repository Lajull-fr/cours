# Explorateur de fichiers

**Objectifs** : savoir créer et utiliser un délégué

On souhaite analyser les fichiers d’un dossier dont le chemin est saisi
par l’utilisateur. On veut pouvoir afficher :

-  Le nombre total de fichiers, et le nombre de fichiers .cs

-  Les noms des fichiers le plus court et le plus long

-  La liste des noms des fichiers projet C# (sans l’extension .csproj)

Exemple de résultat :

```
Saisissez le chemin du dossier à explorer :
D:\\Temp\\Dev\\Console

646 fichiers, dont 154 fichiers .cs

Nom de fichier le plus long :
Microsoft.VisualStudio.TestPlatform.MSTestAdapter.PlatformServices.Interface.dll

Fichiers projets C# :

JobOverview
TestUnitaire
Boites
EssaisCS
POO
TestsUnitaires
Job Overview
UnitTest
```

### Etape 1
Créer un nouveau projet console nommé `ExplorateurFichiers`

### Etape 2 : création de l’explorateur :

-  Créer une classe `Explorateur` dans un nouveau fichier

-  A côté de cette classe, déclarer un type délégué `DelegueExplorateur`
   prenant un paramètre de type `FileInfo` (classe décrivant un fichier)

A la classe Explorateur, ajouter une méthode statique `Explorer` qui prend
en paramètre :

-  Le chemin d'un dossier
-  Un délégué de type DelegueExplorateur

### Etape 3
Dans la méthode Explorer, utiliser la classe `DirectoryInfo`
et sa méthode `EnumerateFiles` pour parcourir les fichiers du dossier
passé en paramètre, ainsi que ses sous-dossiers de façon récursive, et
pour chaque fichier rencontré, exécuter le délégué

### Etape 4
Créer une classe `Analyseur` qui sera chargée de l’analyse
des fichiers parcourus par l’explorateur.

Dans cette classe, créer les propriétés nécessaires pour stocker les
informations qu’on souhaite obtenir.

### Etape 5
Ajouter une méthode `AnalyserDossier`, prenant en paramètre
le chemin du dossier à analyser. Cette méthode doit lancer l’exploration
du dossier à l’aide de l’explorateur, en lui passant un délégué de type
`DelegueExplorateur`.

### Etape 6
Créer les méthodes nécessaires à l’analyse des fichiers,
et les brancher sur le délégué :

-  `CompterFichiers` : chargée de compter les fichiers, en distinguant les
   fichiers .cs

-  `AnalyserNom` : chargée d’analyser les noms des fichiers et d’isoler le
   plus grand

-  `FiltrerProjet` : chargée de filtrer les fichiers csproj et de
   mémoriser leur nom dans une collection

### Etape 7
Dans la méthode Main :

-  Faire saisir et récupérer le chemin du dossier à explorer

-  Instancier un Analyseur et appeler sa méthode Analyse en lui passant
   le chemin du dossier

-  Afficher les informations récupérées par l’analyseur

### Etape 8
Faire en sorte de demander à nouveau la saisie du chemin
du dossier, si le chemin saisi n’était pas valide.