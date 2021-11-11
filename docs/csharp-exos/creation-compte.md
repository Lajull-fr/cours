# Création d’un compte

**Objectifs** : apprendre à émettre et intercepter des exceptions

### Etape 1
Créer une fonction `CréerCompte`, qui :

-  Demande à l’utilisateur de saisir successivement un login puis un mot
   de passe

-  Affiche un message « Votre compte a bien été créé. Un message vient
   de vous être envoyé ».

Appeler cette fonction dans Main.

### Etape 2
Créer les 2 fonctions suivantes pour vérifier les formats des informations du compte :

-  Fonction `VérifierLogin` qui vérifie que le login doit faire au moins 5
   caractères.

-  Fonction `VérifierMotDePasse` qui vérifie que le mot de passe comporter
   au moins 6 caractères, dont au moins une lettre et un chiffre.

La vérification du mot de passe peut se faire en parcourant les
caractères de la chaîne, et en comparant leur valeur ASCII aux plages
suivantes :

-  Chiffres : codes 48 à 57

-  Lettres majuscules : codes 65 à 90

-  Lettres minuscules : codes 97 à 122

Si les formats ne sont pas bons, lever des exceptions du type
`FormatException`, avec des descriptions explicites.

### Etape 3
Dans le code de l’étape 1, appeler la méthode de vérification du login.  
Intercepter l’exception sur le format et afficher
le message correspondant.

### Etape 4
Faire en sorte que la demande de login soit répétée tant
qu’un login correct n’a pas été saisi

### Etape 5
Appliquer les deux dernières étapes à la gestion du mot de passe.  
Le mot de passe ne doit être demandé que si la saisie du login
est correcte.

### Etape 6
Pour masquer le mot de passe pendant sa saisie, définissez
la couleur de police à la même valeur que la couleur de fond d’écran,
grâce aux propriétés `ForegroundColor` et `BackgroudColor` de la classe
Console.

Restaurer les couleurs par défaut après saisie du mot de passe, en
appelant la méthode `ResetColor`.

Vérifier que le message d’erreur s’affiche bien en cas de saisie d’un
mot de passe incorrect.