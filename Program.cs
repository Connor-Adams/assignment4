using System;
using System.IO;
using System.Threading;

namespace A4ConnorAdams
{
    class Program
    {
        private static string[,] _guestList = new string[6,4];
        private static bool[,] _isThisSeatTaken = new bool[6, 4];

        //for debugging only - extended logging
        private static bool verbose = true;

        static void SearchBySeat()
     {
         int table, seat;
         SearchTable:
            try
            {
                Console.WriteLine("Enter Table number");
                table = int.Parse(Console.ReadLine());
                if (table > 6 || table <= 0)
                {
                    throw new ArgumentOutOfRangeException("Table Array",
                        "Table out of bounds; Value from 1-6 expected");
                }
            }
            catch (Exception e)
            {
                if (verbose)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("Response out of bounds");
                goto SearchTable;
            }
            
        }

        static void SearchByName()
        {
            string name;
            bool foundGuest = false;
            Console.WriteLine("what is your guests name?");
           name = Console.ReadLine();
           
           for (int i = 0; i < 6; i++)
           {
               for (int j = 0; j < 4; j++)
               {
                   if (name == _guestList[i, j])
                   {
                       Console.WriteLine(name+" is sitting at table: "+(i+1)+" in seat: "+(j+1));
                       foundGuest = true;
                   }
               }
           }

           if (!foundGuest)
           {
               Console.WriteLine("Could not find a guest by the name of '"+name+"'");
           }
           
        }
        
        static void Lookup()
        {
            int searchType;
            Search:
            try
            {
                Console.WriteLine("Select a search method");
                Console.WriteLine("1). Search by Seat");
                Console.WriteLine("2). Search by Name");
                
                
                searchType = int.Parse(Console.ReadLine());
                if (searchType > 2 || searchType <= 0)
                {
                    throw new ArgumentOutOfRangeException("Search Option",
                        "Table out of bounds; Value from 1-2 expected");
                }
                
            }
            catch (Exception e)
            {
                if (verbose)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("Response out of bounds");
                goto Search;
                
            }

            switch (searchType)
            {
                case 1:
                    SearchBySeat();
                    break;
                case 2:
                    SearchByName();
                    break;

            }
        }
        
        static void FixList()
        {
            for (int m = 0; m < 6; m++)
            {
                for (int n = 0; n < 4; n++)
                {
                    if (_guestList[m, n] == null)
                    {
                        _guestList[m, n] = "Vacant";
                        _isThisSeatTaken[m, n] = false;
                    }
                }
            }
            
        }

        static void RemoveGuest()
        {
            Start:
            do
            {    //rests variable each time the loop is run
                int table = 0, seat = 0;
                
            try
            {
                Console.WriteLine("What table is your guest sitting at?");
                table = int.Parse(Console.ReadLine());


                Console.WriteLine("What seat is your guest in?");
                seat = int.Parse(Console.ReadLine());

                if (table == 0 || seat == 0 || table > 6 || seat > 4)
                {
                    throw new IndexOutOfRangeException("Table or seat invalid");
                }
            }
            catch (Exception e)
            {
                if (verbose) Console.WriteLine(e.ToString());
                goto Start;
            }
          
                if (_isThisSeatTaken[table - 1, seat - 1])
                {
                    _guestList[table - 1, seat - 1] = null;
                }
                else
                {
                    Console.Write("Error! | ");
                    Console.WriteLine("This seat is currently unassigned");
                }

                Console.WriteLine("Seat emptied");
                break;

            }while(true);



    }
        
        static void PopulateList()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("Enter the name of the guest sitting at table: "+ (i+1)+" in seat: "+ (j+1));
                    _guestList[i, j] = Console.ReadLine();
                }
            }
        }

       
        
        static void AddGuest()
        {
            int table, seat;
            string name;
            
            do
            {
                Name:
                try
                {
                    Console.WriteLine("Enter The guests name: (Maximum of 20 characters)");
                    name = Console.ReadLine();

                    if (name.Length > 20)
                    {
                        throw new Exception("Guest Name Exceeeded 20 characters in length");
                    }

                }
                catch (Exception e)
                {
                    if (verbose)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    Console.WriteLine("Please enter a name under 20 characters in length");
                    goto Name;
                }



                Table:
                try
                {
                    Console.WriteLine("Enter the table number (1-6)");
                    table = int.Parse(Console.ReadLine());
                    if (table > 6 || table <= 0)
                    {
                        throw new ArgumentOutOfRangeException("Table Array",
                            "Table out of bounds; Value from 1-6 expected");
                    }
                }
                catch (Exception e)
                {
                    if (verbose)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    Console.WriteLine("Response out of bounds");
                    goto Table;
                }


                Seat:
                try
                {
                    Console.WriteLine("Enter the seat number (1-4)");
                    seat = int.Parse(Console.ReadLine());

                    if (seat > 4 || seat <= 0)
                    {
                        throw new ArgumentOutOfRangeException("Seat Array",
                            "Seat out of bounds; Value from 1-6 expected");
                    }
                }
                catch (Exception e)
                {

                    if (verbose)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    Console.WriteLine("Response out of bounds");
                    goto Seat;
                }

                _guestList[table-1, seat-1] = name;
                _isThisSeatTaken[table - 1, seat - 1] = true;

                break;

            } while (true);



        }

        static void Menu()
        {
            int option;
            MainMenu:
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Wedding Seating Chart - Main Menu");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1). View Seating Chart");
            Console.WriteLine("2). Add a guest");
            Console.WriteLine("3). Remove a guest");
            Console.WriteLine("4). Search a guest");
            Console.WriteLine("5). Quit Program");
            option = int.Parse(Console.ReadLine());
            
            switch (option)
            {
                case 1:
                    PrintFormattedChart();
                    goto MainMenu;
                case 2:
                    AddGuest();
                    goto MainMenu;
                case 3:
                    RemoveGuest();
                    goto MainMenu;
                case 4:
                    Lookup();
                    goto MainMenu;
                default:
                    Console.WriteLine("Program Exiting");
                    break;
                    
            }
        }

        static void PrintFormattedChart()
        {
            FixList();
            //arrays start at 0 but life doesnt - added 1 to output to adjust for real life
            for (int t = 0; t < 6; t++)
            {
                Console.WriteLine("-----------");
                Console.WriteLine("|Table: "+ (t+1)+" |");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("|Name                 | Seat|");
                Console.WriteLine("-----------------------------");
                //for building an individual table
                for (int s = 0; s < 4; s++)
                {
                    Console.WriteLine(String.Format("|{0,-20} |{1,5}|", _guestList[t,s], s+1));   
                }
            }
            
        }       
        
        static void Main(string[] args)
        {
            Menu();
        }
    }
}