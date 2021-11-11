# Tri d’un tableau

**Objectifs** :

-  Modéliser un algorithme avec un diagramme UML d’activité

-  Implémenter cet algorithme en C#

-  Utiliser des fonctions et des surcharges

Un algorithme possible pour trier un tableau est de comparer ses
éléments deux à deux, et les permuter s’ils ne sont pas dans le bon
ordre. Le tableau est trié lorsqu’on parcourt tous ses éléments sans
faire aucune permutation.

### Etape 1
Modéliser l’algorithme de tri en UML

### Etape 2
Avant d’implémenter cet algorithme, créer une fonction
`AfficherTableau` permettant d’afficher le contenu d’un tableau d’entiers
sur une seule ligne.

### Etape 3
Créer une fonction `TrierTableau` permettant de trier par
ordre croissant les éléments d’un tableau d’entiers passé en paramètre,
selon l’algorithme modélisé précédemment.

### Etape 4
Initialiser un tableau de 10 entiers non triés, et le
passer en paramètre à la fonction `TrierTableau`.

Utiliser la fonction `AfficherTableau` pour afficher le contenu du tableau
avant et après l’appel à la fonction `TrierTableau`.

### Etape 5
Faire une seconde fonction `TrierTableau` prenant en
paramètre un tableau de chaînes de caractères.

?> Utiliser la méthode `CompareTo()` pour comparer les chaînes
entre elles. Exemple : `mot1.CompareTo(mot2);`

### Etape 6
Tester cette fonction de la même façon que pour le tableau d’entiers

### Etape 7
Modifier la seconde fonction de tri pour qu’elle renvoie
un tableau trié en retour, sans modifier le tableau original passé en paramètre.