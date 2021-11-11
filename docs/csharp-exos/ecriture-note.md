# Ecriture d’une note

**Objectifs** : illustrer le fonctionnement des instructions `try…finally`
et `using`, et des espaces de noms.

### Etape 1
Dans la classe Program, créer une fonction `SaisirNote`,
qui demande à l’utilisateur de saisir successivement un texte puis un
chemin de fichier.

Appeler cette fonction dans Main.

### Etape 2
Dans une classe `Note`, créer une fonction `EnregistrerNote`
qui prend en paramètre le chemin et le texte saisis précédemment.  
Cette fonction doit créer le fichier et écrire le texte dedans.  
Si le fichier existe déjà, il doit être complété.

Faire en sorte que le fichier soit fermé à la fin de l’opération même si
une erreur se produit.

### Etape 3
Dans la fonction SaisirNote, appeler la méthode
EnregistrerNote, en interceptant l’exception qui se produit dans le cas
où le répertoire spécifié dans le chemin n’existe pas.

### Etape 4
Placer un point d’arrêt sur la ligne de code qui ferme le fichier s’il est ouvert

Vérifier en exécution que ce point d’arrêt est bien atteint, que le
répertoire spécifié existe ou qu’il n’existe pas.

### Etape 5
Refaire le même test en mettant au préalable le fichier
créé précédemment en lecture seule, en modifiant ses options dans
l’explorateur de fichier de Windows.

### Etape 6
Faire en sorte que le point d’arrêt soit bien atteint
quelle que soit l’erreur qui se produit au moment de l’enregistrement.

### Etape 7
Gérer la fermeture du fichier au moyen d’une instruction `using`.

### Etape 8
Dans le fichier Program.cs, utiliser le nom complet de
l’exception `DirectoryNotFoundException` au lieu d’utiliser une directive using.

### Etape 9 : utilisation d’un sous-espace de noms

-  Mettre la classe Note dans un sous-espace de noms nommé `Core` de
   votre espace de noms courant

-  Que se passe-t-il dans le fichier Program.cs ?

-  Faites le nécessaire pour que l’application compile de nouveau