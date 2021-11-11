# 7. Composition et agrégation

La programmation orientée objet permet de décrire les applications comme
des collaborations d'objets. Par exemple, certaines méthodes d'une
classe utilisent les services d'une autre classe.

**La composition** décrit la situation où un objet est lui-même
constitué d'objets d'autres classes.  
Par exemple un livre est constitué de différents chapitres, une voiture est constituée d’un moteur, d’une carrosserie, de roues…etc.

Dans le code, la relation de composition se traduit par le fait qu'une
classe possède des instances d’autres classe, et qu’elle les instancie
elle-même, généralement dans un de ses constructeurs.

**L’agrégation** est une relation moins forte dans laquelle un objet
contient d’autre objets, mais peut exister sans eux.  
Par exemple, un garage peut contenir des voitures.

Dans le code, la relation d’agrégation se traduit par le fait qu’une
classe agrège des instances d’une autre classe, mais que ces dernières
sont créées en dehors de la classe contenante.

Exemple :  

__Composition__
 ```csharp
public class Moteur {   }
public class Voiture
{
    private Moteur _moteur;
    public Voiture()
    {
        _moteur = new Moteur();
    }
}
```
Le moteur fait partie de la voiture et est instancié par elle.  

__Agrégation__
```csharp
public class Garage
{
    private List<Voiture> _voitures;
 
    public void RentrerVoiture(Voiture v)
    {
        _voitures.Add(v);
    }
}
```

Les voitures sont instanciées en dehors de la classe et ajoutées à la liste interne du garage.

NB/ En pratique, même si la relation entre deux classes est une
agrégation du point de vue du sens, il est courant de l’implémenter
comme une composition, sans que cela pose un problème.