using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDB;

namespace Negocios.NegoConfiguracipn
{
   public class Configuracion
    {
        public static int lapsoTiempo(int id, int tiempo)
        {
            int res = AccesoDB.Consultas.Tiempo(id, tiempo);
            return res;
        }
    }
}
