# Jeu du pendu

**Objectifs** : mettre en œuvre les notions suivantes : fonctions,
énumérations, exceptions

### Etape 1
Dans la solution Exercices, ajouter le projet suivant : [Projet jeu de pendu](csharp-exos/fichiers/Pendu.zip ':ignore')

### Etape 2
Compléter le code pour que le jeu fonctionne conformément au scénario suivant :

```
Jouez au pendu!

Saisissez un mot de 3 à 25 lettres sans accent :
en
Le mot doit contenir entre 3 et 25 lettres

Saisissez un mot de 3 à 25 lettres sans accent :
te8st
Le mot ne doit contenir que des lettres, sans accent

Saisissez un mot de 3 à 25 lettres sans accent :
Developpeur
```

Une fois le mot à deviner correctement saisi, on efface l’écran pour que
l’autre joueur ne voit pas le mot, puis on affiche ceci :

```
Mot en cours de déchiffrage : -----------

Proposez une lettre :
```

Dès que le joueur saisi une lettre, on réaffiche la même chose :

-  En mettant à jour le dessin de l’échafaud (pour l’instant vide)
   au-dessus du texte si la lettre proposée n’est pas dans le mot à
   deviner

-  Ou bien en affichant la lettre à la place du ou des tirets
   correspondants dans la première ligne

Exemple, après avoir trouvé plusieurs lettres et fait plusieurs
erreurs :

```
____
|/ |
|  o
| /|\
| / \
|____


Mot en cours de déchiffrage : -evel---e-r

Proposez une lettre :
```

A la fin, on affiche l’une des deux phrases suivantes :

```
Bravo, vous avez gagné !

Perdu ! Le mot à deviner était : developpeur
```

### Etape 3
Modifier le code pour gérer les erreurs du joueur au moyen
d’exceptions de type `FormatException`.

### Etape 4
Remplacer les méthodes GetXXX de la classe Jeu par des propriétés XXX

### Etape 5
Pour la propriété `MotADeviner`, utiliser une implémentation
automatique et faire les modifications nécessaires dans le reste du code.