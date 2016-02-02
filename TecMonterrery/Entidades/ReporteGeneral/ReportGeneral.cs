using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.ReporteGeneral
{
    public class ReportGeneral
    {
        public  DateTime horaInicio { get; set; }
        public  DateTime horaFin { get; set; }
        public double atendidas { get; set; }
        public double abandonadas { get; set; }
        public double total_llamadas { get; set; }
        public double porceAtendidas { get; set; }
        public double porceAbandonadas { get; set; }
        public string Promedio_Conversacion { get; set; }
        public string Promedio_abandonadas { get; set; }
        public double llamadasEnServicio { get; set; }

    }
    public class Contar
    {
        public string contar{get;set;}
    }
}
