# 2. La plateforme .Net

## 2.1 Le framework .Net

Le framework .Net, créé par Microsoft, contient :

-  **Une bibliothèque de classes très vaste** (plusieurs milliers) pour
   créer des applications de tous types.

-  **Des langages de programmation** capables d’exploiter cette
   bibliothèque

-  **Un environnement d'exécution CLR** (Common Language Runtime, appelé
   aussi simplement Runtime), comparable à la machine virtuelle de Java
   (JVM), pour exécuter les applications de façon sécurisée et
   contrôlée.

Comme nous le verrons plus loin, le framework .Net a beaucoup évolué
depuis ses débuts, mais ces 3 éléments restent d’actualité.

Nous découvrirons la bibliothèque de classes et le langage C# tout au
long de cette formation. Revenons maintenant un peu plus en détails sur
les langages et l’environnement d’exécution.

### 2.1.1 Les langages de programmation

**Le langage C#** (C Sharp) est le langage le plus utilisé pour
programmer avec .Net. Il a été créé en même temps que le framework
lui-même.

NB/ L'équipe qui a créé ce langage a été dirigée par *Anders Hejlsberg*,
un informaticien danois également à l'origine de la conception du
langage *Delphi* pour la société Borland (évolution objet du langage
Pascal).

C# est un langage évolué dont la syntaxe est inspirée du C (comme Java).
C’est un langage orienté objet, mais il permet aussi de faire de la
programmation fonctionnelle.

NB/ Le principe de la programmation fonctionnelle est la séparation des
données et des traitements, au moyen d’enregistrements et de fonctions
pures.

Microsoft fournit 2 autres langages pour .Net :

-  **VB.Net** : un langage objet à la syntaxe inspirée du Visual Basic

-  **F#** : un langage fonctionnel

**Mais .Net est compatible avec tout langage qui satisfait les 2
spécifications suivantes :**

-  Le langage doit utiliser les types CTS (Common Type System) définis
   par le framework .Net

-  Le compilateur doit générer du code intermédiaire appelé MSIL
   (Microsoft Intermediate Language)

Grâce à cela, une application exploitant .Net peut facilement être
composée de sources écrites dans différents langages.

NB/ Sans .Net, créer un exécutable à partir de sources écrites en
différents langages impose de choisir des types communs aux langages, et
de respecter des conventions d'appel de fonctions pour chacun des
langages utilisés.

Le schéma ci-dessous illustre le processus de compilation de code source
en code exécutable :

![Code MSIL](images/image2.jpeg)

Le code source est compilé en MSIL dans un fichier .exe. Ce n’est qu’au
moment de l’exécution du programme, que le MSIL est compilé en code
natif par le compilateur JIT (Just In Time) intégré au runtime.

Définir un langage compatible avec .NET revient à fournir un compilateur
qui peut générer du langage MSIL. Les spécifications de .NET étant
publiques (Common Language Specifications), il est possible pour
d’autres éditeurs que Microsoft, de concevoir des langages compatibles
avec .NET. C’est ainsi qu’ont été créés par exemple les langages Delphi
.Net et IronPython.

?> Un code exploitant le framework .Net est dit « **managé »**
(sous-entendu par le runtime).

### 2.1.2 Le runtime (CLR)

Le runtime est un environnement d'exécution qui fournit des services aux
programmes qui s'exécutent sous son contrôle (code managé) :

-  Chargement/exécution/isolation des programmes

-  Vérification des types

-  Conversion du code intermédiaire (IL) en code natif

-  Accès aux métadonnées (informations sur le code contenu dans les
   assemblages .NET)

-  Vérification des accès mémoire (évite les accès en dehors de la zone
   allouée au programme)

-  Gestion de la mémoire (Garbage Collector)

-  Gestion des exceptions

-  Adaptation aux caractéristiques nationales (langue, représentation
   des nombres)

-  Compatibilité avec les DLL et modules COM qui sont en code natif
   (code non managé)

## 2.2 Evolution de la plateforme .Net

### 2.2.1 Le .Net framework

