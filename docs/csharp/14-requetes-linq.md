# 14. Requêtes LINQ

## 14.1 Présentation

**LINQ** : Language Integrated Query

LINQ permet de rechercher facilement dans une collection les éléments
qui correspondent à un ensemble de critères. Par exemple, dans une
collection d’objets Client, trouver tous les clients qui habitent à
Londres, ou encore déterminer la ville qui a le plus de clients.

Sa syntaxe rappelle celle du SQL.

LINQ nécessite que les données soient stockées dans un conteneur qui
implémente l’interface `IEnumerable`. C’est le cas des tableaux et de
toutes les collections, génériques ou non, que nous avons étudiées
précédemment.

**1<sup>er</sup> exemple :**
```csharp
// Extraction des nombres pairs d'un tableau
int[] tab = new int[] { 1, 2, 3, 4, 5, 6 }; 
IEnumerable<int> pairs = from number in tab
                        where number % 2 == 0 
                        select number; 
   
foreach(int i in pairs) Console.WriteLine(i); 
```

Cette requête simple récupère tous les nombres pairs d’un tableau
d’entiers. Elle introduit déjà les concepts essentiels :

-  La syntaxe ressemble à celle d’une requête SQL avec les mots clés
   `from`, `where` et `select`

-  Contrairement à SQL, la clause `select` est toujours placée à la fin.
   Cet ordre est en fait plus naturel et permet à Visual Studio de
   proposer l’intellisense dans l’ensemble de la requête.

-  Le résultat d’une requête est de type `IEnumerable<T>`, (ici T = int).
   Pour simplifier on peut tout à fait utiliser une variable typée
   implicitement (var).

-  `number` est un paramètre. En fait c’est le paramètre d’une expression
   lambda, comme nous le verrons plus bas.

-  Dans la clause `where`, attention à bien utiliser l’opérateur `==` et
   non pas `=`, car on fait bien un test et non une affectation.

**2<sup>ème</sup> exemple**

Prenons cette fois une requête retournant des types références.

Pour cela, créons un jeu de données à partir des classes créées dans les
chapitres précédents. On crée ici une liste d’animaux domestiques
(chiens et chats), en spécifiant leur sexe, poids, nom et date de
naissance.

```csharp
var anx = new List<AnimalDomestique>();

anx.Add(new Chat(Sexes.Femelle, 1.35, "Minette", new DateTime(2015, 11, 01)));
anx.Add(new Chat(Sexes.Male, 2.12, "Ponpon", new DateTime(2014, 02, 28)));
anx.Add(new Chat(Sexes.Femelle, 2.12, "Zica", new DateTime(2014, 09, 13)));
anx.Add(new Chien(Sexes.Male, 10.8, "Clovis", new DateTime(2011, 04, 14)));
anx.Add(new Chien(Sexes.Femelle, 8.4, "Rita", new DateTime(2012, 10, 17)));
anx.Add(new Chat(Sexes.Male, 3.4, "Grisou", new DateTime(2015, 01, 05)));
anx.Add(new Chat(Sexes.Femelle, 1.7, "Nina", new DateTime(2013, 07, 31)));
anx.Add(new Chien(Sexes.Femelle, 9.5, "Tina", new DateTime(2010, 05, 21)));
anx.Add(new Chien(Sexes.Male, 2.1, "Minus", new DateTime(2015, 10, 07)));
anx.Add(new Chien(Sexes.Male, 42.0, "Brutus", new DateTime(2011, 03, 11)));
```

Pour récupérer la liste des chats mâles, on écrira la requête suivante :
```csharp
// Liste des chats mâles
var chats = from a in anx
            where a is Chat && a.Sexe == Sexes.Male
            select a;

// Affichage de leurs noms
foreach (var c in chats) Console.WriteLine(c.Nom);
```

Le résultat est de type `IEnumerable<AnimalDomestique>`. On le récupère
dans une variable typée implicitement.

Dans la clause `where`, il est intéressant de noter qu’on peut utiliser le
type sous-jacent de `AnimalDomestique` (qui est une classe abstraite)
comme critère de sélection au moyen de `is`.

## 14.2 Les 2 syntaxes LINQ

Nous avons vu dans les exemples précédents que les expressions de
requêtes LINQ sont semblables à des requêtes SQL. Il existe cependant
une autre manière de formuler les requêtes LINQ, en utilisant des
méthodes et des expressions lambda. On l’appelle quelques fois **syntaxe pointée**, 
du fait que les appels de méthodes peuvent être enchaînés en les séparant par des points.

Reprenons les exemples précédents et comparons les 2 syntaxes :

