namespace SoundCloud
{
    partial class SoundCloud
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundCloud));
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.txtPosicion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lstCanciones = new System.Windows.Forms.ListBox();
            this.lblTotalCanciones = new System.Windows.Forms.Label();
            this.lblReproduciendo = new System.Windows.Forms.Label();
            this.btnAgregarInicio = new System.Windows.Forms.Button();
            this.btnAgregarFinal = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnPos = new System.Windows.Forms.Button();
            this.btnMoverDerecha = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tmrReproductor = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDuracion
            // 
            this.txtDuracion.Location = new System.Drawing.Point(30, 182);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(169, 20);
            this.txtDuracion.TabIndex = 0;
            this.txtDuracion.Text = "Duracion";
            this.txtDuracion.TextChanged += new System.EventHandler(this.txtDuracion_TextChanged);
            // 
            // txtPosicion
            // 
            this.txtPosicion.Location = new System.Drawing.Point(30, 98);
            this.txtPosicion.Name = "txtPosicion";
            this.txtPosicion.Size = new System.Drawing.Size(169, 20);
            this.txtPosicion.TabIndex = 1;
            this.txtPosicion.Text = "Posicion";
            this.txtPosicion.TextChanged += new System.EventHandler(this.txtPosicion_TextChanged);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(30, 12);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(169, 20);
            this.txtNombre.TabIndex = 2;
            this.txtNombre.Text = "Nombre";
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // lstCanciones
            // 
            this.lstCanciones.FormattingEnabled = true;
            this.lstCanciones.Location = new System.Drawing.Point(431, 38);
            this.lstCanciones.Name = "lstCanciones";
            this.lstCanciones.Size = new System.Drawing.Size(344, 277);
            this.lstCanciones.TabIndex = 3;
            // 
            // lblTotalCanciones
            // 
            this.lblTotalCanciones.AutoSize = true;
            this.lblTotalCanciones.Location = new System.Drawing.Point(403, 22);
            this.lblTotalCanciones.Name = "lblTotalCanciones";
            this.lblTotalCanciones.Size = new System.Drawing.Size(99, 13);
            this.lblTotalCanciones.TabIndex = 4;
            this.lblTotalCanciones.Text = "Total de Canciones";
            // 
            // lblReproduciendo
            // 
            this.lblReproduciendo.AutoSize = true;
            this.lblReproduciendo.Location = new System.Drawing.Point(474, 334);
            this.lblReproduciendo.Name = "lblReproduciendo";
            this.lblReproduciendo.Size = new System.Drawing.Size(80, 13);
            this.lblReproduciendo.TabIndex = 5;
            this.lblReproduciendo.Text = "Reproduciendo";
            // 
            // btnAgregarInicio
            // 
            this.btnAgregarInicio.Location = new System.Drawing.Point(205, 3);
            this.btnAgregarInicio.Name = "btnAgregarInicio";
            this.btnAgregarInicio.Size = new System.Drawing.Size(90, 37);
            this.btnAgregarInicio.TabIndex = 6;
            this.btnAgregarInicio.Text = "Agregar Inicio";
            this.btnAgregarInicio.UseVisualStyleBackColor = true;
            this.btnAgregarInicio.Click += new System.EventHandler(this.btnAgregarInicio_Click);
            // 
            // btnAgregarFinal
            // 
            this.btnAgregarFinal.Location = new System.Drawing.Point(335, 227);
            this.btnAgregarFinal.Name = "btnAgregarFinal";
            this.btnAgregarFinal.Size = new System.Drawing.Size(90, 37);
            this.btnAgregarFinal.TabIndex = 7;
            this.btnAgregarFinal.Text = "AgregarFinal";
            this.btnAgregarFinal.UseVisualStyleBackColor = true;
            this.btnAgregarFinal.Click += new System.EventHandler(this.btnAgregarFinal_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(627, 357);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(90, 37);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnPos
            // 
            this.btnPos.Location = new System.Drawing.Point(205, 89);
            this.btnPos.Name = "btnPos";
            this.btnPos.Size = new System.Drawing.Size(90, 37);
            this.btnPos.TabIndex = 9;
            this.btnPos.Text = "Agregar Posicion";
            this.btnPos.UseVisualStyleBackColor = true;
            this.btnPos.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btnMoverDerecha
            // 
            this.btnMoverDerecha.Location = new System.Drawing.Point(335, 165);
            this.btnMoverDerecha.Name = "btnMoverDerecha";
            this.btnMoverDerecha.Size = new System.Drawing.Size(90, 37);
            this.btnMoverDerecha.TabIndex = 10;
            this.btnMoverDerecha.Text = "Mover Derecha";
            this.btnMoverDerecha.UseVisualStyleBackColor = true;
            this.btnMoverDerecha.Click += new System.EventHandler(this.btnMoverDerecha_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(364, 357);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(90, 37);
            this.btnPlay.TabIndex = 11;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(492, 357);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(90, 37);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tmrReproductor
            // 
            this.tmrReproductor.Interval = 1000;
            this.tmrReproductor.Tick += new System.EventHandler(this.tmrReproductor_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(21, 227);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 188);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // SoundCloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Coral;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnMoverDerecha);
            this.Controls.Add(this.btnPos);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAgregarFinal);
            this.Controls.Add(this.btnAgregarInicio);
            this.Controls.Add(this.lblReproduciendo);
            this.Controls.Add(this.lblTotalCanciones);
            this.Controls.Add(this.lstCanciones);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtPosicion);
            this.Controls.Add(this.txtDuracion);
            this.Name = "SoundCloud";
            this.Text = "SoundCloud";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.TextBox txtPosicion;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ListBox lstCanciones;
        private System.Windows.Forms.Label lblTotalCanciones;
        private System.Windows.Forms.Label lblReproduciendo;
        private System.Windows.Forms.Button btnAgregarInicio;
        private System.Windows.Forms.Button btnAgregarFinal;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnPos;
        private System.Windows.Forms.Button btnMoverDerecha;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer tmrReproductor;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

