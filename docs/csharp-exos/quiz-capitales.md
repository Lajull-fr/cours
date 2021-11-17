# Quiz capitales

**Objectifs** :
- Apprendre la syntaxe de base du C#.
- Afficher des valeurs à l’écran
- Faire saisir des valeurs à l’utilisateur
- Utiliser les instructions `if, else, for, while` et les opérateurs de base

Pour cet exercice, on utilisera une partie de la liste des pays et capitales européens :

| Pays               | Capitale           |
|--------------------|--------------------|
| Albanie            | Tirana             | 
| Allemagne          | Berlin             | 
| Andorre            | Andorre-la-Vieille |
| Autriche           | Vienne             | 
| Belgique           | Bruxelles          | 
| Biélorussie        | Minsk              | 
| Bosnie-Herzégovine | Sarajevo           |
| Bulgarie           | Sofia              |
| Chypre             | Nicosie            |
| Croatie            | Zagreb             |
| Danemark           | Copenhague         |
| Espagne            | Madrid             |
| Estonie            | Tallinn            |
| Finlande           | Helsinki           |
| France             | Paris              |

### Etape 1 : Création du projet
Dans la solution Exercices, créer un nouveau projet nommé Capitales

Dans la méthode `Main`, faire en sorte que le programme ne se termine qu’après avoir appuyé sur Entrée.

Lancer le programme pour tester.

### Etape 2 : première question
Dans la méthode Main, demander à l'utilisateur quelle est la capitale de l'Espagne.  
S'il répond bien, afficher "Bravo !", sinon, afficher "Mauvaise réponse".

Lancer le programme pour tester.

### Etape 3
Ajouter 2 autres questions pour le Portugal et l'Italie.

NB/ Dans les étapes suivantes, on va automatiser le processus, pour ne pas
avoir à écrire plusieurs fois le même code.

### Etape 4

-  Mettre le code précédent en commentaire (il pourra être supprimé à la
   fin de l’étape suivante)

-  Créer un premier tableau contenant les 10 premiers pays européens

-  Créer un second tableau contenant leurs capitales

### Etape 5
Parcourir le 1er tableau, et pour chaque pays :

-  Demander à l'utilisateur la capitale
-  S'il répond bien, afficher "Bravo !", sinon, afficher "Mauvaise
   réponse. La réponse était…"

### Etape 6
Modifier le code précédent pour ne poser qu’une question sur deux.  
Tester

### Etape 7
Remettre le code comme il était pour poser toutes les questions.  
Modifier le code pour poser les questions en partant de la fin.
 
### Etape 8
Créer un compteur de bonnes réponses, et afficher sa valeur à la fin du jeu

### Etape 9
A la fin du jeu, demander à l’utilisateur s’il veut rejouer.  
S’il tape « o » ou « O », vider l’écran et relancer le jeu,
sinon, afficher un message « Merci d’avoir joué »

### Etape 10 : création d’une fonction

Déplacer le code de la fonction `Main` dans une fonction statique `Jouer`,
et appeler cette dernière dans `Main`.

Sortir les tableaux pays et capitales de la méthode Jouer, pour les
mettre directement dans la classe `Program`. Pour qu’ils restent
utilisable dans la méthode Jouer, les faire précéder du mot clé `static`.

Exécuter le programme pour vérifier que tout fonctionne toujours bien.

### Etape 11
Créer une méthode `PoserQuestion` qui prend en paramètre le
numéro de la question à poser (qui correspond à l’indice du pays dans le
tableau des pays).

Tester cette méthode en l’appelant dans la méthode Main

### Etape 12 : Questions aléatoires

Créer une méthode `Jouer2`, avec la logique suivante :

-  Vider la console

-  Afficher le message "Appuyer sur Echap pour arrêter le jeu"

-  Générer un nombre aléatoire entre 0 et le nombre de pays – 1

-  Appeler la méthode PoserQuestion en lui passant ce nombre en
   paramètre

-  Répéter ceci tant que l’utilisateur n’a pas appuyé sur la touche
   Echap

Tester cette méthode en l’appelant dans Main à la place de la méthode
Jouer.

Indication : pour générer un nombre aléatoire compris entre 0 et X, utiliser le code suivant :

```csharp
Random rd = new Random();  
Rd.Next(X)
```
