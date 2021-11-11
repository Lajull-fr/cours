# 6. Les classes

## 6.1 Le modèle objet en bref

### 6.1.1 Définitions
**La programmation orientée objet** (POO) consiste à décrire un système
sous forme de classes et d’interfaces.

**Une classe** est un modèle (=type) d’objet, qui décrit complètement
toutes les caractéristiques permettant de créer des exemplaires.

**Un objet** est un exemplaire d’une classe, sa représentation en
mémoire. Un terme synonyme est **instance de classe**.  
**Instancier une classe** signifie créer un objet de cette classe.

Exemple :

-  Renault Clio est un modèle de voiture. Il est représenté par une
   classe

-  La Renault Clio de Paul et celle de Marie sont 2 exemplaires de ce modèle.  
   Ils sont représentés par des objets (= instances) de cette classe.

**Une classe contient** :

-  **Des champs**, qui modélisent les données

-  **Des méthodes,** qui modélisent des traitements/interactions sur ces
   données

Un objet sert donc à stocker des données dans des champs et à les gérer
au travers de méthodes.

**Une interface représente un contrat**, sous forme d’une liste de
méthodes abstraites.  
Une classe implémente l’interface (= remplit le contrat) si elle fournit une implémentation de toutes les méthodes.

La POO met en œuvre 3 notions fondamentales, que nous allons étudier en détail :

-  L’encapsulation

-  L’héritage (= dérivation)

-  Le polymorphisme


### 6.1.2 Les avantages de la POO

-  **Découpage de la complexité** : la POO permet de mieux isoler les
   responsabilités, et ainsi de découper un système complexe en briques
   plus ou moins autonomes ayant des responsabilités bien définies.

-  **Sécurisation** : via le principe d’encapsulation et via l’isolation
   des responsabilités, on ne peut pas faire n’importe quoi

-  **Modularité, réutilisation** : grâce à ces principes, on peut
   réutiliser des classes dans différents contextes, sans avoir à
   recoder plusieurs fois les mêmes choses

-  **Extensibilité, évolutivité** : un système bien modélisé peut
   facilement être étendu, enrichi au fil du temps, sans remettre en
   cause tout l’architecture du code

## 6.2 Portées et espaces de noms

**La portée** d’une variable ou méthode est la région du programme dans
laquelle elle est utilisable.  
Elle est fonction de l'emplacement de sa déclaration.

**Portée locale**

Les accolades ouvrante et fermante `{ }` qui forment le corps d’une
méthode, définissent une portée.  
Les variables déclarées à l’intérieur du corps d’une méthode sont appelées variables locales, car elles sont locales à la méthode dans laquelle elles sont déclarées ; elles ne sont pas utilisables dans une autre méthode (i.e., pas dans sa portée).

Une méthode peut contenir des sous-blocs, par exemples pour les
instructions `if`, `for`, `switch`… etc.  
Les variables déclarées dans ces sous-blocs ne sont utilisables qu'à l'intérieur de ceux-ci.

**Portée de classe**

Les accolades ouvrante et fermante qui forment le corps d’une classe
créent aussi une portée. Toutes les variables déclarées dans le corps
d’une classe, mais pas dans une méthode sont dans la portée de cette classe.  
Le nom exact en C# pour une variable de classe est **champ**.  
Les champs sont utilisables dans toutes les méthodes de la classe et
permettent donc de partager des informations entre elles.

**Un espace de noms** désigne une portée de niveau supérieur à la portée de classe.  
Les espaces de noms permettent simplement de regrouper les
types (classes, structures, énumérations) de façon cohérente, et
d’éviter des conflits de noms.  
On peut ainsi avoir un même nom de type
dans différents espaces de noms, comme le montre l’exemple ci-dessous :

```csharp
namespace Espace1  // espace de noms contenant deux types A et B
{
   class A { }
   class B { }
}

namespace Espace2  // autre espace de noms contenant des types de mêmes noms
{
   class A { }
   class B { }
}


// espaces de noms imbriqués
namespace Espace2
{
   namespace ClassesImportantes
   {
      class A { }
      class B { }
   }
}

// Autre façon équivalente d’imbriquer les espaces de noms
namespace Espace2.ClassesImportantes
{
   class A { }
   class B { }
}


// Exemple d’utilisation des types des différents espaces de noms
namespace ExempleNamespace
{
   class Program
   {
      static void Main(string[] args)
      {
         Espace1.A obj1 = new Espace1.A();
         Espace2.A obj2 = new Espace2.A();
         Espace2.ClassesImportantes.A obj3 = new Espace2.ClassesImportantes.A();
      }
   }
}
```

