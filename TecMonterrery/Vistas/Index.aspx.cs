using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Entidades;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;



namespace Vistas
{
    public partial class Index : System.Web.UI.Page
    {
        public static List<Entidades.ReporteGeneral.ReportGeneral> list = new List<Entidades.ReporteGeneral.ReportGeneral>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]              // Marcamos el método como uno web
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string BuscarNumAleatorio()    // el método debe ser de static
        {
            if (list != null)
            {
                return JsonConvert.SerializeObject(list);
            }
            else
            {
                return JsonConvert.SerializeObject("Grafica no disponible, por favor realize primero el Reporte");
            
            }
        }
    #region Metodo que trae la conulta de la DB

        public static List<Entidades.ReporteGeneral.ReportGeneral> Consulta(string fi, string ff)
        {
            DateTime fin = Convert.ToDateTime(fi);
            DateTime ffi = DateTime.Parse(ff);
            string fini = fin.ToString(@"yyyy-MM-dd HH:mm:ss");
            string ffin = ffi.ToString(@"yyyy-MM-dd HH:mm:ss");
            List<Entidades.ReporteGeneral.ReportGeneral> list = new List<Entidades.ReporteGeneral.ReportGeneral>();
            list = Negocios.ReporteGeneral.ReporteGene(fini, ffin);
            return list;
        }
 
	    #endregion        
        protected void btngeberarReporte_Click(object sender, EventArgs e)
        {
            



            int pagina = 1;
            list = Consulta(txtfechainicio.Value, txtfechafin.Value);

            var libro = new XLWorkbook();
            int dia2 = list[0].horaInicio.Day;
            var hoja = libro.AddWorksheet(pagina.ToString());
            int f = 1, c = 1;
            hoja.Cell(f, c).Value = "Fecha Inico"; c++;
            hoja.Cell(f, c).Value = "Fecha Fin"; c++;
            hoja.Cell(f, c).Value = "Llamadas Atendidas"; c++;
            hoja.Cell(f, c).Value = "Llamadas Abandonadas"; c++;
            hoja.Cell(f, c).Value = "Total de Llamadas"; c++;
            hoja.Cell(f, c).Value = "Porcentaje de Atención"; c++;
            hoja.Cell(f, c).Value = "Porcentaje de abandono"; c++;
            hoja.Cell(f, c).Value = "Promedio de Conversación"; c++;
            hoja.Cell(f, c).Value = "Promedio de llamadas Perdidas"; c++;
            hoja.Cell(f, c).Value = "Llamadas en servicio"; c++;
            f++; c = 1;
            
            foreach (var llama in list)
            {
                int dia = llama.horaInicio.Day;
                if (dia2 != dia)
                {
                    f = 1; c = 1;
                    pagina++;
                    dia2 = dia;
                    hoja = libro.AddWorksheet(pagina.ToString());
                    hoja.Cell(f, c).Value = "Fecha Inico"; c++;
                    hoja.Cell(f, c).Value = "Fecha Fin"; c++;
                    hoja.Cell(f, c).Value = "Llamadas Atendidas"; c++;
                    hoja.Cell(f, c).Value = "Llamadas Abandonadas"; c++;
                    hoja.Cell(f, c).Value = "Total de Llamadas"; c++;
                    hoja.Cell(f, c).Value = "Porcentaje de Atención"; c++;
                    hoja.Cell(f, c).Value = "Porcentaje de abandono"; c++;
                    hoja.Cell(f, c).Value = "Promedio de Conversación"; c++;
                    hoja.Cell(f, c).Value = "Llamadas en servicio"; c++;
                    f++; c = 1;
                    hoja.Cell(f, c).Value = llama.horaInicio; c++;
                    hoja.Cell(f, c).Value = llama.horaFin; c++;
                    hoja.Cell(f, c).Value = llama.atendidas; c++;
                    hoja.Cell(f, c).Value = llama.abandonadas; c++;
                    hoja.Cell(f, c).Value = llama.total_llamadas; c++;

                    hoja.Cell(f, c).Value = decimal.Round(Convert.ToDecimal(llama.porceAtendidas), 2); c++;
                    hoja.Cell(f, c).Value = decimal.Round(Convert.ToDecimal(llama.porceAbandonadas), 2); c++;
                    hoja.Cell(f, c).Value = llama.Promedio_Conversacion; c++;
                    hoja.Cell(f, c).Value = llama.Promedio_abandonadas; c++;
                    hoja.Cell(f, c).Value = llama.llamadasEnServicio; c++;
                }
                else
                {

                    hoja.Cell(f, c).Value = llama.horaInicio; c++;
                    hoja.Cell(f, c).Value = llama.horaFin; c++;
                    hoja.Cell(f, c).Value = llama.atendidas; c++;
                    hoja.Cell(f, c).Value = llama.abandonadas; c++;
                    hoja.Cell(f, c).Value = llama.total_llamadas; c++;
                    hoja.Cell(f, c).Value = decimal.Round(Convert.ToDecimal(llama.porceAtendidas),2); c++;
                    hoja.Cell(f, c).Value = decimal.Round(Convert.ToDecimal(llama.porceAbandonadas),2); c++;
                    hoja.Cell(f, c).Value = llama.Promedio_Conversacion; c++;
                    hoja.Cell(f, c).Value = llama.Promedio_abandonadas; c++;
                    hoja.Cell(f, c).Value = llama.llamadasEnServicio; c++;
                }
                f++; c = 1;


            }

            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"Reporte_Dia_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xlsx");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                libro.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }
            httpResponse.End();
            //Termina metodoa para excel
        }

        public static void Grafica()
        {
        }
    }
}