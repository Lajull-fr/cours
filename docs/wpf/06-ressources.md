# 6 Les ressources

Le mot « ressources » désigne plusieurs choses différentes :

-  Des objets déclarés en xaml, à l’intérieur de dictionnaires (ex :
   styles, templates, convertisseurs…)

-  Des fichiers externes incorporés dans l’application, mais non
   modifiés par elle (ex : fichiers images, sons, vidéos, textes…)

-  Des valeurs saisies directement dans des fichiers resx. Il s’agit
   généralement, mais pas obligatoirement, de ressources localisables
   (ex : libellés traductibles).

Les objets ressources déclarés dans le xaml ne font pas partie de
l’arbre visuel, mais sont utilisés par les éléments WPF. Ils permettent
de référencer toutes sortes d’objets : styles, templates, instances de
classes, constantes, ressources externes…etc.

**Intérêts des ressources :**

-  La déclaration d’un objet en tant que ressource permet de le partager
   entre tous les éléments qui en ont besoin. Par exemple, un objet
   Brush peut fournir une couleur utilisable partout dans l’application.

-  En séparant les objets utilisés par l’interface, de l’interface
   elle-même, celle-ci devient plus évolutive et adaptable. On peut par
   exemple définir différents dictionnaires de ressources, adaptés à
   différentes situations (paramétrage, culture…)

Nous allons voir maintenant comment créer et utiliser des ressources.
Les ressources localisables seront traitées dans le chapitre
Internationalisation.

## 6.1 Objets ressources xaml

### 6.1.1 Dans une fenêtre

Le code ci-dessous montre comment créer divers types de ressources en
xaml à l’intérieur d’une fenêtre

```xml
<Window x:Class="Ressources.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        xmlns:local="clr-namespace:Ressources"
        Title="Ressources" Height="350" Width="525">
   <Window.Resources>
      <!-- Style -->
      <Style TargetType="TextBlock" x:Key="styleTitre">
         <Setter Property="FontFamily" Value="Verdana"/>
         <Setter Property="FontSize" Value="30"/>
         <Setter Property="TextAlignment" Value="Center"/>
      </Style>

      <!-- Ressource binaire incorporée à l'exe -->
      <Image x:Key="imgAsterix" Height="150"
            Source="pack://application:,,,/Images/Astérix.jpg"/>

      <!-- Type défini dans un autre assembly -->
      <sys:String x:Key="strAsterix">Astérix</sys:String>
   </Window.Resources>
```

Nous déclarons ici trois ressources de types différents dans le
dictionnaire par défaut de la fenêtre (Window.resources) :

-  La première est un élément style WPF qui s’applique à des TextBlock

-  La seconde est une image incorporée à l’exe

-  La dernière est de type String (type défini dans l’assembly externe
   mscorlib.dll, auquel on accède grâce au préfixe d’espace de nom `sys`
   déclaré plus haut)

### 6.1.2 Au niveau de l’application

On peut définir des ressources au niveau de l’application pour qu’elles
soient accessibles par toutes les fenêtres de l’application. Pour cela,
il suffit de les ajouter au dictionnaire Application.Resources défini
dans le fichier App.xaml :

```xml
<Application x:Class="Ressources.App"
               xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
               xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
               xmlns:local="clr-namespace:Ressources"
               StartupUri="MainWindow.xaml">
      <Application.Resources>        
      </Application.Resources>
</Application>
```

**Caractéristiques :**

-  Tout élément WPF dérivant de `FrameworkElement` possède une propriété
   `Resources`, de type `ResourceDictionary`, représentant son
   dictionnaire de ressources **local**.

-  Les ressources d’un élément WPF sont accessibles par tous ses
   éléments enfants dans l’arbre visuel. Généralement, on définit les
   ressources sur l’élément `Window`.

-  L’application possède également un dictionnaire de ressources
   utilisable partout dans l’application. Il se trouve dans le fichier
   `App.xaml`

-  Dans un dictionnaire de ressources, chaque ressource est identifiée
   par une clé, représentée par la propriété `x:Key`. C’est cette clé
   qui est utilisée par les éléments WPF pour accéder à la ressource.

### 6.1.3 Dans un dictionnaire de ressources

Nous avons vu que chaque élément WPF pouvait avoir un dictionnaire de
ressources local. Mais il est également possible de créer un
dictionnaire de ressources dans un fichier xaml indépendant. Pour cela :

-  Cliquer sur le menu « Projet \\ Ajouter un dictionnaire de
   ressources », et donner un nom au dictionnaire. Celui-ci est créé
   sous forme de fichier xaml.

-  Ajouter des ressources au dictionnaire.

