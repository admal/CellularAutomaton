using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectIndividual.UI.Commands
{
    public class BasicCommand : ICommand
    {
        private Action WhatToExecute;
        private Func<bool> WhenToExecute;

        public BasicCommand(Action whatToExecute, Func<bool> whenToExecute)
        {
            WhatToExecute = whatToExecute;
            WhenToExecute = whenToExecute;
        }

        public bool CanExecute(object parameter)
        {
            return WhenToExecute();
        }

        public void Execute(object parameter)
        {
            WhatToExecute();
        }

        public event EventHandler CanExecuteChanged;
    }
}
