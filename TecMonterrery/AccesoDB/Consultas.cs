using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace AccesoDB
{
    public class Consultas
    {
        public static string GetConfig()
        {
            return ConfigurationManager.ConnectionStrings["AccesoDB.Properties.Settings.asteriskcdrdbConnectionString"].ConnectionString;
        }

        public static MySqlConnection Conex()
        {
            MySqlConnection con = new MySqlConnection(GetConfig());
            return con;
        }
        #region Atendidas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ini"></param>
        /// <param name="fin"></param>
        /// <returns></returns>
        public static double atendidas(string ini, string fin)
        {
            string query = "SELECT  COUNT(*) as numero FROM cdr WHERE dst = 4100 AND userfield !=\"\" AND calldate BETWEEN '" + ini + "' AND '" + fin + "';";
            return StorePro(query);
        }

        #endregion

        #region abandonadas
        public static double abandonadas(string ini, string fin)
        {
            string query = "SELECT COUNT(*) AS numero FROM asteriskcdrdb.cdr WHERE dst = 4100 AND userfield = \"\" AND calldate BETWEEN '" + ini + "' AND '" + fin + "' ORDER BY calldate DESC;";
            return StorePro(query);
        }
        #endregion

        #region Total Atendidas
        public static double Total(string ini, string fin)
        {
            string query = "SELECT COUNT(*) AS numero FROM asteriskcdrdb.cdr WHERE dst = 4100 AND calldate BETWEEN '" + ini + "' AND '" + fin + "' ORDER BY calldate DESC;";
            return StorePro(query);
        }
        #endregion

        #region promedio de Conversación
        public static string Promedio(string ini, string fin)
        {
            string query = "SELECT SEC_TO_TIME(AVG (billsec)) AS numero FROM asteriskcdrdb.cdr WHERE dst = 4100 AND userfield != \"\" AND calldate BETWEEN '" + ini + "' AND '" + fin + "' ORDER BY calldate DESC;";
            return StoreProString(query);
        }
        #endregion

        #region promedio de abandonadas
        public static string PromedioAban(string ini, string fin)
        {
            string query = "SELECT SEC_TO_TIME(AVG (billsec)) as numero FROM asteriskcdrdb.cdr where dst = 4100 and duration > 5 and userfield = \"\" and calldate between '" + ini + "' and '" + fin + "' order by calldate desc;";
            return StoreProString(query);
        }
        #endregion

        #region Metodo principal para ejecutar el query

        public static string StoreProString(string consulta)
        {
            MySqlConnection co = Conex();
            string p = "";
            co.Open();
            try
            {
                string query = consulta;
                MySqlCommand comand = new MySqlCommand(query, co);
                comand.CommandTimeout = 30;
                MySqlDataReader lec = comand.ExecuteReader();
                while (lec.Read())
                {
                    p = lec["numero"].ToString();
                }
                return p;
            }
            catch (MySqlException ex)
            {

                throw;
            }
            finally
            {
                co.Close();
            }
        }
        #endregion


        #region Metodo principal para ejecutar el query

        public static double StorePro(string consulta)
        {
            MySqlConnection co = Conex();
            double p = 0;
            co.Open();
            try
            {
                string query = consulta;
                MySqlCommand comand = new MySqlCommand(query, co);
                comand.CommandTimeout = 30;
                MySqlDataReader lec = comand.ExecuteReader();
                while (lec.Read())
                {
                    p = Convert.ToDouble(lec["numero"].ToString());
                }
                return p;
            }
            catch (MySqlException ex)
            {

                throw;
            }
            finally
            {
                co.Close();
            }
        }
        #endregion



        #region metodo para actualizar el Lapso de tiempo
        public static int Tiempo(int id,int tiempo)
        {
            string query = "update Config set tiempo = " + tiempo + " where id_config = 1;";
            int a = UpdateConfig(query);
            return a;
        }

        #endregion

        #region Update tabla Config del campo tiempo
        public static int UpdateConfig(string consulta)
        {
            int res = 0;
            MySqlConnection co = Conex();
            co.Open();
            try
            {
                string query = consulta;
                MySqlCommand comand = new MySqlCommand(query, co);
                res = comand.ExecuteNonQuery();
                return res;
            }
            catch (MySqlException ex)
            {

                throw;
            }
            finally
            {
                co.Close();
            }
        }
        #endregion

        public static int GetTiempo()
        {
            MySqlConnection co = Conex();
            int p = 0;
            co.Open();
            try
            {
                string query = "Select tiempo from Config;";
                MySqlCommand comand = new MySqlCommand(query, co);
                MySqlDataReader lec = comand.ExecuteReader();
                while (lec.Read())
                {
                    p = Convert.ToInt32(lec["tiempo"].ToString());
                }
                return Convert.ToInt32(p);
            }
            catch (MySqlException ex)
            {

                throw;
            }
            finally
            {
                co.Close();
            }
        }
    }
}
