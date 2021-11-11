# 15. Divers

Ce chapitre traite d’aspect particuliers du langage C#, qui sont
utilisés moins fréquemment que les concepts présentés jusqu’ici. Ils
sont présentés de façon succincte, avec pour objectifs de donner des
exemples simples illustrant leur syntaxe de mise en œuvre.

## 15.1 Méthodes d’extensions

**Les méthodes d’extensions** permettent d’étendre un type existant
(structure ou classe), en lui ajoutant de nouvelles méthodes.

**Intérêt** : Toutes les variables déjà existantes de ce type profitent des
nouvelles méthodes ajoutées.

**Exemple** : je souhaite avoir une méthode pour inverser les lettres d’une
chaîne de caractères.  
Je pourrais pour cela dériver la classe string et lui ajouter une méthode Inverser. L’inconvénient est que les variables de type string déjà existantes dans mon code ne pourront pas profiter de cette méthode.  
Il est donc préférable de créer une méthode d’extension, comme ceci :

```csharp
public static class StringHelper
{
    public static int CompterMots(this string s)
    {
        string[] tabMots = s.Trim().Split(' ', '\'', '\t', '\n');
        return tabMots.Length;
    }
}
```

Utilisation de cette méthode :

```csharp
class Program
{
    static void Main(string[] args)
    {
        string phrase = "J'aime le C#";
        int nbMots = phrase.CompterMots();

        Console.WriteLine("Il y a {0} mots dans la phrase", nbMots); // renvoie 4
        Console.ReadKey();
    }
}
```

Pour créer une méthode d’extension :

-  La classe qui contient la méthode d’extension doit être statique
-  La méthode d’extension elle-même doit être statique
-  La méthode d’extension doit contenir un paramètre du type qu’elle
   étend, précédé du mot clé this (dans l’exemple ci-dessus : `this string s`)

**Remarques** :

-  Généralement, on rassemble les méthodes d’extension d’un type dans
   une classe ayant le nom de ce type suffixé par « Helper » (ex :
   `StringHelper`, `DateTimeHelper…`)

-  La méthode d’extension n’est pas réellement ajoutée à la classe
   étendue. Le code MSIL montre que le compilateur génère simplement un
   appel à une méthode statique :
   `Console.WriteLine(StringHelper.Inverser(s));`

## 15.2 Surcharge des opérateurs 

La surcharge d’un opérateur permet d’appliquer l’opération
correspondante sur des opérandes de type que l’on définit soi-même
(classe ou structure).

Opérateurs susceptibles d'être redéfinis :

- **Opérateurs unaires** (1 opérande) : `~` `++` `--` `!`

-  **Opérateurs binaires** (2 opérandes) : `+` `-` `*` `/` `%` `&` `|` `^` `<<` `>>`

Un opérateur est défini avec le mot clé `operator` en tant que méthode
statique sur la classe des opérandes. Il prend un ou deux paramètres en
entrée, et renvoie une instance de la classe d’opérande.

Il est également possible de surcharger les opérateurs de conversion
explicite.

L’exemple de code ci-dessous illustre la syntaxe à mettre en œuvre pour
ces 2 cas :
```csharp
namespace Operateurs
{
   class Point
   {
      public double X { get; set; }
      public double Y { get; set; }
      public double Z { get; set; }

      public Point(double x, double y, double z)
      {
         X = x;
         Y = y;
         Z = z;
      }

      // Surcharge de l'opérateur +
      public static Point operator +(Point c1, Point c2)
      {
         return new Point(c1.X + c2.X, c1.Y+c2.Y, c1.Z+c2.Z);
      }

      // Opérateur de conversion explicite en double
      // (retourne la distance du point par rapport à l'origine)
      public static explicit operator double(Point c)
      {
         return Math.Sqrt(c.X * c.X + c.Y * c.Y + c.Z * c.Z);
      }

      public override string ToString()
      {
         return String.Format("({0},{1},{2})", X, Y, Z);
         // renvoie une chaîne au format : (x,y,z)
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         var P1 = new Point(2, 4, 6);
         var P2 = new Point(3, 5, 7);
         var P3 = P1 + P2;

         Console.WriteLine("{0} + {1} = {2}", P1, P2, P3);
         Console.WriteLine("(double){0} = {1}", P1, (double)P1);
         Console.ReadKey();
      }
   }
}
```

