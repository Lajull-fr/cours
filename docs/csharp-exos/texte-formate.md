# Texte formaté

**Objectifs** : apprendre à utiliser les chaînes de formats

Afficher le texte suivant à l’écran, en respectant exactement les formats :

```
Hier, Bryan a gagné $148.00 à la roulette en 02 heures et 25 minutes.
Durant l'hiver, c'est à dire depuis le 21 Dec 2016, il a gagné $1,234.
```

… et en utilisant les variables suivantes :

```csharp
decimal gain = 148m;

TimeSpan durée = new TimeSpan(2, 25, 46);

DateTime dateDeb = new DateTime(2016, 12, 21);

decimal total = 1234;
```

Puis compter le nombre de mots de ce texte en utilisant la méthode `Split`
de la classe string.