using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleExample
{
    public partial class FormTeste : Form
    {
        private GroundStation groundStation;

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
                    // Faça uma requisição GET para obter as informações
                    HttpResponseMessage response = await client.GetAsync("http://localhost:3000/api/data");
                    
                    if (response.IsSuccessStatusCode)
                    {
                        // Converta o conteúdo da resposta para uma string
                        string content = await response.Content.ReadAsStringAsync();

                        // Faça algo com os dados obtidos, por exemplo, exiba-os em um controle do formulário
                        label1.Text = content;
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dados";
            // 
            // FormTeste
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(921, 441);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTeste";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
    }
}
