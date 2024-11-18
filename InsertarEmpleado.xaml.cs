using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmpleadosApp.vistas.Empleado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsertarEmpleado : ContentPage
    {
        EmpleadoRepositorio repositorio = new EmpleadoRepositorio();
        public InsertarEmpleado()
        {
            InitializeComponent();
        }

        private void RegresarTBI_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }


        private async Task<bool> validarFormulario()
        {
            bool validarNombre = validacionesEmpleado.validarPalabrasConEspacios(txtNombre);
            bool validarApellidoP = validacionesEmpleado.validarApellidos(txtApellidoP);
            bool validarApellidoM = validacionesEmpleado.validarApellidos(txtApellidoM);
            bool validarEmail = validacionesEmpleado.validarEmail(txtEmail);
            bool validarTelefono = validacionesEmpleado.validarTelefono(txtTelefono);
            bool validarEdad = validacionesEmpleado.validarEdad(txtEdad);
            bool validarPuesto = validacionesEmpleado.validarPalabrasConEspacios(txtPuesto);
            bool validarDpto = validacionesEmpleado.validarPalabrasConEspacios(txtDpto);

            
            if (!validarNombre)
            {
                await this.DisplayAlert("Advertencia", "El campo del nombre es obligatorio y solo se pueden ingresar letras.", "Continuar");
                return false;
            }

            if (!validarApellidoP)
            {
                await this.DisplayAlert("Advertencia", "El campo del apellido paterno es obligatorio y solo se pueden ingresar letras.", "Continuar");
                return false;
            }

            if (!validarApellidoM)
            {
                await this.DisplayAlert("Advertencia", "El campo del apellido materno es obligatorio y solo se pueden ingresar letras.", "Continuar");
                return false;
            }

            if (!validarEmail)
            {
                await this.DisplayAlert("Advertencia", "El campo del Email es obligatorio. Ingrese un email con un formato adecuado (Por ejemplo, usuario123@gmail.com).", "Continuar");
                return false;
            }

            if (!validarTelefono)
            {
                await this.DisplayAlert("Advertencia", "El campo del teléfono es obligatorio, solo se aceptan 10 digítos.", "Continuar");
                return false;
            }

            if (!validarEdad)
            {
                await this.DisplayAlert("Advertencia", "El campo de la edad es obligatorio, solo se aceptan digítos en un rango de 18 a 65 años.", "Continuar");
                return false;
            }

            if (!validarPuesto)
            {
                await this.DisplayAlert("Advertencia", "El campo del puesto es obligatorio, solo se aceptan letras.", "Continuar");
                return false;
            }

            if (!validarDpto)
            {
                await this.DisplayAlert("Advertencia", "El campo del departamento es obligatorio, solo se aceptan letras.", "Continuar");
                return false;
            }

            return true;

        }


            private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (await validarFormulario())
            {

                EmpleadoModelo empleado = new EmpleadoModelo();

                empleado.nombreEmpleado = txtNombre.Text;
                empleado.apellidoPaterno = txtApellidoP.Text;
                empleado.apellidoMaterno = txtApellidoM.Text;
                empleado.email = txtEmail.Text;
                empleado.telefono = txtTelefono.Text;
                empleado.edad = int.Parse(txtEdad.Text);
                empleado.puesto = txtPuesto.Text;
                empleado.departamento = txtDpto.Text;

                var empleadoGuardado = await repositorio.GuardarEmpleado(empleado);

                if (empleadoGuardado)
                {
                    await DisplayAlert("Exito", $"La información se guardó exitosamente.", "OK");

                    txtNombre.Text = "";
                    txtApellidoP.Text = "";
                    txtApellidoM.Text = "";
                    txtEmail.Text = "";
                    txtTelefono.Text = "";
                    txtEdad.Text = "";
                    txtPuesto.Text = "";
                    txtDpto.Text = "";

                    await Navigation.PopModalAsync();

                }
                else
                {
                    await DisplayAlert("Error", $"La información no se guardó exitosamente.", "OK");
                }


            }
        }
    }
}
