using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeDatos;
using System.Data;


namespace CapaLogica
{
    public static class TaskController
    {
        public static void Crear(string title, string authorName, string body, string creationDate, string expirationDate)
        {
            TaskModel task = new TaskModel();
            task.title = title;
            task.authorName = authorName;
            task.body = body;
            task.creationDate = creationDate;
            task.expirationDate = expirationDate;

           task.Save();
        }

        public static void Editar(int id, string title, string authorName, string body, string creationDate, string expirationDate)
        {
            TaskModel task = new TaskModel();
            task.id = id;
            task.title = title;
            task.authorName = authorName;
            task.body = body;
            task.creationDate = creationDate;
            task.expirationDate = expirationDate;

            task.Edit();
        }

        public static List<Dictionary<string,string>> ObtenerTodos()
        {
            TaskModel task = new TaskModel();
            List<TaskModel> tasks = task.Todos();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (TaskModel t in tasks)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("id", t.id.ToString());
                elemento.Add("title", t.title);
                elemento.Add("authorName", t.authorName);
                elemento.Add("body", t.body);
                elemento.Add("creationDate", t.creationDate);
                elemento.Add("expirationDate", t.expirationDate);

                resultado.Add(elemento);
            }
            return resultado;
        }

        public static List<Dictionary<string, string>> ObtenerUno(int id)
        {
            TaskModel task = new TaskModel();
            List<TaskModel> tasks = task.Uno(id);

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (TaskModel t in tasks)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("id", t.id.ToString());
                elemento.Add("title", t.title);
                elemento.Add("authorName", t.authorName);
                elemento.Add("body", t.body);
                elemento.Add("creationDate", t.creationDate);
                elemento.Add("expirationDate", t.expirationDate);

                resultado.Add(elemento);
            }
            return resultado;
        }

        public static void Borrar(int id)
        {
            TaskModel task = new TaskModel();
            task.id = id;
            task.Borrar();
        }
    }
}
