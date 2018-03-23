using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Generator Object", menuName = "Level Generator/Level Generator Object")]
public class LevelGenPrefab : ScriptableObject {
	public GameObject prefab;
	public Vector2 dimensions;
	public Vector2[] doorLocations;
	public bool LockedRoom;
	//public GameObject[] doorObjs;
}
