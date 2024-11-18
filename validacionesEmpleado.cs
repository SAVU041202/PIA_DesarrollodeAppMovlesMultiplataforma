using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace EmpleadosApp.vistas.Empleado
{
    public class validacionesEmpleado
    {
        public static bool validarPalabrasConEspacios(Entry cajaTexto) 
        {
            bool respuesta = false;

            if (!(String.IsNullOrEmpty(cajaTexto.Text)))
            {
                cajaTexto.Text = cajaTexto.Text.Trim();
                string patronPalabrasConEspacios = "^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+(\\s[a-zA-ZáéíóúÁÉÍÓÚñÑ]+)*$";

                bool resultadoFuncion = Regex.IsMatch(cajaTexto.Text, patronPalabrasConEspacios, RegexOptions.IgnoreCase);

                if (resultadoFuncion)
                {
                    respuesta= true;
                }
            }

            else
            {
                respuesta= false;
            }

            return respuesta;

        }

        public static bool validarApellidos(Entry cajaTexto)
        {
            bool respuesta = false;

            if (!(String.IsNullOrEmpty(cajaTexto.Text)))
            {
                cajaTexto.Text = cajaTexto.Text.Trim();
                string patronApellidos = "^[a-zA-ZñÑáéíóúÁÉÍÓÚ\\s'-]+$";

                bool resultadoFuncion = Regex.IsMatch(cajaTexto.Text, patronApellidos, RegexOptions.IgnoreCase);

                if (resultadoFuncion)
                {
                    respuesta = true;
                }
            }

            else
            {
                respuesta = false;
            }

            return respuesta;
        }

        public static bool validarNombres(Entry cajaTexto)
        {
            bool respuesta = false;

            if (!(String.IsNullOrEmpty(cajaTexto.Text)))
            {
                cajaTexto.Text = cajaTexto.Text.Trim();
                string patronSinEspacio = "^(?:\\b[a-zA-ZáéíóúÁÉÍÓÚüÜ]+\\b)(?:\\s\\b[a-zA-ZáéíóúÁÉÍÓÚüÜ]+\\b){1,2}$";

                bool resultadoFuncion = Regex.IsMatch(cajaTexto.Text, patronSinEspacio, RegexOptions.IgnoreCase);

                if (resultadoFuncion)
                {
                    respuesta = true;
                }
                else
                {
                    respuesta = false;
                }

            }
            else
            {
                respuesta = true;
            }

            return respuesta;
        }

        public static bool validarEmail(Entry cajaTexto)
        {
            bool respuesta = false;

            if (!(String.IsNullOrEmpty(cajaTexto.Text)))
            {
                cajaTexto.Text = cajaTexto.Text.Trim();
                string patronEmail = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

                bool resultadoFuncion = Regex.IsMatch(cajaTexto.Text, patronEmail, RegexOptions.IgnoreCase);

                if (resultadoFuncion)
                {
                    respuesta = true;
                }
            }

            else
            {
                respuesta = false;
            }

            return respuesta;
        }

        public static bool validarTelefono(Entry cajaTexto)
        {
            if (String.IsNullOrWhiteSpace(cajaTexto.Text))
            {
                return false;
            }
            //Valida si la cantidad de digitos ingresados es menor a 10
            else if (cajaTexto.Text.Length != 10)
            {
                return false;
            }
            else
            {
                //Valida que solo se ingresen numeros
                if (!cajaTexto.Text.ToCharArray().All(Char.IsDigit))
                {

                    return false;
                }
            }

            return true;
        }

        public static bool validarEdad(Entry cajaTexto)
        {
            bool respuesta = false;

            if (!(String.IsNullOrWhiteSpace(cajaTexto.Text)))
            {
                string patronEdad = "^(1[8-9]|[2-5][0-9]|6[0-5])$";

                bool resultadoFuncion = Regex.IsMatch(cajaTexto.Text, patronEdad, RegexOptions.IgnoreCase);

                if (resultadoFuncion)
                {
                    respuesta = true;
                }

            }

            else
            {
                respuesta = false;
            }

            return respuesta;
        }

    }
}
