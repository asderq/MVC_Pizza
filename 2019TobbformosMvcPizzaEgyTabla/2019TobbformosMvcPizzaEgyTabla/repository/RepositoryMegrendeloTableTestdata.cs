
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobbbformosPizzaAlkalmazasEgyTabla.Repository;

namespace _2019TobbformosMvcPizzaEgyTabla.repository
{

    partial class RepositoryMegrendeloTable
    {
        /*     public void fillPizzasWithTestDataFromTestData(List<Pizza> pizzas)
             {
                 MySqlConnection connection = new MySqlConnection(connectionString);

                 try
                 {
                     connection.Open();
                     foreach (Pizza p in pizzas)
                     {
                         string query = p.getInsert();
                         MySqlCommand cmd = new MySqlCommand(query, connection);
                         cmd.ExecuteNonQuery();
                     }
                     connection.Close();
                 }
                 catch (Exception e)
                 {
                     connection.Close();
                     Debug.WriteLine(e.Message);
                     throw new RepositoryException("Tesztadatok feltöltése sikertelen.");
                 }
             }*/

        public void fillPizzasWithTestDataFromSQLCommand()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                string query =
                    "INSERT INTO `megrendelo` (`mazon`, `mnev`, `mcim`) VALUES " +
                            " (1, 'Lázár Martin', Kis utca 96), " +
                            " (2, 'Székely Szabó Csanád', Nagy utca 11), " +
                            " (3, 'Lakatos Brendon', Bajnok utca 74), " +
                            " (4, 'Hibá Zoltán', Andrássy utca 23), " +
                            " (5, 'Valami János', Csillag utca 44); ";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Tesztadatok feltöltése sikertelen.");
            }
        }

    }
}
