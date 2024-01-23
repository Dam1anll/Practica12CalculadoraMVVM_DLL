using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;
using Practica12CalculadoraMVVM_DLL.View;
using System.Threading.Tasks;
using Practica12CalculadoraMVVM_DLL.ViewModel;
using System.ComponentModel;

namespace Practica12CalculadoraMVVM_DLL.ViewModel
{
    public class VMCalculadora : BaseViewModel
    {
        #region VARIABLES
        private string _displayText = "0";
        private double _currentValue = 0;
        #endregion

        #region PROPERTIES
        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged(nameof(DisplayText));
                }
            }
        }

        public ICommand NumberCommand { get; private set; }
        public ICommand OperatorCommand { get; private set; }
        public ICommand EqualsCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion

        #region CONTRUCTOR
        public VMCalculadora(INavigation navigation)
        {
            Navigation = navigation;
            InitializeCommands();
        }
        #endregion

        #region OBJETOS
        #endregion

        #region PROCESOS
        #endregion

        #region COMANDOS
        private void InitializeCommands()
        {
            NumberCommand = new Command<string>(UpdateDisplay);
            OperatorCommand = new Command<string>(ApplyOperator);
            EqualsCommand = new Command(PerformCalculation);
            DeleteCommand = new Command(DeleteLastDigit);
        }

        private void UpdateDisplay(string number)
        {
            if (DisplayText == "0" || DisplayText == "Error")
            {
                DisplayText = number;
            }
            else
            {
                DisplayText += number;
            }
        }

        private void ApplyOperator(string operatorSymbol)
        {
            if (double.TryParse(DisplayText, out double enteredValue))
            {
                _currentValue = enteredValue;
                DisplayText = "0";
            }
        }

        private void PerformCalculation()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                _currentValue += currentValue;
                DisplayText = _currentValue.ToString();
            }
        }

        private void DeleteLastDigit()
        {
            if (DisplayText.Length > 1)
            {
                DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
            }
            else
            {
                DisplayText = "0";
            }
        }
        #endregion
    }
}

