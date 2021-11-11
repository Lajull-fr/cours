# 4. Héritages du langage C

Le C# reprend beaucoup d'éléments de syntaxe du C :

-  Structures d’instructions similaires (terminées par ;) : déclaration
   de variables, affectation, appels de fonctions, passage des
   paramètres, opérations arithmétiques

-  Blocs délimités par { }

-  Commentaires // ou /\* \*/

-  Structures de contrôles identiques : if...else, while, do...while, for

-  Portée des variables : limitée au bloc de la déclaration

## 4.1 Premier exemple console
Voici un petit programme simple qui illustre quelques notions de base du C# :
```csharp
using System;
namespace AppliConsole
{
   class Program
   {
      static void Main(string[] args) // Point d'entrée de l'application
      {
         int x = 2;  // Déclaration de variable
         for (int i = 0; i < 5; i++)
         {
            x += i;
            Console.WriteLine("i={0} x={1}", i, x); // Affichage formaté
         }

         float f = 2F; // Déclaration de variable
         while (f < 1000F)
         {
            f = f * f;
            Console.WriteLine("f={0}", f);
         }
         
         // Lecture de la ligne saisie par l’utilisateur
         Console.WriteLine("Saisissez du texte et appuyez sur Entrée");            
         string str = Console.ReadLine();

         // Affichage de la valeur lue
         Console.WriteLine("Ligne lue = {0}", str);
         
         // Attend l’appui d’une touche afin d’empêcher la console de se fermer 
         Console.ReadKey(true);
      }
   }
}
```

Une application .Net possède toujours une fonction `Main()`, qui est exécutée dès le lancement de l'application.

Les variables doivent être déclarées avant leur utilisation, mais pas obligatoirement au début de la fonction.

La classe `Console` fournit des fonctions pour lire (`ReadLine`) et écrire (`WriteLine`) une  ligne à l'écran.

L'exécution de l'application produit l'affichage suivant :
```
   i=0 x=2
   i=1 x=3
   i=2 x=5
   i=3 x=8
   i=4 x=12
   f=4
   f=16
   f=256
   f=65536
   Saisissez du texte et appuyez sur Entrée
   Ligne lue = ...
   Appuyez sur une touche pour continuer...
```


## 4.2 Types primitifs du C# (alias de types .NET)

