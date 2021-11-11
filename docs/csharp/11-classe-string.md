# 11. La classe string

## 11.1 Généralités 

La classe `System.String` de .NET permet de gérer des chaînes de caractères.  
Il s'agit d'un type référence, mais dont l'usage ressemble à
celui des types valeurs.  
En effet, pour le type string, l'affectation (le signe =)
fait en réalité une copie des valeurs, et non des références.  

L'autre caractéristique est que les objets de cette classe sont
***immutables***. Cela signifie que les objets gardent la même valeur du
début à la fin de leur vie. Toutes les opérations visant à changer la
valeur de l'objet retourneront en réalité un nouvel objet.

Voici plusieurs façons de créer des instances de la classe `string `:
```csharp
class Program
{
   static void Main(string[] args)
   {
      string s1 = "abc";
      string s2 = new string('*', 7);
      var ar = new char[] { 'e', 'f', 'g' }
      string s3 = new string(ar);

      Console.WriteLine(s1); // abc
      Console.WriteLine(s2); // *******
      Console.WriteLine(s3); // efg
   }
}
```

Les membres principaux de la classe `string` :

**Propriétés**

`[]`: indexeur qui permet d’accéder au caractère à la position
spécifiée

`Static readonly string Empty` : Représente la chaîne vide

`int Length` : obtient le nombre de caractères

**Méthodes**

`bool EndsWith(string value)` : renvoie vrai si la chaîne se termine par value  

`bool StartsWith(string value)` : renvoie vrai si la chaîne commence par value

`virtual bool Equals(object obj)` : renvoie vrai si la chaîne est égale à obj

`static string Format(string format, params object\[\] args)` : remplace
un ou plusieurs éléments de mise en forme d’une chaîne par la
représentation sous forme de chaîne d’un objet spécifié

`static bool IsNullOrEmpty(string value)` : indique si la chaîne spécifiée
est null ou une chaîne String.Empty

`int IndexOf(string value, int startIndex)` : renvoie la première position
de value dans la chaîne. La recherche commence à partir du caractère n° startIndex

`int IndexOf(char value, int startIndex)` : idem, mais pour le caractère value

`int LastIndexOf(string value, int startIndex)` : renvoie la dernière
position de value dans la chaîne. La recherche commence à partir du
caractère n° startIndex.

`string Replace(string oldValue, string newValue)` : renvoie une chaîne
dans laquelle la chaîne oldValue a été remplacée par la chaîne newValue

`string\[\] Split(char\[\] separator)` : la chaîne est vue comme une suite
de champs séparés par les caractères présents dans le tableau separator.
Le résultat est le tableau de ces champs

`string Substring(int startIndex, int length)` : sous-chaîne de la chaîne
courante commençant à la position startIndex et ayant length caractères

`string ToLower()` : renvoie la chaîne courante en minuscules

`string ToUpper()` : renvoie la chaîne courante en majuscules  

`string Trim()` : supprime les espaces blancs en début et en fin de chaîne

```csharp
static void Main(string[] args)
{
   string s1 = "chaîne de caractères";
   Console.WriteLine(s1.Contains("car"));  // True
   Console.WriteLine(s1.StartsWith("ch")); // True
   Console.WriteLine(s1.IndexOf('c'));     // 0
   Console.WriteLine(s1.LastIndexOf('c')); // 14
   Console.WriteLine(s1.Substring(4, 2));  // ne
}
```

Remarque : Pour vérifier si une chaîne est vide, utiliser la méthode
ci-dessus ou la comparaison avec String.Empty (si on est sûr que la
valeur n’est pas null). Ne **pas** utiliser la comparaison avec "", qui
est moins performante.

## 11.2 Méthode ToString et formats

Tout objet dérive de la classe `Object`, et possède donc une méthode
`ToString()`. L’implémentation par défaut de cette méthode se contente de
renvoyer le nom de la classe de l’objet (peu intéressant).

Les classes qui redéfinissent la méthode `ToString()` font généralement en
sorte que la méthode renvoie une représentation plus significative de
l’objet. Par exemple, une classe Personne redéfinira ToString pour
qu’elle renvoie une chaîne composée du prénom et du nom de la personne.

Il peut cependant être nécessaire de spécifier en plus un format pour
cette chaîne, notamment si l’objet contient des nombres ou des dates.
Pour ce faire, la classe de l’objet doit implémenter l'interface
`IFormattable`, qui contient une surcharge de la méthode ToString
prenant 2 paramètres :

```csharp
interface IFormattable
{	
   string ToString(string format, IFormatProvider formatProvider)
}
```

Paramètres :

-  `format` : chaîne fournissant des instructions de formatage,
   constituée d'une lettre et d’un nombre optionnel.

-  `formatProvider` : objet permettant de réaliser les instructions de
   formatage selon une culture donnée

Le code ci-dessous affiche un prix avec un format dans différentes cultures :

```csharp
Console.OutputEncoding = Encoding.Unicode;
decimal prix = 3.5m;

string s0 = prix.ToString("C2");
Console.WriteLine(s0);

string s1 = prix.ToString("C2", CultureInfo.GetCultureInfo("en-US"));
Console.WriteLine(s1);

string s2 = prix.ToString("C2", CultureInfo.InvariantCulture);
Console.WriteLine(s2);
```

Sortie console :
```
3,50 €
$3.50
¤3.50
```
Le format `C2` indique que le nombre doit être formaté en monnaie
(Currency), avec 2 chiffres après la virgule.

