using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace CRUD_1
{
    /**
     * Esta clase contiene los atributos y metodos para establecer una conexion
     * con una base de datos postgresSQL
     */
    public class Connection
    {
        //Se crea un objeto de conexion
        NpgsqlConnection connection;

        //Propiedades de la conexion
        private string server = "localhost";
        private string port = "5432";
        private string user = "postgres";
        private string password = "2513";
        private string database = "crud-1";

        //Formato de la conexion
        private string connectionFormat;

        //Indica si el objeto esta conectado
        private bool isConnected = false;

        //Constructor
        public Connection()
        {
            //Estable la sintaxis de la conexion
            SetConnectionFormat();
            //Se establece la sintaxis de la conexion, pero aun no se ha establecido
            connection = new NpgsqlConnection(connectionFormat);
        }

        //Establece la sintaxis para abrir la conexion y la asigana a connectionFormat
        private void SetConnectionFormat()
        {
            connectionFormat = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", server, port, user, password, database);
        }

        //Retorna verdadero si esta conectado o falso si no lo esta
        public bool IsConnected()
        {
            return isConnected;
        }

        public void Connect()
        {
            //Se intenta conectar
            try
            {
                //Si tadavia no hay una conexion, ya que dara error si se intenta abrir una conexion ya inicida
                if (!isConnected)
                {
                    //Abre conexion con la base de datos
                    connection.Open();
                }
                //Indica que estamos conectados
                isConnected = true;
            }//Si no se logro establecer la conexion se indica
            catch (PostgresException ex)
            {
                MessageBox.Show(String.Format("No se pudo establecer conexcion en: {0}/nError: {1}", connectionFormat, ex.Message));
            }
        }

        //Ciera la conexion con la base de datos
        public void Disconnect()
        {
            //Se cierra la conexion
            connection.Close();
            //Cambia el estado de la conexion
            isConnected = false;
            MessageBox.Show(String.Format("Se cerro la conexcion en: {0}", connectionFormat));
        }

        //
        public DataTable Select()
        {
            //Se estable una conexion con la base de datos
            Connect();
            //Creamos una tabla para guardar los datos
            DataTable dataTablePersonas = new DataTable();

            if (isConnected)
            {
                //Tenemos el comando en un string
                string commandText = @"select * from person_select()";
                //Esta variable ejecuta comandos SQL
                NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
                //Se intententa ejecutar el comando
                try
                {
                    //INVESTIGATE
                    dataTablePersonas.Load(command.ExecuteReader());
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Error: {0}", ex.Message));
                }
                
                //Cerramos la conexion
                Disconnect();
                return dataTablePersonas;
            } else
            {
                MessageBox.Show("No se pudo establecer conexion con la base de datos");
            }

            return dataTablePersonas;
        }
    }
}
