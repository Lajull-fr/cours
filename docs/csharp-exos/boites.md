# Boîtes

**Objectifs** :  
Mettre en œuvre les notions suivantes : constantes,
propriétés, méthodes, constructeurs, surcharges et agrégation.  
Et de façon annexe : énumérations, exceptions et structuration du code.

## Création du modèle objet

### Etape 1 : Définition de la boîte et de ses dimensions

Dans un nouveau projet nommé `Boites`, créer une classe nommée `Boite`

Ajouter des propriétés en lecture seule pour sa hauteur, sa largeur, sa
longueur, toutes 3 initialisées par défaut à 30.0.

Ajouter une propriété `Volume` en lecture seule qui retourne le volume
calculé d’après les dimensions.

### Etape 2 : Matière

Créer un type énuméré pour les matières (Carton, Plastique, Bois, Métal).  
Ajouter sur la classe `Boite` une propriété de ce type en lecture seule et
initialisée par défaut à Carton.

### Etape 3 : Destinataire

Créer une propriété `Destinataire` de type string en lecture seule  
Créer une méthode publique `Etiqueter` permettant d’affecter la valeur de
la propriété.

### Etape 4 : Fragile

Créer une propriété `Fragile` de type booléen en lecture seule.  
Créer une surcharge de la méthode précédente permettant d’affecter des
valeurs aux propriétés Destinataire et Fragile.

### Etape 5 : Comparaison de 2 boîtes

Ajouter une méthode `Compare` qui renvoie vrai si les dimensions et la
matière des 2 boîtes sont identiques. La tester.

### Etape 6 : Initialisation des boîtes

Ajouter 2 constructeurs à la classe Boîte.  
Le 1<sup>er</sup> permet
d’initialiser ses dimensions.  
Le 2d permet d’initialiser en plus sa matière.

Tester ces constructeurs en créant des boîtes dans la fonction Main

### Etape 7 : Etiquettes

Créer une classe Etiquette avec 3 propriétés en lecture/écriture :

-  `Texte` (string)
-  `Couleur` (énuméré avec les valeurs Blanc, Bleu, Vert, Jaune, Orange,
   Rouge, Marron
-  `Format` (énuméré avec valeurs XS, S, M, L, XL)

Dans Main, créer et initialiser une étiquette à l’aide d’un initialiseur.

### Etape 8 : Comptage des boîtes

Ajouter un compteur d’instances sur la classe Boite, en utilisant une
propriété statique pour retourner sa valeur.  
Tester son fonctionnement.

### Etape 9 : Etiquetage des boîtes (composition)

Dans la classe Boite, ajouter 2 champs privés de type Etiquette.  
Nommez-les `_etiquetteDest` et `_etiquetteFragile`

Dans la première méthode `Etiqueter` créée à l’étape 5, créer une instance
d’Etiquette de couleur blanche, de format L, et dont le texte est le
destinataire. Cette instance doit être référencée par le champ privé
`_etiquetteDest`

Dans la seconde méthode `Etiqueter` créée à l’étape 5, si le paramètre
fragile vaut Vrai, créer une instance d’étiquette de couleur rouge, de
format S, et dont le texte vaut « FRAGILE ». Cette instance doit être
accessible via le champ `_etiquetteFragile`.

Dans la fonction Main, créer une boîte, et l’étiqueter avec son
destinataire et une étiquette « FRAGILE ».

On vient d’illustrer la composition.

### Etape 10 : Etiquetage des boîtes (agrégation)

Créer une 3ème surcharge de la méthode `Etiqueter`, qui prend deux
paramètres de type Etiquette (une pour le destinataire et une pour
indiquer le caractère fragile)

Les étiquettes passées en paramètre doivent être affectées aux champs
`_etiquetteDest` et `_etiquetteFragile`

Dans la méthode Main de la classe Program :

-  En utilisant le constructeur adéquat, créer une boîte en plastique de
   dimensions 30 x 40 x 50

-  En utilisant un initialiseur, créer une étiquette de couleur blanche,
   de format L, avec comme texte un destinataire de votre choix

-  Créer de la même façon une étiquette rouge de format S, avec le texte
   « FRAGILE »

-  Affecter ces étiquettes à la boîte à l’aide de la méthode Etiqueter
   créée précédemment

On vient d’illustrer l’agrégation.