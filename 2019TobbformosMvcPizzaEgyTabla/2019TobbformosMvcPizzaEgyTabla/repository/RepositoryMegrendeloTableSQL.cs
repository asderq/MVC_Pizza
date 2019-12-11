using _2019TobbformosMvcPizzaEgyTabla.model;
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
        public List<Megrendelo> getMegrendeloFromDatabase()
        {
            List<Megrendelo> megrendelok = new List<Megrendelo>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = Megrendelo.getSQLCommandGetAllRecord();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["mnev"].ToString();
                    bool goodResult = false;
                    int id = -1;
                    goodResult = int.TryParse(dr["mazon"].ToString(), out id);
                    if (goodResult)
                    {
                        string location = dr["pcim"].ToString();                       
                        if (goodResult)
                        {
                            Megrendelo m = new Megrendelo(id, name, location);
                            megrendelok.Add(m);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Megrendelői beolvasása az adatbázisból nem sikerült!");
            }
            return megrendelok;
        }

        public void deleteMegrendeloFromDatabase(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM megrendelo WHERE mazon=" + id;
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(id + " idéjű megrendelő törlése nem sikerült.");
                throw new RepositoryException("Sikertelen törlés az adatbázisból.");
            }
        }

        public void updateMegrendeloInDatabase(int id, Megrendelo modified)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = modified.getUpdate(id);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(id + " idéjű megrendelő módosítása nem sikerült.");
                throw new RepositoryException("Sikertelen módosítás az adatbázisból.");
            }
        }

        public void insertPizzaToDatabase(Megrendelo ujMegrendelo)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = ujMegrendelo.getInsert();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(ujMegrendelo + " Megrendelő beszúrása adatbázisba nem sikerült.");
                throw new RepositoryException("Sikertelen beszúrás az adatbázisból.");
            }
        }
    }
}
