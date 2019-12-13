

using _2019TobbformosMvcPizzaEgyTabla.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobbbformosPizzaAlkalmazasEgyTabla.Repository;

namespace _2019TobbformosMvcPizzaEgyTabla.repository
{
        partial class MRepository
    {
            List<Megrendelo> megrendelok;

            public List<Megrendelo> getMegrendelok()
            {
                return megrendelok;
            }
            
            public void setMegrendelok(List<Megrendelo> megrendelok)
            {
                this.megrendelok = megrendelok;
            }

            public DataTable getMegrendeloDataTableFromList()
            {
                DataTable MegrendeloDT = new DataTable();
                MegrendeloDT.Columns.Add("azon", typeof(int));
                MegrendeloDT.Columns.Add("nev", typeof(string));
                MegrendeloDT.Columns.Add("cim", typeof(string));
                foreach (Megrendelo m in megrendelok)
                {
                    MegrendeloDT.Rows.Add(m.getId(), m.getName(), m.getLocation());
                }
                return MegrendeloDT;
            }

            private void fillMegrendeloListFromDataTable(DataTable MegrendeloDT)
            {
                foreach (DataRow row in MegrendeloDT.Rows)
                {
                    int azon = Convert.ToInt32(row[0]);
                    string nev = row[1].ToString();
                    string cim = row[2].ToString(); 
                    Megrendelo m = new Megrendelo(azon, nev, cim);
                    megrendelok.Add(m);
                }
            }

            public void deleteMegrendeloFromList(int id)
            {
                Megrendelo m = megrendelok.Find(x => x.getId() == id);
                if (m != null)
                    megrendelok.Remove(m);
                else
                    throw new RepositoryExceptionCantDelete("A Megrendelőt nem lehetett törölni.");
            }

            public void updateMegrendeloInList(int id, Megrendelo modified)
            {
                Megrendelo m = megrendelok.Find(x => x.getId() == id);
                if (m != null)
                    m.update(modified);
                else
                    throw new RepositoryExceptionCantModified("A Megrendelő módosítása nem sikerült");
            }

            public void addMegrendeloToList(Megrendelo ujMegrendelo)
            {
                try
                {
                    megrendelok.Add(ujMegrendelo);
                }
                catch (Exception e)
                {
                    throw new RepositoryExceptionCantAdd("A Megrendelő hozzáadása nem sikerült");
                }
            }

            public Megrendelo getMegrendelo(int id)
            {
                return megrendelok.Find(x => x.getId() == id);
            }

            public int getNextMegrendeloId()
            {
                if (megrendelok.Count == 0)
                    return 1;
                else
                    return megrendelok.Max(x => x.getId()) + 1;
            }





        }
    }

