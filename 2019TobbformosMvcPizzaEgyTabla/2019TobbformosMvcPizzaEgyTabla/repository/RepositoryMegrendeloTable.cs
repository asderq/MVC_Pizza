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
        private readonly string connectionStringCreate;
        private readonly string connectionString;

        /// <summary>
        /// Konstruktor - kezdőértékadások
        /// </summary>
        public RepositoryMegrendeloTable()
        {
            connectionStringCreate =
                "SERVER=\"localhost\";"
                + "DATABASE=\"test\";"
                + "UID=\"root\";"
                + "PASSWORD=\"\";"
                + "PORT=\"3306\";";
            connectionString =
                "SERVER=\"localhost\";"
                + "DATABASE=\"csarp\";"
                + "UID=\"root\";"
                + "PASSWORD=\"\";"
                + "PORT=\"3306\";";
        }

        /// <summary>
        /// csarp adatbázisban megrendelo tábla létrehozása
        /// </summary>
        public void createTableMegrendelo()
        {
            string queryUSE = "USE csarp;";
            string queryCreateTable =
                "CREATE TABLE `megrendelo` ( " +
                "   `mazon` int(3) NOT NULL DEFAULT '0', " +
                "   `mnev` varchar(40) COLLATE utf8_hungarian_ci NOT NULL DEFAULT '', " +
                "   `mcim` varchar(30) NOT NULL DEFAULT '', " +
            ")ENGINE = InnoDB; ";
            string queryPrimaryKey =
                "ALTER TABLE `megrendelo`  ADD PRIMARY KEY(`mazon`); ";

            MySqlConnection connection =
                new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand cmdUSE = new MySqlCommand(queryUSE, connection);
                cmdUSE.ExecuteNonQuery();
                MySqlCommand cmdCreateTable = new MySqlCommand(queryCreateTable, connection);
                cmdCreateTable.ExecuteNonQuery();
                MySqlCommand cmdPrimaryKey = new MySqlCommand(queryPrimaryKey, connection);
                cmdPrimaryKey.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Tábla lérehozása sikertelen.");
            }
        }

        /// <summary>
        /// pizza tábla törlése csarp adatbázisból
        /// </summary>
        public void deleteTableMegrendelo()
        {
            string query =
                "USE csarp; " +
                "DROP TABLE megrendelo;";

            MySqlConnection connection =
                new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Tábla törlése nem sikerült.");
            }
        }

        /*public void deleteDataFromTable()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = Pizza.getSQLCommandDeleteAllRecord();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Tesztadatok törlése sikertelen.");
            }
        }*/
    }
}
