using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_1
{
    public partial class Form1 : Form
    {
        private Connection connectionDB;
        public Form1()
        {
            InitializeComponent();
        }
        //Cuando se carga el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            //Se inicializa la conexion
            connectionDB = new Connection();
            //Se cargan los datos de la tabla
            InitializeTable();
            
        }

        private void InitializeTable()
        {
            dataGridViewPerson.DataSource = null;//resetea la tabla
            dataGridViewPerson.DataSource = connectionDB.Select();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {

        }

    }
}