```csharp
// Extraction des nombres pairs d'un tableau
int[] tab = new int[] { 1, 2, 3, 4, 5, 6 }; 

IEnumerable<int> pairs = from number in tab
                        where number % 2 == 0 
                        select number; 

IEnumerable<int> pairs2 = tab.Where(n => n % 2 == 0);

// Liste des chats mâles
var chats = from a in anx
            where a is Chat && a.Sexe == Sexes.Male
            select a;

var chats2 = anx.Where(c => (c is Chat) && (c.Sexe == Sexes.Male));
```

Dans la syntaxe pointée, le filtrage est réalisé au moyen d'une expression lambda utilisée dans la méthode `Where()`.

La syntaxe sous forme de requêtes est sans doute plus intuitive, tandis que la syntaxe pointée est plus condensée. Cependant, la différence majeure réside surtout dans le fait que :

!> La syntaxe sous forme de requêtes ne permet pas de réaliser toutes les opérations permises par LINQ.

Lorsqu'on souhaite réaliser une opération qui n'est pas exprimable sous forme de requête, on peut soit l'écrire entièrement en syntaxe pointée, soit écrire uniquement la partie nécessaire en syntaxe pointée, comme le montre l'exemple suivant :

```csharp
// Date de naissance du chat le plus vieux
DateTime dateNais = (from a in anx
                  where a is Chat
                  select a.DateNais).Min();
```

## 14.3 Opérateurs courants de LINQ

Les opérateurs de requête standard sont des fonctions de filtrage, de
projection, d'agrégation, de tri, etc.

Nous allons voir ici les plus communs :

**Filtrage**

La liste des chats mâles peut en fait s’obtenir également de cette façon :
```csharp
var chats = anx.OfType<Chat>().Where(c => c.Sexe == Sexes.Male);
```

La méthode `OfType<>()` permet de filtrer selon le type.

La méthode `Where()` permet de filtrer selon n’importe quelle propriété de
ce type

**Calculs d’agrégation : min, max, somme, moyenne, dénombrement**

Ces opérations ne sont réalisables qu’avec la syntaxe pointée. Voici
quelques exemples :

```csharp
// Poids moyen des chiens
var poidsMoyen = anx.OfType<Chien>().Average(a => a.Poids);
Console.WriteLine("Poids moyen des chiens : {0}", poidsMoyen);

// Date de naissance du chien mâle le plus vieux
var dateNais = anx.OfType<Chien>().Where(c => c.Sexe == Sexes.Male).Min(a => a.DateNaissance);
Console.WriteLine("Date de naissance du chien mâle le plus vieux : {0}", dateNais);

// Nombre total d'animaux
Console.WriteLine("Nombre d'animaux : {0}", anx.Count());

// Regroupement des animaux par année de naissance
IEnumerable<IGrouping<int, Animal>> groupes =
   anx.OrderBy(a => a.DateNaissance).GroupBy(a => a.DateNaissance.Year);

foreach (var gp in groupes)
{
   Console.WriteLine($"{gp.Count()} animal(aux) né(s) en {gp.Key} :");
   foreach (var ani in gp)
   {
      Console.WriteLine($"\t{ani.Nom}");
   }
}
```

La méthode `GroupBy` renvoie ici une collection de groupes d’animaux. Le
premier groupe contient les animaux nés en 2010, le second contient les
animaux nés en 2011…etc. Sur chaque groupe, on peut obtenir la valeur de
regroupement (ici l’année) avec la propriété `Key`.

**Tri**
```csharp
// Chat le plus vieux
var ani = anx.OfType<Chat>().OrderBy(a => a.DateNaissance).FirstOrDefault();
Console.WriteLine("Le chat le plus vieux est {0}. Il est né le {1})", ani.Nom, ani.DateNaissance);
```

Dans cet exemple, on trie la liste des chats selon leur date de
naissance avec la méthode `Orderby()`, et on prend le premier chat de la
liste avec la méthode `First()`.

!> LINQ offre un moyen commode pour filtrer, trier et faire des calculs sur
les données. Il ne doit cependant pas se substituer aux requêtes SQL sur
la base de données !

Dans une architecture n-tiers où les données sont stockées dans une base
de données, il est généralement beaucoup plus performant de faire le
maximum d’opérations en SQL via des requêtes ensemblistes, plutôt que
de récupérer des données brutes, puis les traiter par le code C#.

Bien que les requêtes LINQ soient généralement très concises, elles
peuvent engendrer des traitements très peu performants, du fait des
multiples parcours de collections nécessaires. Il convient donc toujours
de se demander si l’utilisation de LINQ est pertinente dans le
contexte courant, et si oui, de vérifier les performances des requêtes.
