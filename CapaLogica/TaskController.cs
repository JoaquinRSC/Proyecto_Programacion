using System;
using System.Collections.Generic;
using CapaDeDatos;
using Entities;
using System.Globalization;

namespace CapaLogica
{
    public static class TaskController
    {
        public static void Crear(TaskEntity taskToSave)
        {

            DateTime creationDate = DateTime.ParseExact(taskToSave.creationDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string creationDateMysqlFormat = creationDate.ToString("yyyy-MM-dd HH:mm:ss");

            string expirationDateMysqlFormat = DateTime.Now.ToString();
            if (!string.IsNullOrEmpty(taskToSave.expirationDate))
            {
                DateTime expirationDate = DateTime.ParseExact(taskToSave.expirationDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                expirationDateMysqlFormat = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");
            }

            TaskModel task = new TaskModel();
            task.title = taskToSave.title;
            task.authorName = taskToSave.authorName;
            task.body = taskToSave.body;
            task.creationDate = creationDateMysqlFormat;
            task.expirationDate = expirationDateMysqlFormat;

            task.Save();
        }

        public static void Editar(TaskEntity taskToEdit)
        {
            DateTime creationDate = DateTime.ParseExact(taskToEdit.creationDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string creationDateMysqlFormat = creationDate.ToString("yyyy-MM-dd HH:mm:ss");

            string expirationDateMysqlFormat = DateTime.Now.ToString();
            if (!string.IsNullOrEmpty(taskToEdit.expirationDate))
            {
                DateTime expirationDate = DateTime.ParseExact(taskToEdit.expirationDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                expirationDateMysqlFormat = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
         

            TaskModel task = new TaskModel();
            task.id = taskToEdit.id;
            task.title = taskToEdit.title;
            task.authorName = taskToEdit.authorName;
            task.body = taskToEdit.body;
            task.creationDate = creationDateMysqlFormat;
            task.expirationDate = expirationDateMysqlFormat;

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
