using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
// This script is used for keeping information on the doors generated in a level
*/

public class DoorInfo {
	public DoorInfo doorComplement = null;	// If null, this door is open for use; otherwise, it is connected to another door
	public Vector2 doorLocation;			// Where the door is located

	public DoorInfo(Vector2 loc) {
		doorLocation = loc;
	}

	public bool PairDoors(DoorInfo other) {
		if (this.doorComplement == null && other.doorComplement == null && Vector2.Distance(this.doorLocation, other.doorLocation) == 1) {
			this.doorComplement = other;
			other.doorComplement = this;
			return true;
		} else return false;
	}
}
