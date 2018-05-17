﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaApparat
{
    class Program
    {
        public struct produkter {
            public int id;
            public string vareNavn;
            public double pris;

        }

        public struct solgtProdukter
        {
            public int id;
            public int antal;
            public double sum;
        }

        static List<produkter> listerAfProdukter = new List<produkter>();
        static List<solgtProdukter> listerAfSolgtProdukter = new List<solgtProdukter>();

        static void Main(string[] args) {


        }

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
