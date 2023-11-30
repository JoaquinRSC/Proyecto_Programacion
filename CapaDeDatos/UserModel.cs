using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using MD5Hash;


namespace CapaDeDatos
{
    public class UserModel : Model
    {

        public string Username;
        public string Password;

        public int Id;

        public void Save()
        {
            // Para la tabla 'user'
            this.Command.CommandText = @"
                CREATE TABLE IF NOT EXISTS user (
                    id INT NOT NULL AUTO_INCREMENT,
                    username VARCHAR(255),
                    password VARCHAR(255),
                    PRIMARY KEY (id)
                )";

                this.Command.ExecuteNonQuery();

            // Para la tabla 'task'
            this.Command.CommandText = @"
                CREATE TABLE IF NOT EXISTS task (
                    id INT AUTO_INCREMENT,
                    title VARCHAR(255),
                    authorName VARCHAR(255),
                    body VARCHAR(255),
                    creationDate VARCHAR(255),
                    expirationDate VARCHAR(255) NULL,
                    PRIMARY KEY (id)
                )";

            this.Command.ExecuteNonQuery();

            // Ejecutar el comando SQL
            this.Command.ExecuteNonQuery();
            this.Command.CommandText =
                $"INSERT INTO user(username,password) " +
                $"VALUES ('{this.Username}','{Hash.Content(this.Password)}')";

            this.Command.ExecuteNonQuery();
        }

        public bool Get()
        {

            Dictionary<string, string> resultado = new Dictionary<string, string>();

            this.Command.CommandText = $"SELECT id, username, password " +
                $"From user where username = '{this.Username}'";
            this.Reader = this.Command.ExecuteReader();

            if (this.Reader.HasRows)
            {
                this.Reader.Read();
                this.Id = Int32.Parse(this.Reader["id"].ToString());
                this.Username = this.Reader["username"].ToString();
                this.Password = this.Reader["password"].ToString();

                return true;
            }

            return false;

        }

        public List<UserModel> Todos()
        {
            this.Command.CommandText = "SELECT * FROM user";
            this.Reader = this.Command.ExecuteReader();

            List<UserModel> resultado = new List<UserModel>();

            while (this.Reader.Read())
            {
                UserModel elemento = new UserModel();
                elemento.Id = Int32.Parse(this.Reader["Id"].ToString());
                elemento.Username = this.Reader["username"].ToString();
                resultado.Add(elemento);

            }

            return resultado;
        }

    }

}
