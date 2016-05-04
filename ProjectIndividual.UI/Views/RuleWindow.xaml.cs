using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.ViewModels;

namespace ProjectIndividual.UI.Views
{
    /// <summary>
    /// Interaction logic for Rule.xaml
    /// </summary>
    public partial class RuleWindow : Window
    {
        public RuleWindow()
        {
            InitializeComponent();
        }

        public RuleWindow(RulesSet rules)
        {
            InitializeComponent();
            var ruleViewModel = ((RulesSetViewModel)Resources["rulesSet"]);
            ruleViewModel.rules = rules;
            ruleViewModel.RaisePropertyChanged("RuleName");
            ruleViewModel.RaisePropertyChanged("Rules");
        }
    }
}
