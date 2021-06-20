/*
Badrit Bin Imran 
8 February, 2021
Class name: Node.cs
Purpose: This class contains our Node object and its constructors 
 */
using System;

namespace Subway_Visualization
{
	public enum Colour { RED, YELLOW, GREEN, BLUE}
    class Node
	{		
	    public Station Connection {get;set;}   //Adjacent Station
		public Colour Line { get; set; } //Color line
		public Node Next { get; set; } //Next node
		public Node(Station connection, Colour c, Node next) //Station Constructors
		{
			this.Connection = connection;
			this.Line = c;
			this.Next = next;
		}    		
	}
}
