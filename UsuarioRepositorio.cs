using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmpleadosApp.vistas.Usuario
{

    public class UsuarioRepositorio
    {
        static string webAPIKey = "AIzaSyDihpUb2_W0psEZ5DMIi99h4HBrfPZVUZA";
        FirebaseAuthProvider authProvider;

        public UsuarioRepositorio()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        public async Task<bool> RegistrarUsuario(string email, string nombre, string apellidoP, string apellidoM, string password)
        {

            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, $"{nombre} {apellidoP} {apellidoM}");

            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;


        }

        public async Task<string>IniciarSesion(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

            if (!(string.IsNullOrEmpty(token.FirebaseToken)))
            {
                return token.FirebaseToken;
            }

            return "";

        }


    }


}
