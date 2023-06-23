using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SimpleExample
{
    public partial class FormTeste : Form
    {
        private GroundStation groundStation;
        public static string GetBodyContent(string html)
        {
            // Utiliza expressão regular para obter o conteúdo dentro do body
            string pattern = @"<body\b[^>]*>(.*?)</body>";
            string bodyContent = Regex.Match(html, pattern, RegexOptions.Singleline).Groups[1].Value;

            return bodyContent;
        }
        public FormTeste(GroundStation groundStation)
        {
            InitializeComponent();
            this.groundStation = groundStation;

            // Chame um método para fazer as requisições HTTP
            FetchData();
        }

        private async Task FetchData()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // /gps, /temperature, /instrumentation, /
                    // Faça uma requisição GET para obter as informações
                    HttpResponseMessage response = await client.GetAsync("http://localhost:8000/");

                    if (response.IsSuccessStatusCode)
                    {
                        HttpResponseMessage gps = await client.GetAsync("http://localhost:8000/gps.html");
                        HttpResponseMessage temperature = await client.GetAsync("http://localhost:8000/temperature.html");
                        HttpResponseMessage instrumentation = await client.GetAsync("http://localhost:8000/instrumentation.html");

                        // Converta o conteúdo da resposta para uma string
                        string gps_html = await gps.Content.ReadAsStringAsync();
                        string temperature_html = await temperature.Content.ReadAsStringAsync();
                        string instrumentation_html = await instrumentation.Content.ReadAsStringAsync();

                        string gps_data = GetBodyContent(gps_html);
                        string temperature_data = GetBodyContent(temperature_html);
                        string instrumentation_data = GetBodyContent(instrumentation_html);

                        // Faça algo com os dados obtidos, por exemplo, exiba-os em um controle do formulário
                        label1.Text = gps_data;
                        label2.Text = temperature_data;
                        label3.Text = instrumentation_data;

                    }
                    else
                    {
                        // A requisição falhou, faça o tratamento apropriado
                        MessageBox.Show("A requisição falhou: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate exceções caso ocorram
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dados";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dados";
            this.label2.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(488, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dados";
            // 
            // FormTeste
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(921, 441);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTeste";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label label2;
        private Label label3;
        private System.Windows.Forms.Label label1;
    }
}
