# Véhicules

**Objectifs** : mettre en pratique les notions suivantes : héritage,
appels des constructeurs, classes abstraites, implémentation d’interface

### Etape 1
Créer un type énuméré nommé `Energies` avec les valeurs :
Aucune, Essence, Gazole, GPL, Electrique

Créer une classe `Véhicule` avec 3 propriétés en lecture : `Nom`, `NbRoues` et `Energie`

Ajouter un constructeur pour initialiser ces propriétés.

### Etape 2
Créer une classe dérivée `Voiture`

Générer le constructeur proposé par VS, qui appelle celui de Véhicule

Mettre la valeur 4 pour le paramètre du nombre de roues.

Tester en instanciant une voiture et en exécutant le code pas à pas en debug.

Noter qu’on n’a pas eu besoin de propriétés en écriture.

### Etape 3
Dans la classe Véhicule, ajouter une propriété virtuelle
`Description` qui renvoie une chaîne « Véhicule X roule sur X roues et à
l’énergie X » (remplacer X par les valeurs de propriétés).

Dans Voiture, redéfinir cette propriété. Par défaut VS génère
automatiquement le corps en ajoutant un appel à la propriété de la
classe ancêtre. Ajouter « Je suis une voiture \\r\\n » devant l’appel.

Dans Main, ajouter l’affichage de la description de la voiture et
exécuter.

### Etape 4
Créer une classe `Moto` dérivée de `Véhicule` et redéfinir la
propriété `Description`

Dans la fonction Main, créer une instance de Voiture et une instance de
Moto en les référençant par des variables de type Véhicule. Afficher la
valeur de la propriété Description à partir de ces 2 variables.

On obtient un comportement polymorphique

### Etape 5
Rendre la classe Véhicule abstraite et ajouter une méthode
abstraite `void CalculerConso`

Noter que Visual Studio souligne Voiture et Moto en rouge pour signaler que la
méthode CalculerConso n’est pas implémentée.

Cliquer sur le nom puis sur l’ampoule qui apparaît dans la marge pour
implémenter cette méthode dans chaque classe. On se contentera de lever
une exception car implémenter cette méthode avec du code plus réaliste
nécessiterait un scénario d’exercice beaucoup plus poussé.

Dans Main, essayer d’instancier Véhicule. VS souligne la ligne en rouge
car cette classe est abstraite.

### Etape 6
Sur Véhicule, ajouter une propriété `PRK` (Prix de revient
kilométrique) abstraite en lecture seule

Fournir une implémentation dans les classes dérivées (renvoyer
simplement des valeurs de PRK arbitraires).

### Etape 7
Faire dériver Véhicule de `IComparable`. Dans l’ampoule VS
propose d’implémenter l’interface de différentes façons. Choisir
l’implémentation explicite.

Dans la méthode `CompareTo` ajouter le code suivant :

```csharp
return PRK.CompareTo(((Vehicule)obj).PRK);
```

En effet, le type Double implémente `IComparable`, donc autant s’en servir.

Dans Main, afficher le résultat de la comparaison des deux véhicules
(voiture et moto) créés précédemment. On voit que pour appeler la
méthode `CompareTo`, il faut utiliser des variables de type `IComparable` et
non `Vehicule`.

Jouer avec les valeurs des PRK pour tester les 3 cas possibles.

### Etape 8
En repartant des variables de type `IComparable`, utiliser
`is` sur l’une d’elles avant de la transtyper en Véhicule, puis afficher
sa propriété Description.

```csharp
if (zoé is Véhicule) Console.WriteLine(((Véhicule)zoé).Description);
```

### Etape 9
Dans la classe Program, créer une fonction `Décrire`,
prenant en paramètre un objet, et affichant :

-  Le nom de son type
-  Le nom du type dont il hérite
-  Le nom des interfaces qu’il implémente
-  La liste de ses membres (type et nom)

Dans `Main`, appeler cette fonction en lui passant successivement 2
véhicules de types différents, et vérifier le résultat.