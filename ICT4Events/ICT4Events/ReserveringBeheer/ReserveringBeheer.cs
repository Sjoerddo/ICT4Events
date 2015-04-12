﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ICT4Events.ReserveringBeheer
{
    class ReserveringBeheer
    {
        static Database.Database database = new Database.Database();

        public static bool Reserveren(string gebruikersnaam, DateTime aankomstDatum, DateTime vertrekDatum,  int betaald)
        {
            try
            {
                string query =
                    "INSERT INTO RESERVERING (Nummer, Aankomstdatum, Vertrekdatum, Betaald, Gastgebruikersnaam) VALUES(SEQ_RESERVERING.NEXTVAL, TO_DATE('" +
                    aankomstDatum + "','DD/MM/YYYY HH24:MI:SS'),TO_DATE('" + vertrekDatum +
                    "','DD/MM/YYYY HH24:MI:SS'),'" + betaald + "','" + gebruikersnaam + "')";
                database.Insert(query);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool KoppelKampeerplaats(string gebruikersnaam, DateTime aankomstDatum, DateTime vertrekDatum, string kampeerplaatsnummer)
        {
            try
            {
                string reserveringNummer = VindReserveringNummer(gebruikersnaam, aankomstDatum, vertrekDatum);
                string queryInsert =
                    "INSERT INTO Reservering_Kampeerplaats (KampeerplaatsNummer, ReserveringNummer) VALUES('" +
                    kampeerplaatsnummer + "','" + reserveringNummer + "')";
                database.Insert(queryInsert);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool VoegMedereizigerToe(string medereizigerGebruikersnaam, string reserveringsNummer)
        {
            try
            {
                string queryInsert = "INSERT INTO Medereiziger (GebruikerGebruikersnaam, ReserveringNummer) VALUES('" +
                                     medereizigerGebruikersnaam + "','" + reserveringsNummer + "')";
                database.Insert(queryInsert);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string VindReserveringNummer(string gebruikersnaam, DateTime aankomstDatum, DateTime vertrekDatum)
        {
            try
            {
                string query = "SELECT r.nummer FROM Reservering r WHERE r.GASTGEBRUIKERSNAAM = '" + gebruikersnaam +
                               "' AND r.AANKOMSTDATUM = TO_DATE('" + aankomstDatum +
                               "', 'DD/MM/YYYY HH24:MI:SS') AND r.VERTREKDATUM = TO_DATE('" + vertrekDatum +
                               "', 'DD/MM/YYYY HH24:MI:SS')";
                DataTable reserveringen = database.voerQueryUit(query);
                string[] array = new string[1];
                foreach (DataRow dr in reserveringen.Rows)
                {
                    array[0] = dr[0].ToString();
                }
                string reserveringNummer = array.GetValue(0).ToString();
                return reserveringNummer;

            }
            catch (Exception)
            {
                MessageBox.Show("Reserveringsnummer kan niet worden gevonden.");
                return null;
            }
        }

        public static List<string> AllePlaatsen()
        {
            string query = "SELECT * FROM KAMPEERPLAATS";
            DataTable kampeerplaatsen = database.voerQueryUit(query);
            List<String> stringlist= new List<string>();
            foreach (DataRow dr in kampeerplaatsen.Rows)
            {
                stringlist.Add("Nummer: " + dr[0] + " Soort: " + dr[1] + " Aantal Personen: " + dr[2]);
            }
            return stringlist;
        }

        public static List<string> AlleGebruikers()
        {
            string query = "SELECT g.gebruikersnaam, g.naam FROM GEBRUIKER g";
            DataTable gebruikers = database.voerQueryUit(query);
            List<String> stringlist = new List<string>();
            foreach (DataRow dr in gebruikers.Rows)
            {
                stringlist.Add("Gebruiker: " + dr[0] + " Naam: " + dr[1]);
            }
            return stringlist;
        }

        public void VrijePlaatsen()
        {
            throw new NotImplementedException();
        }
    }
}
