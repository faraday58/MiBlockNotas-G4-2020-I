using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MiBlockNotas_G4_2020_I
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Texto (*.txt)| *.txt";

                if(saveFileDialog.ShowDialog()== DialogResult.OK )
                {
                    fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(txtbAutor.Text + "," + txtbNombre.Text + "," + txtbFecha.Text);
                    for(int i=0; i < richtxtbBlocNotas.Lines.Length; i++ )
                    {
                        sw.WriteLine(richtxtbBlocNotas.Lines[i]);
                    }
                }
                else
                {
                    fs = new FileStream("Actividad.txt", FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine("Intento de crear archivo");
                }

            }
            catch(IOException error)
            {
                MessageBox.Show(error.Message, "Error al intentar guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }

        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            StreamReader sr = null;
            BinaryReader br = null;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Texto (*.txt)|*.txt|Binarios (*.bin)|*.bin|Todos los Archivos (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(openFileDialog.FileName);
                    if(extension ==".bin")
                    { 
                        fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                        br = new BinaryReader(fs);
                        double fecha = br.ReadDouble();
                        string autor = br.ReadString();

                        txtbAutor.Text = autor;
                        txtbFecha.Text = fecha.ToString();
                       

                    }
                    else
                    {
                        fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                        sr = new StreamReader(fs);
                        string cadena = sr.ReadLine();
                        string[] cabeceras = cadena.Split(',');
                        // txtbAutor.Text = cabeceras[0];
                        txtbAutor.Text = File.GetAccessControl(openFileDialog.FileName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();

                        //txtbNombre.Text = cabeceras[1];
                        DirectoryInfo directoryInfo = new DirectoryInfo(openFileDialog.FileName);
                        txtbNombre.Text = directoryInfo.Name;
                        // txtbFecha.Text = cabeceras[2];
                        txtbFecha.Text = File.GetCreationTime(openFileDialog.FileName).ToShortDateString();

                        string aux = "";

                        while (cadena != null)
                        {
                            cadena = sr.ReadLine();

                            aux = aux + cadena + "\n";
                        }
                        richtxtbBlocNotas.Text = aux;

                    }


                }



            }
            catch(IOException error)
            {
                MessageBox.Show(error.Message, "Error al intentar Abrir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if( sr != null )
                {
                    sr.Close();
                }
                if(fs != null)
                {
                    fs.Close();
                }
                if( br !=null)
                {
                    br.Close();

                }
                
            }
           
        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            BinaryWriter bw = null;

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Binario(*.bin)|*.bin";
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    bw = new BinaryWriter(fs);
                    string fecha = txtbFecha.Text;
                    double dfecha = double.Parse(fecha);
                    string autor = txtbAutor.Text;

                    bw.Write(dfecha);
                    bw.Write(autor);
                                                         
                }
            }
            catch(IOException error)
            {
                MessageBox.Show(error.Message, "Error al guardar");
            }
            finally
            {
                bw.Close();
                fs.Close();
            }
        }

        private void DestruirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Texto(*.txt)|*.txt";
                if(openFileDialog.ShowDialog() == DialogResult.OK )
                {
                    fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Write);
                    Random aleatorio = new Random();

                    for(int i=1; i <= 10; i++  )
                    {
                        fs.Seek(i, SeekOrigin.Begin);
                        fs.WriteByte((byte)aleatorio.Next(255));
                    }

                    for (int i=10; i >= 1; i--)
                    {
                        fs.Seek(-i, SeekOrigin.End);
                        fs.WriteByte((byte)aleatorio.Next(255));
                    }
                }
            }
            catch(IOException error)
            {
                MessageBox.Show(error.Message, "Error al intentar abrir");
            }
            finally
            {
                if( fs != null)
                {
                    fs.Close();
                }
                
            }
        }
    }
}
