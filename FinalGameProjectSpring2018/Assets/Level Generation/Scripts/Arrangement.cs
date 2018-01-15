using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrangement {
	public LevelGenPrefab original;
	public int rotations;
	public int direction;
	public Vector2 offset;
	public Node sourceNode;
	public Node pathExitNode = null;
	public bool markForRemoval = false;
}
