using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeDatos;
using MD5Hash;

namespace CapaLogica
{
    public static class Auth
    {
        public static Dictionary<string,string> Login(string username, string password)
        {
            Dictionary<string, string> resultado = new Dictionary<string, string>();
            UserModel user = new UserModel();
            user.Username = username;
            
           
            if (user.Get() && Hash.Content(password) == user.Password)
            {
                resultado.Add("Id", user.Id.ToString());
                resultado.Add("Username", user.Username);
                resultado.Add("resultado", "OK");
                return resultado;
            }

            resultado.Add("resultado", "Credenciales invalidas");
            return resultado;
        }
    }
}
