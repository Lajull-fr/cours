# 8. Héritage

**L'héritage (ou dérivation)** est une seconde relation essentielle de la programmation
orientée objet.
C'est un mécanisme qui permet de **spécialiser** une classe
existante en modifiant le comportement de ses méthodes, et/ou en lui
ajoutant de nouveaux membres.  

La classe dérivée possède tous les champs, propriétés et méthodes publics ou protégés de la classe de base, et à ce titre on peut toujours considérer un objet de la classe dérivée comme une instance de la classe de base.   

Exemple : un poisson est un dérivé d'animal qui a des caractéristiques spécifiques (poisson d'eau douce ou de mer, avec ou sans écailles...), mais un poisson reste un animal.


Le code ci-dessous montre la syntaxe de mise en oeuvre d'une relation d'héritage :

```csharp
   public class Derivee : Ancetre
   {
   }

   class Program
   {
      static void Main(string[] args)
      {
         Derivee d = new Derivee();
         Ancetre a = new Derivee(); // autorisé
      }
   }
```

Sur la première ligne, on crée une classe dérivée en spécifiant le nom de la classe ancêtre après les ":".

## 8.1 Appels des constructeurs

Il est recommandé qu’un constructeur d’une classe dérivée appelle le
constructeur de sa classe de base dans le code d’initialisation. Ceci
peut être réalisé au moyen du mot clé `base`.

Si on ne le fait pas explicitement, le compilateur tente d’insérer
automatiquement un appel au constructeur par défaut de la classe de base
avant l’exécution du code du constructeur de la classe dérivée.

```csharp
using System;

namespace EssaiHeritage
{
   // Classe ancêtre
   public class Animal
   {
      private float _poids;
      private float _taille;

      // constructeur sans paramètre
      public Animal()
      {
      }

      // constructeur avec paramètres
      public Animal(float poids, float taille)
      {
         _poids = poids;
         _taille = taille;
      }
   }

   // Classe dérivée
   public class Mammifere : Animal
   {
      // base permet d'appeler le constructeur de la classe ancêtre
      public Mammifere(float poids, float taille) : base(poids, taille)
      {

      }
   }
}
```

Dans cet exemple, si le constructeur de la classe Mammifere n’appelait
pas explicitement un constructeur de son ancêtre avec le mot clé base,
le constructeur sans paramètres de la classe de base serait
automatiquement appelé.

!> Ne pas confondre les mots clés `this` et `base`.<br>
`this` permet d’appeler un autre constructeur de la même classe<br>
`base` permet d’appeler un constructeur de la classe ancêtre.

## 8.2 Méthodes virtuelles et redéfinies

**Une méthode virtuelle** d’une classe ancêtre est une méthode destinée
à être **redéfinie** dans ses classes dérivées.

Dans la classe ancêtre, le caractère virtuel d’une méthode est précisé à
l’aide du mot clé `virtual` placé avant son type de retour.

Dans la classe dérivée, pour indiquer qu’une méthode redéfinit une
méthode virtuelle, on utilise le mot clé `override`.

```csharp
using System;

namespace EssaiHeritage
{
   public class Animal
   {
      private float _poids;
      private float _taille;

      // Constructeur
      public Animal(float poids, float taille)
      {
         _poids = poids;
         _taille = taille;
      }

      // Propriété
      public float Poids
      {
         get { return _poids; }
         set { _poids = value; }
      }

      // Méthode virtuelle (peut être redéfinie dans les dérivées)
      protected virtual void Grossir(float qteNourriture)
      {
         _poids += (float)0.1 * qteNourriture;
      }

      // Méthode non virtuelle
      public void Manger(float qte)
      {
         Grossir(qte);
      }
   }

   public class Mammifere : Animal
   {
      // Appel du constructeur de base
      public Mammifere(float poids, float taille) : base(poids, taille) { }

      // Méthode redéfinie
      protected override void Grossir(float qteNourriture)
      {
         Poids += (float)0.2 * qteNourriture;
      }
   }
}
```

Dans l’exemple ci-dessus, la méthode virtuelle Grossir est redéfinie
dans la classe dérivée.

