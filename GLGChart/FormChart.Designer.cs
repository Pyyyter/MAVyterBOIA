namespace SimpleExample
{
    partial class FormChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Configuration1Btn = new System.Windows.Forms.Button();
            this.Configuration2Btn = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            // 
            // Configuration1Btn
            // 
            this.Configuration1Btn.Location = new System.Drawing.Point(12, 12);
            this.Configuration1Btn.Name = "Configuration1Btn";
            this.Configuration1Btn.Size = new System.Drawing.Size(113, 27);
            this.Configuration1Btn.TabIndex = 0;
            this.Configuration1Btn.Text = "Configuration1";
            this.Configuration1Btn.UseVisualStyleBackColor = true;
            this.Configuration1Btn.Click += new System.EventHandler(this.Configuration1Btn_Click);
            // 
            // Configuration2Btn
            // 
            this.Configuration2Btn.Location = new System.Drawing.Point(131, 12);
            this.Configuration2Btn.Name = "Configuration2Btn";
            this.Configuration2Btn.Size = new System.Drawing.Size(113, 27);
            this.Configuration2Btn.TabIndex = 1;
            this.Configuration2Btn.Text = "Configuration2";
            this.Configuration2Btn.UseVisualStyleBackColor = true;
            this.Configuration2Btn.Click += new System.EventHandler(this.Configuration2Btn_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 45);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(37, 13);
            this.StatusLabel.TabIndex = 2;
            this.StatusLabel.Text = "Status";
            // 
            // GlgRTChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 800);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.Configuration2Btn);
            this.Controls.Add(this.Configuration1Btn);
            this.Name = "GlgRTChartForm";
            this.Text = "GLG Combo Chart Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GlgRTChartForm_FormClosed);
            this.Resize += new System.EventHandler(this.GlgRTChartForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Configuration1Btn;
        private System.Windows.Forms.Button Configuration2Btn;
        private System.Windows.Forms.Label StatusLabel;
    }
}

