using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrangement {
	public LevelGenPrefab original;
	public Vector2 offset;
	public Node sourceNode;
	public Node pathExitNode = null;
	public Vector2 doorEntry;
	public bool markForRemoval = false;
}