Exemple :

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                     xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
   <SolidColorBrush x:Key="appBrush" Color="AliceBlue"/>
</ResourceDictionary>
```
Les dictionnaires de ressources indépendants permettent de regrouper,
classer et partager les ressources.

**Exemple de scénario possible :**

On crée plusieurs dictionnaires indépendants, regroupant chacun des
ressources de même nature : styles, templates, convertisseurs…etc.

Ces dictionnaires peuvent être regroupés dans le dictionnaire global de
l’application si on veut rendre les ressources accessibles à toutes les
fenêtres de l’application. Ou bien, ils peuvent être référencés
individuellement par les fenêtres qui en ont besoin, au cas par cas.

Dans une application vaste, dont la solution Visual Studio contient
plusieurs projets, on peut centraliser toutes les ressources à partager
dans un projet spécifique contenant uniquement des dictionnaires de
ressources, et référencer ensuite ce projet dans les autres projets.

NB/ Il ne s’agit là que de possibilités, et non de pratiques à adopter
systématiquement.

### 6.1.4 Utilisation d’une ressource xaml

Un élément xaml peut utiliser un objet ressource xaml de façon statique
ou dynamique.

**De façon Statique**

On utilise une ressource de façon statique au moyen d’une des syntaxes
suivantes, selon que la ressource est utilisée comme élément WPF
indépendant, ou comme propriété d’un autre élément :

```xml
<StaticResource ResourceKey="clé"/>
{StaticResource ResourceKey="clé"}
```
Remarque : dans la 2ème syntaxe, on peut omettre « ResourceKey= », car
il s’agit de la propriété par défaut.

Exemple d’utilisation des ressources définies plus haut :

```xml
<!-- Utilisation de l'image déclarée en tant que ressource -->
<StaticResource ResourceKey="imgAsterix"/>
   
<!-- Utilisation du style et de la chaîne déclarés en tant que ressources -->
<TextBlock Style="{StaticResource styleTitre}"
            Text="{StaticResource strAsterix}"/>
```
Lorsqu’on fait référence à une ressource de façon statique, la ressource
est chargée une fois pour toutes par l’élément qui la référence. L’objet
ressource référencé reste toujours le même, mais cela n’empêche pas que
ses propriétés changent (elles peuvent être modifiées par le code C# par
exemple).

**De façon dynamique**

Pour utiliser une ressource de façon dynamique, on utilise la syntaxe :

```xml
{DynamicResource ResourceKey="clé"}
```

Lorsqu’on fait référence à une ressource de façon dynamique, si l’objet
ressource référencé change (c’est-à-dire si la ressource désigne une
autre instance d’objet), l’élément WPF qui l’utilise en est notifié, et
utilise cette nouvelle instance.

!> Les ressources dynamiques étant chargées à chaque fois qu’elles
sont utilisées, leur utilisation diminue les performances de
l’application. Sauf besoin spécifique, il est donc conseillé d’utiliser
des ressources statiques

**Accès à un dictionnaire de ressources indépendant**

Pour pouvoir accéder aux ressources d’un dictionnaire crée dans un
fichier xaml indépendant, il faut tout d’abord fusionner ce dictionnaire
avec un dictionnaire déjà accessible (par exemple un dictionnaire local,
ou celui de l’application).

Exemple : fusionnons un dictionnaire indépendant nommé DicoRes.xaml,
avec celui de la fenêtre :

```xml
<Window.Resources>
   <ResourceDictionary>
      <!--Type simple-->
      <sys:String x:Key="strAsterix">Astérix</sys:String>

      <!--Fusion d'un dictionnaire externe-->
      <ResourceDictionary.MergedDictionaries>
         <ResourceDictionary Source="DicoRes.xaml"/>
      </ResourceDictionary.MergedDictionaries>
   </ResourceDictionary>
