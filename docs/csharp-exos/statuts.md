# Statuts

**Objectifs** : apprendre à manipuler les énumérations de type flag, et
utiliser les listes génériques

Dans une entreprise, une personne peut avoir les statuts suivants :  
Salarié en CDI, salarié en CDD, délégué du personnel, membre du CHSCT,
représentant syndical.

Ces statuts ne sont pas exclusifs, c’est-à-dire qu'une personne peut
avoir plusieurs statuts à la fois.

### Etape 1
Créer une classe `Personne` avec des propriétés Nom, Prénom
et Statut, ainsi qu’un constructeur permettant d’initialiser ces 3
propriétés.

### Etape 2
Sur la classe Personne, redéfinir la méthode `ToString` pour
qu’elle renvoie une chaîne contenant le nom, le prénom et les statuts de
la personne.

### Etape 3
Dans Main, créer une liste contenant les personnes avec les statuts suivants :

|   Nom   |   Prénom   |   CDI   |   CDD   |   DP   |   CHSCT   |   SYND   |
|---------|------------|---------|---------|--------|-----------|----------|
| TURPIN  | Rémy       | 1       | 0       | 0      | 0         | 0        |
| BONNEAU | Zoé        | 0       | 1       | 1      | 0         | 0        |
| BLONDEL | Théo       | 1       | 0       | 1      | 1         | 1        |
| BLAIN   | Claire     | 1       | 0       | 0      | 0         | 0        |
| PERRIN  | Eric       | 1       | 0       | 0      | 0         | 0        |
| JORDAN  | Alain      | 0       | 1       | 0      | 1         | 0        |
| BAUDRY  | Alban      | 0       | 1       | 0      | 0         | 0        |
| ORLEANS | Fabrice    | 1       | 0       | 1      | 0         | 1        |
| VALOIS  | Alexandra  | 1       | 0       | 0      | 0         | 1        |
| WEST    | Alexis     | 1       | 0       | 1      | 1         | 0        |

### Etape 4
Parcourir la liste des personnes, et en utilisant des
masques, déterminer les personnes qui sont :

-  A la fois en CDD **et** membre du CHSCT (+ éventuellement d’autres
   fonctions)

-  A la fois en CDI **et** DP (+ éventuellement d’autres fonctions)

Copier ces personnes dans 2 autres listes distinctes.

### Etape 5
Afficher le détail des personnes de la seconde liste en
utilisant la méthode ToString redéfinie à l’étape 2

### Etape 6
Pour ces personnes, leur affecter en plus le statut de
membre du CHSCT, et afficher de nouveau le détail pour vérifier.