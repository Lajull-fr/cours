# Mois et saisons

**Objectifs** : apprendre à manipuler les énumérations à bits indicateurs.

### Etape 1
Créer 2 énumérations de type flags pour les mois et les saisons.

### Etape 2
Ecrire une fonction nommée `SaisonsDuMois`, qui permet de
déterminer la ou les saisons d’un mois passé en paramètre de type énuméré.  
La fonction doit retourner également un énuméré, et doit
utiliser exclusivement des opérations binaires.

La figure suivante montre la correspondance des mois et saisons :

```
| Janv. | Fév. |  Mars |  Avr. |  Mai  | Juin  | Juil. |  Août | Sept. |  Oct. |  Nov. |  Déc. |
________________________________________________________________________________________________
        Hiver       |       Printemps       |          Eté          |         Automne       |
```

Les changements de saisons ont lieu les 21 mars, 21 juin, 21 sept. et 21 déc.  
Le mois de mars est donc à cheval sur l'hiver et le printemps.  
Le mois de juin est à cheval sur le printemps et l’été.  
...etc.

### Etape 3
Tester cette fonction en l’appelant dans la méthode Main
et en affichant son résultat pour différents mois

### Etape 4
Enlever l’attribut `Flags` sur les énumérations pour voir le résultat.

### Etape 5
Afficher les saisons de chaque mois en appelant la fonction dans une boucle for