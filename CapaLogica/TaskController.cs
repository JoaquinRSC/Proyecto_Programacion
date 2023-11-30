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
            TaskModel task = new TaskModel();
            task.title = taskToSave.title;
            task.authorName = taskToSave.authorName;
            task.body = taskToSave.body;
            task.creationDate = taskToSave.creationDate;
            task.expirationDate = taskToSave.expirationDate;

            task.Save();
        }

        public static void Editar(TaskEntity taskToEdit)
        {
            TaskModel task = new TaskModel();
            task.id = taskToEdit.id;
            task.title = taskToEdit.title;
            task.authorName = taskToEdit.authorName;
            task.body = taskToEdit.body;
            task.creationDate = taskToEdit.creationDate;
            task.expirationDate = taskToEdit.expirationDate;

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
