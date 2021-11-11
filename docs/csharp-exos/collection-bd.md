# Collection de bandes dessinées

**Objectifs** : désérialiser un fichier xml en liste d’entités, utiliser
la notion d’attribut

[Ce fichier xml](csharp-exos/fichiers/CollectionsBD.xml ':ignore') décrit des collections de bandes dessinées.

Il s’agit de charger le contenu de ce fichier dans une liste mémoire, en
utilisant les attributs définis par le .net framework pour la sérialisation xml.

Le chargement doit être fait dans une classe statique nommée `BD_DAL`,
comportant une seule méthode `ChargerCollectionsBD`, qui renvoie une `List<CollectionBD>`.

Créer dans le même fichier toutes les classes que vous jugerez
nécessaires pour charger les données par sérialisation xml. Ces classes
ne doivent comporter aucune méthode.