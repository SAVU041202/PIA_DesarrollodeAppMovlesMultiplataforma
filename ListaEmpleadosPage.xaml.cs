using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database.Query;
using EmpleadosApp.vistas.Usuario;
using EmpleadosApp.vistas;
using System.Security.Principal;

namespace EmpleadosApp.vistas.Empleado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListaEmpleadosPage : ContentPage
    {
      

        private bool botonHabilitado = true;
        bool cerrarSesion = false;

        EmpleadoRepositorio empleadoRepositorio = new EmpleadoRepositorio();
        public ListaEmpleadosPage(string token)
        {
            InitializeComponent();
            EmpleadosListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });

            if (cerrarSesion)
            {
                token = null;
            }    

        }

        protected override bool OnBackButtonPressed()
        {
           
            return true;
            
        }

        protected override async void OnAppearing()
        {
            var listaEmpleados = await empleadoRepositorio.SeleccionarTodosLosEmpleados();
            EmpleadosListView.ItemsSource = null;
            EmpleadosListView.ItemsSource = listaEmpleados;

            EmpleadosListView.IsRefreshing= false;

            base.OnAppearing();
            botonHabilitado = true;

        }
        
        private async void AgregarTBI_Clicked(object sender, EventArgs e)
        {
            
            if (botonHabilitado)
            {
                botonHabilitado = false;
                await Navigation.PushModalAsync(new InsertarEmpleado());
            }

           
           
        }

        private void EmpleadosListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(e.Item == null)
            {
                return;
            }
            var empleado = e.Item as EmpleadoModelo;

            if (empleado != null)
            {
                if (botonHabilitado)
                {
                    botonHabilitado = false;
                    Navigation.PushModalAsync(new EmpleadoDetallesPage(empleado));
                    ((ListView)sender).SelectedItem = null;
                }
            }
            
        }

        private async void btnBorrar_Tapped(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Eliminar empleado", "¿Desea eliminar los datos del empleado seleccionado?", "Eliminar", "Cancelar");
            if (respuesta)
            {
                string idEmpleado = ((TappedEventArgs)e).Parameter.ToString();
                bool empleadoEliminado = await empleadoRepositorio.eliminarEmpleado(idEmpleado);

                if (empleadoEliminado)
                {
                    await DisplayAlert("Operación completada", "El empleado ha sido eliminado con éxito", "OK");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "El empleado no ha sido eliminado con éxito", "OK");
                }
            }

        }

        private async void btnEditar_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Eliminar empleado", "¿Desea eliminar al empleado seleccionado?", "Eliminar", "Cancelar");

            string idEmpleado = ((TappedEventArgs)e).Parameter.ToString();  

            var empleado = await empleadoRepositorio.SeleccionarEmpleadoPorId(idEmpleado);
             
            if (empleado == null)
            {
                await DisplayAlert("Advertencia", "No se pudo encontrar información.", "Continuar");
            }

            empleado.idEmpleado = idEmpleado;

            if (idEmpleado != null)
            {
                if (botonHabilitado)
                {
                    botonHabilitado= false;
                    await Navigation.PushModalAsync(new EditarEmpleado(empleado));
                }
            }

            
        }

        private async void TxtBuscar_SearchButtonPressed(object sender, EventArgs e)
        {
            string valorBuscado = txtBuscar.Text.Trim();
            if (!(string.IsNullOrEmpty(valorBuscado)))
            {
                var empleados = await empleadoRepositorio.BuscarEmpleadoPorNombre(valorBuscado);
                EmpleadosListView.ItemsSource = null;
                EmpleadosListView.ItemsSource = empleados;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string valorBuscado = txtBuscar.Text.Trim();
            if (!(string.IsNullOrEmpty(valorBuscado)))
            {
                var empleados = await empleadoRepositorio.BuscarEmpleadoPorNombre(valorBuscado);
                EmpleadosListView.ItemsSource = null;
                EmpleadosListView.ItemsSource = empleados;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void btnLogOut_Clicked(object sender, EventArgs e)
        {
            var logOut = await DisplayAlert("Salir", "¿Desea cerrar su sesión?", "Cerrar sesión", "Cancelar");
            

            if (logOut)
            {
                cerrarSesion = true;
                App.Current.MainPage = new NavigationPage(new Login());
                
            }
        }

    }
}
