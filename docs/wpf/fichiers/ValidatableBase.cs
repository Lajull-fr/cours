using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestValidation.ViewModel
{
    /// <summary>
    /// C.Seguenot - 2018
    /// Classe ancêtre pour la validation compatible avec une architecture MVVM
    /// Implémente la validation globale d'un objet et la validation individuelle d'une propriété,
    /// avec notification des erreurs. le code est inspiré de cet article tout en apportant des améliorations :
    /// http://www.c-sharpcorner.com/UploadFile/tirthacs/inotifydataerrorinfo-in-wpf/
    /// Ajouter une référence à System.ComponentModel.DataAnnotations pour l'utiliser
    /// </summary>
    public abstract class ValidatableBase : BindableBase, INotifyDataErrorInfo
    {
        #region Implementation de INotifyDataErrorInfo 
        // Dictionnaire des erreurs. La clé est le nom de la propriété et la 
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private object _lock = new object();

        /// <summary>
        /// Evènement qui se produit lorsque les erreurs de validation ont été modifiées pour une propriété
        /// ou pour l'entité entière
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        ///  Obtient un booléen qui indique si l'entité comporte des erreurs de validation
        /// </summary>
        public bool HasErrors
        {
            //get { return _errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Any()); }
            get { return _errors.Any(); }
        }

        /// <summary>
        ///  Obtient un booléen qui indique si l'entité est valide (valeur inverse de HasErrors)
        /// </summary>
        public bool IsValid
        {
            get { return !HasErrors; }
        }

        /// <summary>
        /// Obtient les erreurs de validation pour une propriété spécifiée ou pour l'ensemble de l'entité
        /// </summary>
        /// <param name="propertyName">Nom de la propriété pour laquelle récupérer les erreurs de validation,
        /// ou null pour récupérer toutes les erreurs de l'entité.</param>
        /// <returns>Liste des messages d'erreurs</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            // Si aucun nom de propriété n'est spécifié, on renvoie toutes les erreurs de l'entité
            if (string.IsNullOrEmpty(propertyName))
                return _errors.SelectMany(err => err.Value.ToList());

            List<string> propertyErrors = null;
            _errors.TryGetValue(propertyName, out propertyErrors);
            return propertyErrors;
        }
        #endregion

        #region Méthodes spécifiques de la classe
        /// <summary>
        /// Renvoie un message d'erreur global contenant tous les messages d'erreurs
        /// séparés par des retours à la ligne
        /// </summary>
        /// <returns>Message d'erreur global</returns>
        public string GetErrorMessage()
        {
            if (IsValid) return null;

            StringBuilder message = new StringBuilder();
            foreach (var err in GetErrors(null))
            {
                message.AppendLine(err.ToString());
            }

            return message.ToString();
        }

        /// <summary>
        /// Validation d'une propriété
        /// </summary>
        /// <param name="value">Valeur à valider</param>
        /// <param name="propertyName">Nom de la propriété (optionnel, car déterminé automatiquement)</param>
        public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            // Empêche 2 threads d'exécuter le bloc de code en même temps
            lock (_lock)
            {
                // S'il y avait déjà une entrée dans le dico des erreurs pour cette propriété,
                // on la supprime puis on notifie le changement
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                    NotifyErrorsChanged(propertyName);
                }

                // Exécute les règles de validation définies par les attributs sur la propriété
                // et récupère les erreurs éventuelles
                var context = new ValidationContext(this);
                context.MemberName = propertyName;
                var results = new List<ValidationResult>();
                Validator.TryValidateProperty(value, context, results);

                // S'il y a des erreurs
                if (results.Any())
                {
                    // on récupère leurs messages et on ajoute l'erreur au dictionnaire
                    var messages = results.Select(r => r.ErrorMessage).ToList();
                    _errors.Add(propertyName, messages);

                    // Puis on notifie l'erreur
                    NotifyErrorsChanged(propertyName);
                }
            }
        }

        /// <summary>
        /// Validation de toutes les propriétés de l'entité
        /// </summary>
        public void ValidateAll()
        {
            // Empêche 2 threads d'exécuter le bloc de code en même temps
            lock (_lock)
            {
                // Mémorise les noms des propriétés qui étaient déjà en erreur
                // puis vide le dictionnaire des erreurs
                var propNames = _errors.Keys.ToList();
                _errors.Clear();

                // Envoie une notification pour chaque propriété qui était en erreur
                // (de façon que ces propriétés ne soient plus vues comme non valides)
                propNames.ForEach(pn => NotifyErrorsChanged(pn));

                // Exécute les règles de validation définies par les attributs
                // et récupère les erreurs éventuelles
                var context = new ValidationContext(this);
                var results = new List<ValidationResult>();
                Validator.TryValidateObject(this, context, results, true);

                // Groupe les résultats de validation par nom de propriété
                var resultsByPropNames = from res in results
                                         from mname in res.MemberNames
                                         group res by mname into g
                                         select g;

                // Ajoute les erreurs au dictionnaire et notifie les bindings
                foreach (var prop in resultsByPropNames)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();
                    _errors.Add(prop.Key, messages);
                    NotifyErrorsChanged(prop.Key);
                }
            }
        }

        /// <summary>
        /// Déclenche l'évènement de notification des erreurs pour une propriété 
        /// </summary>
        /// <param name="propertyName">nom de la propriété</param>
        private void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion
    }
}
