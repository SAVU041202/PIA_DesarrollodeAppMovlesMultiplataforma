using EmpleadosApp.vistas.Empleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace EmpleadosApp.vistas.Usuario
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        
        UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        private bool botonHabilitado = true;
        public Login ()
		{
			InitializeComponent ();
            // Ocultar botón de retroceso
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            botonHabilitado = true;
        }

        // Guardar el token
        


        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
			try
			{

                

                // Recuperar el token
               
                if (!(String.IsNullOrEmpty(txtEmail.Text)))
                   {
                     string patronEmail = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

                     bool emailValido = Regex.IsMatch(txtEmail.Text, patronEmail, RegexOptions.IgnoreCase);

                     if ((string.IsNullOrEmpty(txtPass.Text)))
                    {
                        await DisplayAlert("Acceso denegado", "La contraseña es un campo obligatorio.", "Intentar de nuevo");
                        return;
                    }

                     if (emailValido && !(string.IsNullOrEmpty(txtPass.Text)))
                        {
                        
                        await SecureStorage.SetAsync("Token", await _usuarioRepositorio.IniciarSesion(txtEmail.Text, txtPass.Text));
                        string tokenGuardado = await SecureStorage.GetAsync("Token");


                        if (!string.IsNullOrEmpty(tokenGuardado))
                        {
                            //App.Current.MainPage = new NavigationPage(new ListaEmpleadosPage());

                            if (botonHabilitado)
                            {
                                botonHabilitado = false;
                                await Navigation.PushAsync(new ListaEmpleadosPage(tokenGuardado));
                            }
                          
                            
                        }
                        else
                        {
                            await DisplayAlert("Acceso denegado", "No se pudo iniciar la sesión", "Intentar de nuevo");
                            return;
                        }
                    }
                    if (!emailValido)
                    {
                        await this.DisplayAlert("Advertencia", "Ingrese un email con un formato adecuado (Por ejemplo, usuario123@gmail.com).", "Continuar");
                        return;
                    }
                    }
                else
                {
                    await DisplayAlert("Acceso denegado", "El email es un campo obligatorio.", "Intentar de nuevo");
                    return;
                }

 
            }
			catch (Exception excepcion)
			{
                if (excepcion.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("Acceso denegado", "No existe una cuenta asociada al email ingresado.", "Intentar de nuevo");
                }

                else if (excepcion.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("Acceso denegado", "La contraseña es incorrecta.", "Intentar de nuevo");
                }

                else
                {
                    await DisplayAlert("Error", excepcion.Message, "Intentar de nuevo");
                }


            }
 
        }

        private async void RegistroTap_Tapped(object sender, EventArgs e)
        {
            if (botonHabilitado)
            {
                botonHabilitado = false;
                await Navigation.PushAsync(new RegistrarUsuario());
            }
            
        }

    }
}
