# Relevés météo

**Objectifs** : mettre en œuvre les notions suivantes :

-  Méthodes de la classe string et formats (étape 1)

-  Types nullables (étape 2)

-  Listes génériques (étapes 3 et 4)

-  Requêtes Linq (étape 5)

[Fichier données météo Paris](csharp-exos/fichiers/DonneesMeteoParis.txt ':ignore')

Le fichier ci-dessus donne les relevés météo mensuels de la ville de
Paris depuis 2007 (données issues du [site de Météo
France](http://www.meteofrance.com/climat/france/paris/75114001/releves)).
Il contient une ligne d’en-tête et des lignes de données. Les valeurs
sont séparées par des **tabulations**

### Etape 1 : Affichage du contenu du fichier à l’écran

On souhaite afficher les données à l’écran sous la forme suivante :

```
Mois    | T° min | T° max | Précip (mm) | Ensol (H)
----------------------------------------------------
01/2007 |    6,2 |   10,2 |        28,0 |      46,0
02/2007 |    6,2 |   11,6 |        79,2 |      76,4
03/2007 |    5,4 |   12,3 |        41,4 |     145,6
04/2007 |   10,5 |   21,7 |         5,2 |     312,4
05/2007 |   12,0 |   20,4 |       102,4 |     153,5
06/2007 |   14,7 |   23,0 |        52,6 |     146,0  
...
```

Pour cela :

-  Copier le fichier de données dans le répertoire du projet. Son chemin
   pourra ainsi être spécifié dans le code de la façon suivante :
   «..\\..\\DonnéesMétéoParis.txt ». Il s’agit d’un chemin relatif par
   rapport à l’exécutable de l’application qui se trouve dans le
   répertoire bin\\debug

-  Lire son contenu en utilisant la méthode `File.ReadAllLines`

-  Utiliser une classe `RelevéMensuel` pour modéliser les infos d’un
   relevé mensuel et les extraire d’une ligne de fichier. Trouver deux
   méthodes différentes pour cette extraction, utilisant chacune des
   méthodes différentes de la classe string.

-  Utiliser la méthode `ToString` de RelevéMensuel pour générer chaque
   ligne à afficher

Le programme devra bien séparer l’interface visuelle de la logique métier.

NB/ Pour changer le jeu de caractères de la console, utiliser la ligne
suivante :

Console.OutputEncoding = Encoding.UTF8;

### Etape 2 : Gestion des valeurs non renseignées

-  Effacer la valeur d’ensoleillement sur quelques lignes du fichier de
   données en laissant la tabulation qui précède

-  Dans la classe RelevéMensuel, modifier le code d’extraction des infos
   d’une ligne de fichier pour faire en sorte de gérer la valeur null
   pour l’ensoleillement

-  Vérifier que les données s’affichent toujours bien et que la durée
   d’ensoleillement est vide (et non pas égale à 0) pour les lignes où
   cette valeur n’est pas renseignée

### Etape 3 : Chargement des données en mémoire

On va charger les données dans une liste de façon à pouvoir en extraire
quelques statistiques. Pour cela :

-  Créer une classe statique nommée `DAL` (pour Data Access Layer) contenant :

   -  Une constante représentant le chemin du fichier

   -  Une méthode `GetRelevésMensuels` qui charge le contenu du fichier
      dans une liste générique de relevés mensuels, et renvoie cette
      liste en retour.

-  Dans la méthode Main, tester la méthode précédente en affichant le
   contenu de la liste qu’elle renvoie

### Etape 4 : Statistiques sur les données

-  Créer une classe `Stats`, avec un constructeur prenant une liste de
   relevés en paramètres

-  Ajouter une propriété `ReleveTempMax` qui renvoie le relevé
   correspondant à la température maximale la plus élevée.

-  Ajouter une méthode `GetPrécipitationsAnnée` qui renvoie la somme des
   précipitations de l’année passée en paramètre

-  Dans la méthode Main, tester les deux méthodes précédentes

### Etape 5 : Statistiques avec Linq

Dans la classe Stats, refaire le code de ReleveTempMax et
GetPrécipitationsAnnée en utilisant des requêtes Linq, puis ajouter les
propriétés suivantes :

-  `EnsoleillementMoyenJuillet` : durée d’ensoleillement moyenne du mois
   de juillet sur toutes les années

-  `NbMoisChauds` : nombre de mois dont la T° maxi a été supérieure à la
   T° moyenne de tous les mois

-  `PrécipitationsMoyennesParAnnée` : précipitations mensuelles moyennes par année