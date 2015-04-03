﻿using System;
using System.Windows.Forms;

namespace ICT4Events_user_interface
{
    public partial class Form1 : Form
    {
        //private Enum e = new Enum;
        


        public Form1()
        {
            InitializeComponent();
            dtpDatumVan.MinDate = DateTime.Today;
            dtpDatumTot.MinDate = DateTime.Today;
            DateTime oneYearAgoToday = DateTime.Now.AddYears(-18);
            dtpGeboortedatum.MaxDate = oneYearAgoToday;
            for(int i = 1; i < 101; i++)
            {
                cbAantalPersonen.Items.Add(i);
            }

            cbVoorkeursplek.Items.AddRange(new string[] { "Lawaai", "Schaduw", "Noodafstand vanaf facaliteiten*", "Rookvrij", "Adults Only Area"});
            //*Noodafstand houdt in dat het binnen 2 minuten te lopen is vanaf je staplek
            cbKampeerplaats.Items.AddRange(new string[] { "Comfortplaatsen", "Huurtentjes", "Plaatsen voor eigen tenten", "Stacaravans", "Invalidenaccomodaties", "Bungalows", "Blokhutten", "Bungalinos" });
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dtpDatumVan_ValueChanged(object sender, EventArgs e)
        {
            DateTime reservatievan = new DateTime();
            reservatievan = dtpDatumVan.Value;
            dtpDatumTot.MinDate = reservatievan;
            dtpDatumTot.Refresh();
        }

        private void btReserveer_Click(object sender, EventArgs e)
        {
            if(tbVoornaam.Text == "" || tbAchternaam.Text == "" || tbWoonplaats.Text == "" || tbPostcodegetal.Text == "" || tbPostcodeletter.Text == "" || tbTelefoonnummer.Text == "" || tbEmail.Text == "")
            {
                MessageBox.Show("Vul alle velden in!");
            }
        }

        private void btvoegpersoontoe_Click(object sender, EventArgs e)
        {
            if (tbVoornaam.Text == "" || tbAchternaam.Text == "" || tbWoonplaats.Text == "" || tbPostcodegetal.Text == "" || tbPostcodeletter.Text == "" || tbTelefoonnummer.Text == "" || tbEmail.Text == "")
            {
                MessageBox.Show("Vul alle velden in!");
            }
        }
    }
}
