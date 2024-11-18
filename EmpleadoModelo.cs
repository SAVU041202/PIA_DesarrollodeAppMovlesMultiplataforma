using System;
using System.Collections.Generic;
using System.Text;

namespace EmpleadosApp
{
    public class EmpleadoModelo
    {
        public string idEmpleado { get; set; }
        public string nombreEmpleado { get;set; }
        public string apellidoPaterno { get; set;}
        public string apellidoMaterno { get; set;}
        public string email { get; set;}
        public string telefono { get; set;}
        public int edad { get;set;}

        public string puesto { get; set;}

        public string departamento { get; set;}
    }
}
