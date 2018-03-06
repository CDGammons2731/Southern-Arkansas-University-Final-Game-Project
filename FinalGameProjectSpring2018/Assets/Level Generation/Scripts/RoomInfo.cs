using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour {

	List<GameObject> neighbors = new List<GameObject>(0);
	float minX, maxX, minY, maxY;

	public void SetInfo(LevelGenerator _settings, Vector3 _roomPos, float _x, float _y) {
		minX = (_roomPos.x - _x/2)*_settings.gridScale;
		maxX = (_roomPos.x + _x/2)*_settings.gridScale;
		minY = (_roomPos.y - _y/2)*_settings.gridScale;
		maxY = (_roomPos.y + _y/2)*_settings.gridScale;
	}

	public bool WithinBounds(Vector3 other) {
		if (other.x >= minX && other.x <= maxX && other.z >= minY && other.z <= maxY) {
			return true;
		} else return false;
	}

	public void AddToNeighborList(GameObject n) {
		neighbors.Add(n);
	}
}
