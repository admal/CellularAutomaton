
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.Commands;

namespace ProjectIndividual.UI.ViewModels
{
    public class RulesSetViewModel : INotifyPropertyChanged
    {
        #region Fields
        public RulesSet rulesSet = new RulesSet();
        private ObservableCollection<RuleViewModel> rules;
        private BasicCommand addRuleCommand;
        #endregion

        #region Properties
        public string RuleName { get { return rulesSet.Name; } set { rulesSet.Name = value; } }
        public ObservableCollection<RuleViewModel> Rules { get { return rules; } }

        public BasicCommand AddRuleCommand
        {
            get { return addRuleCommand; }
        }

        #endregion

        #region Constructors

        public RulesSetViewModel()
        {
            addRuleCommand = new BasicCommand(AddNewRule, () => true );
            rules = new ObservableCollection<RuleViewModel>(rulesSet.Rules.Select(r => new RuleViewModel(r)));
        }

        private void AddNewRule()
        {
            rulesSet.Rules.Add(new Rule());
            RaisePropertyChanged("Rules");
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