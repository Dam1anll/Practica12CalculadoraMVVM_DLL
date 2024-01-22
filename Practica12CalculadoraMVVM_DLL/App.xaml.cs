using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Practica12CalculadoraMVVM_DLL.View;

namespace Practica12CalculadoraMVVM_DLL
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Calculadora();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
