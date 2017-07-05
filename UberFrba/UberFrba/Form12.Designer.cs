namespace UberFrba
{
    partial class frmRol
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
            this.cajaListaFuncionesSegunRol = new System.Windows.Forms.ListBox();
            this.cajaListaFunciones = new System.Windows.Forms.ListBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtNombreRol = new System.Windows.Forms.TextBox();
            this.lblNombreRol = new System.Windows.Forms.Label();
            this.lblCajaFuncionesSegunRol = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cajaListaFuncionesSegunRol
            // 
            this.cajaListaFuncionesSegunRol.FormattingEnabled = true;
            this.cajaListaFuncionesSegunRol.Location = new System.Drawing.Point(364, 53);
            this.cajaListaFuncionesSegunRol.Name = "cajaListaFuncionesSegunRol";
            this.cajaListaFuncionesSegunRol.Size = new System.Drawing.Size(221, 212);
            this.cajaListaFuncionesSegunRol.TabIndex = 0;
            // 
            // cajaListaFunciones
            // 
            this.cajaListaFunciones.FormattingEnabled = true;
            this.cajaListaFunciones.Location = new System.Drawing.Point(12, 53);
            this.cajaListaFunciones.Name = "cajaListaFunciones";
            this.cajaListaFunciones.Size = new System.Drawing.Size(221, 212);
            this.cajaListaFunciones.TabIndex = 1;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(275, 111);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(45, 31);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = ">";
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(275, 161);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(45, 31);
            this.btnQuitar.TabIndex = 3;
            this.btnQuitar.Text = "<";
            this.btnQuitar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAceptar.Location = new System.Drawing.Point(125, 286);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(108, 30);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(364, 286);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(108, 30);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtNombreRol
            // 
            this.txtNombreRol.Location = new System.Drawing.Point(200, 18);
            this.txtNombreRol.Name = "txtNombreRol";
            this.txtNombreRol.Size = new System.Drawing.Size(195, 20);
            this.txtNombreRol.TabIndex = 6;
            // 
            // lblNombreRol
            // 
            this.lblNombreRol.AutoSize = true;
            this.lblNombreRol.Location = new System.Drawing.Point(265, 2);
            this.lblNombreRol.Name = "lblNombreRol";
            this.lblNombreRol.Size = new System.Drawing.Size(63, 13);
            this.lblNombreRol.TabIndex = 63;
            this.lblNombreRol.Text = "Nombre Rol";
            // 
            // lblCajaFuncionesSegunRol
            // 
            this.lblCajaFuncionesSegunRol.AutoSize = true;
            this.lblCajaFuncionesSegunRol.Location = new System.Drawing.Point(421, 37);
            this.lblCajaFuncionesSegunRol.Name = "lblCajaFuncionesSegunRol";
            this.lblCajaFuncionesSegunRol.Size = new System.Drawing.Size(109, 13);
            this.lblCajaFuncionesSegunRol.TabIndex = 64;
            this.lblCajaFuncionesSegunRol.Text = "Funciones Segun Rol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Funciones";
            // 
            // frmRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(601, 332);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCajaFuncionesSegunRol);
            this.Controls.Add(this.lblNombreRol);
            this.Controls.Add(this.txtNombreRol);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.cajaListaFunciones);
            this.Controls.Add(this.cajaListaFuncionesSegunRol);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRol";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox cajaListaFuncionesSegunRol;
        private System.Windows.Forms.ListBox cajaListaFunciones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtNombreRol;
        private System.Windows.Forms.Label lblNombreRol;
        private System.Windows.Forms.Label lblCajaFuncionesSegunRol;
        private System.Windows.Forms.Label label2;
    }
}