**Remarque** : attention à ne pas confondre redéfinition et surcharge.
La surcharge consiste à créer plusieurs méthodes de même nom, mais avec
des signatures différentes au sein d’une même classe. Une méthode
redéfinie dans une classe dérivée a la même signature que la méthode de
son ancêtre.

?> Une méthode redéfinie est par défaut aussi virtuelle.  
Il n’est donc pas nécessaire de remettre le mot clé `virtual` devant son nom. 

Cela permet de gérer facilement plusieurs étages de dérivation.  
Dans l’exemple ci-dessus, on pourrait créer une classe Chien dérivée de Mammifere et
redéfinir sa méthode Grossir.

### 8.2.1 Appel de la méthode de base dans la méthode redéfinie

Nous avons vu plus haut comment appeler un constructeur d’une classe
ancêtre dans un constructeur de la classe dérivée à l’aide du mot clé
`base`. Cette technique s’applique également à n’importe quelle méthode
virtuelle redéfinie, comme le montre l’exemple suivant :

```csharp
using System;

namespace EssaiHeritage
{
      public class Animal
      {
         public override string ToString()
         {
            return "Je suis un animal";
         }
      }

      public class Mammifere : Animal
      {
         public override string ToString()
         {
            string s = base.ToString(); // Appelle la méthode de la classe ancêtre
            return s + ", et je suis un mammifère";
         }
      }
}
```

**Remarque** : dans la classe Animal, on remarque que la méthode `ToString`
est déjà redéfinie (mot clé `override`). En effet, en .net, toutes les
classes héritent implicitement de la classe `Object`, qui contient une
méthode virtuelle `ToString()`.

### 8.2.2 Masquage

Que se passe-t-il si on utilise le mot clé `virtual`, mais pas `override` ?  
Il y a un avertissement à la compilation. Le code s’exécute quand-même,
mais le comportement n’est pas celui attendu d’une relation de
dérivation classique ; il n’est pas polymorphique (cf. plus bas).  
On dit que la méthode de la classe de base est **masquée** par la méthode de la
classe dérivée.

Le tableau ci-dessous résume ce qui se passe dans les différents cas :

|**Classe de base ⇨**<br>**Classe dérivée ⇩**|**Méthode déclarée avec virtual**           |**Méthode déclarée sans virtual**           |
|-------------------------------------------|--------------------------------------------|--------------------------------------------|
|**Méthode déclarée avec override**         |Redéfinition                                |Erreur de compilation                       |
|**Méthode déclarée sans override**         |Masquage avec avertissement à la compilation|Masquage avec avertissement à la compilation|

!> Le masquage est très rarement souhaité, donc attention à ne pas oublier override !

## 8.3 Propriétés virtuelles et redéfinies

Comme les méthodes, les propriétés peuvent également être virtuelles et
redéfinies.

```csharp
using System;

namespace EssaiHeritage
{
      public class Animal
      {
         private float _dureeVie;

         // Propriété virtuelle
         public virtual float DureeVie {
            get { return _dureeVie; }
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
      }

      public class Mammifere : Animal
      {
         // Propriété redéfinie
         public override float DureeVie
         {
            get { return base.DureeVie; }
            set
            {
                  if (value > 0 && value <= 99)
                     base.DureeVie = value;
                  else
                  {
                     // lève une exception pour indiquer une valeur incorrecte
                     throw new ArgumentOutOfRangeException();
                  }
            }
         }
      }
}
```

Dans cet exemple, il est intéressant de noter que dans la classe dérivée
Mammifere, grâce à l’utilisation de `base`, on n’a pas besoin d’accéder
directement au champ \_dureeVie, qui peut rester privé.

## 8.4 Polymorphisme d’héritage

**Le polymorphisme** désigne le fait qu’à l’exécution, un même code peut
produire des comportements différents selon le type d’entité qu’il
manipule.

Pour obtenir un comportement polymorphique avec la notion d’héritage, il
faut utiliser des variables du type ancêtre contenant des objets du type
dérivé.

Considérons les classes suivantes :

