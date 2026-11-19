namespace ServicioDeCombis
{
    partial class SerivicioDeCombis
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerivicioDeCombis));
            this.PicCombi = new System.Windows.Forms.PictureBox();
            this.TerminalObelisco = new System.Windows.Forms.GroupBox();
            this.lblPasajero = new System.Windows.Forms.Label();
            this.btnSubir = new System.Windows.Forms.Button();
            this.btnAnotar = new System.Windows.Forms.Button();
            this.txtPasajero = new System.Windows.Forms.TextBox();
            this.lblEspera = new System.Windows.Forms.Label();
            this.lstEnEspera = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicCombi)).BeginInit();
            this.TerminalObelisco.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicCombi
            // 
            this.PicCombi.Image = ((System.Drawing.Image)(resources.GetObject("PicCombi.Image")));
            this.PicCombi.Location = new System.Drawing.Point(34, 47);
            this.PicCombi.Name = "PicCombi";
            this.PicCombi.Size = new System.Drawing.Size(287, 251);
            this.PicCombi.TabIndex = 0;
            this.PicCombi.TabStop = false;
            // 
            // TerminalObelisco
            // 
            this.TerminalObelisco.Controls.Add(this.lstEnEspera);
            this.TerminalObelisco.Location = new System.Drawing.Point(327, 2);
            this.TerminalObelisco.Name = "TerminalObelisco";
            this.TerminalObelisco.Size = new System.Drawing.Size(470, 449);
            this.TerminalObelisco.TabIndex = 1;
            this.TerminalObelisco.TabStop = false;
            this.TerminalObelisco.Text = "TerminalObelisco";
            this.TerminalObelisco.Enter += new System.EventHandler(this.TerminalObelisco_Enter);
            // 
            // lblPasajero
            // 
            this.lblPasajero.AutoSize = true;
            this.lblPasajero.Location = new System.Drawing.Point(336, 95);
            this.lblPasajero.Name = "lblPasajero";
            this.lblPasajero.Size = new System.Drawing.Size(48, 13);
            this.lblPasajero.TabIndex = 3;
            this.lblPasajero.Text = "Pasajero";
            this.lblPasajero.Click += new System.EventHandler(this.lblPasajero_Click);
            // 
            // btnSubir
            // 
            this.btnSubir.Location = new System.Drawing.Point(577, 372);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(84, 29);
            this.btnSubir.TabIndex = 6;
            this.btnSubir.Text = "Subir";
            this.btnSubir.UseVisualStyleBackColor = true;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // btnAnotar
            // 
            this.btnAnotar.Location = new System.Drawing.Point(718, 86);
            this.btnAnotar.Name = "btnAnotar";
            this.btnAnotar.Size = new System.Drawing.Size(79, 30);
            this.btnAnotar.TabIndex = 7;
            this.btnAnotar.Text = "Anotar";
            this.btnAnotar.UseVisualStyleBackColor = true;
            this.btnAnotar.Click += new System.EventHandler(this.btnAnotar_Click);
            // 
            // txtPasajero
            // 
            this.txtPasajero.Location = new System.Drawing.Point(428, 96);
            this.txtPasajero.Name = "txtPasajero";
            this.txtPasajero.Size = new System.Drawing.Size(264, 20);
            this.txtPasajero.TabIndex = 8;
            this.txtPasajero.TextChanged += new System.EventHandler(this.txtPasajero_TextChanged);
            // 
            // lblEspera
            // 
            this.lblEspera.AutoSize = true;
            this.lblEspera.Location = new System.Drawing.Point(343, 165);
            this.lblEspera.Name = "lblEspera";
            this.lblEspera.Size = new System.Drawing.Size(40, 13);
            this.lblEspera.TabIndex = 9;
            this.lblEspera.Text = "Espera";
            this.lblEspera.Click += new System.EventHandler(this.lblEspera_Click);
            // 
            // lstEnEspera
            // 
            this.lstEnEspera.FormattingEnabled = true;
            this.lstEnEspera.Location = new System.Drawing.Point(96, 151);
            this.lstEnEspera.Name = "lstEnEspera";
            this.lstEnEspera.Size = new System.Drawing.Size(320, 199);
            this.lstEnEspera.TabIndex = 0;
            // 
            // SerivicioDeCombis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblEspera);
            this.Controls.Add(this.txtPasajero);
            this.Controls.Add(this.btnAnotar);
            this.Controls.Add(this.btnSubir);
            this.Controls.Add(this.lblPasajero);
            this.Controls.Add(this.TerminalObelisco);
            this.Controls.Add(this.PicCombi);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "SerivicioDeCombis";
            this.Text = "SerivicioDeCombis";
            ((System.ComponentModel.ISupportInitialize)(this.PicCombi)).EndInit();
            this.TerminalObelisco.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicCombi;
        private System.Windows.Forms.GroupBox TerminalObelisco;
        private System.Windows.Forms.Label lblPasajero;
        private System.Windows.Forms.Button btnSubir;
        private System.Windows.Forms.Button btnAnotar;
        private System.Windows.Forms.TextBox txtPasajero;
        private System.Windows.Forms.Label lblEspera;
        private System.Windows.Forms.ListBox lstEnEspera;
    }
}

