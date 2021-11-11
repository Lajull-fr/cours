# Entretien d’un véhicule

**Objectifs** : savoir créer et utiliser un délégué

On souhaite modéliser les opérations d’entretien réalisées sur un
véhicule par un garage. L’exécution d’une opération se traduit par
l’ajout de la description de l’opération dans le carnet d’entretien du véhicule.

En repartant de l’exercice des véhicules (il suffit d’avoir la classe
ancêtre Vehicule) :

### Etape 1
A côté de la classe Vehicule, créer un nouveau type
délégué nommé `DelegueEntretien` prenant en paramètre une date et un véhicule.

### Etape 2
Dans la classe Véhicule ajouter une propriété `CarnetEntretien` en lecture seule.  
Ce carnet contiendra les dates et descriptions des opérations d’entretien réalisées au cours de la vie du véhicule.  
Plusieurs opérations peuvent avoir lieu à la même date.

### Etape 3
Dans la classe Véhicule, ajouter une méthode `Entretenir`,
prenant en paramètre une date, et un délégué du type déclaré plus haut.
Dans cette méthode, faire en sorte de compléter le carnet d’entretien du
véhicule courant avec les opérations d’entretien représentées par le
délégué.

### Etape 4
Dans une nouvelle classe `Garage`, créer les méthodes
privées statiques suivantes qui devront être compatibles avec le délégué
DelegueEntretien : `ChangerPneus`, `Vidanger`, `RetoucherPeinture`

Chacune d’elles ajoutera l’opération effectuée dans le carnet d’entretien du véhicule.

### Etape 5
Dans la classe Garage, ajouter une méthode publique
`RéviserVéhicule` qui prend en paramètre un véhicule, et qui appelle sa
méthode Entretenir en lui passant un délégué représentant les trois
méthodes précédentes.

### Etape 6
Dans la classe Program, appeler la méthode `RéviserVéhicule`
sur un véhicule de votre choix (moto ou voiture), et afficher le contenu
de son carnet d’entretien.  
Exemple de résultat :

```
Carnet d’entretien du véhicule Mégane :

23/04/17 : Changement des pneus
23/04/17 : Vidange
23/04/17 : Retouche de peinture
```