Le nom complet d’un type est donc composé de ses espaces de noms et du
nom du type, séparés par des points.

Si on ne spécifie pas d’espace de noms, le compilateur en ajoute un par
défaut, sans nom, qu’on appelle **espace de noms global**.

!> Le découpage d’un ensemble de classes en assemblies (dll) est
indépendant du découpage des espaces de noms. Ne pas confondre les deux
notions.

**Directive using**

Dans un espace de noms donné, pour utiliser un type contenu dans un
autre espace de noms, il faut utiliser son nom complet (c’est-à-dire
préfixé de son espace de nom), ce qui peut être fastidieux et rendre le
code très verbeux.  
L’utilisation de la directive `using` facilite les choses.

Cette directive permet d’utiliser les éléments d’un espace de noms, sans
avoir à les qualifier par leur nom complet (espace de noms + nom).

Ex : le nom complet de la classe Console est `System.Console` (System est
son espace de nom).  
Pour l’utiliser, on devrait donc écrire :

```csharp
System.Console.WriteLine("Hello!);
```

Mais grâce à la directive **using**, on peut simplifier cette écriture :

```csharp
using System;

namespace ConsoleApplication
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Hello!);
         // le type Console est automatiquement trouvé dans le namespace System
         }
   }
}
```

On peut bien entendu appliquer ce principe à tous les espaces de noms
dont on a besoin dans le code :

```csharp
using System;
using System.Globalization;

namespace ConsoleApplication
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Hello!);
         // le type Console est automatiquement trouvé dans le namespace System
         
         var s = String.Format(CultureInfo.InvariantCulture, "La date : {0}",  DateTime.Today);
         // le type CultureInfo est automatiquement trouvé dans le namespace System.Globalization 

         Console.WriteLine(s);
      }
   }
}
```

**Remarque** : si on utilise plusieurs directives using, et que les
espaces de noms correspondants comportent des noms de types identiques,
dans ce cas, pour utiliser ces types il faudra tout de même spécifier
leurs noms complets.

Pour utiliser une classe du .net framework, il faut connaître son espace
de noms. Visual Studio sait reconnaître les types du .net framework
utilisés dans le code, et propose spontanément de préfixer le nom du
type par celui de son namespace, ou d’ajouter la directive using
manquante.