</Window.Resources>
```

La fusion se fait par ajout du dictionnaire à la collection
ResourceDictionary.MergedDictionaries

**Accès aux objets ressources depuis le code**

On peut également accéder à une ressource depuis le code, pour peu qu’on
ait accès au dictionnaire de ressources qui la contient. La syntaxe est
celle de l’utilisation classique d’une collection non générique de type
dictionnaire. Les valeurs obtenues doivent être transtypées.

Ex1 : accès depuis le code-behind à une ressource stockée dans le
dictionnaire d’une fenêtre :

```csharp
string s = (string)Resources["strAstérix"];
```

Ex2 : accès à une ressource stockée dans le dictionnaire de
l’application

```csharp
decimal seuil = (decimal)Application.Current.Resources["Seuil"];
```

## 6.2 Fichiers externes au projet

Pour être facilement utilisable dans l’application, un fichier externe
(ex : une image, une vidéo, un fichier texte…) doit être référencé dans
le projet. Pour cela, dans Visual Studio, il faut :

-  Cliquer sur le menu Projet \\ Ajouter un élément existant

-  Sélectionner le fichier à ajouter

-  Dans la fenêtre des propriétés, affecter les propriétés « Action de
   génération » (Build Action) et « Copier dans le répertoire de
   sortie » du fichier

**La propriété « Action de génération »** définit la façon dont sera
traité le fichier à la compilation. Il faut sélectionner l’une des deux
valeurs suivantes :

-  "Ressource" : pour incorporer le fichier dans la dll ou l’exe

-  "Contenu" : pour garder le fichier indépendant

**Remarques :**

-  Il existe une valeur "Ressource incorporée", mais elle n’est pas
   adaptée aux fichiers externes, mais plutôt aux fichiers resx.
-  La valeur "Contenu" sera surtout utilisée pour de gros fichiers
   qu’on ne souhaite pas incorporer à l’exe, ou pour des fichiers
   utilisés par plusieurs dll. Mais dans ce dernier cas, on peut aussi
   créer un projet spécifique pour les ressources partagées, et le
   référencer dans les autres projets.
-  Les ressources incorporées ne sont pas modifiables sans recompiler le
   projet. Si on veut pouvoir modifier une ressource sans avoir à
   recompiler, il faut affecter la valeur Contenu à sa propriété Action
   de génération.
-  L’application peut utiliser des fichiers non référencés dans le
   projet, mais dans ce cas, il faut être vigilant à fournir les
   fichiers avec l’exécutable, ou faire en sorte que leur absence ne
   fasse pas planter l’application

**La propriété « Copier dans le répertoire de sortie »** définit si on
veut que le fichier soit copié à côté de la dll ou de l’exe lors de la
compilation. La valeur « Toujours copier » est pertinente si « Action de
génération = Contenu ». Sinon, il faut laisser la valeur « Ne pas
copier ».

En copiant le fichier à côté de l’exe, on simplifie beaucoup son chemin
d’accès, puisqu’il se résume au nom du fichier.

### 6.2.1 Fichier à côté de l’exe

Si on veut accéder à un fichier non connu de l’application au moment de
la compilation (c’est-à-dire pas ajouté au projet), on peut utiliser le
code ci-dessous pour obtenir le chemin du répertoire de l'exe, et ainsi
construire un chemin d’accès au fichier souhaité :

```csharp
string dirPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
```

### 6.2.2 Ressource externe incorporée à l’exe

L’exemple ci-dessous montre comment accéder à une image ajoutée au
projet, et incorporée à l’exe

```xml
<DockPanel>
   <!-- Utilisation directe d'une image incorporée à l'exe -->
   <Image Source="pack://application:,,,/Images/Lucky Luke.jpg" Height="150"/>
</DockPanel>
```

La chaîne spécifiée pour la propriété Source est ce qu’on appelle une URI pack.

La première partie (`pack://application:,,,/`) désigne une ressource
incorporée à l’exe

La seconde partie (`/Images/Lucky Luke.jpg`) désigne le chemin relatif de
la ressource. Il reflète l’arborescence du projet. Ici, l’image a été
placée dans un répertoire nommé « Images » à l’intérieur du projet.

Certaines ressources ne sont pas accessibles via une URI pack. Dans ce
cas, on peut les charger par le code, de la façon suivante :

```csharp
using System;
using System.IO;
using System.Windows;
using System.Text;
using System.Windows.Documents;
   
namespace Ressources
{
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
   
         // Chargement d'une ressource incorporée à l'exe (ici un fichier texte)
         var streamResInfo = Application.GetResourceStream(new Uri("Textes/Readme.txt", UriKind.Relative));
         var reader = new StreamReader(streamResInfo.Stream, Encoding.Default);
         string txt = reader.ReadToEnd();
   
         // Le texte est chargé dans un document affiché dans le viewer
         var doc = new FlowDocument(new Paragraph(new Run(txt)));
         doc.TextAlignment = TextAlignment.Left;
         docViewer.Document = doc;
      }
   }
}
```

Dans cet exemple, la ressource est un fichier texte nommé readme.txt,
placé dans le sous-répertoire Textes du projet. Il est géré comme
ressource externe incorporée à l’exe.

Pour charger cette ressource, on utilise la méthode statique
`GetResourceStream` de la classe Application.

Le flux obtenu est ici lu au moyen d’un objet `StreamReader`, puis chargé
dans un objet `FlowDocument`. docViewer est le nom d’un élément
`<FlowDocumentScrollViewer>` placé dans le code xaml de la fenêtre, et
qui permet d’afficher le document créé.
