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

        private List<JsonResponses.User> deserializar(string content)
        {
            return JsonConvert.DeserializeObject<List<JsonResponses.User>>(content);
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
            RestResponse response = pedirListaDeUsuariosEnAPI();

            DataTable tabla = new DataTable();
            tabla.Columns.Add("Id", typeof(int));
            tabla.Columns.Add("Username", typeof(string));

            foreach (JsonResponses.User user in deserializar(response.Content))
            {
                llenarDataTable(tabla, user);
            }

            return tabla;
        }

        private static void llenarDataTable(DataTable tabla, JsonResponses.User user)
        {
            DataRow fila = tabla.NewRow();
            fila["Id"] = user.Id;
            fila["Username"] = user.Username;
            tabla.Rows.Add(fila);
        }

        private static RestResponse pedirListaDeUsuariosEnAPI()
        {
            RestClient client = new RestClient("http://localhost:49923");
            RestRequest request = new RestRequest("/api/user", Method.Get);
            request.AddHeader("Accept", "application/json");
            RestResponse response = client.Execute(request);
            return response;
        }
    }
}
