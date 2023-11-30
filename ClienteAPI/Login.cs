using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using RestSharp;



namespace ClienteAPI
{

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private JsonResponses.ResultadoLogin deserializar(string content)
        {
            return JsonConvert.DeserializeObject<JsonResponses.ResultadoLogin>(content);
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            RestResponse response = AutenticarConAPI();
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                MessageBox.Show("Login Invalido");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JsonResponses.ResultadoLogin resultado = this.deserializar(response.Content);
                Principal formPrincipal = new Principal();
                formPrincipal.GetResultado(resultado);
                formPrincipal.Show();
            }
        }

        private RestResponse AutenticarConAPI()
        {
            Dictionary<string, string> requestBody = new Dictionary<string, string>(){
                { "username", txtUsername.Text },
                { "password", txtPassword.Text }
            };
            string requestBodyString = JsonConvert.SerializeObject(requestBody);

            RestClient client = new RestClient("http://localhost:49923");

            RestRequest request = new RestRequest("/api/login", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(requestBodyString);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            RestResponse response = client.Execute(request);
            return response;
        }
    }
}
