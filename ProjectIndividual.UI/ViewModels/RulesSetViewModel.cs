﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using ProjectIndividual.Domain.FileManagment;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.Commands;

namespace ProjectIndividual.UI.ViewModels
{
    public class RulesSetViewModel : INotifyPropertyChanged
    {
        #region Fields

#warning TMP TO CHANGE!!!
        public GridViewModel ViewModel;
        public RulesSet rules = new RulesSet();

        private BasicCommand addRuleCommand;
        private BasicCommand saveRuleCommand;
        private BasicCommand clearRuleCommand;
        private BasicCommand loadRuleCommand;
        private BasicCommand applyRuleCommand;
        #endregion

        #region Properties
        public string RuleName { get { return rules.Name; } set { rules.Name = value; } }
        public List<Rule> Rules { get { return rules.Rules as List<Rule>; } }

        public BasicCommand AddRuleCommand
        {
            get { return addRuleCommand; }
        }

        public BasicCommand SaveRuleCommand
        {
            get { return saveRuleCommand; }
        }

        public BasicCommand ClearRuleCommand
        {
            get { return clearRuleCommand; }
        }

        public BasicCommand LoadRuleCommand
        {
            get { return loadRuleCommand; }
        }

        public BasicCommand ApplyRuleCommand
        {
            get { return applyRuleCommand; }
        }

        #endregion

        #region Constructors

        public RulesSetViewModel()
        {
            addRuleCommand = new BasicCommand(AddNewRule, () => true );
            saveRuleCommand = new BasicCommand(SaveNewRuleSet, () => true);
            clearRuleCommand = new BasicCommand(ClearRuleSet, ()=>true);
            loadRuleCommand = new BasicCommand(LoadRuleSet, ()=>true);
            applyRuleCommand = new BasicCommand(ApplyRulesToGrid, ()=>true);
        }

        private void ApplyRulesToGrid()
        {
            ViewModel.grid.Rules = rules;
            ViewModel.RaisePropertyChanged("isStartable");
            ViewModel.RaisePropertyChanged("RulesName");
            ViewModel.RaisePropertyChanged("Rules");
        }

        #endregion

        #region Methods
        private void LoadRuleSet()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Rules files (.rules)|*.rules";
            dialog.FilterIndex = 1;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = dialog.ShowDialog();
            if (userClickedOK == true)
            {
                rules = FileLoader.ReadFromBinaryFile<RulesSet>(dialog.FileName);
                RaisePropertyChanged("RuleName");
                RaisePropertyChanged("Rules");
                MessageBox.Show("Rules loaded properly!");
            }
        }
        private void AddNewRule()
        {
            rules.Rules.Add(new Rule());
            RaisePropertyChanged("Rules");
        }

        private void SaveNewRuleSet()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Grid file (*.rules)|*.rules";
            if (dialog.ShowDialog() == true)
            {
                FileCreator.WriteToBinaryFile(dialog.FileName, rules);
            }
        }

        private void ClearRuleSet()
        {
            this.rules = new RulesSet();
            RaisePropertyChanged("RuleName");
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