namespace API_BARCODE
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.MSG_LOGS = new System.Windows.Forms.TextBox();
            this.Pausa = new System.Windows.Forms.Button();
            this.titulo_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Iniciar_Processo = new System.Windows.Forms.Button();
            this.label_Encerrar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MSG_LOGS
            // 
            this.MSG_LOGS.BackColor = System.Drawing.Color.White;
            this.MSG_LOGS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MSG_LOGS.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MSG_LOGS.ForeColor = System.Drawing.Color.Black;
            this.MSG_LOGS.Location = new System.Drawing.Point(7, 95);
            this.MSG_LOGS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MSG_LOGS.Multiline = true;
            this.MSG_LOGS.Name = "MSG_LOGS";
            this.MSG_LOGS.ReadOnly = true;
            this.MSG_LOGS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MSG_LOGS.Size = new System.Drawing.Size(743, 172);
            this.MSG_LOGS.TabIndex = 0;
            this.MSG_LOGS.Text = "Processamento em curso...";
            // 
            // Pausa
            // 
            this.Pausa.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Pausa.FlatAppearance.BorderSize = 2;
            this.Pausa.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pausa.ForeColor = System.Drawing.Color.Black;
            this.Pausa.Location = new System.Drawing.Point(659, 287);
            this.Pausa.Margin = new System.Windows.Forms.Padding(0);
            this.Pausa.Name = "Pausa";
            this.Pausa.Size = new System.Drawing.Size(91, 34);
            this.Pausa.TabIndex = 1;
            this.Pausa.Text = "Pausa";
            this.Pausa.UseVisualStyleBackColor = false;
            this.Pausa.Visible = false;
            this.Pausa.Click += new System.EventHandler(this.Pausa_Click);
            // 
            // titulo_label
            // 
            this.titulo_label.AutoSize = true;
            this.titulo_label.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titulo_label.ForeColor = System.Drawing.Color.Black;
            this.titulo_label.Location = new System.Drawing.Point(270, 31);
            this.titulo_label.Name = "titulo_label";
            this.titulo_label.Size = new System.Drawing.Size(314, 23);
            this.titulo_label.TabIndex = 3;
            this.titulo_label.Text = "Sistema de Digitalização de Processos.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(207, 68);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Iniciar_Processo
            // 
            this.Iniciar_Processo.BackColor = System.Drawing.Color.White;
            this.Iniciar_Processo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Iniciar_Processo.FlatAppearance.BorderSize = 0;
            this.Iniciar_Processo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Iniciar_Processo.Location = new System.Drawing.Point(669, 9);
            this.Iniciar_Processo.Name = "Iniciar_Processo";
            this.Iniciar_Processo.Size = new System.Drawing.Size(81, 45);
            this.Iniciar_Processo.TabIndex = 5;
            this.Iniciar_Processo.UseVisualStyleBackColor = false;
            this.Iniciar_Processo.Visible = false;
            // 
            // label_Encerrar
            // 
            this.label_Encerrar.AutoSize = true;
            this.label_Encerrar.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label_Encerrar.Location = new System.Drawing.Point(271, 296);
            this.label_Encerrar.Name = "label_Encerrar";
            this.label_Encerrar.Size = new System.Drawing.Size(304, 18);
            this.label_Encerrar.TabIndex = 6;
            this.label_Encerrar.Text = "O programa vai encerrar dentro de 5 segundos...";
            this.label_Encerrar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(759, 330);
            this.Controls.Add(this.label_Encerrar);
            this.Controls.Add(this.Iniciar_Processo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.titulo_label);
            this.Controls.Add(this.Pausa);
            this.Controls.Add(this.MSG_LOGS);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MSG_LOGS;
        private System.Windows.Forms.Button Pausa;
        private System.Windows.Forms.Label titulo_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Iniciar_Processo;
        private System.Windows.Forms.Label label_Encerrar;
    }
}

