namespace UberFrba
{
    partial class frmListados
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.comboListados = new System.Windows.Forms.ComboBox();
            this.lblSeleccionListado = new System.Windows.Forms.Label();
            this.selectorDiaHoraInicio = new System.Windows.Forms.DateTimePicker();
            this.lblAnio = new System.Windows.Forms.Label();
            this.selectorTrimestre = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAceptar.Location = new System.Drawing.Point(254, 21);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(131, 29);
            this.btnAceptar.TabIndex = 24;
            this.btnAceptar.Text = "Listar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(254, 92);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(131, 29);
            this.btnCancelar.TabIndex = 25;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // comboListados
            // 
            this.comboListados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboListados.FormattingEnabled = true;
            this.comboListados.Location = new System.Drawing.Point(12, 26);
            this.comboListados.Name = "comboListados";
            this.comboListados.Size = new System.Drawing.Size(221, 21);
            this.comboListados.TabIndex = 63;
            // 
            // lblSeleccionListado
            // 
            this.lblSeleccionListado.AutoSize = true;
            this.lblSeleccionListado.Location = new System.Drawing.Point(53, 10);
            this.lblSeleccionListado.Name = "lblSeleccionListado";
            this.lblSeleccionListado.Size = new System.Drawing.Size(100, 13);
            this.lblSeleccionListado.TabIndex = 62;
            this.lblSeleccionListado.Text = "Seleccionar Listado";
            // 
            // selectorDiaHoraInicio
            // 
            this.selectorDiaHoraInicio.CustomFormat = "yyyy";
            this.selectorDiaHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.selectorDiaHoraInicio.Location = new System.Drawing.Point(24, 80);
            this.selectorDiaHoraInicio.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.selectorDiaHoraInicio.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.selectorDiaHoraInicio.Name = "selectorDiaHoraInicio";
            this.selectorDiaHoraInicio.ShowUpDown = true;
            this.selectorDiaHoraInicio.Size = new System.Drawing.Size(57, 20);
            this.selectorDiaHoraInicio.TabIndex = 67;
            // 
            // lblAnio
            // 
            this.lblAnio.AutoSize = true;
            this.lblAnio.Location = new System.Drawing.Point(39, 64);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(26, 13);
            this.lblAnio.TabIndex = 68;
            this.lblAnio.Text = "Año";
            // 
            // selectorTrimestre
            // 
            this.selectorTrimestre.Location = new System.Drawing.Point(119, 62);
            this.selectorTrimestre.Name = "selectorTrimestre";
            this.selectorTrimestre.Size = new System.Drawing.Size(63, 24);
            this.selectorTrimestre.TabIndex = 70;
            this.selectorTrimestre.Text = "Trimestre";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "d";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(116, 80);
            this.dateTimePicker1.MaxDate = new System.DateTime(2003, 5, 4, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(2003, 5, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(57, 20);
            this.dateTimePicker1.TabIndex = 69;
            this.dateTimePicker1.Value = new System.DateTime(2003, 5, 4, 0, 0, 0, 0);
            // 
            // frmListados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(404, 136);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.selectorTrimestre);
            this.Controls.Add(this.lblAnio);
            this.Controls.Add(this.selectorDiaHoraInicio);
            this.Controls.Add(this.comboListados);
            this.Controls.Add(this.lblSeleccionListado);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListados";
            this.Text = "Listados";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox comboListados;
        private System.Windows.Forms.Label lblSeleccionListado;
        private System.Windows.Forms.DateTimePicker selectorDiaHoraInicio;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.Label selectorTrimestre;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}