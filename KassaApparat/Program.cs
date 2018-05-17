using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaApparat
{
    class Program
    {
        /// <summary>
        /// Data object med en produkter som säljs
        /// </summary>
        public struct produkter {
            public int id;
            public string vareNavn;
            public double pris;

        }

        /// <summary>
        /// Data object med en produkter som har sålts
        /// </summary>
        public struct solgtProdukter
        {
            public int id;
            public int antal;
            public double sum;
        }

        /// <summary>
        /// Lista med alla produkter som kan säljas
        /// </summary>
        static List<produkter> listerAfProdukter = new List<produkter>();

        /// <summary>
        /// Lista med alla produkter som har sålts vid ett tillfälle.
        /// Kommer att noll ställas efter att kvitto(bong) visas.
        /// </summary>
        static List<solgtProdukter> listerAfSolgtProdukter = new List<solgtProdukter>();

        /// <summary>
        /// Main funtion - Körs alltid vid start
        /// </summary>
        /// <param name="args">Argument som kan skickas ifrån kommand promt.</param>
        static void Main(string[] args) {


        }

        /// <summary>
        /// Läser produkt data ifrån en fil, och lägger varje produkt i en lista med all data
        /// </summary>
        static void ReadProdukterFraFil() {
            //  Läser data ifrån fil
            string allaVaror = System.IO.File.ReadAllText(@"C:\Users\OokamiChan\source\repos\TEC_KassaApparat\Produkter.csv");

            //  Delar upp till de olika raderna
            string []itemVara = allaVaror.Split(';');

            //  Skapar ett objekt där vi kan lägga in våra värden i
            produkter preProdukt = new produkter();

            //  Delar upp id, varans namn och pris i olika delar
            foreach (string vara in itemVara) {
                string[] delVara = vara.Split(',');

                //  Iterera igen alla delar utav våran varas data
                for(int i=0; i<3; i++) {
                    switch (i) {
                        case 0:
                            //  Hämta varans ID
                            preProdukt.id = Convert.ToInt16(delVara[i]);
                            break;
                        case 1:
                            //  Hämta varans Namn
                            preProdukt.vareNavn = delVara[i];
                            break;
                        case 2:
                            //  Hämta varans Pris
                            preProdukt.pris = Convert.ToDouble(delVara[i]);
                            break;
                    }
                }

                //  Lägg till vara i listerAfProdukter
                listerAfProdukter.Add(preProdukt);
            }

        }

        /// <summary>
        /// Letar igenom listan med produkter och när produkten hittas så returneras priset på vara.
        /// </summary>
        /// <param name="id">Produkt som vi letar efter</param>
        /// <returns>Priset på den vara som vi letar efter</returns>
        static double GetVarePris(int id)
        {
            //  Henter prisen fra listen
            foreach (produkter vara in listerAfProdukter)
            {
                //  Tjekker om valgtproduktet har er de samme som i listen
                if (vara.id == id)
                {
                    // returnere varens pris
                    return vara.pris;
                }
            }
            // error - hit vill inte
            return -1;
        }

        /// <summary>
        /// Låter säljare skriva in vilken produkt som säljs,
        /// och där efter så skall säljaren skriva in antalet produkter.
        /// </summary>
        static void kasseIndtast()
        {
            Console.Write("Skriv vare id: ");
           string indtastetVareId = Console.ReadLine();

            //  Check om det ær siffror = true, Print antal solde varaer
            //  False = kolla om det ær bokstæbver eller enter
            //  Check om sifrorna motsvara en var i listanAfProdukter

            if (indtastetVareId)
            {

            }
        }
    }
}
