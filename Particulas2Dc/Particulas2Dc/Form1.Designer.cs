namespace Particulas2Dc
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
            this.cBoton1 = new CTCBASE.cBoton();
            this.cBoton2 = new CTCBASE.cBoton();
            this.BtnStop = new CTCBASE.cBoton();
            this.BtnPlay = new CTCBASE.cBoton();
            this.FrmBuscaDir = new System.Windows.Forms.FolderBrowserDialog();
            this.FrmBuscaFich = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cBoton1
            // 
            this.cBoton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cBoton1.BackColor = System.Drawing.Color.Transparent;
            this.cBoton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cBoton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cBoton1.Location = new System.Drawing.Point(903, 12);
            this.cBoton1.Name = "cBoton1";
            this.cBoton1.Size = new System.Drawing.Size(75, 23);
            this.cBoton1.TabIndex = 0;
            this.cBoton1.Text = "Particulas ...";
            this.cBoton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cBoton1.UseVisualStyleBackColor = true;
            this.cBoton1.Click += new System.EventHandler(this.cBoton1_Click);
            // 
            // cBoton2
            // 
            this.cBoton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cBoton2.BackColor = System.Drawing.Color.Transparent;
            this.cBoton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cBoton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cBoton2.Location = new System.Drawing.Point(903, 41);
            this.cBoton2.Name = "cBoton2";
            this.cBoton2.Size = new System.Drawing.Size(75, 23);
            this.cBoton2.TabIndex = 1;
            this.cBoton2.Text = "Entorno ...";
            this.cBoton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cBoton2.UseVisualStyleBackColor = true;
            this.cBoton2.Click += new System.EventHandler(this.cBoton2_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnStop.BackColor = System.Drawing.Color.Transparent;
            this.BtnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnStop.Image = global::Particulas2Dc.Properties.Resources.bullet_square_red;
            this.BtnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnStop.Location = new System.Drawing.Point(945, 70);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(33, 23);
            this.BtnStop.TabIndex = 3;
            this.BtnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // BtnPlay
            // 
            this.BtnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlay.BackColor = System.Drawing.Color.Transparent;
            this.BtnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnPlay.Image = global::Particulas2Dc.Properties.Resources.bullet_triangle_green;
            this.BtnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnPlay.Location = new System.Drawing.Point(903, 70);
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.Size = new System.Drawing.Size(33, 23);
            this.BtnPlay.TabIndex = 2;
            this.BtnPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnPlay.UseVisualStyleBackColor = true;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // FrmBuscaFich
            // 
            this.FrmBuscaFich.Filter = "Variables de Entorno|*.env";
            this.FrmBuscaFich.FileOk += new System.ComponentModel.CancelEventHandler(this.FrmBuscaFich_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 462);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnPlay);
            this.Controls.Add(this.cBoton2);
            this.Controls.Add(this.cBoton1);
            this.Name = "Form1";
            this.Text = "Pizarra";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private CTCBASE.cBoton cBoton1;
        private CTCBASE.cBoton cBoton2;
        private CTCBASE.cBoton BtnPlay;
        private CTCBASE.cBoton BtnStop;
        private System.Windows.Forms.FolderBrowserDialog FrmBuscaDir;
        private System.Windows.Forms.OpenFileDialog FrmBuscaFich;
    }
}

