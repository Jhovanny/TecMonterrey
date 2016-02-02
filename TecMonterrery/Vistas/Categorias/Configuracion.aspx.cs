using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;

namespace Vistas.Categorias
{
    public partial class Configuracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddltiempo.SelectedValue);
            int value = Convert.ToInt32(ddltiempo.Text  );
            int res = Negocios.NegoConfiguracipn.Configuracion.lapsoTiempo(id, value);
            if (res == 1)
            {
                string script = @"<script type='text/javascript'>
                            alert('Se actualizo correctamente');
                        </script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
            else
            {
                string script = @"<script type='text/javascript'>
                            alert('Error al Actualizar');
                        </script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }


        }
    }
}