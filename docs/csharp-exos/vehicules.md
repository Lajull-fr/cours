# Véhicules

**Objectifs** : mettre en pratique les notions suivantes : héritage,
appels des constructeurs, classes abstraites, implémentation d’interface

### Etape 1 : constructeur
Créer un type énuméré nommé `Energies` avec les valeurs :
Aucune, Essence, Diesel, GPL, Electrique

Créer une classe `Véhicule` avec 3 propriétés en lecture : `Nom`, `Energie` et `PRK` (Prix de revient kilométrique).

Ajouter un constructeur pour initialiser ces propriétés.

### Etape 2
Créer une classe dérivée `Voiture`

Générer le constructeur proposé par Visual Studio, qui appelle celui de Véhicule

Tester en instanciant une voiture et en exécutant le code pas à pas en debug.

Noter qu’on n’a pas eu besoin de propriétés en écriture.

### Etape 3 : redéfinition
Dans la classe Véhicule, ajouter une propriété virtuelle
`Description` qui renvoie une chaîne "Véhicule [Nom] [Energie]"
(remplacer [Nom] et [Energie] par les valeurs de propriétés correspondantes).

Dans Voiture, redéfinir cette propriété pour qu'elle affiche "Voiture [nom] [energie]"

Dans Main, tester en affichant la description de la voiture.

### Etape 4
Créer une classe `Moto` dérivée de `Véhicule` avec un constructeur qui appelle celui de Véhicule.  
Redéfinir la propriété `Description` pour qu'elle affiche "Moto [nom] [energie]".  
Tester dans Main.

### Etape 5 : polymorphisme
Dans la fonction Main, créer un tableau de 2 véhicules contenant une voiture et une moto.  
Parcourir le tableau et afficher la description de chaque véhicule.

On obtient un comportement polymorphique.

### Etape 6
Ajouter une propriété `NbPortes` en lecture / écriture dans la classe Voiture.

Dans Main, affecter cette propriété sur la voiture déjà créée précédemment.  
Puis dans le parcours du tableau de véhicules, afficher le nombre de portes si le véhicules est une voiture.

### Etape 7 : classe et méthode abstraites
Rendre la classe Véhicule abstraite et ajouter une méthode abstraite `GetPrixRevente`
destinée à calculer le prix de revente d'un véhicule en fonction de son année de mise en circulation 
et de son kilométrage (passés en paramètre à la méthode).

Dans les classes Voiture et Moto, notez que Visual Studio souligne le nom de la classe en rouge pour signaler que la
méthode abstraite n’est pas implémentée.  
Dans chaque classe, cliquer sur l’ampoule qui apparaît dans la marge pour implémenter automatiquement cette méthode.  
Puis modifier l'implémentation selon les règles de calcul suivantes :

- Pour les voitures : prix de revente = prix d'achat * 100 000 / (kilométrage * ancienneté)  
- Pour les motos : prix de revente = prix d'achat * 70 000 / (kilométrage * ancienneté) 

Dans Main, tester ces méthodes pour une voiture et une moto.

### Etape 8 : interfaces
Faire dériver Véhicule de `IComparable` et cliquer sur l’ampoule pour
implémenter l’interface de façon explicite.

Dans la méthode `CompareTo`, faire en sorte de comparer les véhicules selon leur PRK.

?> Indice : le type double implémente déjà lui-même IComparable.

Dans Main, afficher le résultat de la comparaison des deux véhicules
(voiture et moto) créés plus haut à l'aide de variables de type `IComparable`.

Jouer avec les valeurs des PRK pour tester les 3 cas possibles.

### Etape 9
Afficher la description d'un des 2 véhicules après avoir transtypé de façon sécurisée la variable de type `IComparable`.

### Etape 10
Dans la classe Program, créer une fonction `Décrire`,
prenant en paramètre un objet, et affichant :

-  Le nom de son type
-  Le nom du type dont il hérite
-  Le nom des interfaces qu’il implémente
-  La liste de ses membres (type et nom)

Dans `Main`, appeler cette fonction en lui passant successivement 2
véhicules de types différents, et vérifier le résultat.