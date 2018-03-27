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
                case 3:
                    PrintBikes(exploded[1], exploded[2], null);
                    return 0;
                case 4:
                    try
                    {
                        int delay = Int32.Parse(exploded[3]);
                        PrintBikes(exploded[1], exploded[2], delay);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Le troisième argument {exploded[3]} doit être un entier.");
                    }
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
            Console.WriteLine("bikes [contract] [station] [delay - optionnal] : blahblahblah."); // TODO problème pour splitter....
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

        private static void PrintBikes(string contract, string station, int? delay)
        {
            int delayP;
            if (delay == null)
                delayP = 0;
            else
                delayP = (int)delay;

            int res = client.getAvailableBikes(contract, station, delayP); // TODO Gérer les cas qui n'existent pas.........

            Console.WriteLine(res);
        }

    }
}
