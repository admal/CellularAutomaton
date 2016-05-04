
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ProjectIndividual.Domain.RulesComponent.Entities;

namespace ProjectIndividual.UI.ViewModels
{
    public class RulesSetViewModel : INotifyPropertyChanged
    {
        #region Fields
        public RulesSet rules = new RulesSet();
        #endregion

        #region Properties
        public string RuleName { get { return rules.Name; } set { rules.Name = value; } }
        public List<Rule> Rules { get { return rules.Rules as List<Rule>; } }
        #endregion

        #region Constructors

        public RulesSetViewModel()
        {
        }
        #endregion

        #region IPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}