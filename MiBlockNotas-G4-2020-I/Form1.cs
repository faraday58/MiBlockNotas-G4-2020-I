﻿using System;
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
                    fs = new FileStream("error_log.txt", FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine("Error al intentar guardar archivo " + File.GetLastWriteTime("error_log.txt"));
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
            

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Texto (*.txt)|*.txt|Todos los Archivos (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
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
                    sr.Close();

                }
                else
                {
                    StreamWriter sw = null;
                    fs = new FileStream("error_Abrir.txt", FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine("Error al intentar abrir  archivo " + File.GetLastWriteTime("error_Abrir.txt"));
                    sw.Close();
                }


            }
            catch(IOException error)
            {
                MessageBox.Show(error.Message, "Error al intentar Abrir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                fs.Close();

            }
           
        }

        private void DestruirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Texto (*.txt)|*.txt|Todos los Archivos (*.*)|*.*";

                if(openFileDialog.ShowDialog()== DialogResult.OK)
                {
                    fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Write);
                    Random aleatorio = new Random();

                    for (int i = 0; i <= 14; i++)
                    {
                        fs.Seek(i, SeekOrigin.Begin);
                        fs.WriteByte((byte)aleatorio.Next(255));
                    }
                }
                

            }
            catch (IOException error)
            {
                Console.WriteLine(" error  " + error.Message);
            }

            fs.Close();
        }
    }
}
