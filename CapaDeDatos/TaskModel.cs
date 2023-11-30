using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using MD5Hash;


namespace CapaDeDatos
{
    public class TaskModel : Model
    {
        public string title;
        public string authorName;
        public string body;
        public string creationDate;
        public string expirationDate;

        public int id;

        public void Save()
        {
            this.Command.CommandText =
                $"INSERT INTO task(username,password) " +
                $"VALUES ('{this.title}','{this.authorName}','{this.body}','{this.creationDate}','{this.expirationDate}')";

            this.Command.ExecuteNonQuery();
        }

        public void Edit()
        {

            this.Command.CommandText =
                $"UPDATE task " +
                $"SET username = '{this.title}', " +
                $"    password = '{this.authorName}', " +
                $"    body = '{this.body}', " +
                $"    creationDate = '{this.creationDate}', " +
                $"    expirationDate = '{this.expirationDate}' " +
                $"WHERE id = {this.id}";

            this.Command.ExecuteNonQuery();
        }

        public List<TaskModel> Todos()
        {
            this.Command.CommandText = "SELECT * FROM task";
            this.Reader = this.Command.ExecuteReader();

            List<TaskModel> resultado = new List<TaskModel>();

            while (this.Reader.Read())
            {
                TaskModel elemento = new TaskModel();
                elemento.id = Int32.Parse(this.Reader["id"].ToString());
                elemento.title = this.Reader["title"].ToString();
                elemento.authorName = this.Reader["authorName"].ToString();
                elemento.body = this.Reader["body"].ToString();
                elemento.creationDate = this.Reader["creationDate"].ToString();
                elemento.expirationDate = this.Reader["expirationDate"].ToString();
                resultado.Add(elemento);
            }

            return resultado;
        }
        public List<TaskModel> Uno(int id)
        {
            this.Command.CommandText =
            $"SELECT * FROM task " +
            $"WHERE id = {id}";
            this.Reader = this.Command.ExecuteReader();

            List<TaskModel> resultado = new List<TaskModel>();

            while (this.Reader.Read())
            {
                TaskModel elemento = new TaskModel();
                elemento.id = Int32.Parse(this.Reader["id"].ToString());
                elemento.title = this.Reader["title"].ToString();
                elemento.authorName = this.Reader["authorName"].ToString();
                elemento.body = this.Reader["body"].ToString();
                elemento.creationDate = this.Reader["creationDate"].ToString();
                elemento.expirationDate = this.Reader["expirationDate"].ToString();

                resultado.Add(elemento);
            }

            return resultado;
        }

        public void Borrar()
        {
            this.Command.CommandText =
                 $"DELETE FROM task " +
                 $"WHERE id = {this.id}";

            this.Command.ExecuteNonQuery();
        }
    }
}