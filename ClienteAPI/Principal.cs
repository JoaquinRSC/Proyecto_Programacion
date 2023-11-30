using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;

namespace ClienteAPI
{
    public partial class Principal : Form
    {
        public void GetResultado(JsonResponses.ResultadoLogin data)
        {
            lblUsername.Text = data.Username;
        }

        private List<JsonResponses.Task> deserializar(string content)
        {
            return JsonConvert.DeserializeObject<List<JsonResponses.Task>>(content);
        }


        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            DataTable tabla = obtenerDataTable();
            dataGridView1.DataSource = tabla;
        }

        private DataTable obtenerDataTable()
        {
            RestResponse response = pedirListaDAutoresEnAPI();

            DataTable tabla = new DataTable();
            tabla.Columns.Add("id", typeof(int));
            tabla.Columns.Add("title", typeof(string));
            tabla.Columns.Add("authorName", typeof(string));
            tabla.Columns.Add("body", typeof(string));
            tabla.Columns.Add("creationDate", typeof(string));
            tabla.Columns.Add("expirationDate", typeof(string));

            foreach (JsonResponses.Task task in deserializar(response.Content))
            {
                llenarDataTable(tabla, task);
            }

            return tabla;
        }

        private static void llenarDataTable(DataTable tabla, JsonResponses.Task task)
        {
            DataRow fila = tabla.NewRow();
            fila["id"] = task.id;
            fila["title"] = task.title;
            fila["authorName"] = task.authorName;
            fila["body"] = task.body;
            fila["creationDate"] = task.creationDate;
            fila["expirationDate"] = task.expirationDate;
            tabla.Rows.Add(fila);
        }

        private static RestResponse pedirListaDAutoresEnAPI()
        {
            RestClient client = new RestClient("http://localhost:49923");
            RestRequest request = new RestRequest("/api/task", Method.Get);
            request.AddHeader("Accept", "application/json");
            RestResponse response = client.Execute(request);
            return response;
        }
    }
}
