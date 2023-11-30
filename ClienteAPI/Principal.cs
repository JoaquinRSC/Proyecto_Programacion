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

            // Agregar la columna de botones para eliminar
            DataGridViewButtonColumn eliminarButtonColumn = new DataGridViewButtonColumn();
            eliminarButtonColumn.HeaderText = "Eliminar";
            eliminarButtonColumn.Text = "Eliminar";
            eliminarButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(eliminarButtonColumn);

            DataGridViewButtonColumn editarButtonColumn = new DataGridViewButtonColumn();
            editarButtonColumn.HeaderText = "Editar";
            editarButtonColumn.Text = "Editar";
            editarButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editarButtonColumn);

            dataGridView1.DataSource = tabla;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                 if (e.ColumnIndex == dataGridView1.Columns[0].Index)
                 {
                     if (row != null && row.Cells["id"] != null && row.Cells["id"].Value != null)
                     {
                         int idTareaSeleccionada = Convert.ToInt32(row.Cells["id"].Value);
                         EliminarTarea(idTareaSeleccionada);

                         DataTable tablaActualizada = obtenerDataTable();
                         dataGridView1.DataSource = tablaActualizada;
                        MessageBox.Show("Tarea eliminada correctamente");
                    }
                 }

                if (e.ColumnIndex == dataGridView1.Columns[1].Index)
                {
                if (row != null && row.Cells["id"] != null && row.Cells["id"].Value != null)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                    string title = dataGridView1.Rows[e.RowIndex].Cells["title"].Value.ToString();
                    string authorName = dataGridView1.Rows[e.RowIndex].Cells["authorName"].Value.ToString();
                    string body = dataGridView1.Rows[e.RowIndex].Cells["body"].Value.ToString();
                    string creationDate = dataGridView1.Rows[e.RowIndex].Cells["creationDate"].Value.ToString();
                    string expirationDate = dataGridView1.Rows[e.RowIndex].Cells["expirationDate"].Value.ToString();

                    MostrarFormularioEdicion(id, title, authorName, body, creationDate, expirationDate);

                    DataTable tablaActualizada = obtenerDataTable();
                    dataGridView1.DataSource = tablaActualizada;
                }
                }
            }
        }

        private void EliminarTarea(int idTarea)
        {
            RestClient client = new RestClient("http://localhost:49923");
            RestRequest request = new RestRequest($"/api/task/{idTarea}", Method.Delete);
            request.AddHeader("Accept", "application/json");

            RestResponse response = client.Execute(request);
        }

        private void MostrarFormularioEdicion(int id, string title, string authorName, string body, string creationDate, string expirationDate)
        {
            Form formEditarTarea = new Form();
            TextBox txtTitle = new TextBox();
            TextBox txtAuthorName = new TextBox();
            TextBox txtBody = new TextBox();
            TextBox txtCreationDate = new TextBox();
            TextBox txtExpirationDate = new TextBox();
            Button btnEditar = new Button();

            txtTitle.Text = title;
            txtAuthorName.Text = authorName;
            txtBody.Text = body;
            txtCreationDate.Text = creationDate;
            txtExpirationDate.Text = expirationDate;

            formEditarTarea.Text = "Editar Tarea";
            formEditarTarea.Size = new System.Drawing.Size(300, 250);

            int textBoxWidth = 200;
            int textBoxHeight = 20;
            int textBoxSpacing = 30;

            txtTitle.Location = new System.Drawing.Point(50, 20);
            txtTitle.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            txtAuthorName.Location = new System.Drawing.Point(50, 20 + textBoxSpacing);
            txtAuthorName.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            txtBody.Location = new System.Drawing.Point(50, 20 + 2 * textBoxSpacing);
            txtBody.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            txtCreationDate.Location = new System.Drawing.Point(50, 20 + 3 * textBoxSpacing);
            txtCreationDate.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            txtExpirationDate.Location = new System.Drawing.Point(50, 20 + 4 * textBoxSpacing);
            txtExpirationDate.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            btnEditar.Text = "Editar";
            btnEditar.Size = new System.Drawing.Size(80, 30);
            btnEditar.Location = new System.Drawing.Point(100, 20 + 5 * textBoxSpacing);;

            formEditarTarea.Controls.Add(txtTitle);
            formEditarTarea.Controls.Add(txtAuthorName);
            formEditarTarea.Controls.Add(txtBody);
            formEditarTarea.Controls.Add(txtCreationDate);
            formEditarTarea.Controls.Add(txtExpirationDate);
            formEditarTarea.Controls.Add(btnEditar);

            btnEditar.Click += async (sender, e) =>
            {
                var datosActualizados = new
                {
                    id = id,
                    title = txtTitle.Text,
                    authorName = txtAuthorName.Text,
                    body = txtBody.Text,
                    creationDate = txtCreationDate.Text,
                    expirationDate = txtExpirationDate.Text
                };

                RestClient client = new RestClient("http://localhost:49923");
                RestRequest request = new RestRequest($"/api/task", Method.Put);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(datosActualizados);

                formEditarTarea.Close();

                RestResponse response = await client.ExecuteAsync(request);
                MessageBox.Show("Tarea editada correctamente");
            };

            // Mostrar el formulario
            formEditarTarea.ShowDialog();
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

        private void button_agregar_tarea_Click(object sender, EventArgs e)
        {
            MostrarFormularioAgregar();
        }

        private void MostrarFormularioAgregar()
        {
            Form formAgregarTarea = new Form();
            TextBox txtTitle = new TextBox();
            TextBox txtAuthorName = new TextBox();
            TextBox txtBody = new TextBox();
            TextBox txtCreationDate = new TextBox();
            TextBox txtExpirationDate = new TextBox();
            Button btnAgregar = new Button();

            formAgregarTarea.Text = "Agregar Nueva Tarea";
            formAgregarTarea.Size = new System.Drawing.Size(300, 250);

            txtTitle.Text = "Título";
            txtAuthorName.Text = "Nombre del Autor";
            txtBody.Text = "Cuerpo";
            txtCreationDate.Text = "Fecha de Creación";
            txtExpirationDate.Text = "Fecha de Vencimiento";

            int textBoxWidth = 200;
            int textBoxHeight = 20;
            int textBoxSpacing = 30;

            txtTitle.Location = new System.Drawing.Point(50, 20);
            txtTitle.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            txtAuthorName.Location = new System.Drawing.Point(50, 20 + textBoxSpacing);
            txtAuthorName.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            txtBody.Location = new System.Drawing.Point(50, 20 + 2 * textBoxSpacing);
            txtBody.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            txtCreationDate.Location = new System.Drawing.Point(50, 20 + 3 * textBoxSpacing);
            txtCreationDate.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            txtExpirationDate.Location = new System.Drawing.Point(50, 20 + 4 * textBoxSpacing);
            txtExpirationDate.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);

            btnAgregar.Text = "Agregar";
            btnAgregar.Size = new System.Drawing.Size(80, 30);
            btnAgregar.Location = new System.Drawing.Point(100, 20 + 5 * textBoxSpacing); ;

            formAgregarTarea.Controls.Add(txtTitle);
            formAgregarTarea.Controls.Add(txtAuthorName);
            formAgregarTarea.Controls.Add(txtBody);
            formAgregarTarea.Controls.Add(txtCreationDate);
            formAgregarTarea.Controls.Add(txtExpirationDate);
            formAgregarTarea.Controls.Add(btnAgregar);

            btnAgregar.Click += async (sender, e) =>
            {
                if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtAuthorName.Text) || string.IsNullOrEmpty(txtBody.Text) || string.IsNullOrEmpty(txtCreationDate.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios, excepto Expiration Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var nuevaTarea = new
                {
                    title = txtTitle.Text,
                    authorName = txtAuthorName.Text,
                    body = txtBody.Text,
                    creationDate = txtCreationDate.Text,
                    expirationDate = txtExpirationDate.Text
                };

                RestClient client = new RestClient("http://localhost:49923");
                RestRequest request = new RestRequest($"/api/task", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(nuevaTarea);

                formAgregarTarea.Close();

                RestResponse response = await client.ExecuteAsync(request);
                MessageBox.Show("Nueva tarea agregada correctamente");
            };

            // Mostrar el formulario
            formAgregarTarea.ShowDialog();
        }

    }
}
