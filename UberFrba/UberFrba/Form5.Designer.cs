namespace UberFrba
{
    partial class frmResultadoBusquedaUsuarioABM
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
            this.grillaDatosResultadoBusqueda = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grillaDatosResultadoBusqueda)).BeginInit();
            this.SuspendLayout();
            // 
            // grillaDatosResultadoBusqueda
            // 
            this.grillaDatosResultadoBusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaDatosResultadoBusqueda.Location = new System.Drawing.Point(6, 9);
            this.grillaDatosResultadoBusqueda.Name = "grillaDatosResultadoBusqueda";
            this.grillaDatosResultadoBusqueda.Size = new System.Drawing.Size(725, 234);
            this.grillaDatosResultadoBusqueda.TabIndex = 0;
            this.grillaDatosResultadoBusqueda.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grillaDatosResultadoBusqueda_CellContentDoubleClick);
            // 
            // frmResultadoBusquedaUsuarioABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 315);
            this.Controls.Add(this.grillaDatosResultadoBusqueda);
            this.Name = "frmResultadoBusquedaUsuarioABM";
            ((System.ComponentModel.ISupportInitialize)(this.grillaDatosResultadoBusqueda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grillaDatosResultadoBusqueda;
    }
}