Dans le 1<sup>er</sup> cas, comme on ne spécifie aucune culture, c’est
la culture par défaut (`CurrentCulture`) qui est utilisée. Celle-ci est
définie par les paramètres régionaux du système d'exploitation.

Dans le 2<sup>ème</sup> cas, on a spécifié explicitement la culture
« en-US ». Le prix est donc affiché en dollars.

Dans le 3<sup>ème</sup> cas, on a spécifié la culture "Invariant", qui est
indépendante de toute langue, tout pays et tout paramétrage du poste.

**Chaînes de formats possibles**

Il existe un ensemble de chaînes de formats prédéfinies pour chaque
type. Exemples :

-  **Pour les nombres** : C = Currency, D = Decimal, G = général, F = fixed
   point…etc.

-  **Pour les dates** : d = date courte, D = date longue, f = date et heure
   courtes…etc.

-  **Pour les types énumérés** : G et F affichent la valeur textuelle, D
   affiche la valeur numérique

En plus de formats prédéfinis, il est possible de définir des formats
personnalisés.

Exemples :
```csharp
DateTime date = new DateTime(2016, 07, 14, 13, 58, 35);
Console.WriteLine(date.ToString("d"));
Console.WriteLine(date.ToString("F"));
Console.WriteLine(date.ToString("ddd %d/%M/yy"));
```

Sortie console :
```
14/07/2016
jeudi 14 juillet 2016 13:58:35
jeu. 14/7/16
```

On a appliqué ici successivement trois formats différents à la même
date. Les deux premiers sont des formats prédéfinis, et le
3<sup>ème</sup> est un format personnalisé.

?> L’ensemble des formats possibles est présenté sur [cette page de doc Microsoft](https://docs.microsoft.com/fr-fr/dotnet/standard/base-types/formatting-types).

## 11.3 Méthode Format

Cette méthode permet de construire des chaînes de caractères formatées
contenant des valeurs paramétrables.
```csharp
string s = string.Format("{0} est né en {1}\nIl a donc {2} ans",
                        "Paul", 1998, DateTime.Now.Year-1998);
Console.WriteLine(s);
```

Sortie console :
```
Paul est né en 1998
Il a donc 23 ans
```

`Format` accepte un nombre variable de paramètres de tous types,
grâce au fait qu’elle utilise un tableau d’objets params. C’est un
concept que nous verrons dans le chapitre sur les tableaux.  

Le premier paramètre est une chaîne appelée **chaîne de format composite**.  
Elle contient du texte fixe et des éléments de format `{0}, {1}…{N}` qui sont substitués par les valeurs passées aux paramètres suivants de la fonction.

Dans notre exemple :
- {0} est substitué par "Paul""
- {1} est substitué par 1998
- {2} est substitué par le résultat du calcul de l'âge
- \\n permet de faire un retour à la ligne

La méthode `Format` appelle en interne la méthode `ToString` sur chacun des paramètres
qu’on lui passe.

`Format` possède une surcharge qui prend en premier paramètre la culture à utiliser.  
De plus, on peut préciser le format à appliquer sur chaque paramètre, comme le montre l’exemple suivant :

```csharp
string s = string.Format(CultureInfo.GetCultureInfo("en-US"),
            " Un réel : {0,12:C2}\n Une chaîne : {1}\n Une date : {2:d}",
            123.1548, "coucou", DateTime.Now);
Console.WriteLine(s);
```
Sortie console :
```
Un réel :      $123.15
Une chaîne : coucou
Une date : 2/11/2017
```

Dans cet exemple :

-  Le premier paramètre spécifie la culture "en-US", c'est-à-dire la
   langue anglaise et le pays USA.

-  Le format `{0,12:C2}` indique que le paramètre N°1 sera affiché sur 12
   caractères alignés à droite, sous le format monétaire, avec 2 chiffres après la virgule.

NB/ Avec le format `{0,-12:C2}` le prix serait aligné à gauche.

**Remarques :**

-  La méthode `Console.WriteLine` se comporte comme `String.Format`, à ceci
   près qu’on ne peut pas spécifier la culture. On peut donc écrire par exemple :

```csharp
Console.WriteLine (" Un réel : {0:C1}\n Une chaîne : {1}\n Une date : {2:d}",
                   123.1548, "coucou", DateTime.Now);
```

-  Pour afficher certains caractères spéciaux dans la console, il peut
   être nécessaire de changer le jeu de caractères à utiliser, comme par
   exemple UTF8. Voici la syntaxe :

```csharp
Console.OutputEncoding = Encoding.UTF8;
```

## 11.4 Chaînes interpolées
C#6 introduit la notion de **chaîne interpolée**, dont la construction est plus explicite et intuitive que celle d'une chaîne de format composite :

```csharp
string nom = "Paul";
int annéeNais = 1998;
int age = DateTime.Today.Year - annéeNais;

Console.WriteLine($"{nom} est né en {annéeNais}\nIl a donc {age} ans");
```
Caractéristiques :
- Le signe `$` indique que la chaîne qui suit est une chaîne interpolée.  
- La chaîne utilise directement des variables, ce qui plus explicite que des numéros.  
- On peut appliquer sur les variables les mêmes formats d'affichage que dans les chaînes de format composites. Par exemple, `{nom, 30}` affiche le nom sur 30 caractères alignés à droite.