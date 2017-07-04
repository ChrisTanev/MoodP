using System;
using System.Windows.Input;

namespace MP.Application.Implementation.Utility
{
    public class CommandBase : ICommand
    {
        private readonly bool _canExecute;
        private readonly Action<object> _parameterizedAction;

        public CommandBase(Action<object> parameterizedAction, bool canExecute = true)
        {
            //  Set the _action.
            _parameterizedAction = parameterizedAction;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _parameterizedAction(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}