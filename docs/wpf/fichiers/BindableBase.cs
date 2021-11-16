using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestValidation.ViewModel
{
    /// <summary>
    /// C.Seguenot - 2018
    /// Classe ancêtre pour des classes utilisant la liaisons de données
    /// avec notification des changements. Code inspiré de l'article suivant :
    /// http://danrigby.com/2012/04/01/inotifypropertychanged-the-net-4-5-way-revisited//// 
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region Implémentation de INotifyPropertyChange
        /// <summary>
        /// Evènement de notification de changement de valeur 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Vérifie si la nouvelle valeur de la propriété qu'on affecte est différente
        /// de sa valeur actuelle, et si c'est le cas, affecte la nouvelle valeur, puis
        /// déclenche l'évènement PropertyChange
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">référence à la propriété à modifier</param>
        /// <param name="value">valeur à affecter</param>
        /// <param name="propName">Nom de la propriété (optionnel, car déterminé automatiquement) </param>
        /// <returns>
        ///     Vrai si la valeur a changé. Faux si la valeur à affecter est égale à la valeur actuelle
        /// </returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propName = null)
        {
            // Inutile de notifier un changement si la nouvelle valeur est identique
            if (Equals(field, value)) return false;

            field = value;
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }

            return true;
        }
        #endregion
    }
}
