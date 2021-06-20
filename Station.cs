/*
Badrit Bin Imran 
8 February, 2021
Class name: Station.cs
Purpose: This class contains our Station object and its constructors 
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Subway_Visualization
{
    class Station
    {
        public string Name { get; set; } // Name of the subway station
        public bool Visited { get; set; } // Used for depth-first and breadth-first searches
        public Node E { get; set; } // Linked list of adjacent stations
        public Station(string name) { // Station constructor
            this.Name = name;
            this.Visited = false;            
        } 
    }
}
