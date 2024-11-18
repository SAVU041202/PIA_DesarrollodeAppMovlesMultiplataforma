using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EmpleadosApp.vistas.Empleado;
using EmpleadosApp.vistas.Usuario;

namespace EmpleadosApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

         

            MainPage = new NavigationPage(new Login());

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
