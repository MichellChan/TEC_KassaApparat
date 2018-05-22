using System;
using System.Collections.Generic;
using System.IO;

namespace KassaApparat
{
    class Program
    {
        /// <summary>
        /// Data object med en produkter som säljs
        /// </summary>
        public struct produkter
        {
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
            //  Läser in alla varor
            ReadProdukterFraFil();
            //  Kriv in vilka varaor som sälgs
            kasseIndtast();
            //  Skriv ut lista med sålda varor
            PrintBong();

            //  Exit
            Console.WriteLine("enter to exit");
            Console.ReadLine();
        }

        /// <summary>
        /// Läser produkt data ifrån en fil, och lägger varje produkt i en lista med all data
        /// </summary>
        static void ReadProdukterFraFil() {
            //  Läser data ifrån fil
            string sti = @"C:\Users\OokamiChan\source\repos\TEC_KassaApparat\Produkter.csv";
            //string sti = @"C:\Users\Tec\source\repos\TEC_KassaApparat\Produkter.csv";
            using (StreamReader sr = File.OpenText(sti)) {
                //  Declarerar vara string
                string itemVara = "";

                //  Delar upp till de olika raderna
                while ((itemVara = sr.ReadLine()) != null) {
                    //  Delar upp id, varans namn och pris i olika delar
                    string[] delVara = itemVara.Split(';');

                    //  Skapar ett objekt där vi kan lägga in våra värden i
                    produkter preProdukt = new produkter {
                        id = Convert.ToInt16(delVara[0].Trim()),
                        vareNavn = delVara[1].Trim(),
                        pris = Convert.ToDouble(delVara[2].Trim())
                    };

                    //  Lägger till produkten i våran lista
                    listerAfProdukter.Add(preProdukt);
                }
            }
        }

        /// <summary>
        /// Letar igenom listan med produkter och när produkten hittas så returneras priset på vara.
        /// </summary>
        /// <param name="id">Produkt som vi letar efter</param>
        /// <returns>Priset på den vara som vi letar efter</returns>
        static double GetVarePris(int id) {
            //  Henter prisen fra listen
            foreach (produkter vara in listerAfProdukter) {
                //  Tjekker om valgtproduktet har er de samme som i listen
                if (vara.id == id) {
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
        static void kasseIndtast() {
            do {
                int vareid = 0;
                bool ok = false;

                //  Loop för in skrivining utav varor som säljs
                do {
                    //  Läser in varans id
                    Console.Write("Skriv vare id: ");
                    string indtastetVareId = Console.ReadLine();

                    //  Kollar om vi skrivier in en sifra eller bokstäver
                    ok = CheckIntastvardi(indtastetVareId, ref vareid);

                    //  vi fic enter som vareid då ær vi klara
                    if (!ok && vareid == -1)
                        return;

                    //  Kollar om varan finns i våran lista med varor
                    if (!CheckProduktIDExists(vareid)) {
                        //  Om varan saknas så skriv ut de varor som finns i närheten
                        ok = false;
                        VisProdukterNaraProduktID(vareid);
                    }
                } while (!ok);  //  Gör detta s¨länge som vi får false

                //  Vi gör samma sak för antalet sålda varor
                do {
                    int antal = 0;

                    //  Skriv in antalet som säljs
                    Console.Write("Skriv vare antal: ");
                    string indtastetVareId = Console.ReadLine();

                    //  Kolla om det är en siffra eller bokstav som skrivs in
                    ok = CheckIntastvardi(indtastetVareId, ref antal);

                    //  vi fic enter som vareid då ær vi klara
                    if (!ok && antal == -1)
                        return;

                    //  Skapa ett object där vi kan lägga in all data i
                    solgtProdukter solgtItem = new solgtProdukter {
                        id = vareid,
                    antal = antal,
                    vareName = getVareNamn(vareid),
                    sum = antal * GetVarePris(vareid)
                };

                    //  Lägg till varan i sålda varor listan
                    listerAfSolgtProdukter.Add(solgtItem);

                    //  Om vi har mänd rabbat så gör en beräkning på hur stor rabbat som vi får
                    //  Och lägg till den i listan
                    if (antal > 4) {
                        MangaRabat(solgtItem.sum, vareid);
                    }

                } while (!ok);  //  Gör detta så länge som vi får false

            } while (true); //  Denna loop går ej att bryta om inte du trycker enter i antal eller id fälten


        }

        /// <summary>
        /// Kollar om en vara finns i listan med produkter
        /// </summary>
        /// <param name="produktID">Det ID som vi söker</param>
        /// <returns>Ture/false om varan finns eller inte</returns>
        static bool CheckProduktIDExists(int produktID) {
            foreach (produkter vareItem in listerAfProdukter) {
                if (vareItem.id == produktID) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Kollar om det värde som vi skriver in är ett siffra
        /// </summary>
        /// <param name="intastVareID">den sträng med våran vara ID</param>
        /// <param name="vareID">Returnerar en int med vara ID</param>
        /// <returns>true om det var en siffra som skrevs in</returns>
        static bool CheckIntastvardi(string intastVareID, ref int vareID) {
            if (!int.TryParse(intastVareID, out vareID)) {
                if (intastVareID == "") {
                    vareID = -1;
                } else {
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
        static void VisProdukterNaraProduktID(int id) {
            //  kollar alla produkters id tills vårat id är lägren än listans id
            for (int indexVara = 0; (indexVara < listerAfProdukter.Count); indexVara++) {
                //  Check om fåran vara ID är mindre än listans vara ID
                if (!(id > listerAfProdukter[indexVara].id)) {
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
                        Console.WriteLine("{0,5} {1,-20} {2,5:C}", listerAfProdukter[index].id, listerAfProdukter[index].vareNavn, listerAfProdukter[index].pris);
                    }

                    //  Vi har skrivit ut våra varor som ligger ovan och under det vare ID som vi letade efter
                    //  Vi är där med klara
                    return;
                }
            }
        }

        /// <summary>
        /// Skriver ut de sålda varorna tillsammans med total summan för alla varor
        /// </summary>
        static void PrintBong() {
            //  Här i kommer total summan för alla varor
            double totalSumma = 0;

            //  Rensar skärmen
            Console.Clear();
            Console.WriteLine("Sålda varor:");
            //  Skriver ut alla varor som vi har sålt
            foreach (solgtProdukter solgtVare in listerAfSolgtProdukter) {
                Console.WriteLine("{0,-20} {1,5} {2,10:C}", solgtVare.vareName, solgtVare.antal, solgtVare.sum);
                totalSumma += solgtVare.sum;
            }
            //  Skriver ut total summan för alla varor
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Totalt: {0,30:C}", Math.Round(totalSumma, 2, MidpointRounding.AwayFromZero));
        }
    }
}
