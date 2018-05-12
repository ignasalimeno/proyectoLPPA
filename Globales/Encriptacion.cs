using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globales
{
    public class Encriptacion
    {
        public static string encriptar(string texto)
        {
            try
            {
                return texto;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public static string desencriptar(string texto)
        {
            try
            {
                return texto;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