On peut aussi se référer à [cette
page](https://docs.microsoft.com/fr-fr/dotnet/api/?view=netframework-4.7.2)
de doc Microsoft qui décrit tous les espaces de noms, et qui permet donc
de rechercher les classes dont on a besoin sans connaître leur nom à
l’avance.

## 6.3 Accessibilité des membres d’une classe

?> Rappel : la portée d’une variable ou méthode est la région du programme
dans laquelle elle est utilisable.

Les membres (champs, propriétés, méthodes) définis au sein d’une classe
sont par défaut dans la portée de cette classe, c’est-à-dire qu’ils ne
sont accessibles que dans le corps de la classe. Cependant, on peut leur
appliquer individuellement des **modificateurs d’accès**, afin qu’ils
soient visibles en dehors de la classe.  
.Net fournit les modificateurs suivants, du plus restrictif au plus permissif :

-  `private` (valeur par défaut) : ne modifie rien ; le membre reste
   accessible uniquement à l’intérieur de la classe

-  `protected` : le membre est accessible aussi dans les classes dérivées

-  `internal`: le membre est accessible dans tout l’assembly (=projet)
   courant

-  `public` : le membre est accessible dans tout l’assembly courant, et
   dans les assemblies qui y font référence.

Remarques :

-  Il n’est pas obligatoire de spécifier explicitement le modificateur private,
   puisque c’est la valeur par défaut, mais il est tout de même
   bon de le faire pour éviter toute ambiguïté.

-  Les modificateurs d’accès s’appliquent aussi aux membres des
   structures, sauf protected, puisqu’une structure ne peut pas être dérivée.

!> Les modificateurs d'accès sont à la base du principe d'encapsulation.  
On ne doit rendre public que le strict nécessaire pour que les objets
soient manipulables.

Exemple :
```csharp
public class Animal
{
   // Champs privés (accessibles uniquement à l’intérieur de la classe)
   private float _dureeVie;
   private float _poids;

   // Propriété publique (accessible à l’extérieur de la classe)
   public float Poids
   {
      get { return _poids; }
   }
   
   // Méthode protégée (accessible dans les classes dérivées de Animal)
   protected void Grossir(float qteNourriture)
   {
      _poids += (float)0.1 * qteNourriture;
   }

   // Méthodes publiques (accessibles à l’extérieur de la classe)
   public void Manger(float qte)
   {
      Grossir(qte);
   }

   public override string ToString()
   {
      return String.Format("Je suis un animal qui pèse {0} kg", _poids);
   }
}
```

Les classes dérivées d’Animal ont accès à la propriété Poids et aux
méthodes Grossir, Manger et ToString. Le reste du code (en dehors de la
classe et de ses dérivées) n’a pas accès à la méthode Grossir.

## 6.4 Les propriétés

### 6.4.1 Présentation

En C#, on peut définir des **propriétés**. Il s'agit de membres qui se
manipulent comme des champs, mais dont l’accès est contrôlé par des
fonctions en lecture et en écriture (get et set), appelés **accesseurs**.

Dans les propriétés, le mot clé `value` dans le bloc set désigne la
valeur reçue en écriture.

```csharp
public class Animal
{
   private float _dureeVie;
   private float _poids;

   // Propriété DureeVie qui encapsule le champ privé _dureeVie
   public float DureeVie
   {
      // Bloc get pour accéder au champ en lecture
      get { return _dureeVie;}

      // Bloc set pour modifier le champ de façon contrôlée
      set
      {
         if (value > 0 && value <= 150)
            _dureeVie = value;
         else
         {
            // lève une exception pour indiquer une valeur incorrecte
            throw new ArgumentOutOfRangeException();
         }
      }
   }

   public float Poids
   {
      get { return _poids; }
      set
      {
         if (value > 0)
            _poids = value;
         else
         {
            // lève une exception pour indiquer une valeur incorrecte
            throw new ArgumentOutOfRangeException();
         }
      }
   }

   public override string ToString()
   {
      return String.Format("Je suis un animal qui pèse {0} kg", _poids);
   }
}

public class Program
{
   static void Main(string[] args)
   {
      Animal a = new Animal();
      a.Poids = 50; // une propriété s’utilise comme un champ
      a.DureeVie = 160;  // produit une exception car la valeur est 
                           // en dehors de la plage définie dans la partie set
   }
}
```

La propriété `DureeVie` évite d’avoir à créer 2 fonctions du style
`GetDureeVie` et `SetDureeVie` pour la lecture et l’écriture du champ
`_dureeVie`. La syntaxe est plus propre et l’utilisation plus intuitive.

**Caractéristiques :**

-  Les deux accesseurs `get` et `set` d’une propriété ne sont pas
   obligatoires. Si on ne met pas d’accesseur `set`, la propriété n’est
   pas modifiable, sauf par le constructeur (de façon à pouvoir l’initialiser).

-  On peut mettre un modificateur d’accès devant un des deux accesseurs.
   Typiquement, on peut rendre l’accesseur set protégé, de façon que la
   propriété ne soit modifiable que dans la classe et ses dérivées, tout
   en étant accessible en lecture depuis les autres classes.

```csharp
private float _dureeVie;

public float DureeVie
{
   get { return _dureeVie; }
   protected set { _dureeVie = value; }
}
```

-  Le niveau d'accessibilité sur l'accesseur doit être plus restrictif
   que celui sur la propriété, et le modificateur est autorisé
   uniquement sur un des deux accesseurs.

!> Pour respecter le principe d’encapsulation, attention à ne rendre
les propriétés modifiables que lorsque c’est nécessaire. La plupart des
classes du .net framework fournissent des propriétés non modifiables.

### 6.4.2 Implémentation automatique

Lorsqu’une propriété est destinée uniquement à stocker une donnée, sans
fournir de code spécifique pour sa lecture ou son écriture, on peut
utiliser une syntaxe très simplifiée, très commode et couramment
utilisée, présentée dans l’exemple suivant :

```csharp
public class Animal
{
   // Propriétés
   public float DureeVie { get; private set; }
   public float Poids{ get; set; }
   ...
}
```

Il s’agit d’une implémentation automatique : le compilateur génère
lui-même une variable privée en interne pour manipuler la donnée.

Depuis C# 6, il est possible de ne pas mettre l’accesseur `set` d’une
propriété implémentée automatiquement, de sorte qu’elle ne soit pas
modifiable.  
Exemple :

```csharp
public float DureeVie { get; }
```

### 6.4.3 Initialisation

La valeur d’une propriété est par défaut initialisée avec la valeur par
défaut de son type :

- `0` pour un nombre ou un énuméré

- `false` pour un booléen

- `null` pour un type référence

Pour une propriété implémentée automatiquement, on n’a pas accès à la
variable privée interne qui stocke la donnée. Dans ce cas, il y a deux
façons d’initialiser la propriété :

- Grâce au constructeur de la classe, qui est une méthode spécifique
  pour initialiser les champs et propriétés d’un objet. Nous aborderons
  les constructeurs dans un prochain chapitre.

- Au moment de sa déclaration (valable depuis C# 6) : public float
  DureeVie { get; } = 20F;

?> Notez que ceci fonctionne même si la propriété est
non modifiable (sans bloc set).

## 6.5 Le principe d’encapsulation

**L’encapsulation** désigne le fait que :

-  Un objet contient à la fois des données (champs) et les propriétés et
   méthodes pour les gérer

-  L’utilisateur de l’objet (développeur) ne peut pas accéder
   directement aux champs, ni aux méthodes destinées à la gestion
   interne de l’objet.  
   Pour utiliser l’objet, il n’a accès qu’à un ensemble restreint de
   méthodes et propriétés. Tous les rouages internes lui sont
   invisibles.

<div style="display:flex; flex-wrap:wrap; justify-content:center; align-items:flex-start;">
	<p style="flex:1;"><img src="csharp/images/image4.jpeg" alt="boitier commande"/></p>
	<p style="flex:2">L’image ci-contre représente des boitiers permettant de commander une machine complexe (ex : une machine industrielle).<br/>
		 La complexité interne de la machine est inaccessible et n’intéresse de toute façon pas l’opérateur.<br/>
		 Ce dernier interagit avec la machine uniquement au travers des manettes et boutons des boîtiers de commandes, et souhaite que cette interface de commande soit simple.
   </p> 
</div>

Concrètement, l’encapsulation est mise en œuvre par le respect des
principes suivants :

> -  Laisser tous les champs privés
> -  Mettre publiques les propriétés et méthodes qui permettent
>   de manipuler l’objet d’un point de vue extérieur. Laisser les autres privées.
> -  Rendre les propriétés non modifiables (enlever le bloc set), si leur
   modification par l’extérieur de la classe n’est pas nécessaire

Les propriétés et méthodes publiques forment ce que l’on appelle
l’**interface publique** de l’objet (à ne pas confondre avec le mot clé
Interface que nous verrons plus loin).  
Il faut s’efforcer de la rendre la plus simple possible pour faciliter la compréhension de l’utilisation de l’objet.

**Cas particuliers :**

**Un champ constant** (ou constante) est un champ dont la valeur ne peut
jamais changer.  
Il doit pour cela être déclaré avec le mot clé `const`, et sa valeur doit être définie dès sa déclaration. 
Ce type de champ est généralement public.

```csharp
class Math
{
   public const double PI = 3.14159265358979323846; 
}
```

**Un champ en lecture seule** est semblable à une constante dont
l'initialisation de la valeur peut être faite par le constructeur de la
classe (cf. plus bas), et donc retardée jusqu'au moment de l'exécution.  
On le déclare avec le mot clé `readonly` et on le met généralement public.

```csharp
class MaClasse
{
   public readonly int R;
   public MaClasse(int val)
   {
      R = val;
   } 
}
```

## 6.6 Signature et surcharge des méthodes

**La signature** d’une méthode se compose :
- du nom de la méthode
- du type et du genre (valeur, ref ou out) de chacun de ses paramètres, considérés de gauche à droite.

!> Le type de retour et les noms des paramètres d'une fonction ne font pas
partie de sa signature.  
Ainsi, plusieurs méthodes avec des en-têtes différents peuvent avoir la même signature.

**La surcharge** des méthodes permet à une classe, une structure ou une
interface de déclarer plusieurs méthodes avec le même nom, sous réserve
que leurs signatures soient différentes.

Voici un exemple de surcharge d’une méthode :

```csharp
class Essai
{
      public static double Carré(double d) {
         return d * d;
      }
      
      public static void Carré(ref double d) {
         d *= d;
      }

      // Pas bon car le type de retour ne permet pas de différentier 2 surcharges
      public static double Carré(ref double d) {
         d *= d;
         return d;
      }

      public static void Carré(double d, out double res) {
         res = d * d;
      }
}
```

Autre exemple : la méthode `ToString` possède également plusieurs surcharges.

## 6.7 Constructeurs et initialiseurs

**Un constructeur** est une méthode qui initialise l'état d'un objet au
moment de son instanciation avec `new`.  
L’exemple ci-dessous montre une classe avec 2 constructeurs :

```csharp
public class CompteBancaire
{
    #region Champs privés
    private long _numéro;
    private DateTime _dateCréation;
    private decimal _découvertAutorisé;
    #endregion
   
    /// <summary>
    /// Initialisation d'un compte avec son numéro
    /// </summary>
    public CompteBancaire(long numéro)
    {
        _numéro = numéro;
    }
   
    /// <summary>
    /// Initialisation d'un compte avec son numéro, 
    /// sa date de création et son découvert autorisé
    /// </summary>
    public CompteBancaire(long numéro, DateTime dateCréa, decimal découvertAutorisé)
    {
         _numéro = numéro;        
         _dateCréation = dateCréa;
        _découvertAutorisé = découvertAutorisé;
    }
...
}

class Program
{
    static void Main(string[] args)
    {
        CompteBancaire cpt1 = new CompteBancaire(6546589);
        CompteBancaire cpt2 = new CompteBancaire(8712564, DateTime.Today, 800m);
        Console.ReadKey();
    }
}
```

Cet exemple illustre les caractéristiques des constructeurs :

-  Ils portent le même nom que la classe

-  Ils peuvent avoir des paramètres, dont les valeurs sont fournies au
   moment de l’instanciation de l’objet avec new

-  Ils n’ont pas de type de retour, même pas void

-  Ils peuvent être surchargés

?> Une classe a toujours au moins un constructeur.  
Si on n’en crée pas au moins un, le compilateur en génère un par
défaut, sans paramètre et avec un corps vide.  
Si on définit au moins un constructeur, le compilateur ne génère pas de constructeur par défaut.

### 6.7.1 Appel d’un constructeur par un autre

Un constructeur peut en appeler un autre. Cela permet de bénéficier d'un
traitement d'initialisation déjà fourni par cet autre constructeur. Pour
réaliser cet appel, on utilise le mot clé `this`.  
Ainsi, dans l’exemple précédent, on aurait pu écrire :

```csharp
public CompteBancaire(long numéro)
{
    _numéro = numéro;
}
   
public CompteBancaire(long numéro, DateTime dateCréa, decimal découvertAutorisé) :
 this(numéro)
{
    _dateCréation = dateCréa;
    _découvertAutorisé = découvertAutorisé;
}
```

Le second constructeur appelle le premier avec `this`, en lui transmettant
son paramètre numéro. Il n’a ainsi pas besoin d’initialiser lui-même le
champ \_numéro, ce qui évite de la répétition de code.

**Remarque** : Le mot clé `this` est aussi utilisé pour représenter
l’instance courante de la classe, ce qui peut être utile pour éviter les
ambiguïtés de noms, comme illustré dans l’exemple suivant :

```csharp
class Personne
{
   private string nom; 

   // Constructeur
   public Personne(string nom)
   {
      this.nom = nom;
   }
...
```

Dans cet exemple, `this` désigne l’instance courante de personne et
`this.nom` fait référence à son champ `nom`. Comme le paramètre du
constructeur a le même nom que le champ privé, l’utilisation du mot clé
`this` est le seul moyen de distinguer les deux.

Il est cependant préférable de respecter les conventions de nommage des
champs privés (préfixés par \_) afin d’éviter de se retrouver dans cette
situation.

### 6.7.2 Initialiseurs

Dans l’exemple précédent, on initialise les champs des objets grâce aux
différents constructeurs :

```csharp
CompteBancaire cpt1 = new CompteBancaire(6546589);
CompteBancaire cpt2 = new CompteBancaire(8712564, DateTime.Today, 8000m);
```

…mais si les données sont accessibles via des propriétés modifiables, C#
propose également une autre technique pour initialiser des objets : les
initialiseurs :

```csharp
public class CompteBancaire
{
    // Constructeur
    public CompteBancaire(long numéro)
    {
        Numéro = numéro;
    }

      // Propriété non modifiable
      public long Numéro { get; }

      // Propriétés modifiables
    public DateTime DateCréation { get; set; }
    public decimal DécouvertAutorisé { get; set; }
...
}

class Program
{
    static void Main(string[] args)
    {
        // Initialisation d’un compte avec un constructeur + un initialiseur
         CompteBancaire cpt = new CompteBancaire(8712564)
        {
            DateCréation = DateTime.Now,
            DécouvertAutorisé = 800m
        };
      }
}
```

L’initialiseur est la partie entre accolades. Il permet d’affecter les
valeurs des propriétés modifiables de l’instance en les nommant, et en
séparant les affectations par des virgules.

?> Un initialiseur n’est donc qu’une syntaxe condensée regroupant
l’initialisation de propriétés avec l’appel d’un constructeur.

NB/ Lorsque le constructeur appelé est celui sans paramètre, on peut
omettre les parenthèses du constructeur :

```csharp
CompteBancaire cpt = new CompteBancaire
{
   DateCréation = DateTime.Now,
   DécouvertAutorisé = 800m  
};
```

On peut aussi utiliser un initialiseur pour initialiser les éléments
d’un tableau ou d’une collection :

```csharp
int[] tab = new int[] { 12, 7, 25, 9 };
```

## 6.8 Membres et classes statiques

**Un champ statique** (appelé aussi *variable de classe*) est un champ
dont la valeur est partagée par toutes les instances de cette classe (si
cette classe n’est pas elle-même statique).

NB/ Les constantes sont des champs statiques.

**Une méthode ou propriété statique** (appelée aussi *méthode de
classe*) est une méthode ou propriété qui peut être appelée sans
instance de classe, simplement en mettant le nom de la classe devant
(ex : `Console.WriteLine()`).  
Elle n’a accès qu’aux champs statiques de la classe.

?> Une application alloue de la mémoire à ses objets et membres statiques dès son lancement.  Alors qu'elle alloue de la mémoire aux autres objets de façon dynamique, c'est à dire au fur et à mesure des besoins.

Dans l’exemple ci-dessous, `Cos` et `PI` sont respectivement une méthode
statique et un champ statique de la classe `Math`.  
On voit qu’on peut les utiliser directement sans instancier la classe Math.

```csharp
Console.WriteLine(Math.Cos(Math.PI / 3));
```

**Une classe statique** est une classe qui ne peut pas être instanciée.
Elle ne peut contenir **que** des membres statiques.

NB/ Une classe non statique peut contenir des membres statiques et non
statiques.

Pour déclarer une classe ou un membre statique, on utilise le mot clé
`static` devant son nom

L’exemple ci-dessous montre comment utiliser un champ statique pour compter le nombre d’instances de classes créées.

```csharp
using System;
namespace SyntaxeCSharp
{
   class MaClasse
   {
      private static int _nbObjetsCrees = 0;
      public static int NbObjets
      {
         get { return _nbObjetsCrees; }
      }

      public MaClasse()
      {
         _nbObjetsCrees++;
      }
   }
   
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine(MaClasse.NbObjets); // Affiche 0
         MaClasse m = new MaClasse();
         Console.WriteLine(MaClasse.NbObjets); // Affiche 1
      }
   }
}
```

**Un constructeur statique** est appelé une seule fois avant tout autre
constructeur, et permet d'initialiser des champs statiques, ou de faire
des actions qui ne doivent être faites qu’une seule fois.  
Il est appelé automatiquement avant la création de la première instance, ou avant
l’accès à un membre statique.

Il ne faut pas préciser de modificateur de visibilité
(`public/protected/private`) pour ce constructeur.

```csharp
public class Couleur
{
   // Constructeur statique (appelé automatiquement en premier)
   static Couleur()
   {
      Console.WriteLine("Appel au constructeur statique");
   }

   // Constructeur ordinaire
   public Couleur(string c)
   {
      Console.WriteLine(c);
   }
}
class Program
{
   static void Main(string[] args)
   {
      Couleur c = new Couleur("Vert");
   }
}
```

Sortie console :

```
Appel au constructeur statique
Vert
```
