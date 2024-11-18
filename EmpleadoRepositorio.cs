using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmpleadosApp.vistas.Empleado;
using System.Linq;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace EmpleadosApp
{
    public class EmpleadoRepositorio
    {
        FirebaseClient firebaseCliente = new FirebaseClient("https://empleadosfirebase-10f03-default-rtdb.firebaseio.com/");

        public async Task<bool> GuardarEmpleado(EmpleadoModelo empleado)
        {
            var datosEmpleado = await firebaseCliente.Child(nameof(EmpleadoModelo)).PostAsync(JsonConvert.SerializeObject(empleado));

            if(!(string.IsNullOrEmpty(datosEmpleado.Key)))
            {
                return true;
            }

            return false;
        }
        public async Task<List<EmpleadoModelo>> SeleccionarTodosLosEmpleados()
        {
            return (await firebaseCliente.Child(nameof(EmpleadoModelo)).OnceAsync<EmpleadoModelo>()).Select(item => new EmpleadoModelo
            {
                idEmpleado = item.Key,
                nombreEmpleado = item.Object.nombreEmpleado,
                apellidoPaterno = item.Object.apellidoPaterno,
                apellidoMaterno = item.Object.apellidoMaterno,
                email = item.Object.email,
                telefono = item.Object.telefono,
                edad = item.Object.edad,
                puesto = item.Object.puesto,
                departamento = item.Object.departamento


            }).ToList();
        }

        public async Task<EmpleadoModelo> SeleccionarEmpleadoPorId(string idEmpleado)
        {
            return (await firebaseCliente.Child(nameof(EmpleadoModelo)+"/"+idEmpleado).OnceSingleAsync<EmpleadoModelo>());
        }

        public async Task<bool> actualizarEmpleado(EmpleadoModelo empleado)
        {
            var a = nameof(EmpleadoModelo) + "/" + empleado.idEmpleado;
            await firebaseCliente.Child(a).PutAsync(JsonConvert.SerializeObject(empleado));
            return true;
        }


        public async Task<bool> eliminarEmpleado(string idEmpleado)
        {
            await firebaseCliente.Child(nameof(EmpleadoModelo) + "/" + idEmpleado).DeleteAsync();
            return true;
        }

        public async Task<List<EmpleadoModelo>> BuscarEmpleadoPorNombre(string name)
        {
            return (await firebaseCliente.Child(nameof(EmpleadoModelo)).OnceAsync<EmpleadoModelo>()).Select(item => new EmpleadoModelo
            {
                idEmpleado = item.Key,
                nombreEmpleado = item.Object.nombreEmpleado,
                apellidoPaterno = item.Object.apellidoPaterno,
                apellidoMaterno = item.Object.apellidoMaterno,
                email = item.Object.email,
                telefono = item.Object.telefono,
                edad = item.Object.edad,
                puesto= item.Object.puesto,
                departamento = item.Object.departamento

            }).Where(c => c.nombreEmpleado.ToLower().Contains(name.ToLower())).ToList();
        }

    }
}
