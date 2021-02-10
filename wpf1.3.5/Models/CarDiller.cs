using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace wpf1._3._5.Models
{
    class CarDiller
    {
        private SqlConnection connection;
        public CarDiller()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ektu\course1\progtech\wpf1.3.5\wpf1.3.5\wpf1.3.5\db\CarsDillers.mdf;Integrated Security=True";
            this.connection = new SqlConnection(connectionString);
        }

        public List<CarDillerItem> All()
        {
            this.connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CarsDillers";
            command.Connection = this.connection;

            Console.WriteLine(command.CommandText);
            SqlDataReader reader = command.ExecuteReader();

            List<CarDillerItem> result = new List<CarDillerItem>();


            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                string name = Convert.ToString(reader["name"]);
                string logo= Convert.ToString(reader["logo"]);

                result.Add(new CarDillerItem
                {
                    Id = id,
                    Name = name,
                    Logo = logo,
                });
            }

            reader.Close();

            return result;
        }

        public DataSet AllAlt()
        {
            string command = "SELECT * FROM CarsDillers";

            this.connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command, this.connection);

            DataSet result = new DataSet();

            adapter.Fill(result);

            this.connection.Close();
            return result;
        }
    }

    class CarDillerItem
    {
        public int Id;
        public string Name;
        public string Logo;
    }
}
