using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TobbbformosPizzaAlkalmazasEgyTabla.Repository;
using TobbbformosPizzaAlkalmazasEgyTabla.Model;
using System.Diagnostics;
using _2019TobbformosMvcPizzaEgyTabla.repository;
using _2019TobbformosMvcPizzaEgyTabla.model;

namespace _2019TobbformosMvcPizzaEgyTabla
{
    public partial class FormPizzaFutarKft : Form
    {
        /// <summary>
        /// Pizzákat tartalmazó adattábla
        /// </summary>
        private DataTable MegrendeloDT = new DataTable();
        /// <summary>
        /// Tárolja a pizzákat listában
        /// </summary>
        private MRepository mrepo = new MRepository();

        bool ujAdatMegadas = false;

        private void buttonMegrendeloBetoltes_Click(object sender, EventArgs e)
        {
            //Adatbázisban pizza tábla kezelése
            RepositoryMegrendeloTable rmp = new RepositoryMegrendeloTable();
            //A repo-ba lévő pizza listát feltölti az adatbázisból
            mrepo.setMegrendelok(rmp.getMegrendeloFromDatabase());
            RefreshDataToDataGridview();
            beallitPizzaDataGriViewt();
            setButtonsForStart();

            dataGridViewMegrendelo.SelectionChanged += dataGridViewMegrendelo_SelectionChanged;


        }

        private void setButtonsForStart()
        {
            panelMegrendelok.Visible = false;
            panelMódosítTörölGombok.Visible = false;
            if (dataGridViewMegrendelo.SelectedRows.Count != 0)
                buttonMegrendeloUj.Visible = false;
            else
                buttonMegrendeloUj.Visible = true;
        }


        private void DataGridViewMegrendelo_SelectionChanged(object sender, EventArgs e)
        {

            if (ujAdatMegadas)
            {
                beallitGombokatKattintaskor();
            }
            if (dataGridViewPizzak.SelectedRows.Count == 1)
            {
                panelMegrendelok.Visible = true;
                panelMódosítTörölGombok.Visible = true;
                buttonMegrendeloUj.Visible = true;
                textBoxMegrendeloAzon.Text =
                    dataGridViewMegrendelo.SelectedRows[0].Cells[0].Value.ToString();
                textBoxMegrendeloNev.Text =
                    dataGridViewMegrendelo.SelectedRows[0].Cells[1].Value.ToString();
                textBoxMegrendeloCim.Text =
                    dataGridViewMegrendelo.SelectedRows[0].Cells[2].Value.ToString();
            }
            else
            {
                panelMegrendelok.Visible = false;
                panelMódosítTörölGombok.Visible = false;
                buttonMegrendeloUj.Visible = false;
            }
        }



        private void RefreshDataToDataGridview()
        {
            //Adattáblát feltölti a mrepoba lévő pizza listából
            MegrendeloDT = mrepo.getMegrendeloDataTableFromList();
            //Megrendelo DataGridView-nak a forrása a megrendelo adattábla
            dataGridViewMegrendelo.DataSource = null;
            dataGridViewMegrendelo.DataSource = MegrendeloDT;
        }




        private void setMegrendeloDataGriView()
        {
            MegrendeloDT.Columns[0].ColumnName = "Azonosító";
            MegrendeloDT.Columns[0].Caption = "Megrendelő azonosító";
            MegrendeloDT.Columns[1].ColumnName = "Név";
            MegrendeloDT.Columns[1].Caption = "Megrendelő név";
            MegrendeloDT.Columns[2].ColumnName = "Cím";
            MegrendeloDT.Columns[2].Caption = "Megrendelő cím";

            dataGridViewMegrendelo.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMegrendelo.ReadOnly = true;
            dataGridViewMegrendelo.AllowUserToDeleteRows = false;
            dataGridViewMegrendelo.AllowUserToAddRows = false;
            dataGridViewMegrendelo.MultiSelect = false;
        }


