using UnityEngine;

public class Node
{
	public int x, y;
	public int s_Cost, f_Cost;
	public int weight;
	public int roomType;
	public Node cameFrom;
	public bool occupied = false;
	public Node pathNode = null;

	public static int NodeDistance(Node i, Node j) {
		return Mathf.Abs(i.x-j.x)+Mathf.Abs(i.y-j.y);
	}

	public Vector2 Location() {
		return new Vector2(x,y);
	}
}
