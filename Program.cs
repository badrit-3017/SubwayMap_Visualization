/*
Badrit Bin Imran
8 February, 2021
Class name: Program.cs
Purpose: This class contains our main method
 */
using System;

namespace Subway_Visualization
{
    class Program 
    {
        static void Main(string[] args)
        {

            SubwayMap Peterborough = new SubwayMap();
            //We just hardcode some stations, connections and a subway map first

            //Assumptions:
            //Connections cannot exist outside of a Subway Map
            //Hence we do not allow modifying any station outside of a subway map     
            //All subway stations are loaded into the system in the order of the map they are part of
            Peterborough.InsertStation("Downtown");
            //bottom part of the map
            Peterborough.InsertStation("Water St");
            Peterborough.InsertStation("King St");
            Peterborough.InsertStation("Rink St");
            Peterborough.InsertStation("Fleming College");
            Peterborough.InsertStation("West Bank");
            Peterborough.InsertStation("Vaughan");
            //top part of the map
            Peterborough.InsertStation("Charlotte");
            Peterborough.InsertStation("George North");
            Peterborough.InsertStation("George South");
            Peterborough.InsertStation("Park");
            Peterborough.InsertStation("Osmows");
            Peterborough.InsertStation("OC Annex");
            Peterborough.InsertStation("Trent University");
            Peterborough.InsertStation("University Heights");
            Peterborough.InsertStation("Best Buy");
            Peterborough.InsertStation("Walmart");
            //left side of the map
            Peterborough.InsertStation("LakeField");
            Peterborough.InsertStation("Oshawa");
            Peterborough.InsertStation("Kingston");
            Peterborough.InsertStation("Durham Campus");
            Peterborough.InsertStation("Couberg");
            Peterborough.InsertStation("Toronto");
            Peterborough.InsertStation("Ottawa");
            Peterborough.InsertStation("Airport");

            //Start Connecting bottom side
            Peterborough.InsertConnection("Downtown", "Water St", Colour.GREEN);
            Peterborough.InsertConnection("Water St", "King St", Colour.YELLOW);
            Peterborough.InsertConnection("King St", "Rink St", Colour.RED);
            Peterborough.InsertConnection("King St", "Rink St", Colour.YELLOW);
            Peterborough.InsertConnection("Rink St", "West Bank", Colour.BLUE);
            Peterborough.InsertConnection("Rink St", "Fleming College", Colour.YELLOW);
            Peterborough.InsertConnection("West Bank", "Vaughan", Colour.GREEN);
            Peterborough.InsertConnection("Vaughan", "Water St", Colour.GREEN);

            //Connecting Top side
            Peterborough.InsertConnection("Downtown", "Charlotte", Colour.RED);
            Peterborough.InsertConnection("Charlotte", "George North", Colour.BLUE);
            Peterborough.InsertConnection("George North", "George South", Colour.YELLOW);
            Peterborough.InsertConnection("George South", "Park", Colour.YELLOW);
            Peterborough.InsertConnection("George South", "Park", Colour.BLUE);
            //Connecting Top side loop
            Peterborough.InsertConnection("Charlotte", "Osmows", Colour.BLUE);
            Peterborough.InsertConnection("Osmows", "Trent University", Colour.RED);
            Peterborough.InsertConnection("Osmows", "OC Annex", Colour.RED);
            Peterborough.InsertConnection("Osmows", "Trent University", Colour.YELLOW);
            Peterborough.InsertConnection("Trent University", "Best Buy", Colour.RED);
            Peterborough.InsertConnection("Best Buy", "Walmart", Colour.RED);
            Peterborough.InsertConnection("Downtown", "Walmart", Colour.BLUE);
            Peterborough.InsertConnection("Osmows", "University Heights", Colour.GREEN);
            Peterborough.InsertConnection("Best Buy", "University Heights", Colour.GREEN);

            //Connecting left side (left portion)
            Peterborough.InsertConnection("Airport", "LakeField", Colour.BLUE);
            Peterborough.InsertConnection("Downtown", "Airport", Colour.GREEN);
            Peterborough.InsertConnection("Oshawa", "LakeField", Colour.BLUE);
            Peterborough.InsertConnection("Oshawa", "LakeField", Colour.GREEN);
            Peterborough.InsertConnection("Oshawa", "Kingston", Colour.RED);
            Peterborough.InsertConnection("Kingston", "Durham Campus", Colour.YELLOW);
            Peterborough.InsertConnection("Oshawa", "Durham Campus", Colour.GREEN);
            //Connecting left side (bottom portion)
            Peterborough.InsertConnection("Oshawa", "Couberg", Colour.YELLOW);
            Peterborough.InsertConnection("Couberg", "Ottawa", Colour.RED);
            Peterborough.InsertConnection("Couberg", "Toronto", Colour.BLUE);
            Peterborough.InsertConnection("Toronto", "Ottawa", Colour.RED);
            Peterborough.InsertConnection("Toronto", "Oshawa", Colour.GREEN);
            //End of preloaded map
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("END OF PRELOADED STATIONS");               
            Console.WriteLine("Hello! We will now implement the subway system of Peterborough");
            //Declaring variables to be used later on
            int col;
            Colour colo;
            String first;
            String second;
            int option = 10;
            //While loop contains the main portion of our main method
            //A switch statement is nested to allow for multiple user options
            while (option != 9)
            {
                Console.WriteLine();
                Console.WriteLine("Press 1 to add station, press 2 to remove station, press 3 to add a connection, press 4 to remove a connection,");
                Console.WriteLine("5 to find out the critical points, 6 to find shortest distance, 7 to perform a depth first search, 8 to perform a breadth first search and 9 to quit ");
                Console.WriteLine();
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1: //Add station Option
                        Console.WriteLine("Please enter the name of the station to be added: ");
                        Peterborough.InsertStation(Console.ReadLine());
                        break;
                    case 2: //Remove Station Option
                        Console.WriteLine("Please enter the name of the station to be removed: ");
                        Peterborough.RemoveStation(Console.ReadLine());
                        break;
                    case 3:
                        //Add Station Option
                        //Input checking for station 1          
                        do
                        {
                            Console.WriteLine("Please enter a valid name of the first station to be added ");
                            first = Console.ReadLine();
                        } while (Peterborough.FindStation(first) == -1);

                        //Input checking for station 12
                        do
                        {
                            Console.WriteLine("Please enter a valid name of the second station to be added ");
                            second = Console.ReadLine();
                        } while (Peterborough.FindStation(second) == -1 || first == second);
                        //Input checking for color
                        Console.WriteLine("Please enter the colour: (3 for BLUE, 2 for GREEN, 1 for YELLOW, 0 for RED ");
                        col = Convert.ToInt32(Console.ReadLine());
                        while (col >= 4 || col <= -1)
                        {
                            Console.WriteLine("Please enter the correct numerical value for color : ");
                            col = Convert.ToInt32(Console.ReadLine());
                        }
                        colo = (Colour)col;
                        //Initializing connection
                        Peterborough.InsertConnection(first, second, colo);
                        break;

                    case 4:
                        //Option for removing our connection
                        //Input checking for station 1          
                        do 
                        {
                            Console.WriteLine("Please enter a valid name of the first station: ");
                            first = Console.ReadLine();
                        } while (Peterborough.FindStation(first) == -1) ;

                        //Input checking for station 2
                        do
                        {
                            Console.WriteLine("Please enter a valid name of the second station: ");
                            second = Console.ReadLine();
                        } while (Peterborough.FindStation(second) == -1 || first==second);

                        Console.WriteLine("Please enter the colour: (3 for BLUE, 2 for GREEN, 1 for YELLOW, 0 for RED )");
                        col = Convert.ToInt32(Console.ReadLine());
                        while (col >= 4 || col <= -1)
                        {
                            Console.WriteLine("Please enter the correct numerical value for color : ");
                            col = Convert.ToInt32(Console.ReadLine());
                        }
                        colo = (Colour)col;
                        //Removing the connection
                        Peterborough.RemoveConnection(first, second, colo);
                        break;
                    case 5: //option display critical points
                        Peterborough.CriticalPoints();
                        break;
                    case 6://Option shortest distance
                        //Input checking station 1 
                        do
                        {
                            Console.WriteLine("Please enter a valid name of the first station: ");
                            first = Console.ReadLine();
                        } while (Peterborough.FindStation(first) == -1);

                        //Input checking for station 2
                        do
                        {
                            Console.WriteLine("Please enter a valid name of the second station: ");
                            second = Console.ReadLine();
                        } while (Peterborough.FindStation(second) == -1 || first == second);
                        //Shortest Distance Calculation
                        Peterborough.ShortestDistance(first, second);
                        break;
                    case 7: //Option Depth First Search
                        Peterborough.DepthFirstSearch();
                        break;
                    case 8: //Option Breadth First Search
                        Peterborough.BreadthFirstSearch();
                        break;
                    case 9://Option user quits
                        Console.WriteLine("Have a good day! "); ;
                        break;
                    default: //All other input choices
                        Console.WriteLine("Please enter an appropriate option: ");
                        break;
                }                
            }
          //End program
            Console.WriteLine();
        }
    }   
}
