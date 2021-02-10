using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace wpf1._3._5.Models
{
    class Cart
    {
        SqlConnection connection;
        public Cart()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ektu\course1\progtech\wpf1.3.5\wpf1.3.5\wpf1.3.5\db\CarsDillers.mdf;Integrated Security=True";
            this.connection = new SqlConnection(connectionString);
        }

        public DataSet AllAlt()
        {
            string command = "SELECT " +
                                    "CarsModels.Id as id, " +
                                    "CarsModels.name as name, " +
                                    //"CarsModels.image as image, " +
                                    "CarsModels.price as price " +
                                    //"CarsModels.diller_id as diller_id, " +
                                    //"Cart.Id as order_id " +
                                    "FROM CarsModels INNER JOIN  Cart ON CarsModels.Id = Cart.ModelId";

            this.connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command, this.connection);

            DataSet result = new DataSet();

            adapter.Fill(result);

            this.connection.Close();
            return result;
        }

        public bool Add(int id)
        {
            string Query = $"INSERT INTO Cart ([ModelId]) VALUES ({id})";

            Console.WriteLine(Query);

            int result = (int)this.MakeQuery(Query);

            if (result > 0)
                return true;
            return false;
        }

        private object MakeQuery(string query)
        {
            this.connection.Open();
            SqlCommand command = new SqlCommand();

            command.CommandText = query;
            command.Connection = this.connection;

            Console.WriteLine(command.CommandText);

            int intReader;
            SqlDataReader reader;

            if (command.CommandText.IndexOf("INSERT") >= 0)
            {
                intReader = command.ExecuteNonQuery();

                return intReader;
            }
            else
            {
                reader = command.ExecuteReader();
                return reader;
            }

            //this.connection.Close();
        }
    }
}

