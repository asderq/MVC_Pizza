using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019TobbformosMvcPizzaEgyTabla.repository
{
    class RepositoryMegrendeloDatabase
    {
        private readonly string connectionStringCreate;
        private readonly string connectionString;

        /// <summary>
        /// Konstruktor - kezdőértékadások
        /// </summary>
        public RepositoryMegrendeloDatabase()
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
    }
}

