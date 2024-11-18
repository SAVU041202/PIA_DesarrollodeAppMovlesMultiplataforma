using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmpleadosApp.vistas.Empleado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmpleadoDetallesPage : ContentPage
	{
        public EmpleadoDetallesPage(EmpleadoModelo empleado)
        {
            InitializeComponent();


            lblNombre2.Text = empleado.nombreEmpleado;
            lblApellidos2.Text = $"{empleado.apellidoPaterno} {empleado.apellidoMaterno}";
            lblEmail2.Text = empleado.email;
            lblTel2.Text = empleado.telefono;
            lblEdad2.Text = $"{empleado.edad} años";
            lblPuesto2.Text = empleado.puesto;
            lblDpto2.Text = empleado.departamento;
            
        }

        private void RegresarTBI_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