```csharp
using System;

namespace EssaiHeritage
{
   public class Animal
   {
      private float _poids;
      private float _taille;

      #region Constructeurs
      public Animal()
      {
      }

      public Animal(float poids, float taille)
      {
         _poids = poids;
         _taille = taille;
      }
      #endregion

      public override string ToString()
      {
         return String.Format("Je suis un animal qui pèse {0} kg", _poids);
      }
   }

   public class Mammifere : Animal
   {
      public Mammifere(float poids, float taille) : base(poids, taille) { }

      public override string ToString()
      {
         string desc = base.ToString();
         return desc + ", et je suis un mammifère";
      }
   }

   public class Poisson : Animal
   {
      public override string ToString()
      {
         return "Je suis un petit poisson";
      }
   }
}
```

Les classes `Mammifere` et `Poisson` héritent toutes deux de `Animal`.  
Animal fournit une implémentation par défaut de la méthode virtuelle `ToString`,
qui est redéfinie dans les 2 classes dérivées.

On peut faire une utilisation polymorphique de ces classes de la façon
suivante :

```csharp
Animal a1 = new Mammifere(10, 80);
Animal a2 = new Poisson();

Console.WriteLine(a1);
Console.WriteLine(a2);
```

Sortie console :
```
Je suis un animal qui pèse 10 kg, et je suis un mammifère
Je suis un petit poisson
```

On constate donc que pour le premier animal, c’est la méthode ToString
de la classe Mammifere qui est appelée, et que pour le second, c’est la
méthode ToString de la classe Poisson qui est appelée.  
A partir de 2 variables de type Animal, on a obtenu des comportements différents de la
méthode ToString. C’est bien un comportement polymorphique.

## 8.5 Transtypage et opérateurs is et as

Le transtypage (ou cast) consiste à transformer un objet ou une valeur d'un type donné en un objet ou valeur d'un autre type.  
Exemple :

```csharp
Animal ani = new Poisson();
Poisson p = (Poisson)ani;   // Transtypage de l'animal en poisson
p.Nager();   // Appel d'une méthode spécifique de la classe Poisson
```
On transtype l'animal en poisson de façon à pouvoir utiliser la méthode Nager, spécifique à la classe Poisson.  

Dans cet exemple, si la variable `ani` ne stockait pas une instance de `Poisson`, le transtypage provoquerait une erreur à l'exécution mais pas à la compilation.
La réussite du transtypage repose donc sur la fiabilité du code produit par le développeur.

Pour sécuriser cette opération, on pourrait gérer l’exception `InvalidCastException`, afin que l’application ne plante pas lorsque cette exception se produit.  
Mais C# propose d’autres alternatives plus adaptées via les opérateurs `is` et `as`.

**L’opérateur `is`** permet de vérifier le type d’un objet avant sa
conversion, et renvoie un booléen indiquant si la conversion est possible ou non.

Voici comment l'utiliser :

```csharp
Animal ani = new Poisson();
if (ani is Poisson)
{
   Poisson plouf = (Poisson)ani; // cast sécurisé
   // suite du code
}
```

Depuis C# 7, cette syntaxe peut être simplifiée de la façon suivante :

```csharp
if (ani is Poisson plouf)
{
   // suite du code
}
```

**L’opérateur `as`** essaie de transtyper et renvoie `null` si l'opération à échoué (sans lever d'exception).

Voici comment l'utiliser :

```csharp
Animal ani = new Poisson();
Poisson plouf = ani as Poisson;
if (plouf != null)
{
   // suite du code
}
```

L’opérateur `as` renvoie donc :
- directement la référence convertie si la conversion est possible
- `null` si la conversion est impossible.

?> Les opérateurs `is` et `as` fonctionnent aussi bien avec les types valeurs
que les types références, y compris les interfaces, comme le montrent les exemples suivants :

```csharp
if (j is Enum)
{
   int k = (int)j + 3;
}

Poisson p = new Poisson();
IDomestique dom = p as IDomestique;
if (dom != null)
{
   // suite du code
}
```

**Remarque** :

L’utilisation fréquente du transtypage peut être révélatrice d’un
problème de conception du code. En effet, il est toujours préférable de
ne pas avoir à faire de conversion de types, mais plutôt d’utiliser le
polymorphisme et les génériques, qui permettent de faire du code plus
robuste et plus performant.