        private void buttonMegrendeloTorol_Click(object sender, EventArgs e)
        {
            torolHibauzenetet();
            if ((dataGridViewMegrendelo.Rows == null) ||
                (dataGridViewMegrendelo.Rows.Count == 0))
                return;
            //A felhasználó által kiválasztott sor a DataGridView-ban            
            int sor = dataGridViewMegrendelo.SelectedRows[0].Index;
            if (MessageBox.Show(
                "Valóban törölni akarja a sort?",
                "Törlés",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //1. törölni kell a listából
                int id = -1;
                if (!int.TryParse(
                         dataGridViewMegrendelo.SelectedRows[0].Cells[0].Value.ToString(),
                         out id))
                    return;
                try
                {
                    mrepo.deleteMegrendeloFromList(id);
                }
                catch (RepositoryExceptionCantDelete recd)
                {
                    kiirHibauzenetet(recd.Message);
                    Debug.WriteLine("A megrendelo törlés nem sikerült, nincs a listába!");
                }
                //2. törölni kell az adatbázisból
                RepositoryMegrendeloTable rmt = new RepositoryMegrendeloTable();
                try
                {
                    rmt.deleteMegrendeloFromDatabase(id);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. frissíteni kell a DataGridView-t  
                RefreshDataToDataGridview();
                if (dataGridViewMegrendelo.SelectedRows.Count <= 0)
                {
                    buttonMegrendeloUj.Visible = true;
                }
                setMegrendeloDataGriView();
            }

        }

         private void buttonMegrendeloModosit_Click(object sender, EventArgs e)
         {
            torolHibauzenetet();
            errorProviderMegrendeloNev.Clear();
            errorProviderMegrendeloCim.Clear();
            try
            {
                Megrendelo modosult = new Megrendelo(
                    Convert.ToInt32(textBoxMegrendeloAzon.Text),
                    textBoxMegrendeloNev.Text,
                    textBoxMegrendeloCim.Text
                    );
                int azonosito = Convert.ToInt32(textBoxMegrendeloAzon.Text);
                //1. módosítani a listába
                try
                {
                    mrepo.updateMegrendeloInList(azonosito, modosult);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                    return;
                }
                //2. módosítani az adatbáziba
                RepositoryMegrendeloTable rmt = new RepositoryMegrendeloTable(); 
                try
                {
                    rmt.updateMegrendeloInDatabase(azonosito, modosult);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. módosítani a DataGridView-ban           
                RefreshDataToDataGridview();
            }
            catch (ModelMegrendeloNotValidNameExeption mvn)
            {
                errorProviderMegrendeloNev.SetError(textBoxMegrendeloNev, mvn.Message);
            }
            catch (ModelMegrendeloNotValidLocationExeption nlp)
            {
                errorProviderMegrendeloCim.SetError(textBoxMegrendeloCim, nlp.Message);
            }
            catch (RepositoryExceptionCantModified recm)
            {
                kiirHibauzenetet(recm.Message);
                Debug.WriteLine("Módosítás nem sikerült, a Megrendelő nincs a listába!");
            }
            catch (Exception ex)
            {

            }

         }

        private void buttonMegrendeloUjMent_Click(object sender, EventArgs e)
        {

            torolHibauzenetet();
            errorProviderMegrendeloNev.Clear();
            errorProviderMegrendeloCim.Clear();
            try
            {
                Megrendelo ujMegrendelo = new Megrendelo(
                    Convert.ToInt32(textBoxMegrendeloAzon.Text),
                    textBoxMegrendeloNev.Text,
                    textBoxMegrendeloCim.Text
                    );
                int azonosito = Convert.ToInt32(textBoxMegrendeloAzon.Text);
                //1. módosítani a listába
                try
                {
                    mrepo.addMegrendeloToList(ujMegrendelo);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                    return;
                }
                //2. Hozzáadni az adatbázishoz
                RepositoryMegrendeloTable rmt = new RepositoryMegrendeloTable();
                try
                {
                    rmt.insertPizzaToDatabase(ujMegrendelo);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. módosítani a DataGridView-ban           
                RefreshDataToDataGridview();
            }
            catch (ModelMegrendeloNotValidNameExeption mvn)
            {
                errorProviderMegrendeloNev.SetError(textBoxMegrendeloNev, mvn.Message);
            }
            catch (ModelMegrendeloNotValidLocationExeption nlp)
            {
                errorProviderMegrendeloCim.SetError(textBoxMegrendeloCim, nlp.Message);
            }
            catch (RepositoryExceptionCantModified recm)
            {
                kiirHibauzenetet(recm.Message);
                Debug.WriteLine("Módosítás nem sikerült, a Megrendelő nincs a listába!");
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonMegrendeloUj_Click(object sender, EventArgs e)
        {

            ujAdatMegadas = true;
            beallitGombokatTextboxokatUjPizzanal();
            int ujPizzaAzonosito = repo.getNextPizzaId();
            textBoxPizzaAzonosito.Text = ujPizzaAzonosito.ToString();





        }







    }
}
