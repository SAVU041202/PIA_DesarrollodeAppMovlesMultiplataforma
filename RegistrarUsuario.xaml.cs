using EmpleadosApp.vistas.Empleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmpleadosApp.vistas.Usuario
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrarUsuario : ContentPage
	{

        private bool botonHabilitado = true;
        UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
		public RegistrarUsuario ()
		{
			InitializeComponent ();
		}

        private async Task<bool> validarFormulario()
        {
            bool validarNombre = validacionesEmpleado.validarPalabrasConEspacios(txtNombre);
            bool validarApellidoP = validacionesEmpleado.validarApellidos(txtApellidoP);
            bool validarApellidoM = validacionesEmpleado.validarApellidos(txtApellidoM);
            bool validarEmail = validacionesEmpleado.validarEmail(txtEmail);
            

            if (!validarNombre)
            {
                await this.DisplayAlert("Advertencia", "El campo del nombre es obligatorio y solo se pueden ingresar letras.", "Continuar");
                return false;
            }

            if (!validarApellidoP)
            {
                await this.DisplayAlert("Advertencia", "El campo del apellido paterno es obligatorio y solo se pueden ingresar letras sin espacios.", "Continuar");
                return false;
            }

            if (!validarApellidoM)
            {
                await this.DisplayAlert("Advertencia", "El campo del apellido materno es obligatorio y solo se pueden ingresar letras sin espacios.", "Continuar");
                return false;
            }

            if (!validarEmail)
            {
                await this.DisplayAlert("Advertencia", "El campo del Email es obligatorio. Ingrese un email con un formato adecuado (Por ejemplo, usuario123@gmail.com).", "Continuar");
                return false;
            }
            if ((string.IsNullOrWhiteSpace(txtPass.Text)))
            {
                await this.DisplayAlert("Advertencia", "El campo de la contraseña es obligatorio.", "Continuar");
                return false;
            }

            if ((string.IsNullOrWhiteSpace(txtPassConf.Text)))
            {
                await this.DisplayAlert("Advertencia", "Es necesario confirmar la contraseña.", "Continuar");
                return false;
            }

            string patronPass = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&._-])[A-Za-z\\d@$!%*?&._-]{8,}$";
            bool resultadoPass = Regex.IsMatch(txtPass.Text, patronPass, RegexOptions.IgnoreCase);
            bool resultadoPassConf = Regex.IsMatch(txtPassConf.Text, patronPass, RegexOptions.IgnoreCase);

            if (!resultadoPass)
            {
                await this.DisplayAlert("Advertencia", "La contraseña debe tener al menos 8 caracteres y puede contener letras (mayúsculas y minúsculas) dígitos y caracteres especiales (@, $, !, %, *, ?, &).", "Continuar");
                return false;
            }


            if (txtPass.Text != txtPassConf.Text)
            {
                await this.DisplayAlert("Advertencia", "Las contraseñas no coinciden", "Continuar");
                return false;
            }


            return true;

        }


        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await validarFormulario())
                {
                    bool nuevoUsuario = await _usuarioRepositorio.RegistrarUsuario(txtEmail.Text, txtNombre.Text, txtApellidoP.Text, txtApellidoM.Text, txtPass.Text);

                    if (nuevoUsuario)
                    {
                        await this.DisplayAlert("Aviso", $"El usuario {txtNombre.Text} {txtApellidoP.Text} {txtApellidoM.Text} ha sido registrado exitosamente", "Continuar");
                        await Navigation.PushAsync(new Login());
                    }
                    else
                    {
                        await this.DisplayAlert("Error", $"El usuario no ha sido registrado exitosamente", "Continuar");
                    }

                }
            }
            catch (Exception exception)
            {

                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Advertencia", $"Ya existe una cuenta asociada al Email ingresado.", "Continuar");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "Continuar");
                }
            }

        }

        private async void LoginTap_Tapped(object sender, EventArgs e)
        {
            if (botonHabilitado)
            {
                botonHabilitado = false;
                await Navigation.PopAsync();
            }
            
        }
    }
}
