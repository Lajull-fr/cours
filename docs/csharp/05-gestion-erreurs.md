# 5. Gestion des erreurs

Au cours de l’exécution d’une application, de nombreuses erreurs peuvent
survenir, pour de multiples raisons. Les erreurs peuvent être liées à
des défauts de conception/réalisation de l’application, à son
interaction avec son environnement extérieur (système d’exploitation,
réseau, autres applications…), ou bien aux informations saisies ou
importées dans l’application.

Exemples :

-  L’utilisateur réalise un enchaînement d’actions qui n’a pas été prévu à la conception

-  Une coupure réseau intervient durant un accès à des ressources réseau

-  Une autre application monopolise une trop grande partie de la mémoire ou du processeur, ce qui provoque des problèmes dans notre application

-  L’utilisateur saisit des données dont les valeurs ou les formats ne sont pas conformes à ce qu’attend l’application

-  …etc.

Gérer correctement les erreurs consiste à :

-  Identifier les erreurs que l’on peut et veut gérer

-  Faire en sorte qu’elles ne fassent pas planter l’application
   lorsqu’elles surviennent, tout en informant l’utilisateur de façon
   approprié (affichage de messages, log dans un fichier…)

## 5.1 Lever une exception

Une **exception** est une classe .net qui modélise une erreur. Lorsqu’un
bloc de code identifie une situation qui l’empêche de fonctionner
correctement, il peut **lever** (= émettre) une exception décrite par un
type (une classe) d’exception.

Ex : une méthode est chargée d’écrire dans un fichier existant, mais
celui-ci est en lecture seule. La méthode peut dans ce cas lever une
exception afin de signaler au code appelant qu’elle n’est pas en mesure
de s’exécuter correctement, et lui fournir des informations sur le type
d’erreur rencontré.

C’est un mécanisme souple et puissant, qui permet au code appelant de
gérer proprement les erreurs. Il est abondamment utilisé dans les
classes du .net framework, et on peut bien entendu l’utiliser dans notre
propre code.

**Les types d’exception**

