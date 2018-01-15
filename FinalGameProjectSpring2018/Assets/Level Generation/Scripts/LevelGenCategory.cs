using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Generator Category", menuName = "Level Generator/Level Generator Category")]
public class LevelGenCategory : ScriptableObject {
	public LevelGenPrefab[] components;
}
