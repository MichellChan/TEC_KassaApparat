using System;
using System.Collections.Generic;

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
            public string vareName;
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
        /// Letar igenom listan med produkter och hämtar namnet på produkten
        /// </summary>
        /// <param name="id">Produkten som vi letar efter</param>
        /// <returns>Produketens namn returnaras</returns>
        static string getVareNamn(int id) {
            //  Henter prisen fra listen
            foreach (produkter vara in listerAfProdukter) {
                //  Tjekker om valgtproduktet har er de samme som i listen
                if (vara.id == id) {
                    // returnere varens pris
                    return vara.vareNavn;
                }
            }
            // error - hit vill inte
            return "";
        }

        /// <summary>
        /// Låter säljare skriva in vilken produkt som säljs,
        /// och där efter så skall säljaren skriva in antalet produkter.
        /// </summary>
        static void kasseIndtast()
        {
            int vareid = 0;
            bool ok = false;

            do
            {
                Console.Write("Skriv vare id: ");
                string indtastetVareId = Console.ReadLine();

                ok = CheckIntastvardi(indtastetVareId, ref vareid);

                //  vi fic enter som vareid då ær vi klara
                if (!ok && vareid == -1)
                    return;
                if ()
                {

                }


                //  Check om det ær siffror = true, 
                //      Check om sifrorna motsvara en var i listanAfProdukter,
                //      true = Print antal solde varaer,
                //      false = vi printar de varor som ligger närmast det id som blev inskriviet, där efter så ber vi säljaren att skriva in ett nytt id
                //  False = kolla om det ær bokstæbver eller enter
                //  Vid enter = KLAR  skriv ut bong


            } while (!ok);


            
        }

        static bool CheckIntastvardi(string intastVareID, ref int vareID)
        {
            if (!int.TryParse(intastVareID, out vareID))
            {
                if (intastVareID == "")
                {
                    vareID = -1;
                }
                else
                {
                    vareID = -2;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Vi beräknar den nya summan för mängd rabbat
        /// </summary>
        /// <param name="summa">Normal total summa för produkten.</param>
        /// <param name="produktID">Produktens ID som skall ha rabbat.</param>
        static void MangaRabat(Double summa, int produktID) {
            //  Nytt object där vi lägger in ny data
            solgtProdukter rabatProdukt = new solgtProdukter();

            //  Lägger in de olika delaran i de olika delarna utav objectet
            rabatProdukt.id = produktID;
            //  På varans namn så lägger vi till rabbat så att vi vet att det är rabbat
            rabatProdukt.vareName = getVareNamn(produktID) + " - Rabbat";
            //  Denna produkt kan bara förekomma en gång
            rabatProdukt.antal = 1;
            //  Beräknar hur stor rabbaten är
            rabatProdukt.sum = -(Math.Round(summa * 0.1, 2, MidpointRounding.AwayFromZero));

            //  Lägger till posten med information för rabbaten i sålda varor
            listerAfSolgtProdukter.Add(rabatProdukt);
        }

        /// <summary>
        /// Listar alla de närmsta produkter till det produkt id som saknas.
        /// </summary>
        /// <param name="id">Produkt id som saknas, och vars närliggande produkter vi skall visa.</param>
        static void VisProdukterNaraProduktID (int id) {
            //  kollar alla produkters id tills vårat id är lägren än listans id
            for (int indexVara=0; (indexVara < listerAfProdukter.Count) && !(id < listerAfProdukter[indexVara].id); indexVara++) {
                //  Start och slut värden för att lista produkter
                int startIndex = indexVara - 2;
                int slutIndex = indexVara + 1;

                //  Om startIndex är lägre än noll, så fixa detta
                if (startIndex < 0)
                    startIndex = 0;

                //  Om slutIndex är större än listan med alla produkter, så fixa detta
                if (slutIndex > listerAfProdukter.Count)
                    slutIndex = listerAfProdukter.Count;

                //  Töm kommand promt och skriv text
                Console.Clear();
                Console.WriteLine("Produkten som du söker finns inte. Är det någon av dessa produkter som du söker?");

                //  Skriv ut produkt ifrån startIndex till slutIndex
                for (int index = startIndex; index < slutIndex; index++) {
                    Console.WriteLine(listerAfProdukter[index].ToString());
                }
            }
        }

        /// <summary>
        /// Skriver ut de sålda varorna tillsammans med total summan för alla varor
        /// </summary>
        static void PrintBong () {
            //  Här i kommer total summan för alla varor
            double totalSumma = 0;

            //  Rensar skärmen
            Console.Clear();
            Console.WriteLine("Sålda varor:");
            //  Skriver ut alla varor som vi har sålt
            foreach(solgtProdukter solgtVare in listerAfSolgtProdukter) {
                Console.WriteLine("{1,-20} {2,5} {3,10:C}", solgtVare.vareName, solgtVare.antal, solgtVare.sum);
                totalSumma += solgtVare.sum;
            }
            //  Skriver ut total summan för alla varor
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Totalt: {0,30:C}", Math.Round(totalSumma, 2, MidpointRounding.AwayFromZero));
        }
    }
}
