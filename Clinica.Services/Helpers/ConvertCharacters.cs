using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Helpers
{
    public static class ConvertCharacters
    {

        public static string verifyUpperCharacter(string Genero)
        {
            if(Genero == Genero.ToLower())
            {
                Genero = Genero.ToUpper();
                return Genero;
            }
            return Genero;
        }


    }
}
