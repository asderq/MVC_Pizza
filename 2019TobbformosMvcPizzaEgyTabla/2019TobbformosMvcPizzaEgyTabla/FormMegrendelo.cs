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
            frissitAdatokkalDataGriedViewt();
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
                panelPizza.Visible = false;
                panelModositTorolGombok.Visible = false;
                buttonUjPizza.Visible = false;
            }
        }





    }
}
