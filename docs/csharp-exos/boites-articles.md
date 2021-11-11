# Boîtes et articles

**Objectifs** : mettre en œuvre les collections génériques et exploiter
un modèle objet.

## Enoncé

On repart de l’exercice précédent. Il s’agit d’ajouter le fonctionnel suivant :

Dans une boîte, on peut ajouter ou enlever des articles.  
Un article est décrit par son id (de type guid, généré automatiquement),
son libellé, son volume et son poids. Seul le libellé est modifiable
après création de l’article.

Une boîte peut contenir plusieurs articles si son volume le permet (on
ne tient pas compte de la forme des articles).  
Toutefois, le poids total ne doit pas dépasser 10 kg (valeur qui doit
être modifiable de façon centralisée).

Les boîtes sont numérotées automatiquement dans l’ordre de leur création.

On doit pouvoir transférer le contenu d’une boîte vers une autre.  
Si la capacité (volume et poids) de la boîte de destination n’est pas
suffisante pour contenir tous les articles, on transfère ceux qu’on peut
(les derniers ajoutés sont les premiers transférés) et on ne lève pas
d’erreur. L’utilisateur doit pouvoir connaître le nombre d’articles transférés.  
Nb/ La boîte de destination peut éventuellement déjà contenir des articles.

**Indications :**

-  La fonction Main ne doit contenir que le code de test et d’affichage.
   L’affichage doit se faire exclusivement dans la fonction Main.

-  Pour générer un Guid, utiliser la méthode statique `Guid.NewGuid()`

-  Si le poids total d’une boîte excède 10 Kg ou si elle est trop petite
   pour contenir les articles souhaités, lever une exception de type
   `InvalidOperationException`

**Bonus pour les plus rapides** :

Ecrire une autre méthode de transfert en tout ou rien. C'est-à-dire que
si la capacité de la boîte de destination (volume et poids) n’est pas
suffisante, on ne transfère aucun article de la boîte d’origine.  
La méthode doit renvoyer un booléen indiquant si le transfert a été fait ou non.