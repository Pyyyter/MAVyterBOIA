namespace SimpleExample
{
    partial class FormPixhawk
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.mAVCMDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonEnviar = new CustomControls.RJControls.RJButton();
            this.richTextBoxEntrada = new System.Windows.Forms.RichTextBox();
            this.mAVCMDBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxCommandParameters = new CustomControls.RJControls.RJTextBox();
            this.comboBoxCommandSelection = new CustomControls.FancyComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.mAVCMDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAVCMDBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Coral;
            this.labelTitle.Location = new System.Drawing.Point(362, 24);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(127, 32);
            this.labelTitle.TabIndex = 16;
            this.labelTitle.Text = "Pixhawk";
            // 
            // mAVCMDBindingSource
            // 
            this.mAVCMDBindingSource.DataSource = typeof(Mavlink.MAV_CMD);
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.buttonEnviar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.buttonEnviar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.buttonEnviar.BorderRadius = 10;
            this.buttonEnviar.BorderSize = 0;
            this.buttonEnviar.FlatAppearance.BorderSize = 0;
            this.buttonEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnviar.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnviar.ForeColor = System.Drawing.Color.Coral;
            this.buttonEnviar.Location = new System.Drawing.Point(13, 104);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(210, 25);
            this.buttonEnviar.TabIndex = 20;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.TextColor = System.Drawing.Color.Coral;
            this.buttonEnviar.UseVisualStyleBackColor = false;
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // richTextBoxEntrada
            // 
            this.richTextBoxEntrada.Location = new System.Drawing.Point(243, 104);
            this.richTextBoxEntrada.Name = "richTextBoxEntrada";
            this.richTextBoxEntrada.Size = new System.Drawing.Size(665, 317);
            this.richTextBoxEntrada.TabIndex = 22;
            this.richTextBoxEntrada.Text = "";
            // 
            // mAVCMDBindingSource1
            // 
            this.mAVCMDBindingSource1.DataSource = typeof(Mavlink.MAV_CMD);
            // 
            // textBoxCommandParameters
            // 
            this.textBoxCommandParameters.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxCommandParameters.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.textBoxCommandParameters.BorderFocusColor = System.Drawing.Color.HotPink;
            this.textBoxCommandParameters.BorderRadius = 0;
            this.textBoxCommandParameters.BorderSize = 2;
            this.textBoxCommandParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCommandParameters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxCommandParameters.Location = new System.Drawing.Point(13, 196);
            this.textBoxCommandParameters.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCommandParameters.Multiline = false;
            this.textBoxCommandParameters.Name = "textBoxCommandParameters";
            this.textBoxCommandParameters.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.textBoxCommandParameters.PasswordChar = false;
            this.textBoxCommandParameters.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.textBoxCommandParameters.PlaceholderText = "";
            this.textBoxCommandParameters.Size = new System.Drawing.Size(209, 31);
            this.textBoxCommandParameters.TabIndex = 23;
            this.textBoxCommandParameters.Texts = "";
            this.textBoxCommandParameters.UnderlinedStyle = false;
            // 
            // comboBoxCommandSelection
            // 
            this.comboBoxCommandSelection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxCommandSelection.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.comboBoxCommandSelection.BorderSize = 1;
            this.comboBoxCommandSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxCommandSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBoxCommandSelection.ForeColor = System.Drawing.Color.DimGray;
            this.comboBoxCommandSelection.IconColor = System.Drawing.Color.MediumSlateBlue;
            this.comboBoxCommandSelection.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.comboBoxCommandSelection.ListTextColor = System.Drawing.Color.DimGray;
            this.comboBoxCommandSelection.Location = new System.Drawing.Point(13, 151);
            this.comboBoxCommandSelection.MinimumSize = new System.Drawing.Size(30, 10);
            this.comboBoxCommandSelection.Name = "comboBoxCommandSelection";
            this.comboBoxCommandSelection.Padding = new System.Windows.Forms.Padding(1);
            this.comboBoxCommandSelection.Size = new System.Drawing.Size(210, 23);
            this.comboBoxCommandSelection.TabIndex = 18;
            this.comboBoxCommandSelection.Texts = "";
            
            // 
            // FormPixhawk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(937, 480);
            this.Controls.Add(this.textBoxCommandParameters);
            this.Controls.Add(this.richTextBoxEntrada);
            this.Controls.Add(this.comboBoxCommandSelection);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPixhawk";
            this.Text = "FormPixhawk";
            ((System.ComponentModel.ISupportInitialize)(this.mAVCMDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAVCMDBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private CustomControls.FancyComboBox comboBoxCommandSelection;
        private CustomControls.RJControls.RJButton buttonEnviar;
        private System.Windows.Forms.RichTextBox richTextBoxEntrada;
        private System.Windows.Forms.BindingSource mAVCMDBindingSource;
        private System.Windows.Forms.BindingSource mAVCMDBindingSource1;
        private CustomControls.RJControls.RJTextBox textBoxCommandParameters;
    }
}