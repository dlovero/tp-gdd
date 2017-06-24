namespace UberFrba
{
    partial class frmIngreso
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.usuario = new System.Windows.Forms.Label();
            this.clave = new System.Windows.Forms.Label();
            this.textoUsuario = new System.Windows.Forms.TextBox();
            this.textoClave = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(128, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Ingresar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // usuario
            // 
            this.usuario.AutoSize = true;
            this.usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usuario.Location = new System.Drawing.Point(7, 27);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(68, 20);
            this.usuario.TabIndex = 2;
            this.usuario.Text = "Usuario:";
            this.usuario.Click += new System.EventHandler(this.label1_Click);
            // 
            // clave
            // 
            this.clave.AutoSize = true;
            this.clave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clave.Location = new System.Drawing.Point(23, 73);
            this.clave.Name = "clave";
            this.clave.Size = new System.Drawing.Size(52, 20);
            this.clave.TabIndex = 3;
            this.clave.Text = "Clave:";
            this.clave.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // textoUsuario
            // 
            this.textoUsuario.Location = new System.Drawing.Point(91, 27);
            this.textoUsuario.Name = "textoUsuario";
            this.textoUsuario.Size = new System.Drawing.Size(133, 20);
            this.textoUsuario.TabIndex = 4;
            // 
            // textoClave
            // 
            this.textoClave.Location = new System.Drawing.Point(91, 73);
            this.textoClave.Name = "textoClave";
            this.textoClave.PasswordChar = '*';
            this.textoClave.Size = new System.Drawing.Size(133, 20);
            this.textoClave.TabIndex = 5;
            // 
            // frmIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 174);
            this.Controls.Add(this.textoClave);
            this.Controls.Add(this.textoUsuario);
            this.Controls.Add(this.clave);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.button2);
            this.Name = "frmIngreso";
            this.Text = "Ingreso";
            this.Load += new System.EventHandler(this.frmIngreso_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label usuario;
        private System.Windows.Forms.Label clave;
        private System.Windows.Forms.TextBox textoUsuario;
        private System.Windows.Forms.TextBox textoClave;

    }
}

