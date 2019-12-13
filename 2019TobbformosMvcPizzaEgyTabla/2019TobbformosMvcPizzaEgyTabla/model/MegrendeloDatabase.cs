using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019TobbformosMvcPizzaEgyTabla.model
{
        partial class Megrendelo
        {
            public string getInsert()
            {
                return
                    "INSERT INTO `megrendelo` (`mazon`, `mnev`, `mcim`) " +
                    "VALUES ('" +
                    id +
                    "', '" +
                    getName() +
                    "', '" +
                    getLocation() +
                    "');";
            }

            public string getUpdate(int id)
            {
                return
                    "UPDATE `megrendelo` SET `mnev` = '" +
                    getName() +
                    "', `mcim` = '" +
                    getLocation() +
                    "' WHERE `mazon`.`mnev` = " +
                    id;
            }

            public static string getSQLCommandDeleteAllRecord()
            {
                return "DELETE FROM mnev";
            }

            public static string getSQLCommandGetAllRecord()
            {
                return "SELECT * FROM megrendelo";
            }
        }
}
