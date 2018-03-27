using SOAP_CONSOLE_VELIB.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAP_CONSOLE_VELIB
{
    class Program
    {
        private static ServiceVelibClient client = new ServiceVelibClient();

        static void Main(string[] args)
        {
            string input = null;
            client.getCities();

            Console.WriteLine("Bienvenue dans le client en mode CLI de SOAP_VELIB.");
            Console.WriteLine("Tapez help pour avoir une aide sur les commandes disponibles et leur utilisation.");

            do
            {
                Console.Write(">> ");
                input = Console.ReadLine();
                Interprete(input);
            } while (input != "exit");
        }

        private static int Interprete(string input)
        {
            string[] exploded = input.Split(' ');

            switch(exploded.Length)
            {
                case 1:
                    switch(exploded[0])
                    {
                        case "exit":
                            return 0;
                        case "help":
                            DoHelp();
                            return 0;
                        case "contracts":
                            PrintContracts();
                            return 0;
                    }
                    break;
                case 2:
                    switch(exploded[0])
                    {
                        case "stations":
                            PrintStations(exploded[1]);
                            return 0;
                    }
                    break;
            }
            if(exploded.Length >= 3 && exploded[0] == "bikes")
            {
                PrintBikes(exploded);
                return 0;
            }

            Console.WriteLine($"{input} n'est pas reconnu comme une commande.");
            return 1;
        }

        private static void DoHelp()
        {
            Console.WriteLine("Liste des commandes disponibles :");
            Console.WriteLine("exit : permet de quitter le programme proprement.");
            Console.WriteLine("help : affiche une aide concernant la liste des commandes disponibles.");
            Console.WriteLine("contracts : affiche la liste des contrats disponibles.");
            Console.WriteLine("stations [contract] : affiche la liste des stations du contrat passé en paramètre.");
            Console.WriteLine("bikes [contract] [station_reg] : affiche le nombre de vélos disponibles aux stations contenant [station_reg] du contrat [contract].");
        }

        private static void PrintContracts()
        {
            string[] res = client.getCities();
            foreach(string elem in res)
            {
                Console.WriteLine(elem);
            }
        }

        private static void PrintStations(string contract)
        {
            string[] res = client.getStations(contract);
            
            if(res == null)
            {
                Console.WriteLine($"Le contrat {contract} n'existe pas.");
                return;
            }
            foreach (string elem in res)
            {
                Console.WriteLine(elem);
            }
        }

        private static void PrintBikes(string[] datas)
        {
            string regex_station = "";
            for(int i = 2; i < datas.Length - 1; i++)
            {
                regex_station += datas[i] + " ";
            }
            regex_station += datas[datas.Length - 1];

            string[] res = client.getStations(datas[1]);

            if (res == null)
            {
                Console.WriteLine($"Le contrat {datas[1]} n'existe pas.");
                return;
            }

            bool tst = false;
            foreach (string elem in res)
            {
                if (elem.ToLower().Contains(regex_station.ToLower()))
                {
                    tst = true;
                    int num = client.getAvailableBikes(datas[1], elem, 0);
                    Console.WriteLine("Station détectée : " + elem + ", nombre de vélos disponibles : " + num);
                }
            }

            if(!tst)
            {
                Console.WriteLine("Aucune station détectée.");
            }
        }

    }
}