Sortie console :
```
(2,4,6) + (3,5,7) = (5,9,13)
(double)(2,4,6) = 7,48331477354788
```

## 15.3 Métadonnées et réflexion

Au moment de la compilation du code, le compilateur produit des
métadonnées de description pour tous les types (valeur ou référence)
utilisés dans l’application.

Ces métadonnées sont décrites par la classe
[System.Type](https://msdn.microsoft.com/fr-fr/library/system.type(v=vs.110).aspx),
qui comporte de nombreux membres, tels que le nom du type, la liste de
tous ses membres…etc.

**La réflexion** désigne le fait d’accéder à ces informations durant
l’exécution. La réflexion est utilisée par exemple pour lire les
attributs qui décorent les classes ou propriétés (cf. paragraphe
suivant). Visual Studio l’utilise également pour afficher le contenu de
l’explorateur de classes, et pour l’auto-complétion (IntelliSense).

Pour accéder aux métadonnées, on utilise la méthode `GetType` définie sur
la classe object, et donc accessible sur tout objet. Cette méthode
renvoie un objet `Type`.

Ex : considérons la classe suivante :
```csharp
public class Cerf : Animal
{
   public Cerf() { }
   public Cerf(Sexes sexe, double poids) : base(sexe, poids) { }

   // Propriétés issues de l'nterface IClassable
   public override string Espece { get { return "Cerf"; } }
   public override string Famille { get { return "Cervidé"; } }
   public override string Ordre { get { return "Artiodactyle"; } }
   public override string Classe { get { return "Mammifère"; } }
   public override string Embranchement { get { return "Vertébré"; } }
}
```

Le code ci-dessous permet d’afficher le type et le nom de tous les
membres de la classe :
```csharp
class Program
{
   static void Main(string[] args)
   {
      Animal cerf = new Cerf();
      Type t = cerf.GetType();
      Console.WriteLine("Membres de " + t.Name);
      foreach (var m in t.GetMembers())
      {
            Console.WriteLine("Type: {0}, Nom: {1}", m.MemberType, m.Name);
      }

      Console.ReadKey();
   }
}
```

Sortie console :
```
Membres de Cerf
Type: Method, Nom: get_Espece
Type: Method, Nom: get_Famille
Type: Method, Nom: get_Ordre
Type: Method, Nom: get_Classe
Type: Method, Nom: get_Embranchement
Type: Method, Nom: get_Poids
Type: Method, Nom: set_Poids
Type: Method, Nom: get_Sexe
Type: Method, Nom: ToString
Type: Method, Nom: Equals
Type: Method, Nom: GetHashCode
Type: Method, Nom: GetType
Type: Constructor, Nom: .ctor
Type: Constructor, Nom: .ctor
Type: Property, Nom: Espece
Type: Property, Nom: Famille
Type: Property, Nom: Ordre
Type: Property, Nom: Classe
Type: Property, Nom: Embranchement
Type: Property, Nom: Poids
Type: Property, Nom: Sexe
```

On notera que :

-  Les méthodes commençant par `get_` et `set_` sont les
   accesseurs des propriétés

-  Comme Cerf hérite d’Animal, qui hérite elle-même de Object, elle
   possède les membres de ses ancêtres : les propriétés Poids et Sexe,
   et les méthodes ToString, Equals, GetHashCode et GetType.

## 15.4 Attributs

Les attributs permettent d’associer des métadonnées à des éléments de
code (types, méthodes, propriétés, etc.). Il peut s’agir d’instructions
pour le compilateur, de descriptions, ou d’indications sur la façon de
traiter l’élément de code.

Une fois qu'il est associé à un élément de code, l'attribut peut être
interrogé au moment de l'exécution par réflexion.

On spécifie un attribut en mettant son nom entre crochets devant la
déclaration de l'élément auquel il s'applique :
```csharp
[Conditional("DEBUG")]
void TraceMethod()
{
      // ...
}
```

Dans cet exemple, la méthode TraceMethod est exécutée uniquement si on
est en mode debug.

Certains attributs s’appliquent sur l’assembly lui-même. On les trouve
dans le fichier AssemblyInfo.cs créé automatiquement dans chaque projet.

La liste de toutes les classes d’attributs prédéfinis du .net framework
peut être consultée sur [cette page
MSDN](https://msdn.microsoft.com/en-us/library/system.attribute.aspx#inheritanceContinued).
Il est possible également de créer sa propre classe d’attribut en
dérivant System.Attribute.

La création d’un fichier xml à partir d’une arborescence d’objets
(sérialisation), et le chargement d’un fichier xml dans une arborescence
d’objets (désérialisation), sont des opérations qui peuvent être
grandement facilitées par l’utilisation d’attributs. Ils permettent dans
ce cas d’indiquer à la classe XmlSerializer la nature des liens entre
les éléments et attributs xml, et les propriétés des classes qui
stockent de l’arborescence.

Exemple : nous allons charger le fichier xml ci-dessous dans une liste :

```xml
<?xml version="1.0" encoding="utf-8"?>
<Voitures>
   <Voiture Id="1">
      <Marque>Ferrari</Marque>
      <Modele>458 Speciale A</Modele>
      <Annee>2014</Annee>
   </Voiture>
   <Voiture Id="2">
      <Marque>Ferrari</Marque>
      <Modele>California 30</Modele>
      <Annee>2012</Annee>
   </Voiture>
      <Voiture Id="3">
      <Marque>Lamborghini</Marque>
      <Modele>Aventador</Modele>
      <Annee>2015</Annee>
   </Voiture>
   <Voiture Id="4">
      <Marque>Lamborghini</Marque>
      <Modele>Huracan</Modele>
      <Annee>2014</Annee>
   </Voiture>   
</Voitures>
```

Pour cela, nous pouvons utiliser le code suivant :
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Attributs
{
   class Program
   {
      static void Main(string[] args)
      {
         List<Voiture> voitures;
         XmlSerializer deserializer = new XmlSerializer(typeof(List<Voiture>),
                              new XmlRootAttribute("Voitures"));
         using (var fs = new FileStream("Voitures.xml", FileMode.Open))
         {
               voitures = (List<Voiture>)deserializer.Deserialize(fs);
         }

         foreach (var v in voitures)
               Console.WriteLine("{0} : {1} {2} de {3}",
                  v.Id, v.Marque, v.Modèle, v.Année);

         Console.ReadKey();
      }
   }

   public class Voiture
   {
      [XmlAttribute("Id")]
      public string Id { get; set; }

      [XmlElement("Marque")]
      public string Marque { get; set; }

      [XmlElement("Modele")]
      public string Modèle { get; set; }

      [XmlElement("Annee")]
      public int Année { get; set; }
   }
}
```

Sortie console :
```
1 : Ferrari 458 Speciale A de 2014
2 : Ferrari California 30 de 2012
3 : Lamborghini Aventador de 2015
4 : Lamborghini Huracan de 2014
```

Les attributs `XmlElement` et `XmlAttribute` placés au-dessus des propriétés
de la classe voiture permettent de faire le lien avec les éléments et
attributs xml.

Le chargement du fichier xml dans la liste voitures peut alors se faire
en quelques lignes de codes.

On pourrait de même générer très facilement le fichier xml à partir du
contenu de la liste en appelant la méthode `Serialize` du XmlSerializer.
