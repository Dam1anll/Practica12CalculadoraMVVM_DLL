using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica12CalculadoraMVVM_DLL.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calculadora : ContentPage
    {
        private string currentNumber = string.Empty;
        private string leftOperand = string.Empty;
        private string rightOperand = string.Empty;
        private string currentOperator = string.Empty;

        public Calculadora()
        {
            InitializeComponent();
        }

        private void NumeroButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            currentNumber += button.Text;
            Display.Text = currentNumber;
        }

        private void OperadorButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!string.IsNullOrEmpty(currentNumber))
            {
                if (!string.IsNullOrEmpty(leftOperand) && !string.IsNullOrEmpty(currentOperator))
                {
                    rightOperand = currentNumber;
                    double left = double.Parse(leftOperand);
                    double right = double.Parse(rightOperand);
                    double result = 0;

                    switch (currentOperator)
                    {
                        case "+":
                            result = left + right;
                            break;
                        case "-":
                            result = left - right;
                            break;
                        case "*":
                            result = left * right;
                            break;
                        case "/":
                            if (right != 0)
                                result = left / right;
                            else
                                Display.Text = "Error";
                            break;
                    }

                    Display.Text = result.ToString();
                    currentNumber = result.ToString();
                    leftOperand = result.ToString();
                }
                else
                {
                    leftOperand = currentNumber;
                }

                currentNumber = string.Empty;
                currentOperator = button.Text;
            }
        }


        private void IgualButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentNumber) && !string.IsNullOrEmpty(leftOperand))
            {
                rightOperand = currentNumber;
                double left = double.Parse(leftOperand);
                double right = double.Parse(rightOperand);
                double result = 0;

                switch (currentOperator)
                {
                    case "+":
                        result = left + right;
                        break;
                    case "-":
                        result = left - right;
                        break;
                    case "*":
                        result = left * right;
                        break;
                    case "/":
                        if (right != 0)
                            result = left / right;
                        else
                            Display.Text = "Error";
                        break;
                }

                Display.Text = result.ToString();
                currentNumber = result.ToString();
                leftOperand = string.Empty;
                rightOperand = string.Empty;
                currentOperator = string.Empty;
            }
        }

        private void LimpiarButton(object sender, EventArgs e)
        {
            currentNumber = string.Empty;
            leftOperand = string.Empty;
            rightOperand = string.Empty;
            currentOperator = string.Empty;
            Display.Text = "0";
        }

        void BorrarButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentNumber))
            {
                currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);

                if (string.IsNullOrEmpty(currentNumber))
                {
                    Display.Text = "0";
                }
                else
                {
                    Display.Text = currentNumber;
                }
            }
        }
    }
}