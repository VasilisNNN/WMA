using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	public List<Item> items = new List<Item>();
	
	void Awake()
	{
		items.Add(new Item("SleepingPills",0, "Sleeping Pills"));
		items.Add(new Item("Cure",1, "Cure"));
		
		items.Add(new Item("Ax",2, "Ax"));
		items.Add(new Item("Seeds",3, "Seeds"));
		items.Add(new Item("KidsKey",4, "KidsKey"));
		items.Add(new Item("TurboKey",5, "TurboKey"));
		items.Add(new Item("Car key", 6, "Car key"));
		
		items.Add(new Item("Heart",7, "Heart"));
		items.Add(new Item("Fuel",8,"Part of gun power"));
		items.Add(new Item("Oxidant",9,"Part of gun power"));
		
		items.Add(new Item("Beer",10,"Beer"));
		items.Add(new Item("Water",11, "Water"));
		items.Add(new Item("Key",12,"Key"));
		items.Add(new Item("CasualLeg", 13, "CasualLeg"));
		
		items.Add(new Item("Bomb0",14,"Weak Bomb"));
		items.Add(new Item("Bomb1",15,"Midle Bomb"));
		items.Add(new Item("Bomb2",16,"Strong Bomb"));
		items.Add(new Item("Bomb3",17,"WallBuster Bomb"));
		
		items.Add(new Item("1 $",18, "1 $"));
		items.Add(new Item("Rare book",19, "Rare book"));
		items.Add(new Item("Old cup",20,"Old cup"));
		
		items.Add(new Item("HeartLocked", 21, "HeartLocked"));
		items.Add(new Item("Old crosses", 22, "Old crosses"));
		items.Add(new Item("TubeWide",23,""));
		
		items.Add(new Item("Meat",24, "Meat"));
		items.Add(new Item("Milk",25, "Milk"));

		
		items.Add (new Item ("Peter's Right Eye", 26, "Peter's Right Eye"));
		items.Add (new Item ("Peter's Left Eye", 27, "Peter's Left Eye"));
		items.Add (new Item ("Peter's Right Hand", 28, "Peter's Right Hand"));
		items.Add (new Item ("Peter's Left Hand", 29, "Peter's Left Hand"));

	
		items.Add (new Item ("Plant", 30, "Plant"));
		items.Add (new Item ("Card", 31, "Card"));

    }


}