Le .net framework fournit beaucoup de types d'exceptions pour décrire
toutes sortes d’erreurs. Ces types forment une hiérarchie d’héritage
(une arborescence), dont on pourra avoir un aperçu sur [cette
page](https://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=FR-FR&k=k(System.Exception);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5.2);k(DevLang-csharp)&rd=true).
L’ancêtre de plus haut niveau est la classe `Exception`.

On peut utiliser directement ces classes, ou bien créer une nouvelle
classe dérivée d’Exception, afin de personnaliser davantage l’exception.

**Lever une exception avec throw**

Pour lever une exception, on utilise le mot clé `throw`, suivi d’une
instance d’exception. Exemple :

```csharp
public static string NomMois(int mois) 
{ 
   switch (mois) 
   { 
      case 1 :  
            return "Janvier"; 
      case 2 : 
            return "Février";  
      ... 
      case 12 : 
            return "Décembre";  
      default : 
            throw new ArgumentOutOfRangeException("Ce n'est pas un numéro de mois"); 
   } 
}
```

Cette méthode renvoie le nom d’un mois à partir de son numéro. Si le
nombre passé en paramètre est négatif par exemple, on ne peut pas
renvoyer un nom de mois. Dans ce cas, on lève une exception de type
ArgumentOutOfRangeException (type fourni par le .net framework, et bien
adapté à ce cas).

## 5.2 Intercepter une exception

On s’intéresse maintenant à la façon dont une méthode peut gérer une
exception levée par une autre méthode.

Par exemple, comment le code qui appelle la méthode NomMois ci-dessus
peut-il gérer l’exception `ArgumentOutOfRangeException` lorsqu’elle
survient ?

### 5.2.1 Blocs try…catch

Pour gérer proprement les exceptions, C# propose l’instruction
`try…catch`, qui permet une bonne séparation entre le code de gestion
d’erreur et le code qui implémente la logique applicative.

Voici sa syntaxe :
```csharp
try
{
   // code susceptible de produire une erreur
}
catch (Exception e)
{
   // code de gestion de l'erreur
}
// suite du code
```

A l’intérieur du bloc `try`, si une instruction provoque une erreur,
l’instruction suivante n’est pas jouée, et c’est le bloc `catch` qui est
exécuté. Après le bloc catch, la suite du code est exécutée.

Si aucune erreur n’est rencontrée, toutes les instructions du bloc try
sont exécutées, et le bloc catch n’est pas exécuté.

L’argument `e` de l’instruction catch est un objet de type `Exception`
ou dérivé, tels que `IndexOutOfRangeException`, `FormatException`,
`SystemException`, etc…

En écrivant `catch (Exception e)`, on indique qu'on veut gérer tous les
types d'exceptions. Si on veut gérer une ou plusieurs exceptions
particulières, on utilisera plutôt des types dérivés d’Exception dans un
ou plusieurs blocs catch :

```csharp
try
{
   //code susceptible de générer des exceptions
}
catch (IndexOutOfRangeException e1)
{
   //traiter les exceptions de type indice hors limite
}
catch (FormatException e2)
{
   //traiter les exceptions de type format incorrect
}

//instruction suivante
```

### 5.2.2 Propagation des exceptions

Si aucun des blocs catch fournis ne permet de gérer l’exception, la
méthode qui contient le code se termine immédiatement, et rend la main à
sa méthode appelante.  
Si cette dernière possède un bloc catch permettant
de gérer l’exception, elle le fait, sinon, la méthode se termine et
rend la main à sa méthode appelante, et ainsi de suite...  
C’est ce qu’on appelle la **propagation des exceptions**.

Finalement, si l’erreur est remontée jusqu’au plus haut niveau, sans
être interceptée par un bloc catch, l’application plante.  
L’erreur est dite **non gérée**.

```csharp
static void Main(string[] args)
{
   try
   {
      Methode1();
   }
   catch (NotImplementedException e)
   {
      Console.WriteLine("interception de l'erreur");
   }
}


public static void Methode1()
{
    Methode2();
}

public static void Methode2()
{
    Methode3();
}

public static void Methode3()
{
    throw new NotImplementedException(); // lève une exception
}
```

Dans cet exemple, une exception est levée dans Methode3. Comme elle
n’est pas interceptée par Methode2 (qui ne contient pas de try catch),
elle remonte à Methode1. Mais elle n’est pas non plus interceptée par
cette méthode, elle remonte donc jusqu’à Main, qui elle, l’intercepte.
Si Main n’interceptait pas l’erreur, l’application planterait.

?> L’ordre des gestionnaires d’exceptions est important

Si plusieurs gestionnaires d’exceptions (i.e. plusieurs blocs catchs)
sont aptes à intercepter une erreur, c’est le premier de la liste qui
est exécuté. C’est pourquoi l’ordre des gestionnaires est important ; il
faut les ordonner **du plus spécialisé au plus général**.

!> Ne pas masquer les erreurs !

Le code à l’intérieur du ou des bloc(s) catch doit être pertinent. Si on
ne fait rien dans le bloc catch, l’utilisateur n’a aucun moyen de savoir
qu’une erreur s’est produite, et croit que tout s’est déroulé
normalement, alors que ce n’est pas le cas !  
On dit que l’erreur est masquée.  
Il n’est pas toujours pertinent d’afficher un message d’erreur,
mais dans ce cas, il faut au minimum loguer l’erreur dans un fichier journal.

## 5.3 Finally et using

### 5.3.1 Le bloc Finally

Un bloc `finally` permet de garantir que le code qu’il contient sera
exécuté, même si une erreur survient dans le bloc try associé, pour peu
que l’erreur ne fasse pas planter l’application.  
Cela permet par exemple de libérer des ressources allouées dans le bloc try.

Exemple :
```csharp
StreamWriter outputFile = null;
try
{
   outputFile = new StreamWriter(@"C:\Temp\essai.txt", true);
   outputFile.WriteLine("Coucou !");
}
finally
{
   if (outputFile != null) outputFile.Close();
}
```

Ce code écrit du texte dans un fichier au moyen d’un objet `StreamWriter`.
Cet objet utilise une ressource allouée par le système d’exploitation
(un handle de fichier), qu’il faut libérer lorsqu’on ne s’en sert plus,
en appelant la méthode Close.

Cependant, l’utilisation de l’objet StreamWriter peut provoquer
différents types d’erreurs (ex : tentative d’écriture dans un fichier en
lecture seule…). Si on ne gère pas ces erreurs dans des blocs catch, il
faut au minimum faire en sorte que la méthode `Close` soit appelée. Pour
cela, on place l’appel de cette méthode dans un bloc `finally`.

?> On peut tout à fait combiner les clauses try, catch et finally, comme le
montre l’exemple suivant :

```csharp
class Program
{
   static void Main(string[] args)
   {
      try
      {
         EcritFichier();
      }
      catch (Exception e)
      {
         // permet de gérer notamment l'erreur System.UnauthorizedAccessException
         Console.WriteLine(e.Message);
      }

      Console.ReadKey(true);
   }

   static void EcritFichier()
   {
      StreamWriter outputFile = null;
      try
      {
         outputFile = new StreamWriter(@"C:\Temp\essai.txt", true);
         outputFile.WriteLine("Coucou !");
      }
      catch(DirectoryNotFoundException)
      {
         Console.WriteLine("Le répertoire spécifié n'existe pas");
      }
      finally
      {
         Console.WriteLine("Libération de la ressource");
         if (outputFile != null) outputFile.Close();
      }
   }
}
```

Dans cet exemple, la méthode EcritFichier tente d’ajouter du texte dans
un fichier déjà existant. Elle gère uniquement l’erreur
`DirectoryNotFoundException` qui se produit si le répertoire spécifié
n’existe pas.  
Mais d’autres erreurs peuvent se produite, comme par
exemple `UnauthorizedAccessException` si le fichier est en lecture seule.  
C’est pourquoi l’appel de la méthode `Close` est placée dans un bloc
finally, afin qu’on soit sûr qu’il est exécuté, même si une erreur autre
que DirectoryNotFoundException se produit.

Si le chemin du fichier existe, mais que le fichier est en lecture
seule, on passera d’abord dans le bloc finally, puis dans le bloc catch
de la méthode Main, qui gère toutes les exceptions (type Exception).

!> Un bloc finally n’est pas exécuté si l’erreur fait
planter l’application.

Ainsi, dans l’exemple précédent s’il n’y avait pas le try…catch dans la
fonction Main, le bloc finally de la fonction EcritFichier ne serait
jamais exécuté.

En résumé, dans l’exemple de code précédent :

-  Si aucune erreur ne se produit dans le bloc try, le bloc finally est
   exécuté après le bloc try

-  Si une erreur de type DirectoryNotFoundException se produit dans le
   bloc try, le bloc catch correspondant est exécuté, puis le bloc
   finally est exécuté

-  Si une erreur d’un type différent de DirectoryNotFoundException se
   produit dans le bloc try, comme il n’y a pas de bloc catch
   correspondant dans EcritFichier, c’est le bloc catch de Main qui
   intercepte l’erreur (car le type Exception gère toutes les erreurs).
   Juste avant l’exécution de ce bloc catch, le bloc finally est
   exécuté. S’il n’y avait pas de bloc catch dans Main, le bloc finally
   ne serait pas exécuté.

### 5.3.2 L’Instruction using

Nous avons vu précédemment comment utiliser un bloc `finally` pour être
sûr que le code de libération de certaines ressources soit exécuté.  
Mais cette solution n’est pas idéale car :

-  Elle devient vite complexe si on a plusieurs ressources à libérer,
   car il faut dans ce cas imbriquer les blocs `try` et `finally`.

-  La gestion de la valeur null est sensible : il ne faut pas oublier de
   tester que outputFile n’est pas null, et rien n’empêche d’utiliser
   accidentellement cette variable après le bloc `finally`

L’instruction `using` résout ces problèmes.  
Elle permet de créer un objet utilisant une ressource, et de le détruire automatiquement 
à la fin du bloc de l’instruction (à la fermeture de l’accolade).  
L’exemple de code précédent peut ainsi s’écrire de façon beaucoup plus simple, comme ceci :

```csharp
using(StreamWriter outputFile = new StreamWriter(@"C:\ Temp\essai.txt", true))
{
   outputFile.WriteLine("Coucou !");
}
```

Cette syntaxe appelle automatiquement la méthode `Dispose` du
StreamWriter à la fin du bloc.  
La méthode Dispose appelle elle-même la méthode `Close`.

?> La seule contrainte pour utiliser `using` est que l’objet instancié dans
l’instruction (ici le StreamWriter) doit implémenter l’interface
`IDisposable`.

**Remarques** :

-  Ne pas confondre l’instruction using avec la directive using
   utilisée pour les espaces de noms (cf. plus bas).

-  L’instruction using est beaucoup utilisée pour gérer les accès à la
   base de données (connexion et exécution de commandes).

-  On peut tout à fait imbriquer les instructions using, mettre des
   using dans des blocs try…etc.

