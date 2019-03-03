using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Net;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Imaging;

namespace Escaner___Abogados_y_Notarios_Roldan
{
    public partial class Guardar : DevComponents.DotNetBar.Metro.MetroForm
    {
        string carpeta = @"C:\Escaner AYNR\";
        List<List<System.Drawing.Image>> Escaneado = null;
        public Guardar(List<List<System.Drawing.Image>> escaneado)
        {
            InitializeComponent();
            Escaneado = escaneado;
        }

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
                foreach (List<System.Drawing.Image> imagen in Escaneado)
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

        private void Guardar_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "31.170.166.58";
                builder.UserID = "u215283317_pb";
                builder.Password = "abogados";
                builder.Database = "u215283317_pb";
                MySqlConnection conn = new MySqlConnection(builder.ToString());
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM clientes WHERE visible='si'";
                MySqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    cbClientes.Items.Add(lector["id"] + ", " + lector["nombre"]);
                }
                lector.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text != "")
                {
                    this.Enabled = false;
                    MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                    builder.Server = "31.170.166.58";
                    builder.UserID = "u215283317_pb";
                    builder.Password = "abogados";
                    builder.Database = "u215283317_pb";
                    MySqlConnection conn = new MySqlConnection(builder.ToString());
                    MySqlCommand cmd = conn.CreateCommand();
                    string nombreServer = txtNombre.Text + "_" + DateTime.Now.ToString().Replace(' ', '_');
                    nombreServer = nombreServer.Replace('/', '_');
                    string tabla = "";
                    if (checkBox1.Checked)
                    {
                        tabla = "archivostrabajadores (trabajador_id, ";
                    }
                    else
                    {
                        tabla = "archivos (cliente_id, ";
                    }
                    cmd.CommandText = 
                        "INSERT INTO " + tabla + "nombre, nombre_servidor, descripcion, observaciones, extension, visible) value ('" + 
                        cbClientes.SelectedItem.ToString().Split(',')[0] +"', '" + txtNombre.Text + ".pdf', '" + nombreServer + ".pdf', '" + txtDescripcion.Text + "', '" 
                        + txtObservaciones.Text + "', '.pdf', 'si')";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    guardarPDF(txtNombre.Text);
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.abogadosynotariosroldan.com/" + nombreServer + ".pdf");
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential("u215283317.remoto", "abogadosroldan");
                    StreamReader sourceStream = new StreamReader(carpeta + txtNombre.Text + ".pdf");
                    byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                    sourceStream.Close();
                    request.ContentLength = fileContents.Length;

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                    Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

                    response.Close();
                    MessageBox.Show("Archivo cargado con éxito", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe llenar el campo Nombre para continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                lblParaQuien.Text = "Trabajador";
                try
                {
                    MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                    builder.Server = "31.170.166.58";
                    builder.UserID = "u215283317_pb";
                    builder.Password = "abogados";
                    builder.Database = "u215283317_pb";
                    MySqlConnection conn = new MySqlConnection(builder.ToString());
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM trabajadores WHERE visible='si'";
                    MySqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        cbClientes.Items.Add(lector["id"] + ", " + lector["nombre"]);
                    }
                    lector.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    this.Enabled = true;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                lblParaQuien.Text = "Cliente";
                try
                {
                    MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                    builder.Server = "31.170.166.58";
                    builder.UserID = "u215283317_pb";
                    builder.Password = "abogados";
                    builder.Database = "u215283317_pb";
                    MySqlConnection conn = new MySqlConnection(builder.ToString());
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM clientes WHERE visible='si'";
                    MySqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        cbClientes.Items.Add(lector["id"] + ", " + lector["nombre"]);
                    }
                    lector.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    this.Enabled = true;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
    }
}