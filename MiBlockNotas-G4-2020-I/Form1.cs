using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiBlockNotas_G4_2020_I
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CopiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richtxtbBlocNotas.SelectedText != string.Empty)
            {
                Clipboard.SetData(DataFormats.Text, richtxtbBlocNotas.SelectedText);
            }
        }

        private void PegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int posicion = richtxtbBlocNotas.SelectionStart;
            richtxtbBlocNotas.Text = richtxtbBlocNotas.Text.Insert(posicion, Clipboard.GetText());
        }
    }
}
