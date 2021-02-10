using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace wpf1._3._5.Models
{
    class CarModel
    {
        private SqlConnection connection;
        public string Name;
        public string ImagePath;
        public int DillerId;
        public int Price;

        public CarModel()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ektu\course1\progtech\wpf1.3.5\wpf1.3.5\wpf1.3.5\db\CarsDillers.mdf;Integrated Security=True";
            this.connection = new SqlConnection(connectionString);
            /*connection.Open();
            connection.Close();*/
        }

        public List<CarItem> All()
        {
            string Query = "SELECT " +
                                    "CarsModels.Id as id, " +
                                    "CarsModels.name as name, " +
                                    "CarsModels.image as image, " +
                                    "CarsModels.price as price, " +
                                    "CarsModels.diller_id as diller_id, " +
                                    "CarsDillers.name as diller_name " +
                                    "FROM CarsModels INNER JOIN  CarsDillers ON CarsModels.diller_id = CarsDillers.Id";

            SqlDataReader reader = (SqlDataReader)this.MakeQuery(Query);

            List<CarItem> result = new List<CarItem>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                string name = Convert.ToString(reader["name"]);
                string image = Convert.ToString(reader["image"]);
                int price = Convert.ToInt32(reader["price"]);
                int dillerId = Convert.ToInt32(reader["diller_id"]);
                string dillerName = Convert.ToString(reader["diller_name"]);

                result.Add(new CarItem {
                    Id = id,
                    Name = name,
                    Image = image,
                    Price = price,
                    DillerId = dillerId,
                    DillerName = dillerName
                });
            }

            reader.Close();

            return result;
        }

        public DataSet AllAlt()
        {
            string command = "SELECT * FROM CarsModels";

            this.connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command, this.connection);

            DataSet result = new DataSet();

            adapter.Fill(result);

            this.connection.Close();
            return result;
        }

        public DataSet GetAllByDillerId(int dillerId)
        {
            string command = $"SELECT * FROM CarsModels WHERE diller_id={dillerId}";

            this.connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command, this.connection);

            DataSet result = new DataSet();

            adapter.Fill(result);

            this.connection.Close();
            return result;
        }

        public bool Save()
        {
            string Query = "INSERT INTO CarsModels([name], [image], [diller_id], [price]) VALUES (" +
                           $"'{this.Name}', " +
                           $"'{this.ImagePath}', " +
                           $"{this.DillerId}, '" +
                           Convert.ToString(this.Price) + "')";

            Console.WriteLine(Query);

            int result = (int)this.MakeQuery(Query);

            if(result > 0)
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
            } else
            {
                reader = command.ExecuteReader();
                return reader;
            }
           
            //this.connection.Close();
        }
    }

    class CarItem
    {
        public int Id;
        public string Name;
        public string Image;
        public int Price;
        public int DillerId;
        public string DillerName;
    }
}
