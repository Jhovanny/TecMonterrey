using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AccesoDB;
using System.Data;

namespace Negocios
{
    public class ReporteGeneral
    {
        public static List<Entidades.ReporteGeneral.ReportGeneral> ReporteGene(string ini, string fin)
        {
            int lapso = AccesoDB.Consultas.GetTiempo();
            DateTime interFechainicio = Convert.ToDateTime(ini);
            DateTime interFechaFin = interFechainicio.AddMinutes(lapso);
            List<Entidades.ReporteGeneral.ReportGeneral> reporte = new List<Entidades.ReporteGeneral.ReportGeneral>();
            DateTime ffin = Convert.ToDateTime(fin);
            DateTime IniFin = ffin.AddMinutes(lapso);
            ini = interFechainicio.ToString("yyyy-MM-dd HH:mm:ss");
            fin = interFechainicio.AddMinutes(lapso).ToString("yyyy-MM-dd HH:mm:ss");

            TimeSpan finDelDia = new TimeSpan(20, 0, 0);
            TimeSpan InicioDelDia = new TimeSpan(7, 29, 0);

            while (interFechainicio <= IniFin)
            {
                ini = interFechainicio.ToString("yyyy-MM-dd HH:mm:ss");
                fin = interFechaFin.ToString("yyyy-MM-dd HH:mm:ss");
                string nomDay = interFechainicio.ToString("dddd");
                if (nomDay != "domingo")
                {
                    TimeSpan horas = interFechainicio.TimeOfDay;
                    if (horas > InicioDelDia && horas < finDelDia)
                    {
                        double atendida = AccesoDB.Consultas.atendidas(ini, fin);
                        double abandonadas = AccesoDB.Consultas.abandonadas(ini, fin);
                        double total = AccesoDB.Consultas.Total(ini, fin);
                        string promedio = AccesoDB.Consultas.Promedio(ini, fin);
                        string promedioAban = AccesoDB.Consultas.PromedioAban(ini, fin);
                        double porceAtencion = 0;
                        if (!Double.IsNaN(atendida / total))
                        {
                            porceAtencion = ((atendida / total) * 100);
                            
                        }
                        double porceAbandonada = 0;
                        if (!Double.IsNaN(abandonadas / total))
                        {
                            porceAbandonada = ((abandonadas / total) * 100);
                        }
                        double llamadaServicio = atendida - abandonadas;

                        reporte.Add(new Entidades.ReporteGeneral.ReportGeneral
                           {
                               horaInicio = interFechainicio,
                               horaFin = interFechaFin,
                               atendidas = atendida,
                               abandonadas = abandonadas,
                               total_llamadas = total,
                               porceAtendidas =porceAtencion,
                               porceAbandonadas = porceAbandonada,
                               llamadasEnServicio = llamadaServicio,
                               Promedio_Conversacion = promedio,
                               Promedio_abandonadas = promedioAban
                           });

                    
                    interFechainicio = interFechainicio.AddMinutes(lapso);//Agregamos el tiempo que requiere
                    interFechaFin = interFechainicio.AddMinutes(lapso);//Agregamos el tiempo que requiere
                    ini = interFechainicio.ToString("yyyy-MM-dd HH:mm:ss");
                    fin = interFechaFin.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        interFechainicio = interFechainicio.AddDays(1);//Agregamos el tiempo que requiere
                        interFechainicio = interFechainicio.AddHours(-13);
                        if (lapso == 15)
                        {
                            interFechainicio = interFechainicio.AddMinutes(lapso);
                        }
                       
                        interFechainicio = interFechainicio.AddMinutes(lapso);
                        interFechaFin = interFechainicio.AddMinutes(lapso);
                        int resMinu = 30 - lapso;
                        interFechainicio.AddMinutes(resMinu);
                        
                    }
                }
                else
                {
                    interFechainicio = interFechainicio.AddDays(1);//Agregamos el tiempo que requiere
                    //interFechainicio = interFechainicio.AddHours(7);
                    //interFechainicio = interFechainicio.AddMinutes(30);
                }

            }
            return reporte;
        }
    }
}
