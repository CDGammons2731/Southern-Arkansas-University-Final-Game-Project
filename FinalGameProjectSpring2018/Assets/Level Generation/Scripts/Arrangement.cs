using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrangement {
	public static Vector2 pathFailure  = new Vector2(133.7f, 133.7f);

	public LevelGenPrefab original;
	public Vector2 offset;
	public Node sourceNode;
	public Vector2 pathExitPt = pathFailure;
	public Node pathExitNode;
	public Vector2 doorEntry;
	public bool markForRemoval = false;
}
