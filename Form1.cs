using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exportar
{
    public partial class Form1 : Form
    {
        Acciones acc = new Acciones();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            DGdatos.DataSource = acc.Mostrar();
        }

        

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (acc.ExportarExcel())
                MessageBox.Show("Exportado con exito...");
            else
                MessageBox.Show("No se exporto...");

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {

            if (acc.Importar())
                MessageBox.Show("Importado con exito...");
            else
                MessageBox.Show("No se Importo...");
        }
    }
}
