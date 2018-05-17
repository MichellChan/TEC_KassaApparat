using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaApparat
{
    class Program
    {
        struct produkter {
            int id;
            string vareNavn;
            double pris;

        }

        struct solgtProdukter
        {
            int id;
            int antal;
            double sum;
        }

        List<produkter> listerAfProdukter = new List<produkter>();
        List<solgtProdukter> listerAfSolgtProdukter = new List<solgtProdukter>();



        static void Main(string[] args) {


        }

        static double getVarePris(int id)
        {

        }
    }
}
