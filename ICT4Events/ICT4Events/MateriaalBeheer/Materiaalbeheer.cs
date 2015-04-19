﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICT4Events.GebruikerBeheer;

namespace ICT4Events.MateriaalBeheer
{
    internal class Materiaalbeheer
    {
        //private Gebruiker Harold = new Gast("RussianDaddy", "Harold", "Egelhoorntje96", false, 1, false);

        private static Database.Database database = new Database.Database();

        /// <summary>
        /// Insert de query met id, uitleendatum, retourdatum en gebruikersnaam in de 
        /// database.
        /// </summary>
        public static bool MateriaalHuren(int id, DateTime uitleendatum, DateTime retourdatum, string gebruikersnaam)
        {
            try
            {
                string query =
                    "INSERT INTO UITLENING (ID, Uitleendatum, Retourdatum, Gebruikersnaam) VALUES(" + id + ",TO_DATE('" +
                    uitleendatum.ToShortDateString() + "','DD/MM/YYYY')," + "TO_DATE('" +
                    retourdatum.ToShortDateString() + "','DD/MM/YYYY'),'" + gebruikersnaam + "')";
                database.Insert(query);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UitgevenRFID(string gebruikersnaam, string rfid)
        {
            bool Check = false;
            List<Gebruiker> Gebruikers = new List<Gebruiker>();
            string sqlGebruiker = "SELECT * FROM GEBRUIKER WHERE GEBRUIKERSNAAM = '" + gebruikersnaam + "'";
            Gebruikers = database.GetGebruikerList(sqlGebruiker);
            try
            {  
                foreach (Gebruiker TempGebruiker in Gebruikers)
                {
                    if (TempGebruiker.RFID != "null")
                    {
                        string queryUpdate = "UPDATE Gebruiker SET RFID = '" + rfid + "' WHERE gebruikersnaam = '" + gebruikersnaam + "'";
                        database.Insert(queryUpdate);
                        Check = true;
                    }
                    else
                    {
                        Check = false;
                    }
                }

            }
            catch (Exception)
            {
                Check = false;
            }
            return Check;
        }

        /// <summary>
        /// Gebruikt een SELECT statement om exemplaar en materiaal te koppelen.
        /// Vervolgens laat het het ExemplaarId, borg, de soort zien.
        /// </summary>
        public List<string> AlleExemplaren()
        {
            string query =
                "SELECT e.ID, m.Borg, m.Soort, Opmerkingen FROM Exemplaar e, Materiaal m WHERE m.ID = e.MateriaalID";
            DataTable exemplaren = database.voerQueryUit(query);
            List<string> stringList = new List<string>();
            foreach (DataRow dr in exemplaren.Rows)
            {
                stringList.Add("ID: " + dr[0] + " - Borg: " + dr[1] + " - Soort:" + dr[2] + " -  " + dr[3]);
            }
            return stringList;
        }

        /// <summary>
        /// Gebruikt een SELECT statement om exemplaar en materiaal te koppelen.
        /// Vervolgens laat het het ExemplaarId, borg, de soort zien.
        /// De parameter is het Id van het exemplaar.
        /// </summary>
        public static List<string> ZoekMateriaal(string id)
        {
            try
            {
                string query =
                    "SELECT e.ID, m.Borg, m.Soort, Opmerkingen FROM Exemplaar e, Materiaal m WHERE m.ID = e.MateriaalID AND m.ID = " +
                    id;
                DataTable materiaalZoeken = database.voerQueryUit(query);
                List<string> stringList = new List<string>();
                foreach (DataRow dr in materiaalZoeken.Rows)
                {
                    stringList.Add("ID: " + dr[0] + " - Borg: " + dr[1] + " - Soort:" + dr[2] + " -  " + dr[3]);
                }
                return stringList;
            }
            catch (Exception)
            {
                MessageBox.Show("Materiaal ID kon niet gevonden worden.");
                return null;
            }
        }

        public static List<string> GetTotaleBorg()
        {
            string query = "SELECT Borg FROM Materiaal m";
            DataTable materiaalZoeken = database.voerQueryUit(query);
            List<string> stringList = new List<string>();
            foreach (DataRow dr in materiaalZoeken.Rows)
            {
                stringList.Add(dr[0] + "");
            }
            return stringList;
        }

        public static List<string> GetMaxExemplaar()
        {
            string query = "SELECT ID FROM Materiaal m";
            DataTable materiaalZoeken = database.voerQueryUit(query);
            List<string> stringList = new List<string>();
            foreach (DataRow dr in materiaalZoeken.Rows)
            {
                stringList.Add(dr[0] + "");
            }
            return stringList;
        }

        /// <summary>
        /// Gebruikt een update statement waarbij een exemplaar -en uitleningId
        /// wordt meegegeven.
        /// </summary>
        public bool UpdateUitleningId(int exemplaarId, int uitleningId)
        {
            try
            {
                string queryUpdate = "UPDATE Exemplaar SET UitleningId = " + uitleningId + " WHERE ID = " + exemplaarId;
                database.Insert(queryUpdate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Laat een lijst van alle gebruikers uit de database zien.
        /// </summary>
        public List<string> AlleGasten()
        {
            string query = "SELECT gebruikersnaam, naam FROM Gebruiker";
            DataTable gekozenGebruikersnaam = database.voerQueryUit(query);
            List<string> stringList = new List<string>();
            foreach (DataRow dr in gekozenGebruikersnaam.Rows)
            {
                stringList.Add(dr[0] + " - Naam: " + dr[1]);
            }
            return stringList;
        }
        /// <summary>
        /// Haalt alle uitleningen uit de database en geeft hierbij het ID van 
        /// deze uitlening.
        /// </summary>
        public static List<string> AlleUitleningen()
        {
            string query = "SELECT ID FROM Uitlening";
            DataTable uitlening = database.voerQueryUit(query);
            List<string> stringList = new List<string>();
            foreach (DataRow dr in uitlening.Rows)
            {
                stringList.Add(dr[0] + "");
            }
            return stringList;
        }
    }
}