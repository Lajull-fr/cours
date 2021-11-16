# 2 Présentation de WPF

WPF (Windows Presentation Foundation) est la technologie successeuse de
Windows Forms pour la création des interfaces visuelles des
applications. Elle a été introduite en 2006 avec le .Net Framework
3.0.  
NB/ WCF (Windows Communication Foundation) a été introduit en même
temps.

WPF est utilisée dans les applications du Windows Store (applications
Modern UI), mais aussi dans les applications de bureau classiques
(Win32), pour créer des interfaces plus modernes et plus riches qu’avec
les anciennes technologies (Windows Forms ou autres).

## 2.1 Les apports de WPF

WPF apporte des bénéfices importants sur le plan **fonctionnel** :

-  Comme le rendu graphique est vectoriel, il devient possible de zoomer
   et d’appliquer toutes sortes d’effets et de transformations aux
   éléments de l’interface (cf. application de démo FamilyShow)

-  Il est possible de créer des interfaces bien plus riches et
   personnalisées qu’avec les anciennes technologies (formes, couleurs,
   effets, animations, graphismes…)

… mais aussi dans le **processus de conception et développement** :

-  Sous certaines conditions, WPF permet de découpler complètement la
   description de l’interface visuelle, de la logique applicative, ce
   qui amène deux bénéfices :

   -  Le code est plus facile à maintenir et à faire évoluer

   -  Le code de la logique applicative est plus facilement testable,
      notamment avec des tests unitaires

-  Le visuel peut être conçu et réalisé par d’autres personnes que le
   développeur : ergonome, graphiste, designer. La réalisation peut
   notamment être faite avec les outils Expression Design (création
   d’illustrations) et Expression Blend (création de l’interface
   utilisateur en xaml).

En contrepartie :

-  WPF nécessite des ressources matérielles plus importantes (carte
   graphique, mémoire) pour le développement et l’exécution des
   applications.

-  La maîtrise des techniques de création des interfaces visuelles
   (langage xaml et techniques de design), demande plus d’effort qu’avec
   les anciennes technologies

-  Le débogage est plus compliqué. En effet, l’interface étant décrite
   de façon déclarative, les erreurs de liaisons de données ne sont pas
   détectées au moment de la compilation, mais à l’exécution. Même à
   l’exécution, on peut facilement passer à côté de certains problèmes,
   car ils ne font pas planter l’application.

NB/ La plateforme UWP pour Windows 10 a introduit une nouvelle syntaxe
de databinding, avec des performances améliorées, et une résolution des
erreurs au moment de la compilation. Cette syntaxe n’a toutefois pas été
portée dans WPF.

## 2.2 Caractéristiques techniques

Les principales caractéristiques techniques de WPF sont :

-  Le rendu graphique vectoriel basé sur DirectX

-  La description déclarative de l’interface en XAML (Extensible
   Application Markup Language), qui est un langage basé sur xml, pour
   créer des objets (éléments) WPF

-  La notion de liaisons de données (databinding) pour relier
   l’interface visuelle au code applicatif .net, qui s’appuie sur la
   notion de propriété de dépendance.

[Point d’entrée](https://docs.microsoft.com/fr-fr/dotnet/desktop/wpf/?view=netframeworkdesktop-4.8)
de WPF dans la doc de Microsoft.

## 2.3 Evolution

Les technologies de développement d’interface utilisateur plus récentes
s’appuient sur les mêmes principes que WPF. Ainsi la description de
l’interface en Xaml, le découplage de l’interface et de la logique
applicative via le modèle MVVM, la liaison de données…etc. restent les
principes majeurs en vigueur.

**UWP** (Universal Windows Platform) permet de créer des applications
exécutables sur plusieurs catégories d’appareils fonctionnant sous
Windows 10 (PC, console Xbox, lunettes Hololens, IoT). Elle fournit des
contrôles permettant la création d’interfaces qui s’adaptent aux
différentes résolutions d’écrans (responsive design), et aux différents
modes de saisies (souris, tactile).

**WinUI** est l’évolution de UWP. C’est une couche d'interface
utilisateur utilisée nativement dans Windows, qui contient des commandes
et des styles modernes pour la création d'applications Windows selon les
principes du Fluent Design. Elle peut être mixée avec la technologie
UWP, et pourra prochainement être mixée avec les technologies WPF et
Winforms pour moderniser progressivement les applications anciennes.

**Xamarin.Forms** permet de décrire les interfaces visuelles
d’applications multi-plateformes pour Android, iOS, Windows 10 et macOS.
Le principe est d’avoir un langage descriptif universel, interprété
ensuite de façon spécifique pour chaque système d’exploitation, afin
d’en utiliser les contrôles natifs.

**.Net MAUI** (Multi-platform App UI) est l’évolution de Xamarin.Forms,
qui sera disponible au 2d trimestre 2022. Elle utilisera WinUI pour le rendu des applis sur Windows.