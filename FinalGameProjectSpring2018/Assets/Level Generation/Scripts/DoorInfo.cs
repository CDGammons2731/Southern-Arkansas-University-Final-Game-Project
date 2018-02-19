using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
// This script is used for keeping information on the doors generated in a level
*/

public class DoorInfo : MonoBehaviour {
	public DoorInfo pair = null;	// If null, this door is open for use; otherwise, it is connected to another door
	public Vector2 loc;			// Where the door is located
	public Vector2 face;
	public bool MarkForRemoval = false;

	public DoorInfo(Vector2 _loc, Vector2 _face) {
		loc = _loc;
		face = _face;
	}

	public bool PairDoors(DoorInfo other, Material mat) {
		if (this.pair == null && other.pair == null && Vector2.Distance(this.loc, other.loc) == 1 && Vector2.Distance(this.loc+other.face, other.loc+this.face) == 1 && this.face + other.face == Vector2.zero && ((other.loc-this.loc).normalized.x == (this.face - other.face).normalized.x || (other.loc-this.loc).normalized.y == (this.face - other.face).normalized.y)) {
			this.pair = other;
			other.pair = this;
			other.gameObject.GetComponent<MeshRenderer> ().material = mat;
			gameObject.GetComponent<MeshRenderer> ().material = mat;
			return true;
		} else return false;
	}

	public void PlaceDoorObject(GameObject d) {
		if (pair != null) {
			GameObject _d = Instantiate(d);
			_d.transform.position = (gameObject.transform.position + pair.gameObject.transform.position)/2f;
			_d.transform.LookAt(transform);
			MarkForRemoval = true;
			pair.MarkForRemoval = true;
		}
	}
}
