# Listes de véhicules

En repartant de l’exercice sur les véhicules :

### Etape 1 : ajout d’une propriété Prix

Sur la classe Véhicule :

-  Ajouter une propriété `Prix`

-  Ajouter un constructeur permettant d’initialiser le nom et le prix du véhicule

-  Modifier la méthode CompareTo pour que la comparaison des véhicules
   porte sur le prix (attention au type d’implémentation de cette
   méthode d’interface).

Sur les classes Voiture et Moto, ajouter un constructeur semblable à celui ajouté sur Véhicule.

### Etape 2 : création de véhicules

Dans la fonction Main, créer les 4 véhicules suivants :

-  Voiture Mégane à 19 000 €

-  Moto Intruder à 13 000€

-  Voiture Enzo à 380 000€

-  Moto Yamaha XJR1300 à 11 000€

### Etape 3 : création d’une liste triée

Instancier une liste triée générique nommée `liste1` et ajouter dedans les
4 véhicules, en prenant comme clé le nom du véhicule et comme valeur
l’instance de véhicule.

Afficher le contenu de la liste, chaque élément étant affiché sous la
forme suivante : « Nom : prix ».

Selon quel ordre la liste est-elle triée ?

### Etape 4 : tri d’une liste

Instancier une liste simple générique nommée `liste2` et ajouter dedans les 4 véhicules.

Trier la liste à l’aide de sa méthode `Sort`.

Afficher le contenu de la liste, chaque élément étant affiché sous la
forme suivante : « Nom : prix ».

Selon quel ordre la liste est-elle triée ? Comment s’est fait ce tri ?

### Etape 5 : comparateur

Créer une classe `ComparateurVehicules` qui implémente l’interface
`IComparer<Vehicule>`.  
Faire en sorte qu’elle compare les véhicules selon leur PRK.

Trier la liste précédente à l’aide de ce comparateur.

Afficher le contenu de la liste, chaque élément étant affiché sous la
forme suivante : `Nom : PRK`.

### Etape 6 : recherche

Créer un tableau de chaînes initialisé avec les valeurs suivantes :
Clio, Mégane, Golf, Enzo, Polo

Pour chaque élément de ce tableau, chercher le véhicule correspondant
dans la liste1, et afficher son nom et son prix s’il est trouvé.

### Etape 7 : tableau params

Sur la classe Véhicule, ajouter une méthode statique `LePlusCher`
prenant un nombre quelconque de véhicules en paramètre et renvoyant le
plus cher d’entre eux.

Appeler la méthode LePlusCher en lui passant les 4 véhicules et afficher
le nom du véhicule le plus cher.