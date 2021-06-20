/*
Badrit Bin Imran 0660361
8 February, 2021
Class name: SubwayMap.cs
Purpose: This class contains our SubwayMap object and its constructors 
         This class also contains all methods for our SubwayMap class
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Subway_Visualization
{
    class SubwayMap
    {
        private List<Station> S; //List of stations
        public SubwayMap() //Subway Map constructor
        {
            this.S = new List<Station>();
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        //Method : FindStation
        //Parameters: string name
        //Return type: int
        public int FindStation(string name)
        {
            //looping the list to find relevant station
            for (int i = 0; i < S.Count; i++)
            {
                if (S[i].Name == name) return i;
            }
            return -1;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Method : InsertStation
        //Parameters: string name
        //Return type: void
        public void InsertStation(string name)
        {            
            Station InsertedStation = new Station(name);
            //Error checking for duplicate stations
            if (FindStation(name) > -1)
            {                
                Console.WriteLine("Sorry! Duplicate Station names are not allowed!");
                Console.WriteLine();
            }
            else
            {
               //Adding station successfully
                Console.WriteLine("Successfully added " + name);
                S.Add(InsertedStation);                
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Method : RemoveStation
        //Parameters: string name
        //Return type: void
        //Note: We call RemoveConnection through a loop to remove all connections
        //      associated with the station to be removed

        public void RemoveStation(string name)
        {
            //Checking if station exists
            if (FindStation(name) > -1)
            {
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Removing " + name+ " and all its connections");
                //Looping to remove all relevant connections
                for (int i = 0; i < S.Count; i++)
                {
                    foreach (Colour colour in Enum.GetValues(typeof(Colour)))
                    {
                        RemoveConnection(name, S[i].Name, colour);                    
                    }
                }
                Console.WriteLine("All possible connections have been checked and removed if present. Finally, removing " + name);
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();
                S.Remove(S[FindStation(name)]);
            }
            else
            {
                //Station does not exist
                Console.WriteLine("No such station found");
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
        //Method : InsertConnection
        //Parameters: string name1, string name2, Colour c
        //Return type: void
        //Note: Since all connections are 2-way, we call another private method called
        //      InsertSingleConnection to initialize connection both ways
        
        public void InsertConnection(string name1, string name2, Colour c)
        {
            //Calling the two methods
            if (FindStation(name1) > -1 && FindStation(name2) > -1)
            {
                InsertSingleConnection(name1, name2, c);
                InsertSingleConnection(name2, name1, c);
            }
            //Error checking for Invalid Station Names
            else
            {
                Console.WriteLine("Sorry Invalid Station names!");
            }
        }
        //Method : InsertSingleConnection
        //Parameters: string name1, string name2, Colour c
        //Return type: void
        //Note: This function is called from within InsertConnection twice with different parameters
        private void InsertSingleConnection(string name1, string name2, Colour c)
        {
            int i = FindStation(name1);
            int j = FindStation(name2);
            //if check for valid station names
            if (i > -1 && j > -1)
            {
                Node newConnection = new Node(S[j], c, null);
                //If our station has no connections yet
                if (S[i].E == null)
                {
                    S[i].E = newConnection;
                    Console.WriteLine("Connected from " + (S[i].Name) + " to " + (S[i].E).Connection.Name + " Colour: " + newConnection.Line.ToString());
                }
                else
                {
                    Node pointer = S[i].E;
                    //Looping till we find an empty spot
                    while (pointer.Next != null)
                    {
                        //If check for duplicate connection
                        if (pointer.Connection.Name == name2 && pointer.Line == c)
                        {
                            Console.WriteLine("Duplicate Connection found while connecting " + pointer.Connection.Name + " to " + S[i].Name + " of Color: " + c);
                            return;
                        }
                        //changing pointer value
                        pointer = pointer.Next;
                    }

                    //We found an empty spot
                    //If we found duplicate we will not add. Otherwise we add
                    if (pointer.Connection.Name == name2 && pointer.Line == c)
                    {
                        //Duplicate Connection exists
                        Console.WriteLine("Duplicate Connection found while connecting " + pointer.Connection.Name + " to " + " of Color: " + c);
                        return;
                    }
                    else
                    {
                        //New connection is added
                        //We reference pointer.next to point to the new connection
                        pointer.Next = newConnection;
                        Console.WriteLine("Connected from " + (S[i].Name) + " to " + (pointer.Next).Connection.Name + " Colour: " + pointer.Next.Line.ToString());
                    }
                }
            }
            else
            {
                //Error checking for wrong station names
                Console.WriteLine("Invalid Station name");
            }

        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Method : RemoveConnection
        //Parameters: string name1, string name2, Colour c
        //Return type: void
        //Note: Since all connections are 2-way, we call another private method RemoveSingleConnection 
        //      from within RemoveConnection to remove connections both ways

        public void RemoveConnection(string name1, string name2, Colour c)
        {
            //Checking if station exists
            if (FindStation(name1) > -1 && FindStation(name2) > -1)
            {
                //Calling RemoveSingleConnection
                Console.WriteLine();
                RemoveSingleConnection(name1, name2, c);
                RemoveSingleConnection(name2, name1, c);
                Console.WriteLine();
            }
            else
            {
                //Invalid Station Names
                Console.WriteLine("Sorry Invalid Station names!");
            }
         
        }
        //Method : RemoveSingleConnection
        //Parameters: string name1, string name2, Colour c
        //Return type: void
        //Note: This function is called from RemoveConnection twice
        private void RemoveSingleConnection(string name1, string name2, Colour c)
        {          
            int i = FindStation(name1);
            int j = FindStation(name2);
          
            Node pointer = S[i].E;
            //Small if check to stop running our code if the station has no connections at all
            if (pointer == null)
            {
                return;
            }
            if (i > -1 && j > -1)
            {
                //Duplicate statements; special case for first instance 
                if (pointer.Connection.Name == name2 && pointer.Line == c)
                {
                    //If we remove first value, it becomes null, nothing else to worry about
                    Console.WriteLine("Removing connection from " +S[i].Name + " to " + pointer.Connection.Name + " of color: " + pointer.Line.ToString());
                    S[i].E = pointer.Next?? null;
                    return;
                }
                else
                {
                    //Other cases
                    while (pointer.Next != null)
                    {
                        //Case to remove connections
                        if (pointer.Next.Connection.Name == name2 && pointer.Next.Line == c)
                        {

                            Console.WriteLine("Removing connection from " + S[i].Name + " to " + pointer.Next.Connection.Name + " of color: " + pointer.Next.Line.ToString());

                            pointer.Next = pointer.Next.Next ?? null;
                           
                            return;
                        }
                        pointer = pointer.Next;

                    }
                }
                //If we can reach this part of the code then it means that no connections exist
                Console.WriteLine("Sorry but the connection does not exist from " + name1 + " to " + name2 + " of color: " + c);
            }
            else
            {
                //If we can reach this part of the code then it means that no connections exist
                Console.WriteLine("Error! No connection exists here!");
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Method : DepthFirstSearch
        //Parameters: none
        //Return type: void
        //Note: This function is taken from class notes
        public void DepthFirstSearch()
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("The results of our depth first search: ");
            int i;

            for (i = 0; i < S.Count; i++)     // Set all vertices as unvisited
                S[i].Visited = false;

            for (i = 0; i < S.Count; i++)
                if (!S[i].Visited)                  // (Re)start with vertex i
                {
                    //calling DepthFirstSearch with current station
                    DepthFirstSearch(S[i]);
                    Console.WriteLine();
                }
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
        //Method : DepthFirstSearch
        //Parameters: Station v
        //Return type: void
        //Note: This function is taken from class notes
        private void DepthFirstSearch(Station v)
        {
            v.Visited = true;    // Output vertex when marked as visited
            Console.WriteLine(v.Name);
            //Check if the station's connection is not null
            if (v.E != null)
            {
                Node pointer = v.E;
                //looping and calling the recursive DepthFirstSearch
                while (pointer != null)
                {
                    if (!pointer.Connection.Visited)
                    {
                        DepthFirstSearch(pointer.Connection);
                                              
                    }
                    //changing values of pointer
                    pointer = pointer.Next;                
                }              
            }    
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Method : CriticalPoints
        //Parameters: None
        //Return type: void
        //Note: This function calls another private CriticalPoints method through a loop
        public void CriticalPoints()
        {           
            //We use global variable array for storing the list of critical points
            //list of tuple(distance,lowestDistance,Name)
            List<Tuple<int, int,string>> Q = new List<Tuple<int, int,string>>();
            int i;         

            for (i = 0; i < S.Count; i++)     // Set all vertices as unvisited
                S[i].Visited = false;
         
            for (i = 0; i < S.Count; i++)
                if (!S[i].Visited)                  // (Re)start with vertex i
                {
                    //The first tuple, i.e the root will have a distance and lowest distance value of 1
                    //Calling CriticalPoints recursively
                    CriticalPoints(S[i], Q,1,S[i].Name);
                    Console.WriteLine();
                }
            //Outputting if the array of critical points are not empty
            if (Globals.CP.Count > 0)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("The Critical Points are: ");
                for (i = 0; i < Globals.CP.Count; i++)
                {
                    //Just outputting the names of critical points
                    Console.WriteLine(Globals.CP[i].Item3);
                }
                Console.WriteLine("-----------------------------------------------");
            }
            else Console.WriteLine("Sorry no critical points exist"); //case for no critical points
                      
        }
        //Method : CriticalPoints
        //Parameters: Station v, List<Tuple<int, int,string>> Q, int previousDepth, string parent
        //Return type: void
        //Note: This function is called recursively
        private void CriticalPoints(Station v, List<Tuple<int, int,string>> Q, int previousDepth, string parent)
        {
            //Adding unvisited stations to the list
            if (!v.Visited)
            {
                var testTuple = Tuple.Create(previousDepth, previousDepth, v.Name);
                Q.Add(testTuple);
            }
            v.Visited = true;    
            //increase the distance and lowest distance when called
          
            if (v.E != null)
            {
                //We do distance measuring and adding here
                Node pointer = v.E;
                int p, q;
                while (pointer != null)
                {
                    if (pointer.Connection.Visited)
                    {                       
                        //This is actually the point where backedges are discovered
                        //p is the pointer node's position and q is v's position
                        p = Q.FindIndex(t => t.Item3 == pointer.Connection.Name); //p is downtown
                        q = Q.FindIndex(t => t.Item3 == v.Name); //q is rink

                        //Case for backedge discovery
                        if (Q[p].Item2 < Q[q].Item2 && pointer.Connection.Name!=parent)
                        {
                              Q[q] = Tuple.Create(Q[q].Item1, Q[p].Item2 , Q[q].Item3);

                        }
                    }
                    
                    else
                    {
                        CriticalPoints(pointer.Connection, Q, previousDepth + 1,v.Name);
                    
                    }                  
                    //p is the pointer node's position and q is v's position
                    p = Q.FindIndex(t => t.Item3 == pointer.Connection.Name); 
                    q = Q.FindIndex(t => t.Item3 == v.Name);
                    
                    //item 2 is lowest distance
                    // Q[p] is the current node's position
                    //This is the point where we go back and change lowest points if applicable
                    if (Q[p].Item2 >= Q[q].Item1)
                    {
                        //add stuff for critical points here
                        if (!Globals.CP.Contains(Q[q]))
                            Globals.CP.Add(Q[q]);
                    }
                    else if (Q[p].Item2 < Q[q].Item2 && pointer.Connection.Name != parent)
                    {
                        //Converting and changing all stations on the way back after discovery of a Critical Point
                        p = Q.FindIndex(t => t.Item3 == pointer.Connection.Name); 
                        q = Q.FindIndex(t => t.Item3 == v.Name); 
                        Q[q] = Tuple.Create(Q[q].Item1, Q[p].Item2, Q[q].Item3);
                    }

                    pointer = pointer.Next;                   
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Method : BreadthFirstSearch
        //Parameters: None
        //Return type: void
        //Note: This function is taken from class notes
        public void BreadthFirstSearch()
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("The results of our breadth first search: ");
            int i;

            for (i = 0; i < S.Count; i++)
                S[i].Visited = false;              // Set all vertices as unvisited

            for (i = 0; i < S.Count; i++)
                if (!S[i].Visited)                  // (Re)start with vertex i
                {
                    BreadthFirstSearch(S[i]);
                    Console.WriteLine();
                }
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
        //Method : BreadthFirstSearch
        //Parameters: Station st
        //Return type: void
        //Note: This function is called on recusrively from BreadthFirstSearch
        private void BreadthFirstSearch(Station st)
        {            
            //Declaring variables
            Station w;
            Queue<Station> Q = new Queue<Station>();
            st.Visited = true;        // Mark vertex as visited when placed in the queue
            Q.Enqueue(st);         

            while (Q.Count != 0)
            {
                st = Q.Dequeue();     // Output vertex when removed from the queue
                Console.WriteLine(st.Name);

                //Enqueing and traversing
                if (st.E != null)
                {
                    Node pointer = st.E;
                    w = st.E.Connection;
                    if (!w.Visited)
                    {
                        w.Visited = true;
                        Q.Enqueue(w);
                    }

                    while (pointer.Next != null)
                    {
                        pointer = pointer.Next;
                        w = pointer.Connection;
                        if (!w.Visited)
                        {
                            w.Visited = true;
                            Q.Enqueue(w);
                        }
                    }
                }
            }           
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Method : ShortestDistance
        //Parameters: String source, String target
        //Return type: void
        //Note: This function finds the shortest distance from the given source to the target. It
        //      also calls a private ShortestDistance method with return type String 
        public void ShortestDistance(String source, String target)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            for (int k = 0; k < S.Count; k++)
                S[k].Visited = false;
             
            int i = FindStation(source);
            int j = FindStation(target);
            //Looping only when the stations are found
            if (i > -1 && j > -1)
            {
                //Calling ShortestDistance
                String shortestPath =  ShortestDistance(S[i],S[i].Name, target);
              Console.WriteLine("The shortest path between " + S[i].Name + " and " + target + " is " + shortestPath);
            }
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
        //Method : ShortestDistance
        //Parameters: Station st, String path,String target
        //Return type: String
        //Note: This function is called recursively

        private String ShortestDistance(Station st, String path,String target)
        {
            Station w;
            //Creating the queue
            Queue<Tuple<Station, String>> Q = new Queue<Tuple<Station, String>>();
            var stTuple = Tuple.Create(st, path);
            st.Visited = true;        // Mark vertex as visited when placed in the queue
            //Enqueue                         
            Q.Enqueue(stTuple);
            //Looping while Q is not empty
            while (Q.Count != 0)
            {
                //Dequeue
                stTuple = Q.Dequeue();              
               
                //Item 1 is station
                //We go here only if the station has a non-null connection
                if (stTuple.Item1.E != null)
                {
                    Node pointer = stTuple.Item1.E;
                    w = stTuple.Item1.E.Connection;
                    if (!w.Visited)
                    {
                        //Setting visited as true and updating path
                        w.Visited = true;
                        String newPath = stTuple.Item2 + " ==> " + w.Name;
                        //Finally we enqueue again
                        var newTuple = Tuple.Create(w, newPath);
                        Q.Enqueue(newTuple);
                    }
                    while (pointer.Next != null)
                    {
                        //Transversing adjacent stations
                        pointer = pointer.Next;
                        w = pointer.Connection;
                        if (!w.Visited)
                        {
                            //Mark visited and update path
                            w.Visited = true;
                            String newPath = stTuple.Item2 + " ==> " + w.Name;
                            var newTuple = Tuple.Create(w, newPath);
                            //Enqueueing again
                            Q.Enqueue(newTuple);
                        }
                    }
                } // end if check for null
                if (stTuple.Item1.Name == target)
                {                    
                    return stTuple.Item2;
                }
            } // end while
                return "No connection exists"; //change later
        }
    }
    //Static class for global variable used in critical points
    static class Globals
    {
        // global tuple list to store critical points
        public static List<Tuple<int, int, string>> CP = new List<Tuple<int, int, string>>();       
    }
}