La première version du framework .Net, appelée « **.NET Framework** »
est sortie en 2002, et a évolué selon le tableau ci-dessous (cf. [page
de doc
Microsoft](https://docs.microsoft.com/fr-fr/dotnet/framework/migration-guide/versions-and-dependencies)
pour plus de détails) **:**

| Version | Date sortie | Visual Studio | C# | Installé dans Windows | CLR | Nouveautés majeures |
|:-------:|:-----------:|:-------------:|:--:|:---------------------:|:---:|:--------------------|
|**1.0**|13/02/02 |2002 |1 |                         |1.0 |1ère version |
|**1.1**|24/03/03 |2003 |1 |XP<br>Server 2003        |1.1 |MAJ ASP.Net<br>et ADO.Net |
|**2.0**|07/11/05 |2005 |2 |Server 2003 R2           |2.0 |Génériques<br>Ajouts ASP.Net |
|**3.0**|06/11/06 |     |2 |Vista<br>Server 2008     |2.0 |WPF, WCF, WF |
|**3.5**|19/11/07 |2008 |3 |7<br>Server 2008 R2      |2.0 |Sites web AJAX<br>LINQ<br>Données dynamiques |
|**4.0**|12/04/10 |2010 |4 |Server 2008 R2<br>SP1    |4   |Bibliothèque de classes étendue, classes portables |
|**4.5**|12/09/12 |2012 |5 |8<br>Server 2012         |4   |MAJ WPF, WCF, WF, ASP.Net<br>Prise en charge applis Windows Store |
|**4.5.1**|17/10/13 |2013 |5 |8.1<br>Server 2012 R2  |4   |Prise en charge applis Windows Phone Store<br>Améliorations perf et debug|
|**4.6**|20/07/15 |2015 |6 |10<br>Server 2016        |4   |ASP.Net Core 5<br>Compilation .Net native |
|**4.7**|02/05/17 |2017 |7 |10 (1703)<br>Server 2016 |4   |Prise en charge applis High DPI<br>Support Touch dans WPF pour Windows 10|
|**4.8**|18/04/19 |2019 |7.3 |10 (1905)<br>Server 2019 |4 |Amélioration du support du High DPI<br>WCF ServiceHealthBehavior |

Chaque nouvelle version du framework est généralement accompagnée d’une
nouvelle version de Visual Studio, capable d’exploiter les nouvelles
technologies introduites.

Le .Net Framework a donc évolué pendant plus de 17 ans, mais il présente
par nature plusieurs inconvénients qui ne pouvaient pas être résolus
sans une réécriture complète :

-  Il est assez lourd (plusieurs centaines de Mo) et non modulaire (on
   installe tout ou rien)

-  Il ne fonctionne que sur Windows (contrairement à Java)

-  La cohabitation d’applications utilisant des versions différentes du
   framework peut parfois poser problème.

-  Les performances des applications basées sur le .Net Framework sont
   parfois assez médiocres

C’est pourquoi Microsoft à décidé en 2015 d’entamer l’écriture de son
successeur, appelé « .Net Core ».

?> La société Ximian a créé en 2004 le framework **Mono**, qui est un
portage open source et multi-plateformes du .Net Framework (compatible
avec Windows, Linux et MacOS).  
En 2011, Mono a été repris par la société **Xamarin**, qui l’a utilisé pour
créer son framework de développement d’applis mobiles natives Android et
iOS en C# et XAML. Microsoft a ensuite racheté Xamarin en 2016.

### 2.2.2 .Net Core

.Net Core résout les inconvénients du .Net Framework énumérés
précédemment, car il a été conçu dès le départ pour être :

-  Modulaire

-  Déployable avec l’application, ce qui permet de faire cohabiter des
   applications utilisant des versions différentes de .Net Core

-  Multiplateformes (Windows, Linux, MacOS)

-  Le plus optimisé possible

Il est en plus open source.

Voici son historique des versions majeures (cf. [cette
page](https://docs.microsoft.com/fr-fr/dotnet/core/whats-new/dotnet-core-2-0)
et les suivantes pour plus de détails) :

| Version|Date sortie|Visual Studio|C#|
|:--:|:--:|:--:|:--:|
| **1.0**| 27/06/16| 2015 - 3| 7.3|
| **1.1**| 16/11/16| 2017 (15.0)| 7.3|
| **2.0**| 14/08/17| 2017 (15.3)| 7.3|
| **3.1**| 03/12/19| 2019 (16.4)| 8.0|


![frise](images/image3.png)

La frise ci-dessus montre que le .Net Framework a continué d’évoluer
pendant l’écriture de .Net Core. Cependant, les évolutions ont été assez
mineures, car Microsoft s’est surtout concentré sur .Net Core.

Microsoft maintient toujours le .Net Framework, car de nombreuses
applications se basent toujours sur lui. Mais elle a cessé de lui
apporter de nouvelles fonctionnalités depuis 2019.

### 2.2.3 .Net

.Net est l’évolution naturelle de .Net Core, et prend également le
relais du .Net Framework et de Mono.

Ce nom simplifié marque le caractère universel et unifié de ce
framework. En effet, .Net permet de développer des applications pour
toutes les plateformes (web, Windows, MacOS, Linux, Android, iOS).

La première version de .Net, sortie en novembre 2020, est la version 5,
ce qui marque la continuité par rapport aux dernières versions du .Net
Framework et de .Net Core.

Microsoft prévoit de sortir une nouvelle version majeure de .Net tous
les ans au mois de Novembre.
