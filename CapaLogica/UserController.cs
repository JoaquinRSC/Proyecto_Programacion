using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeDatos;
using System.Data;


namespace CapaLogica
{
    public static class UserController
    {
        public static void Crear(string username, string password)
        {
            UserModel user = new UserModel();
            user.Username = username;
            user.Password = password;
            user.Save();
        }

        public static Dictionary<string,string> Login(string username, string password)
        {
            Dictionary<string, string> resultado = new Dictionary<string, string>();
            resultado = Auth.Login(username, password);
            return resultado;
        }

        public static List<Dictionary<string,string>> ObtenerTodos()
        {
            UserModel user = new UserModel();
            List<UserModel> users = user.Todos();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (UserModel u in users)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", u.Id.ToString());
                elemento.Add("Username",u.Username);

                resultado.Add(elemento);
            }
            return resultado;


        }
    }
}
