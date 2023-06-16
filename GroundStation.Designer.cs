namespace SimpleExample
{
    partial class GroundStation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroundStation));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.panelLateral = new System.Windows.Forms.Panel();
            this.buttonPixhawk = new System.Windows.Forms.Button();
            this.buttonGraficos = new System.Windows.Forms.Button();
            this.buttonDados = new System.Windows.Forms.Button();
            this.buttonMapa = new System.Windows.Forms.Button();
            this.panelNav = new System.Windows.Forms.Panel();
            this.buttonConfigurações = new System.Windows.Forms.Button();
            this.panelTopLeft = new System.Windows.Forms.Panel();
            this.panelTopRight = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelFormLoader = new System.Windows.Forms.Panel();
            this.buttonConnect = new CustomControls.RJControls.RJButton();
            this.labelTitleSelection = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.mavlinkheartbeattBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxSerialPort = new CustomControls.FancyComboBox();
            this.comboBoxBaudRate = new CustomControls.FancyComboBox();
            this.buttonTeste = new System.Windows.Forms.Button();
            this.panelLateral.SuspendLayout();
            this.panelTopLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mavlinkheartbeattBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLateral
            // 
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panelLateral.Controls.Add(this.buttonTeste);
            this.panelLateral.Controls.Add(this.buttonPixhawk);
            this.panelLateral.Controls.Add(this.buttonGraficos);
            this.panelLateral.Controls.Add(this.buttonDados);
            this.panelLateral.Controls.Add(this.buttonMapa);
            this.panelLateral.Controls.Add(this.panelNav);
            this.panelLateral.Controls.Add(this.buttonConfigurações);
            this.panelLateral.Controls.Add(this.panelTopLeft);
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Location = new System.Drawing.Point(0, 0);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(228, 597);
            this.panelLateral.TabIndex = 9;
            // 
            // buttonPixhawk
            // 
            this.buttonPixhawk.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPixhawk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonPixhawk.FlatAppearance.BorderSize = 0;
            this.buttonPixhawk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPixhawk.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPixhawk.ForeColor = System.Drawing.Color.Coral;
            this.buttonPixhawk.Image = global::SimpleExample.Properties.Resources.charts;
            this.buttonPixhawk.Location = new System.Drawing.Point(0, 322);
            this.buttonPixhawk.Name = "buttonPixhawk";
            this.buttonPixhawk.Size = new System.Drawing.Size(228, 55);
            this.buttonPixhawk.TabIndex = 13;
            this.buttonPixhawk.Text = "          Pixhawk  ";
            this.buttonPixhawk.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonPixhawk.UseVisualStyleBackColor = true;
            this.buttonPixhawk.Click += new System.EventHandler(this.buttonPixhawk_Click);
            // 
            // buttonGraficos
            // 
            this.buttonGraficos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonGraficos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonGraficos.FlatAppearance.BorderSize = 0;
            this.buttonGraficos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGraficos.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGraficos.ForeColor = System.Drawing.Color.Coral;
            this.buttonGraficos.Image = global::SimpleExample.Properties.Resources.charts;
            this.buttonGraficos.Location = new System.Drawing.Point(0, 377);
            this.buttonGraficos.Name = "buttonGraficos";
            this.buttonGraficos.Size = new System.Drawing.Size(228, 55);
            this.buttonGraficos.TabIndex = 12;
            this.buttonGraficos.Text = "          Graficos  ";
            this.buttonGraficos.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonGraficos.UseVisualStyleBackColor = true;
            this.buttonGraficos.Click += new System.EventHandler(this.buttonGraficos_Click);
            this.buttonGraficos.Leave += new System.EventHandler(this.button_Leave);
            // 
            // buttonDados
            // 
            this.buttonDados.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonDados.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonDados.FlatAppearance.BorderSize = 0;
            this.buttonDados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDados.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDados.ForeColor = System.Drawing.Color.Coral;
            this.buttonDados.Image = global::SimpleExample.Properties.Resources.diagram;
            this.buttonDados.Location = new System.Drawing.Point(0, 432);
            this.buttonDados.Name = "buttonDados";
            this.buttonDados.Size = new System.Drawing.Size(228, 55);
            this.buttonDados.TabIndex = 10;
            this.buttonDados.Text = "            Dados  ";
            this.buttonDados.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonDados.UseVisualStyleBackColor = true;
            this.buttonDados.Click += new System.EventHandler(this.buttonDados_Click);
            this.buttonDados.Leave += new System.EventHandler(this.button_Leave);
            // 
            // buttonMapa
            // 
            this.buttonMapa.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonMapa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonMapa.FlatAppearance.BorderSize = 0;
            this.buttonMapa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMapa.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMapa.ForeColor = System.Drawing.Color.Coral;
            this.buttonMapa.Image = global::SimpleExample.Properties.Resources.mapicon;
            this.buttonMapa.Location = new System.Drawing.Point(0, 487);
            this.buttonMapa.Name = "buttonMapa";
            this.buttonMapa.Size = new System.Drawing.Size(228, 55);
            this.buttonMapa.TabIndex = 11;
            this.buttonMapa.Text = "            Mapa  ";
            this.buttonMapa.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonMapa.UseVisualStyleBackColor = true;
            this.buttonMapa.Click += new System.EventHandler(this.buttonMapa_Click);
            this.buttonMapa.Leave += new System.EventHandler(this.button_Leave);
            // 
            // panelNav
            // 
            this.panelNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.panelNav.Location = new System.Drawing.Point(3, 479);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(3, 50);
            this.panelNav.TabIndex = 9;
            // 
            // buttonConfigurações
            // 
            this.buttonConfigurações.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonConfigurações.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonConfigurações.FlatAppearance.BorderSize = 0;
            this.buttonConfigurações.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConfigurações.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConfigurações.ForeColor = System.Drawing.Color.Coral;
            this.buttonConfigurações.Image = global::SimpleExample.Properties.Resources.settings;
            this.buttonConfigurações.Location = new System.Drawing.Point(0, 542);
            this.buttonConfigurações.Name = "buttonConfigurações";
            this.buttonConfigurações.Size = new System.Drawing.Size(228, 55);
            this.buttonConfigurações.TabIndex = 8;
            this.buttonConfigurações.Text = "Configurações";
            this.buttonConfigurações.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonConfigurações.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonConfigurações.UseVisualStyleBackColor = true;
            this.buttonConfigurações.Click += new System.EventHandler(this.buttonConfigurações_Click);
            this.buttonConfigurações.Leave += new System.EventHandler(this.button_Leave);
            // 
            // panelTopLeft
            // 
            this.panelTopLeft.Controls.Add(this.panelTopRight);
            this.panelTopLeft.Controls.Add(this.pictureBox2);
            this.panelTopLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopLeft.Location = new System.Drawing.Point(0, 0);
            this.panelTopLeft.Name = "panelTopLeft";
            this.panelTopLeft.Size = new System.Drawing.Size(228, 119);
            this.panelTopLeft.TabIndex = 7;
            // 
            // panelTopRight
            // 
            this.panelTopRight.Location = new System.Drawing.Point(255, 3);
            this.panelTopRight.Name = "panelTopRight";
            this.panelTopRight.Size = new System.Drawing.Size(910, 113);
            this.panelTopRight.TabIndex = 17;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(228, 119);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // panelFormLoader
            // 
            this.panelFormLoader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFormLoader.Location = new System.Drawing.Point(228, 117);
            this.panelFormLoader.Name = "panelFormLoader";
            this.panelFormLoader.Size = new System.Drawing.Size(937, 480);
            this.panelFormLoader.TabIndex = 11;
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.buttonConnect.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.buttonConnect.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.buttonConnect.BorderRadius = 10;
            this.buttonConnect.BorderSize = 0;
            this.buttonConnect.FlatAppearance.BorderSize = 0;
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.ForeColor = System.Drawing.Color.Coral;
            this.buttonConnect.Location = new System.Drawing.Point(418, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(74, 25);
            this.buttonConnect.TabIndex = 17;
            this.buttonConnect.Text = "Abrir";
            this.buttonConnect.TextColor = System.Drawing.Color.Coral;
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelTitleSelection
            // 
            this.labelTitleSelection.AutoSize = true;
            this.labelTitleSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleSelection.ForeColor = System.Drawing.Color.Coral;
            this.labelTitleSelection.Location = new System.Drawing.Point(271, 73);
            this.labelTitleSelection.Name = "labelTitleSelection";
            this.labelTitleSelection.Size = new System.Drawing.Size(101, 32);
            this.labelTitleSelection.TabIndex = 15;
            this.labelTitleSelection.Text = "Dados";
            this.labelTitleSelection.Click += new System.EventHandler(this.labelTitleSelection_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackgroundImage = global::SimpleExample.Properties.Resources.Close;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.MistyRose;
            this.buttonExit.Location = new System.Drawing.Point(1106, 24);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(47, 35);
            this.buttonExit.TabIndex = 10;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // mavlinkheartbeattBindingSource
            // 
            this.mavlinkheartbeattBindingSource.DataSource = typeof(Mavlink.mavlink_heartbeat_t);
            // 
            // comboBoxSerialPort
            // 
            this.comboBoxSerialPort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxSerialPort.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.comboBoxSerialPort.BorderSize = 1;
            this.comboBoxSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxSerialPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBoxSerialPort.ForeColor = System.Drawing.Color.DimGray;
            this.comboBoxSerialPort.IconColor = System.Drawing.Color.MediumSlateBlue;
            this.comboBoxSerialPort.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.comboBoxSerialPort.ListTextColor = System.Drawing.Color.DimGray;
            this.comboBoxSerialPort.Location = new System.Drawing.Point(234, 12);
            this.comboBoxSerialPort.MinimumSize = new System.Drawing.Size(30, 10);
            this.comboBoxSerialPort.Name = "comboBoxSerialPort";
            this.comboBoxSerialPort.Padding = new System.Windows.Forms.Padding(1);
            this.comboBoxSerialPort.Size = new System.Drawing.Size(80, 23);
            this.comboBoxSerialPort.TabIndex = 12;
            this.comboBoxSerialPort.Texts = "";
            this.comboBoxSerialPort.OnSelectedIndexChanged += new System.EventHandler(this.comboBoxSerialPort_OnSelectedIndexChanged);
            this.comboBoxSerialPort.Click += new System.EventHandler(this.comboBoxSerialPort_Click);
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxBaudRate.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.comboBoxBaudRate.BorderSize = 1;
            this.comboBoxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxBaudRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBoxBaudRate.ForeColor = System.Drawing.Color.DimGray;
            this.comboBoxBaudRate.IconColor = System.Drawing.Color.MediumSlateBlue;
            this.comboBoxBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "57600",
            "115200",
            "915200"});
            this.comboBoxBaudRate.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.comboBoxBaudRate.ListTextColor = System.Drawing.Color.DimGray;
            this.comboBoxBaudRate.Location = new System.Drawing.Point(320, 12);
            this.comboBoxBaudRate.MinimumSize = new System.Drawing.Size(30, 10);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Padding = new System.Windows.Forms.Padding(1);
            this.comboBoxBaudRate.Size = new System.Drawing.Size(83, 23);
            this.comboBoxBaudRate.TabIndex = 16;
            this.comboBoxBaudRate.Texts = "";
            this.comboBoxBaudRate.OnSelectedIndexChanged += new System.EventHandler(this.comboBoxBaudRate_OnSelectedIndexChanged);
            // 
            // buttonTeste
            // 
            this.buttonTeste.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonTeste.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonTeste.FlatAppearance.BorderSize = 0;
            this.buttonTeste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTeste.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTeste.ForeColor = System.Drawing.Color.Coral;
            this.buttonTeste.Image = global::SimpleExample.Properties.Resources.diagram1;
            this.buttonTeste.Location = new System.Drawing.Point(0, 267);
            this.buttonTeste.Name = "buttonTeste";
            this.buttonTeste.Size = new System.Drawing.Size(228, 55);
            this.buttonTeste.TabIndex = 14;
            this.buttonTeste.Text = "          Teste";
            this.buttonTeste.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonTeste.UseVisualStyleBackColor = true;
            this.buttonTeste.Click += new System.EventHandler(this.buttonTeste_Click);
            // 
            // GroundStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1165, 597);
            this.Controls.Add(this.panelFormLoader);
            this.Controls.Add(this.labelTitleSelection);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.panelLateral);
            this.Controls.Add(this.comboBoxSerialPort);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.buttonConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GroundStation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BoatStation";
            this.Load += new System.EventHandler(this.GroundStation_Load);
            this.panelLateral.ResumeLayout(false);
            this.panelTopLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mavlinkheartbeattBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource mavlinkheartbeattBindingSource;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Panel panelTopLeft;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonConfigurações;
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonDados;
        private System.Windows.Forms.Panel panelFormLoader;
        private System.Windows.Forms.Button buttonMapa;
        private System.Windows.Forms.Panel panelTopRight;
        private CustomControls.FancyComboBox comboBoxBaudRate;
        private CustomControls.FancyComboBox comboBoxSerialPort;
        private CustomControls.RJControls.RJButton buttonConnect;
        private System.Windows.Forms.Label labelTitleSelection;
        private System.Windows.Forms.Button buttonGraficos;
        private System.Windows.Forms.Button buttonPixhawk;
        public System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button buttonTeste;
    }
}