Le .Net Framework définit les types primitifs communs à tous les langages compatibles (VB.NET, C#, F#…). Voici comment ces types sont nommés en C# :

`bool` : booléen (valeur vrai ou faux) sur 1 bit

`char` : caractère Unicode 16bits

`sbyte / byte` : entier 8 bits signé/non signé

`short / ushort` : entier 16 bits signé/non signé

`int / uint` : entier 32 bits signé/non signé

`long / ulong` : entier 64 bits signé/non signé

`float` : réel 32 bits

`double` : réel 64 bits

`decimal` (128 bits) : entier multiplié ou divisé par une puissance de 10

`string` : chaîne de caractères. Type référence qui s'utilise comme un
type valeur.

## 4.3 Littéraux / format des constantes

En C#, une lettre placée après un littéral numérique permet de spécifier
son type, qui doit être en accord avec le type de la variable qui stocke
le nombre.

**Constantes entières**
```csharp
int x = 10; // entier 
long l = 10000L; // entier long
```

**Constantes réelles**

Par défaut, les littéraux numériques à virgule sont considérés comme des double.  
Pour qu’ils soient considérés comme des float, il faut ajouter le suffixe `f` ou` F`.  
Pour qu’ils soient considérés comme des décimaux, il faut ajouter le suffixe `m` ou `M`.  

```csharp
double d1 = 10.2;  // OK
float f1 = 10.2;   // Erreur
float f2 = 10.2f;  // OK
double d2 = 1.2E-2;  // d2 = 0,012
float f3 = 3.1E2f;   // f3 = 310
decimal d4 = 2.5m;   // m fait référence à money
```

**Constantes chaînes de caractères**

`String s1 = "abc";`

Pour représenter certains caractères spéciaux, il faut utiliser une
séquence d’échappement, constituée par le caractère « \\ » suivi d’un
caractère de contrôle.

**Séquences d’échappement les plus courantes (liste complète
[ici](https://msdn.microsoft.com/fr-fr/library/aa691090(v=vs.71).aspx)) :**

`\n` : newline (nouvelle ligne)

`\t` : horizontal tab (tabulation horizontale)

`\b` : backspace (retour arrière)

`\r` : carriage return (retour charriot)

`\\` : backslash (antislash)

`\"` : double quote (guillemet double)

```csharp
string s2 = "Bonjour\nJe m’appelle \"Zorro\"";
Console.WriteLine(s2) ;
```

L’affichage de s2 ressemblera à ceci :
```
Bonjour
Je m’appelle “Zorro”
```

**Chaîne Verbatim**

Pour ne pas avoir à spécifier les séquences d’échappement, on peut
simplement préfixer la chaîne par `@`. C’est ce que l’on appelle une
chaîne Verbatim.  
Pour afficher des guillemets doubles dans une chaîne Verbatim, il faut les doubler :

```csharp
string s3 = @"Bonjour
Je m’appelle ""Zorro""";
Console.WriteLine(s3) ;
```

L’affichage de s3 sera ici strictement identique à celui de s2.

## 4.4 Fonctions et paramètres

### 4.4.1 Définition des fonctions

En C#, les fonctions sont nécessairement définies au sein de classes ou
structures, c’est pourquoi on les appelle aussi **méthodes** (terme
utilisé en programmation objet).

Une fonction comporte un en-tête et un corps :
```csharp
static int CompterMots(string phrase) // en-tête de la fonction
{
   // Corps de la fonction 
}
```

L’en-tête définit :

- **Le type du résultat** renvoyé par la fonction (ici : int).  
  Si la fonction ne renvoie pas de résultat, il faut tout de même spécifier le type `void`.

- **Le nom** de la fonction (ici `CompterMots`)

- **Les paramètres** de la fonction, placés entre parenthèses et décrits par leurs types et noms.  
S’il y a plusieurs paramètres, on les sépare par des virgules, comme dans l'exemple ci-dessous

```csharp
static int Additionner(int a, int b)
```

### 4.4.2 Paramètres passés par valeur

Lorsqu’on appelle une fonction, il faut renseigner ses paramètres.  
Dans l’exemple ci-dessous, on appelle une fonction `MultiplierPar2` en lui
fournissant en paramètre la variable `n`. La valeur de cette variable est
assignée au paramètre `nombre`, c’est à dire copiée dans la variable nombre.

```csharp
class Program
{
    static void Main(string[] args)
    {
        int n = 3;
        int res = MultiplierPar2(n);
   
        Console.WriteLine("Résultat de la fonction : " + res);
        Console.WriteLine("Valeur de n après appel de la fonction : " + n);
        Console.ReadKey();
      }

      private static int MultiplierPar2(int nombre)
      {
        nombre *= 2;
        return nombre; // return renvoie le résultat de la fonction
      }
}
```

Sortie console :
```
Résultat de la fonction : 6
Valeur de n après appel de la fonction :  3
```

La fonction n’a pas modifié la valeur initiale de la variable n, car
elle a utilisé une copie de n. Les éventuelles modifications du
paramètre dans la fonction n'ont aucune incidence sur la variable passée
en paramètre lors de l'appel de la fonction. C’est ce qu’on appelle le
passage par valeur.

### 4.4.3 Paramètres passés par référence

Il est possible de faire en sorte qu’une fonction puisse modifier les
variables qu’on lui passe en paramètres. On utilise pour cela le mot clé
`ref` placé devant la déclaration du paramètre, et devant la variable
passée en paramètre.

Le mot clé `ref` indique qu'on souhaite passer au paramètre **la référence de la variable** plutôt qu’une copie de sa valeur. C’est-à-dire que le paramètre pointera sur la même case mémoire que la variable.

```csharp
class Program
{
    static void Main(string[] args)
    {
        int n = 3;
        MultiplierPar2(ref n); // Passage de la référence de n en paramètre
   
        Console.WriteLine("Valeur de n après appel de la fonction : " + n);
        Console.ReadKey();
      }

      private static void MultipliePar2(ref int nombre)  
      // Nombre pointe sur la même case mémoire que la variable passée en paramètre
      {
        nombre *= 2; // Modifie réellement n
        // Pas besoin de valeur de retour pour la fonction
      }
}
```

La sortie console est :
```
Valeur de n après appel de la fonction : 6
```

Pour faire un passage de paramètre par référence, il faut que :

-  La variable passée en paramètre soit initialisée avant l’appel de la fonction.

-  Le mot clé `ref` soit placé devant la déclaration du paramètre dans
   l’en-tête de la fonction, et devant le nom de la variable dans
   l’appel de la fonction

?> Notez que comme la variable passée en paramètre est réellement modifiée
par la fonction, la fonction n’a pas besoin de renvoyer de résultat
(retour de type `void`).

### 4.4.4 Paramètres de sortie

Les paramètres que nous avons vus précédemment sont des paramètres
d’entrée de la fonction. La fonction reçoit les valeurs ou références de
variables extérieures et les utilise dans son code interne.

Mais on peut également définir des paramètres de sortie. Dans ce cas,
c’est la fonction qui fournit les valeurs de ces paramètres, et c’est le
code externe qui exploite ces valeurs.

Pour déclarer un paramètre de sortie, on utilise le mot-clé `out` placé juste avant.

Voici un exemple de mise en œuvre avec une fonction qui génère un nombre aléatoire :

```csharp
class Program
{
   static void Main(string[] args)
   {
      int n; // variable non initialisée
      GenererNombreAleatoire(out n); // Passage de la référence de n en paramètre

      Console.WriteLine("Valeur de n : " + n);
      Console.ReadKey(true);
   }

   static void GenererNombreAleatoire(out int nombre)
   {
      Random rd = new Random(); // Classe pour générer des nombres aléatoires
      nombre = rd.Next();
      // La valeur de nombre (et de n) est initialisée par la fonction elle-même
   }
}
```

**Remarque :** les paramètres de sortie sont passés par référence. Le
code qui appelle la fonction récupère donc une référence vers la
variable passée en paramètre de sortie.

Les paramètres de sortie sont surtout utilisés lorsque la fonction doit
fournir plusieurs résultats.  
Dans L’exemple ci-dessous, la fonction renvoie l’espèce et la famille de l’animal reçu :

```csharp
static void DécrireAnimal (string animal, out string espèce, out string famille)
{
}
```

**Evolutions apportées par C#7 :**

-  La variable passée en référence peut être déclarée en même temps que son passage à la méthode, ce
   qui simplifie la syntaxe :

```csharp
GenererNombreAleatoire(out int n);
```

-  La notion de **tuple** apporte une alternative intéressante aux
   paramètres de sortie, car elle permet à une fonction de renvoyer
   plusieurs résultats, comme le montre l’exemple suivant :

```csharp
public static (string espèce, string famille) DecrireAnimal(string animal)
{
      string esp, fam; 
      // Code pour affecter les valeurs de ces 2 variables
      //...
      return (esp, fam); // On renvoie un tuple
}
```

## 4.5 Enumérations

### 4.5.1 Enumération simples

Un type énuméré représente une liste de valeurs prédéfinies et nommées.
On le déclare avec le mot clé `enum`, suivi d’un nom et de la liste
des valeurs possibles entre accolades.

```csharp
// Déclaration d'un type énuméré
public enum Feux { Vert, Orange, Rouge }
```

?> Bonne pratiques :  
   \-  Déclarer le type énuméré en dehors des classes pour pouvoir l'utiliser dans plusieurs classes  
   \-  Utiliser un nom au pluriel

L’exemple ci-dessous montre comment créer et utiliser une variable du
type énuméré défini précédemment :

```csharp
class Program
{
   static void Main(string[] args)
   {
      // Déclaration et initialisation d'une variable énumérée
      Feux feu = Feux.Vert;
   
      // Test de la valeur
      if (feu == Feux.Vert)
         Console.WriteLine("Passez");
```

Les valeurs d’un type énuméré sont stockées en mémoire sous forme
d’entiers 0,1,2,3...etc.  
On peut donc transtyper un énuméré en entier et inversement, et faire
des opérations dessus, comme le montre l’exemple suivant :

```csharp
Feux feu = Feux.Vert;
feu++;   // Passe le feu à l'orange 
Console.WriteLine(feu);        // Affiche : Orange
Console.WriteLine((int)feu);   // Affiche : 1
Console.WriteLine((Feux)2);    // Affiche : Rouge
```

Si besoin, on peut affecter soi-même les valeurs entières correspondant
aux valeurs énumérées :

```csharp
public enum Feux { Vert=50, Orange=60, Rouge=70 }
```

Pourquoi déclarer un type énuméré et ne pas utiliser directement des entiers ?  
Un type énuméré permet de :

-  Restreindre la liste des valeurs que pourront prendre les variables
   de ce type
-  Associer des noms explicites aux valeurs, ce qui facilite le codage
-  Modifier facilement la valeur entière associée à une valeur de la
   liste si besoin

Dans notre exemple, la notion de feu tricolore pourrait être représentée
par trois entiers 0, 1 et 2. Mais il ne serait pas évident pour un
développeur de se souvenir à quelle valeur correspond chaque couleur, et
rien ne l’empêcherait de faire des comparaisons avec des valeurs incorrectes (> 2).

### 4.5.2 Enumérations à indicateurs binaires

On a quelquefois besoin de manipuler des **combinaisons** de valeurs
énumérées.

Par exemple, pour modéliser les droits d’accès d’un utilisateur sur des
fichiers ou répertoires, on pourrait utiliser le type énuméré suivant :

```csharp
public enum Droits { Aucun, Lecture, Création, Exécution, Modification }
```

…mais on ne pourrait pas déclarer une variable de ce type pour
représenter la combinaison des droits de lecture **et** modification par
exemple.

Dans ce cas, il faut déclarer le type énuméré de la façon suivante :
```csharp
[Flags]
public enum Droits
{
   Aucun = 0,
   Lecture = 1,
   Création = 2,
   Exécution = 4,
   Modification = 8
}
```

- On ajoute l’attribut `[Flags]` juste au-dessus de la déclaration.  
- On affecte explicitement les valeurs énumérées avec des puissances de deux
(2° = 1, 2<sup>1</sup> = 2, 2² = 4…).
- La valeur 0 signifie « aucune valeur ».

Une variable de ce type peut stocker n'importe quelle **combinaison**
des valeurs définies dans la liste.

Le fonctionnement de ce type d’énumération est basé sur les
représentations binaires des nombres et sur les **opérateurs binaires** `~ & | ^` dont voici les tables de vérité :

| A | B | \~A <br>(NON) | A & B<br>(ET) | A \| B<br>(OU) | A ^ B<br>(OU exclusif) |
|:-:|:-:|:-------------:|:----------:|:-----------:|:------------------:|
| 0 | 0 | 1 | 0 | 0 | 0 |
| 0 | 1 | 1 | 0 | 1 | 1 |
| 1 | 0 | 0 | 0 | 1 | 1 |
| 1 | 1 | 0 | 1 | 1 | 0 |

Ainsi, pour représenter les droits de lecture et d’exécution sur un
fichier, on pourra utiliser la variable suivante :

`Droits droitsUtilsateur = Droits.Exécution | Droits.Lecture;`

!> On utilise bien l’opérateur binaire **OU** pour additionner les
droits de lecture **et** d’écriture.

NB/ L’opérateur `|` peut être vu comme une somme, et l’opérateur `&` comme une
multiplication.

L’exemple ci-dessous résume les opérations les plus courantes réalisées
avec les énumérations à indicateurs binaires :

```csharp
// Création d'un dictionnaire d’utilisateurs avec des droits associés
var droitsUtilisateurs = new Dictionary<string, Droits>();
droitsUtilisateurs.Add("Yves", Droits.Aucun);
droitsUtilisateurs.Add("Aline", Droits.Lecture);
droitsUtilisateurs.Add("Marie", Droits.Exécution | Droits.Modification);
droitsUtilisateurs.Add("Eric", Droits.Lecture | Droits.Exécution | Droits.Modification);
droitsUtilisateurs.Add("Denis", Droits.Lecture | Droits.Création | Droits.Exécution |  Droits.Modification);
   
Console.WriteLine("Utilisateurs ayant au moins les droits de lecture et d'exécution : ");

Droits LectureExec = Droits.Lecture | Droits.Exécution;
foreach (var d in droitsUtilisateurs)
{
   if ((d.Value & LectureExec) == LectureExec)
      Console.WriteLine(d.Key); // Affiche Eric et Denis
}
   
Console.WriteLine("Utilisateurs ayant au moins le droit de lecture ou d'exécution : ");
foreach (var d in droitsUtilisateurs)
{
   if ((d.Value & LectureExec) != 0)
      Console.WriteLine(d.Key); // Affiche Aline, Marie, Eric et Denis
}
   
// Ajout du droit de modification à Aline
droitsUtilisateurs["Aline"] |= Droits.Modification;

// Retrait du droit de modification à Eric
droitsUtilisateurs["Eric"] &= ~Droits.Modification;
```

Pour comprendre le test réalisé dans la première instruction if, on peut
examiner sa représentation binaire.  
Prenons l’exemple de l’utilisateur Éric :

```
d.Value = 1101

LectureExec = 0001 \| 0100 = 0101

d.Value & LectureExec = 1101 & 0101 = 0101 = LectureExec
```
