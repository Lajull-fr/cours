# 10. Types valeur et référence

## 10.1 Allocation mémoire, pile et tas

Les variables nécessitent qu’on leur alloue de la place en mémoire. Il
existe deux types d'allocations de mémoire :

-  **L'allocation statique** : elle se fait au lancement du programme.
   Les performances sont optimales, puisqu'on évite les coûts de
   l'allocation dynamique durant l'exécution ; la mémoire statique est
   immédiatement utilisable. Elle stocke tous les champs et fonctions
   statiques (cf. plus bas)

-  **L'allocation dynamique** : elle se fait pendant l'exécution du
   programme.

L'allocation dynamique peut être faite dans :

-  **La pile** : c'est une zone mémoire dans laquelle l'allocation et la
   libération sont gérées automatiquement par le programme. Typiquement,
   une variable locale à l’intérieur d’une fonction est gérée dans la
   pile. Elle se voit allouer une zone mémoire au moment de son
   initialisation, et cette zone est automatiquement libérée à la sortie
   de la fonction.

-  **Le tas** : c'est une zone mémoire dans laquelle l'allocation et la
   libération sont gérées par le ramasse-miettes


Différences majeures entre la pile et le tas (cf. [cette
page](http://stackoverflow.com/questions/79923/what-and-where-are-the-stack-and-heap)
pour plus de détails) :

-  Chaque thread a sa pile, tandis que le tas est partagé (1 par appli).

-  La taille de la pile est déterminée quand le thread est créé, tandis
   que la taille du tas peut augmenter au cours de la vie de l'appli,
   selon ses besoins.

-  La pile est plus rapide du fait de sa gestion LIFO (Last Input, First
   Output), et son contenu est fréquemment utilisé, ce qui tend en plus
   à utiliser le cache du processeur, qui est très rapide.

La pile et le tas sont tous deux gérés dans la RAM.

Le CLR .net fournit un **ramasse-miettes (Garbage Collector)**, qui
permet au développeur de faire de l'allocation dynamique sur le tas,
sans avoir à se soucier de la libération de la mémoire, ou presque, ce
qui simplifie beaucoup la tâche, et évite les fuites mémoires.

## 10.2 Les types

En C, on choisit ce que l'on met dans la pile et ce que l'on met dans le tas.

En C#, c’est différent : les variables de type valeur sont créées dans
la pile, tandis que les variables de type référence sont créées dans le
tas. Une variable de type référence est une variable pour laquelle
l'accès se fait via une référence.

**Quels sont les types valeur ?**  
Tous les types primitifs, les structures et les énumérations.

NB/ Les types primitifs (`byte, char, int, bool…`) sont en fait également
des structures en C#

**Quels sont les types référence ?**  
Les classes et les délégués.

NB/ `string` est une classe, bien qu’elle s’utilise comme un type valeur

Quand on instancie une classe (i.e. on crée un objet) avec le mot clé
`new`, on réserve de la mémoire dans le tas, et on récupère une référence
sur l'emplacement réservé.

**Quand la mémoire est-elle libérée ?**  
Pour les variables de type valeur, à la fin du bloc de déclaration.  
Pour les objets de type référence, lorsqu’il n'y a plus aucune référence sur l’objet (il n'y a
alors plus aucun moyen d'y accéder), le ramasse-miettes peut récupérer
son emplacement en mémoire. Il ne le fait pas tout de suite, mais dès
que la quantité de mémoire utilisée par l’application dépasse un certain seuil.

## 10.3 Affectation

L'affectation n'a pas le même sens pour les variables de type valeur et
celles de type référence.

### 10.3.1 Affectation pour les types valeur

Une variable de type valeur contient des données, et est allouée dans la
pile, même si elle est initialisée avec `new`.

```csharp
int i = 18;
int j = new int();
// Les variables i et j sont allouées dans la pile. j prend la valeur par défaut 0
```

…et l'affectation concerne la valeur de la variable.

```csharp
int a=12,b=4; // a contient 12 et b contient 4
a = b;
// a contient désormais la valeur 4. 
// la valeur de b a été affectée à la variable a
```

### 10.3.2 Affectation pour les types référence

L'allocation des variables est faite dans le tas, et la libération de
l’espace mémoire est gérée par le ramasse-miettes (Garbage Collector).

On peut voir les références (les variables qui désignent les objets)
comme des sortes de pointeurs sécurisés. Par exemple, une référence qui
ne désigne aucun objet vaut `null`. C'est la valeur par défaut d'une
référence non initialisée.

Pour les variables de type référence, l'affectation réalise seulement
une **copie des références** (comparable à une copie de pointeurs en C++).

Ex : Considérons le code ci-dessous :
```csharp
class Personne
{
   private string _nom;
   
   public void SetNom(string nom)
   {
      _nom = nom;
   }
   
   public string GetNom()
   {
      return _nom;
   }
}

class Program
{
   static void Main(string[] args)
   {
      Personne p1 = new Personne();
      p1.SetNom("Lenoir");
   
      Personne p2 = p1;   // p1 et p2 désignent la même personne
      p2.SetNom("Leblanc"); // modifie le nom de la personne 
        
      Console.WriteLine(p1.GetNom()); // Affiche : Leblanc
      Console.WriteLine(p2.GetNom()); // Affiche : Leblanc
      Console.ReadKey();
   }
}
```
Puisque Personne est un type référence, après affectation de p1 à p2, p2
fait référence au même objet que p1.  
P1 et p2 font toutes les deux références à la même zone mémoire. C’est pourquoi on peut modifier le
nom de la personne via la variable p2.

## 10.4 Test d’égalité avec l'opérateur ==

| **Type valeur** | **Type référence** |
|-----------------|-----------------|
| L'opérateur `==` teste l'égalité de valeur.<br>Pour une structure, il teste l'égalité de valeur de tous les champs et renvoie `true` seulement si tous les champs sont égaux 2 à 2. | L'opérateur `==` teste si les 2 références désignent le même objet.<br> Cependant, il peut être surchargé pour se comporter de la même façon que pour les types valeur.<br>C’est par exemple ce qui est fait pour le type `string` : bien que ce soit un type référence, l'opérateur == teste l'égalité des chaînes (même longueur et mêmes caractères), et non celle des références. |

## 10.5 Synthèse

Le tableau suivant résume les différences entre les types valeur et les
types référence :

| **Type valeur** | **Type référence** |
|-----------------|----------------------|
| Stocké sur la pile | Stocké sur le tas |
| Contient la valeur effective  | Contient une référence vers une valeur |
| Ne peut pas avoir la valeur null (à moins de spécifier un type nullable) | Peut contenir la valeur null |
| La mémoire est libérée lorsque l’application sort de la portée de la variable | La mémoire est libérée par le ramasse-miettes |

## 10.6 Structures et classes

Les classes sont un concept spécifique à la programmation orientée objet.  
Les structures sont un concept semblable qui existe également dans certains
langages non orientés objet (notamment le C).

**Points communs** :

-  Peuvent contenir des champs, propriétés, méthodes et constructeurs

-  Peuvent implémenter des interfaces

**Différences** :

-  Les structures sont des types valeur, alors que les classes sont des
   types référence.

-  Les structures ne peuvent pas être dérivées

-  On ne peut pas créer de constructeur sans paramètre dans une
   structure

-  Le compilateur n’initialise pas par défaut les champs d’une
   structure, alors qu’il le fait pour une classe.

Si l’on reprend l’exemple précédent en définissant `Personne` comme une
structure en remplaçant le mot `class` par `struct`, alors le même code produit un résultat différent :

```csharp
class Program
{
   static void Main(string[] args)
   {
      Personne p1 = new Personne();
      p1.SetNom("Lenoir");
         
      Personne p2 = p1;   // Crée une personne p2 en copiant les champs de p1
      p2.SetNom("Leblanc"); // modifie le nom de p2
        
      Console.WriteLine(p1.GetNom()); // Affiche : Lenoir
      Console.WriteLine(p2.GetNom()); // Affiche : Leblanc
      Console.ReadKey();
   }
}
```

Puisque Personne est cette fois un type valeur, l’affectation de p1 à p2
crée une nouvelle personne et copie les valeurs des champs de p1 dans
p2. P1 et p2 représentent bien 2 personnes distinctes. C’est
pourquoi la modification de p2 n’affecte pas p1.

**Intérêt des structures**

Le principal avantage des structures par rapport aux classes est leur
accès mémoire plus rapide, du fait qu’elles sont stockées dans la pile.  
Cependant, lorsqu’une variable de type structure est passée en paramètre
à une fonction, les données qu’elle contient sont dupliquées en mémoire,
puisque le passage se fait par valeur. C’est pourquoi les structures ne
devraient pas stocker plus de quelques octets de données pour que leur
usage soit justifié.  
Les structures sont ainsi surtout utilisées dans la programmation de jeux vidéo, 
où la recherche de performance est un point essentiel.

?> les types primitifs (`byte, char, int, bool…`) sont en
fait des alias de structures qui fournissent des méthodes telles que `ToString`,
`Parse`, `TryParse`, `Equals`…etc.  
Il est ainsi possible d’écrire : `7.ToString()`.

## 10.7 Types nullables

La valeur `null` peut être assignée aux variables références. Elle
signifie « aucune mémoire allouée ». Elle permet d’initialiser les
variables de type référence avant de leur affecter une référence
d’instance avec new.

```csharp
Animal a = null;
a = new Animal();
```

Par défaut, on ne peut pas assigner `null` à une variable de type valeur.  
L’instruction suivante est par conséquent invalide en C# :

```csharp
int i = null; // invalide
```

Cependant, C# définit le modificateur `?` pour déclarer qu’une
variable de type valeur est nullable.  
On peut alors lui assigner la valeur `null` :

```csharp
int? i = null; // valide
if (i == null) // valide
{ 
   ...
```

On ne peut pas assigner directement la valeur d’une variable nullable à
une variable non nullable.

```csharp
int? i = 3;
int j = i; // invalide
```

En fait, un type nullable est une structure (cela reste donc bien un
type valeur), qui possède deux propriétés publiques :

-  `HasValue`, de type booléen. Elle a la valeur true lorsque la variable
   contient une valeur différente de null.

-  `Value` qui contient une valeur significative si HasValue est
   vraie, ou qui lève une exception InvalidOperationException dans le
   cas contraire.

**Intérêt des types nullables**

Pour les types numériques, il est parfois utile de faire la différence
entre la valeur 0 et « pas de valeur ».  
Par exemple un score représenté par un entier peut être null si le joueur n’a pas encore joué, 
tandis que la valeur 0 signifiera que le joueur à réellement fait un score de 0.

Dans une base de données, les tables peuvent contenir des champs
nullables. Si on récupère la valeur d’un champ de ce type dans une
propriété C#, on peut définir cette propriété comme nullable de façon à
pouvoir représenter la valeur Null issue de la base.

## 10.8 Typage implicite avec var

Lorsqu’on instancie un objet, on peut utiliser la syntaxe suivante :

```csharp
var obj = new MaClasse(20);
```

Avec `var`, le type de la variable obj n’est pas spécifié explicitement.
Il est déduit implicitement par le compilateur, à partir du nom de la
classe qui suit le mot clé `new`.

**Avantage** : dans le cas de noms de classes longs, éventuellement précédés
du nom de leur espace de noms, cela raccourcit la syntaxe et rend le
code plus lisible :

```csharp
Dictionary<string, Animal> dico = new Dictionary<string, Animal>();
```
...peut s’écrire plus simplement :

```csharp
var dico = new Dictionary<string, Animal>();
```

`var` est également utilisable avec les types primitifs, mais l’intérêt
est moindre. Cela rend au contraire le code moins facile à lire.

```csharp
var x = 12.5; // x est implicitement défini par le compilateur comme un double
```

`var` n’est utilisable que si l’on fournit une valeur d’initialisation, 
qui est nécessaire au compilateur pour déduire le type.

!> `var` n’est qu’une facilité d’écriture. Il ne signifie pas que le
type de la variable peut changer.  
Le type d’une variable ne peut en aucun cas changer après sa déclaration.

## 10.9 Boxing / unboxing

En .NET, le type `System.Object` est la classe ancêtre de plus haut
niveau, de laquelle dérivent implicitement toutes les classes, de façon
directe ou indirecte. 
Comme une variable de classe ancêtre peut faire
référence à une instance de classe dérivée, une variable de type `Object`
peut donc faire référence à n’importe quel type référence (i.e. à
n’importe quelle classe).

**Le boxing** étend ce concept en permettant à une variable de type
Object de faire référence également à n’importe quel type valeur. Ainsi,
il est possible d’écrire ce qui suit :

```csharp
double d = 12.34;
Object o = d; // correct grâce au boxing
```

La deuxième instruction nécessite une explication pour comprendre ce qui
se passe vraiment :  
`d` est de type valeur; sa valeur est donc stockée sur la pile.  
Si la référence à l’intérieur de l’objet `o` pointait directement sur `d`, elle ferait
référence à la pile, ce qui créerait un défaut de sécurité potentiel.  
Par conséquent, le runtime réserve un bloc de mémoire sur le tas, y
copie la valeur du réel `d`, puis référence cette copie dans l’objet `o`.  
Cette copie automatique d’un élément de la pile vers le tas est appelée
conversion boxing. Elle est illustrée par le schéma suivant :

![Boxing](images/boxing.png ':size=50%')

**L’unboxing** est l’opération inverse du boxing. Elle consiste à
convertir une copie boxed en type valeur.  
La conversion doit cette fois être explicite au moyen d'un transtypage :

```csharp
Object o = 12.34; // boxing
double d = (double)o; // unboxing
```
![Unboxing](images/unboxing.png ':size=50%')

> Pour rappel, les opérations de transtypage doivent être sécurisées au moyen des opérateurs `is` ou `as`, sinon l'application est susceptible de lever une exception `InvalidCastException` à l’exécution.

!> Les conversions boxing et unboxing sont des opérations coûteuses en
raison du nombre de vérifications nécessaires et du besoin d’attribuer
de la mémoire supplémentaire à partir du tas.  

La conversion boxing a son utilité, mais une utilisation abusive peut diminuer fortement les
performances d’un programme. Les génériques sont un bon moyen d’éviter le boxing, comme nous le verrons plus loin.
