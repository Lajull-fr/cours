# 4 Les espaces de noms

## 4.1 Déclaration des espaces de noms

Quand on créer une nouvelle fenêtre, Visual Studio génère
automatiquement un code qui ressemble à ceci :

```xml
   1 <Window x:Class="MonAppli.MainWindow"
   2        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
   3        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
   4        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
   5        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
   6        xmlns:local="clr-namespace:MonAppli"
   7        mc:Ignorable="d"
   8        Title="MainWindow" Height="300" Width="300">
```
`xmlns` signifie « XML Namespace », c’est-à-dire « **espace de noms XML** ».  
Il s’agit d’une notion similaire à celle des espaces de noms de
la librairie de classes .net ; cela permet d’éviter les conflits de noms.
[Cette page de doc Microsoft](https://docs.microsoft.com/fr-fr/dotnet/desktop/wpf/advanced/xaml-namespaces-and-namespace-mapping-for-wpf-xaml?view=netframeworkdesktop-4.8) donne plus d'infos.

En XML, les espaces de noms sont identifiés par des url, et on y fait
référence au moyen de **préfixes**, avec la syntaxe : 
`<Préfixe>:<nom de l’élément>`

Ainsi, la 3<sup>ème</sup> ligne déclare un espace de noms préfixé par x,
et la 1<sup>ère</sup> ligne utilise l’attribut Class de cette espace de
noms avec la syntaxe `x:Class`

La 2<sup>ème</sup> ligne déclare l’**espace de noms par défaut**,
c’est-à-dire celui qui peut être utilisé sans préfixe. Tous les éléments
WPF standards sont dans cet espace de noms, c’est pourquoi leurs noms ne
sont pas préfixés dans le code xaml (et heureusement, car ce serait
pénible à lire !).

On est ainsi en mesure de mieux comprendre chaque ligne du code généré :

1. déclare une fenêtre décrite par une classe nommée
   `MonAppli.MainWindow`. MonAppli est en fait le nom de l’espace de
   noms C# (par défaut identique au nom du projet). Cette première ligne
   fait donc le lien entre le code xaml et la classe C# de la fenêtre.

2. déclare l’espace de noms de tous les éléments WPF
   standards (appartenant à l’espace de noms `System.Windows` de la
   librairie de classes .net)

3. déclare l’espace de noms du XAML (appartenant à l’espace
   de noms `System.Windows.Markup` de la librairie de classes .net).

4. déclare l’espace de noms du designer. En effet, celui-ci
   peut ajouter des attributs xaml pour ses propres besoins.

5. Avec la ligne 7 (`mc:Ignorable="d"`) indique que
   les attributs préfixés par `d` (c’est-à-dire ceux ajoutés par le
   designer) seront ignorés à la compilation.  
   Explications complémentaires [ici](http://stackoverflow.com/questions/12347806/where-to-find-xaml-namespace-d-http-schemas-microsoft-com-expression-blend-20)

6. déclare un alias pour l’espace de noms C# courant (celui
   dans lequel se trouve la classe de la fenêtre).

8. décrit le titre et les dimensions de la fenêtre

## 4.2 Accès aux types d’un espace de noms

Le code xaml ne se limite pas à l’utilisation des classes WPF. Il peut
utiliser également des classes définies par nous-même, ou définies dans
des assemblies externes.

### 4.2.1 Accès aux types du projet courant

Revenons sur la ligne 6 de l’extrait de code précédent :

```xml
xmlns:local="clr-namespace:MonAppli"
```

L’attribut `clr-namespace` permet de faire référence à un espace de
noms contenant des classes publiques qu’on souhaite utiliser dans le
xaml. On affecte généralement le préfixe « local » à cet espace de noms,
mais on pourrait utiliser un autre préfixe.

Supposons qu’on ait créé dans le projet un contrôle personnalisé nommée
`MonControle`, dérivée de `UIElement`. On pourra alors l’utiliser dans le
code XAML de la fenêtre, de cette façon :

```xml
<Window x:Class="MonAppli.MainWindow"
         ...
         xmlns:local="clr-namespace:MonAppli"
         Title="MainWindow" Height="300" Width="300">
   ...
   <local:MonControle/>
   ...
</Window>
```

Cette technique s’applique à toutes sortes de types, et pas seulement
des composants visuels. L’exemple suivant montre la syntaxe pour
utiliser une classe de convertisseur (utilisé dans les liaisons de
données), et une constante, c’est-à-dire une variable statique :

```xml
<Window x:Class="LiaisonDonnées.LiaisonCollection"
        ...		  
        xmlns:local="clr-namespace:LiaisonDonnées"
         ...
        Title="LiaisonCollection" Height="560" Width="400">
         <Window.Resources>
            <local:DecimalToColorBrushConverter x:Key="conv"/>
         </Window.Resources>

         <TextBlock Text="{x:Static local:MesConstantes.Titre}"/>
</Window>
```

Le convertisseur est instancié par le code xaml lui-même.  
`DecimalToColorBrushConverter` est bien le nom d’une classe, et non celui
d’une variable contenant un objet.

La variable statique `Titre` est déclarée dans une classe `MesConstantes` :

```csharp
public class MesConstantes
{
   public const string Titre = "Utilisation des espaces de noms";
}
```

### 4.2.2 Accès aux types définis dans d’autres assemblies

Il peut parfois être nécessaire d’accéder à des types définis dans
d’autre assemblies que l’assembly courante. Dans ce cas, il faut :

-  Ajouter une référence vers cet assembly dans le projet courant

-  Dans le xaml, préciser le nom de l’assembly (sans l’extension .dll),
   après celui de l’espace de noms

Exemple :

```xml
<Window x:Class="Ressources.MainWindow"
         xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
         xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
         xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
         xmlns:sys="clr-namespace:System;assembly=mscorlib"
         Title="MainWindow" Height="350" Width="525">
   <Window.Resources>
      <sys:Double x:Key="TitleFontSize">30</sys:Double>
   </Window.Resources>
```

Nous créons ici une ressource de type `Double`. Ce type est défini dans
l’assembly mscorlib.dll du .net framework, et appartient à l’espace de
noms `System`. Nous déclarons donc un préfixe sys pour cet espace de nom
et cet assembly.

NB/ mscorlib (Multilanguage Standard Common Object Runtime Library) est
une des assemblies de base du runtime .net, qui contient entre autres
les définitions de types simples. Tous les projets Desktop contiennent
une référence implicite vers elle, il n’est donc pas nécessaire
d’ajouter soi-même cette référence.
