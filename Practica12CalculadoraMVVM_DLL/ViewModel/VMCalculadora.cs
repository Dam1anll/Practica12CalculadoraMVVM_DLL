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
        private string _datos = "0";
        private double _numeros = 0;
        private List<double> _valores = new List<double>();
        private List<string> _operadores = new List<string>();
        #endregion
        #region CONTRUCTOR
        public VMCalculadora(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region OBJETOS
        public string Datos
        {
            get { return _datos; }
            set
            {
                if (_datos != value)
                {
                    _datos = value;
                    OnPropertyChanged(nameof(Datos));
                }
            }
        }
        #endregion
        #region PROCESOS
        private async Task ActualizarDatos(string numero)
        {
            if (Datos == "0" || Datos == "Error")
            {
                Datos = numero;
            }
            else
            {
                if (numero == "." && Datos.Contains("."))
                {
                    return;
                }

                Datos += numero;
            }
        }
        private async Task AplicarOperador(string operador)
        {
            if (double.TryParse(Datos, out double valor))
            {
                _valores.Add(valor);
                _operadores.Add(operador);
                Datos = "0";
             
            }
        }
        private async Task Calcular()
        {
            if (double.TryParse(Datos, out double currentValue))
            {
                _valores.Add(currentValue);

                for (int i = 0; i < _operadores.Count; i++)
                {
                    switch (_operadores[i])
                    {
                        case "+":
                            _valores[i + 1] += _valores[i];
                            break;
                        case "-":
                            _valores[i + 1] = _valores[i] - _valores[i + 1];
                            break;
                        case "X":
                            _valores[i + 1] *= _valores[i];
                            break;
                        case "÷":
                            if (_valores[i + 1] != 0)
                            {
                                _valores[i + 1] = _valores[i] / _valores[i + 1];
                            }
                            else
                            {
                                Datos = "Error";
                                return;
                            }
                            break;
                        case "%":
                            _valores[i + 1] = _valores[i] * (_valores[i + 1] / 100); 
                            break;
                    }
                }

                Datos = _valores.Last().ToString();
                _valores.Clear();
                _operadores.Clear();
            }
        }
        private async Task BorrarUnoPorUno()
        {
            if (Datos.Length > 1)
            {
                Datos = Datos.Substring(0, Datos.Length - 1);
            }
            else
            {
                Datos = "0";
            }
        }
        private async Task BorrarTodo()
        {
            Datos = "0";
            _valores.Clear();
            _operadores.Clear();
        }
     
        #endregion
        #region COMANDOS
        public ICommand ActualizarDatosCommand => new Command<string>(async (numero) => await ActualizarDatos(numero));
        public ICommand AplicarOperadorCommand => new Command<string>(async (operador) => await AplicarOperador(operador));
        public ICommand CalcularCommand => new Command(async () => await Calcular());
        public ICommand BorrarUnoPorUnoCommand => new Command(async () => await BorrarUnoPorUno());
        public ICommand BorrarTodoCommand => new Command(async () => await BorrarTodo());
        #endregion
    }
}

/*#region VARIABLES
        private string _displayText = "0";
        private double _currentValue = 0;
        private List<double> _values = new List<double>();
        private List<string> _operators = new List<string>();
        #endregion

        #region CONTRUCTOR
        public VMCalculadora(INavigation navigation)
        {
            Navigation = navigation;
            InitializeCommands();
        }
        #endregion*/

/*#region OBJETOS
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
        #endregion*/

        /*#region PROCESOS
        private void InitializeCommands()
        {
            NumberCommand = new Command<string>(UpdateDisplay);
            OperatorCommand = new Command<string>(ApplyOperator);
            EqualsCommand = new Command(PerformCalculation);
            DeleteCommand = new Command(DeleteLastDigit);
            ClearCommand = new Command(ClearAll);
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
                _values.Add(enteredValue);
                _operators.Add(operatorSymbol);
                DisplayText = "0";
            }
        }

        private void PerformCalculation()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                _values.Add(currentValue);

                for (int i = 0; i < _operators.Count; i++)
                {
                    switch (_operators[i])
                    {
                        case "+":
                            _values[i + 1] += _values[i];
                            break;
                        case "-":
                            _values[i + 1] = _values[i] - _values[i + 1];
                            break;
                        case "X":
                            _values[i + 1] *= _values[i];
                            break;
                        case "÷":
                            if (_values[i + 1] != 0)
                            {
                                _values[i + 1] = _values[i] / _values[i + 1];
                            }
                            else
                            {
                                DisplayText = "Error";
                                return;
                            }
                            break;
                    }
                }

                DisplayText = _values.Last().ToString();
                _values.Clear();
                _operators.Clear();
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

        private void ClearAll()
        {
            DisplayText = "0";
            _values.Clear();
            _operators.Clear();
        }
        #endregion

        #region COMANDOS
        public ICommand NumberCommand { get; private set; }
        public ICommand OperatorCommand { get; private set; }
        public ICommand EqualsCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        #endregion
        */

