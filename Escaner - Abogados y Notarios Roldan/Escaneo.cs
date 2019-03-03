using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Imaging;

namespace Escaner___Abogados_y_Notarios_Roldan
{
    public partial class Escaneo : DevComponents.DotNetBar.Metro.MetroForm
    {
        string carpeta = @"C:\Escaner AYNR\";
        List<List<System.Drawing.Image>> escaneado = new List<List<System.Drawing.Image>>();
        List<string> devices = WIAScanner.GetDevicesString();
        String escanerActual = "";

        private String guardarPDF(string nombre)
        {
            try
            {
                if (!System.IO.Directory.Exists(carpeta))
                {
                    System.IO.Directory.CreateDirectory(carpeta);
                }

                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc,
                                            new FileStream(carpeta + nombre + ".pdf", FileMode.Create));
                doc.AddTitle("input");
                doc.AddCreator(Environment.UserName);
                DateTime fecha = DateTime.Now;
                doc.AddCreationDate();
                doc.Open();
                iTextSharp.text.Image ImagenGuardar = null;
                if (!System.IO.Directory.Exists(carpeta + @"temp"))
                {
                    System.IO.Directory.CreateDirectory(carpeta + @"temp");
                }
                String nombreTemp = "";
                int cantidad = 0;
                foreach (List<System.Drawing.Image> imagen in escaneado)
                {
                    nombreTemp = carpeta + @"temp\" + cantidad.ToString() + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpeg";
                    imagen[0].Save(nombreTemp, ImageFormat.Jpeg);
                    ImagenGuardar = iTextSharp.text.Image.GetInstance(nombreTemp);
                    ImagenGuardar.BorderWidth = 0;
                    ImagenGuardar.Alignment = Element.ALIGN_CENTER;
                    float percentage = 0.0f;
                    percentage = 525 / ImagenGuardar.Width;
                    ImagenGuardar.ScalePercent(percentage * 100);

                    // Insertamos la imagen en el documento
                    doc.Add(ImagenGuardar);
                }
                doc.Close();
                if (System.IO.Directory.Exists(carpeta + @"temp"))
                {
                    IEnumerable<String> archivos = System.IO.Directory.EnumerateFiles((carpeta + @"temp"));
                    foreach (var archivo in archivos)
                    {
                        System.IO.File.Delete(archivo);
                    }
                    System.IO.Directory.Delete(carpeta + @"temp");
                }
                return carpeta + nombre + ".pdf";
            }
            catch (Exception ex)
            {
                throw new Exception("Ya existe un archivo con ese nombre.");
            }

        }

        public Escaneo()
        {
            InitializeComponent();
        }

        private void Escaneo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void seleccionarEscaner(object sender, EventArgs e)
        {
            try
            {
                DevComponents.DotNetBar.ButtonItem button = sender as DevComponents.DotNetBar.ButtonItem;
                for (int i = 0; i < devices.Count; i++)
                {
                    if (devices[i].Split('|')[0] == button.Text)
                    {
                        escanerActual = devices[i].Split('|')[1];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Escaneo_Load(object sender, EventArgs e)
        {
            try
            {
                if (devices.Count != 0)
                {
                    DevComponents.DotNetBar.BaseItem[] botones = new DevComponents.DotNetBar.BaseItem[devices.Count];
                    int i = 0;
                    foreach (string device in devices)
                    {
                        botones[i] = new DevComponents.DotNetBar.ButtonItem();
                        botones[i].Text = device.Split('|')[0];
                        botones[i].Click += new System.EventHandler(this.seleccionarEscaner);
                        i++;
                    }
                    btnEscaners.SubItems.AddRange(botones);
                }
                else
                {
                    btnEscaners.SubItems.Add(new DevComponents.DotNetBar.ButtonItem("btnConectados", "No hay escaners conectados"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEscanear_Click(object sender, EventArgs e)
        {
            try
            {
                List<System.Drawing.Image> images = WIAScanner.Scan(escanerActual);
                escaneado.Add(images);
                PictureBox muestra = null;
                foreach (System.Drawing.Image image in images)
                {
                    muestra = new PictureBox();
                    muestra.Width = 150;
                    muestra.Height = 200;
                    muestra.BackgroundImage = image;
                    muestra.BackgroundImageLayout = ImageLayout.Stretch;
                    flpContenedorImagenes.Controls.Add(muestra);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarCargar_Click(object sender, EventArgs e)
        {
            Guardar guardar = new Guardar(escaneado);
            guardar.ShowDialog();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre para el documento", "Documento", "", -1, -1);
            bool continuar = false;
            if (input != "")
            {
                continuar = true;
            }
            while (!continuar)
            {
                input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre para el documento", "Documento", "", -1, -1);
                if (input != "")
                {
                    continuar = true;
                }
            }
            guardarPDF(input);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            escaneado = new List<List<System.Drawing.Image>>();
            escanerActual = "";
            flpContenedorImagenes.Controls.Clear();
            devices = WIAScanner.GetDevicesString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}