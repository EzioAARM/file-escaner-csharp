namespace Escaner___Abogados_y_Notarios_Roldan
{
    partial class Escaneo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Escaneo));
            this.metroStatusBar1 = new DevComponents.DotNetBar.Metro.MetroStatusBar();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btnEscaners = new DevComponents.DotNetBar.ButtonItem();
            this.btnGuardar = new DevComponents.DotNetBar.ButtonItem();
            this.btnGuardarCargar = new DevComponents.DotNetBar.ButtonItem();
            this.btnNuevo = new DevComponents.DotNetBar.ButtonItem();
            this.btnSalir = new DevComponents.DotNetBar.ButtonItem();
            this.btnEscanear = new DevComponents.DotNetBar.ButtonItem();
            this.flpContenedorImagenes = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroStatusBar1
            // 
            // 
            // 
            // 
            this.metroStatusBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroStatusBar1.ContainerControlProcessDialogKey = true;
            this.metroStatusBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroStatusBar1.DragDropSupport = true;
            this.metroStatusBar1.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroStatusBar1.Location = new System.Drawing.Point(0, 729);
            this.metroStatusBar1.Name = "metroStatusBar1";
            this.metroStatusBar1.Size = new System.Drawing.Size(1338, 22);
            this.metroStatusBar1.TabIndex = 0;
            this.metroStatusBar1.Text = "metroStatusBar1";
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.btnEscanear});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1338, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 1;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // buttonItem1
            // 
            this.buttonItem1.AutoExpandOnClick = true;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.buttonItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnEscaners,
            this.btnGuardar,
            this.btnGuardarCargar,
            this.btnNuevo,
            this.btnSalir});
            this.buttonItem1.Text = "Archivo";
            // 
            // btnEscaners
            // 
            this.btnEscaners.AutoExpandOnClick = true;
            this.btnEscaners.Name = "btnEscaners";
            this.btnEscaners.Text = "Escaners conectados";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Text = "Guardar Archivo";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnGuardarCargar
            // 
            this.btnGuardarCargar.Name = "btnGuardarCargar";
            this.btnGuardarCargar.Text = "Guardar y cargar archivo";
            this.btnGuardarCargar.Click += new System.EventHandler(this.btnGuardarCargar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Text = "Nuevo Documento";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnEscanear
            // 
            this.btnEscanear.Name = "btnEscanear";
            this.btnEscanear.Text = "Escanear";
            this.btnEscanear.Click += new System.EventHandler(this.btnEscanear_Click);
            // 
            // flpContenedorImagenes
            // 
            this.flpContenedorImagenes.AutoScroll = true;
            this.flpContenedorImagenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpContenedorImagenes.Location = new System.Drawing.Point(0, 25);
            this.flpContenedorImagenes.Name = "flpContenedorImagenes";
            this.flpContenedorImagenes.Size = new System.Drawing.Size(1338, 704);
            this.flpContenedorImagenes.TabIndex = 2;
            // 
            // Escaneo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 751);
            this.Controls.Add(this.flpContenedorImagenes);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.metroStatusBar1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Escaneo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Escaneo - Abogados y Notarios Roldan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Escaneo_FormClosing);
            this.Load += new System.EventHandler(this.Escaneo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Metro.MetroStatusBar metroStatusBar1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem btnGuardar;
        private DevComponents.DotNetBar.ButtonItem btnGuardarCargar;
        private DevComponents.DotNetBar.ButtonItem btnNuevo;
        private DevComponents.DotNetBar.ButtonItem btnSalir;
        private DevComponents.DotNetBar.ButtonItem btnEscaners;
        private DevComponents.DotNetBar.ButtonItem btnEscanear;
        private System.Windows.Forms.FlowLayoutPanel flpContenedorImagenes;
    }
}