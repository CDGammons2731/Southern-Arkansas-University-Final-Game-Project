using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public int gX = 24, gY = 24;
	public int minDistance = 5;

	public Node[,] grid;
	Node start;
	Node end;
	List<Node> path = new List<Node>(0);

	public Material pathMat;

	public LevelGenCategory[] categories;
	/*
	// 0  - Normal Room
	// 1  - Start Room
	// 2  - End Room
	*/

	void Start() {
		if (gX * gY > 1) {
				//First set up the grid
			InitializeGrid(gX, gY);
				//Assign 2 places to serve as our start and end rooms
			int r1Q = Random.Range(1,4); //What quadrant will the start room be in?
			int r2Q = Random.Range(1,4); //Same for the end room
			if (r1Q == r2Q) {
				r2Q++;
				if (r2Q == 5) r2Q = 1;
			}
			bool r1Assigned = false;
			do { r1Assigned = AssignRandomRoom(1, r1Q); } while (!r1Assigned);
			bool r2Assigned = false;
			do { r2Assigned = AssignRandomRoom(2, r2Q); } while (!r2Assigned);
			for (int i = 0; i < gX; i++) {
				for (int j = 0; j < gY; j++) {
					grid[i,j].weight = Random.Range(0,50);
				}
			}
			start.weight = 0;
			path = FindPath();
			List<Arrangement> roomsToPlace = new List<Arrangement>(0);
			while (path.Count > 0) {
				Debug.Log(path.Count);
				if (roomsToPlace.Count == 0) {
					roomsToPlace.Add(PlaceRoom(path[0], null, true));
				} else {
					roomsToPlace.Add(PlaceRoom(path[0], roomsToPlace[roomsToPlace.Count-1].pathExitNode, true));
				}
				if (path.Count ==1) {
					path.Remove(end);
				}
			}
			InstantiateRooms(roomsToPlace);
		} else {
			Debug.LogWarning("Grid size not large enough for start and end rooms to be put on the grid! Aborting level generation");
		}
	}

	Arrangement PlaceRoom(Node source, Node prev, bool isPathed) {
		List<Arrangement> potential = new List<Arrangement>();
		/*/
		/// Step 1: Get all the initial arrangements that can be in that spot, assuming nothing's there and we don't have to connect to anything else
		/*/
		foreach (LevelGenPrefab pref in categories[source.roomType].components) {
			if (prev == null) { // We aren't working off of a previous node - place any room here
				for (int i = 0; i < pref.dimensions.x; i++) {
					for (int j = 0; j < pref.dimensions.y; j++) {
						for (int k = 0; k < 3; k++) {
							Arrangement room = new Arrangement();
							room.offset = new Vector2(i,j);
							room.original = pref;
							room.sourceNode = source;
							room.rotations = k;
							room.direction = 2+(k*2);
							potential.Add(room);
						}
					}
				}
			} else { // We are working off a previous node, so we need to have this room connect to it
				int dir;
				if (source.Location()-prev.Location() == Vector2.left) {
					dir = 4;
				} else if (source.Location()-prev.Location() == Vector2.right) {
					dir = 6;
				} else if (source.Location()-prev.Location() == Vector2.up) {
					dir = 8;
				} else if (source.Location()-prev.Location() == Vector2.down) {
					dir = 2;
				} else {
					dir = 0;
					Debug.LogWarning("The Source and Previous nodes aren't able to give a direction");
					Debug.LogWarning("Source: "+source.Location()+ " | Previous: "+prev.Location());
				}
				foreach (Vector3 door in pref.doorLocationsAndDirections) {
					Vector3 tempDoor = door;
					int rots = 0;
					for (int r = 0; r < 3; r++) {
						if ((int)tempDoor.z != dir) {
							rots++;
							if (rots%2==1) {
								tempDoor = RotateCW(tempDoor, new Vector2(pref.dimensions.y, pref.dimensions.x));
							} else {
								tempDoor = RotateCW(tempDoor, pref.dimensions);
							}
						}
					}
					Vector2 offs = new Vector2(1,1)-(Vector2)tempDoor;
					Arrangement room = new Arrangement();
					room.offset = offs;
					room.original = pref;
					room.sourceNode = source;
					room.rotations = rots;
					room.direction = dir;
					potential.Add(room);
				}
			}
		}
		/*/
		/// Step 2: Check every door for every arrangement, see which door leads the furthest along the path, if there aren't any, we don't use that arrangement
		/// This is purely for rooms that are on generated paths
		/*/
		if (isPathed) {
			foreach (Arrangement pot in potential) {
				foreach (Vector3 door in pot.original.doorLocationsAndDirections) {
					Vector3 tmpDoor = door;
					for (int r = 0; r < pot.rotations; r++) {
						if (r%2==1) {
							tmpDoor = RotateCW(tmpDoor, new Vector2(pot.original.dimensions.y, pot.original.dimensions.x));
						} else {
							tmpDoor = RotateCW(tmpDoor, pot.original.dimensions);
						}
					}
					tmpDoor += new Vector3(pot.offset.x, pot.offset.y, 0);
					if (tmpDoor.z == pot.direction) continue;
					for (int i = path.IndexOf(pot.sourceNode); i < path.Count; i++) {
						if ((Vector2)tmpDoor == pot.sourceNode.Location()-path[i].Location()+new Vector2(1,1)) {
							pot.pathExitNode = path[i];
						}
					}
				}
				if (pot.pathExitNode == null) {
					pot.markForRemoval = true;
				}
			}
		}
		for (int i = 0; i < potential.Count; i++) {
			if (potential[i].markForRemoval) {
				potential.Remove(potential[i]);
				i--;
			}
		}
		/*/
		/// Step 3: Check every arrangement for collisions with other rooms or any path nodes farther along than where we are, remove the arrangements if there are any
		/*/
		foreach (Arrangement pot in potential) {
			Vector2 dims = pot.original.dimensions;
			if (pot.rotations%2 == 1) { 
				dims = new Vector2(dims.y, dims.x);
			}
			for (int i = 0; i < dims.x; i++) {
				for (int j = 0; j < dims.y; j++) {
					if (grid[(int)pot.sourceNode.x+(int)pot.offset.x, (int)pot.sourceNode.y+(int)pot.offset.y].occupied 
					|| 
					(path.Contains(grid[(int)pot.sourceNode.x+(int)pot.offset.x, (int)pot.sourceNode.y+(int)pot.offset.y]) 
					&& path.IndexOf(grid[(int)pot.sourceNode.x+(int)pot.offset.x, (int)pot.sourceNode.y+(int)pot.offset.y]) > path.IndexOf(pot.pathExitNode))) {
						pot.markForRemoval = true;
					}
				}
			}
		}
		for (int i = 0; i < potential.Count; i++) {
			if (potential[i].markForRemoval) {
				potential.Remove(potential[i]);
				i--;
			}
		}
		Arrangement final = potential[Random.Range(0, potential.Count-1)];
		/*/
		/// Step 4: Clean up eaten path, mark occupied grid spots as occupied
		/*/
		if (isPathed) {
			while (path[0] != final.pathExitNode) {
				path.Remove(path[0]);
			}
			path.Remove(path[0]);
		}
		Vector2 dimF = final.original.dimensions;
		if (final.rotations%2 == 1) { 
			dimF = new Vector2(dimF.y, dimF.x);
		}
		for (int i = 0; i < dimF.x; i++) {
			for (int j = 0; j < dimF.y; j++) {
				grid[(int)final.sourceNode.x+(int)final.offset.x, (int)final.sourceNode.y+(int)final.offset.y].occupied = true;
			}
		}
		return final;
	}

	Vector3 RotateCW(Vector3 orig, Vector2 dim) {
		float x;
		float y;
		float z;
		switch ((int)orig.z) {
			default: //case 2
				z = 4;
				break;
			case 4:
				z = 8;
				break;
			case 8:
				z = 6;
				break;
			case 6:
				z = 2;
				break;
		}
		x = orig.y;
		y = dim.y + 1 - orig.x;
		return new Vector3(x,y,z);
	}

	// Use A* pathfinding algorithm to generate a path from start to end, so that we always have a way to the exit
	List<Node> FindPath() {
		List<Node> closedSet = new List<Node>(0);
		List<Node> openSet = new List<Node>(0);
		openSet.Add(start);
		foreach (Node n in grid) {
			if (n != start) {
				n.s_Cost = 9999999;
				n.f_Cost = 9999999;
			}
			if (n == start) {
				n.s_Cost = 0;
				n.f_Cost = Node.NodeDistance(start,end);
			}
		}
		while (openSet.Count > 0) {
			Node current = openSet[0];
			foreach (Node a in openSet) {
				if (a.f_Cost < current.f_Cost) {
					current = a;
				}
			}
			if (current == end) {
				return BuildPath(current);
			}
			openSet.Remove(current);
			closedSet.Add(current);

			Node neighbor;
			if (current.x != 0) {
				neighbor = grid[current.x-1, current.y]; 
				if (!closedSet.Contains(neighbor) && !openSet.Contains(neighbor)) {
					openSet.Add(neighbor);
					CompareNeighbors(neighbor,current);
				}
			}
			if (current.x < gX-1) {
				neighbor = grid[current.x+1, current.y];
				if (!closedSet.Contains(neighbor) && !openSet.Contains(neighbor)) {
					openSet.Add(neighbor);
					CompareNeighbors(neighbor,current);
				}
			}
			if (current.y != 0) {
				neighbor = grid[current.x, current.y-1];
				if (!closedSet.Contains(neighbor) && !openSet.Contains(neighbor)) {
					openSet.Add(neighbor);
					CompareNeighbors(neighbor,current);
				}
			}
			if (current.y < gY - 1) {
				neighbor = grid[current.x, current.y+1];
				if (!closedSet.Contains(neighbor) && !openSet.Contains(neighbor)) {
					openSet.Add(neighbor);
					CompareNeighbors(neighbor,current);
				}
			}
		}
		return null;
	}

	void CompareNeighbors(Node neighbor, Node current) {
		int tempS_Cost = current.s_Cost + Node.NodeDistance(current, neighbor) + neighbor.weight;
		if (tempS_Cost < neighbor.s_Cost) {
			neighbor.cameFrom = current;
			neighbor.s_Cost = tempS_Cost;
			neighbor.f_Cost = neighbor.s_Cost + Node.NodeDistance(neighbor, end);
		}
	}

	//Get a path from the nodes
	List<Node> BuildPath(Node n) {
		Node currentNode = n;
		List<Node> route = new List<Node>(0);
		while (currentNode != start) {
			route.Add(currentNode);
			currentNode = currentNode.cameFrom;
		}
		route.Add(start);
		route.Reverse();
		return route;
	}

	//Creating the rooms for testing grid generation
	void InstantiateRooms() {
		foreach (Node n in path) {
			GameObject room;
			switch (n.roomType) {
			default: 
				room = Instantiate(categories[0].components[Random.Range(0,categories[0].components.Length-1)].prefab);
				room.transform.position = new Vector3(n.x*4, 0, n.y*4);
				room.transform.Find("Cube").GetComponent<MeshRenderer>().material = pathMat;
				break;
			case 1: 
				room = Instantiate(categories[1].components[Random.Range(0,categories[1].components.Length-1)].prefab);
				room.transform.position = new Vector3(n.x*4, 0, n.y*4);
				break;
			case 2: 
				room = Instantiate(categories[2].components[Random.Range(0,categories[2].components.Length-1)].prefab);
				room.transform.position = new Vector3(n.x*4, 0, n.y*4);
				break;
			}
			room.name = n.x + "|" + n.y;
		}
	}

	//Creating the rooms from a list
	void InstantiateRooms(List<Arrangement> roomsToCreate) {
		foreach (Arrangement room in roomsToCreate) {
			GameObject init = Instantiate(room.original.prefab);
			float x;
			float y;
			if (room.rotations%2==1) {
				x = (room.sourceNode.x*4)+(room.original.dimensions.y*4/2);
				y = (room.sourceNode.y*4)+(room.original.dimensions.x*4/2);
			} else {
				x = (room.sourceNode.x*4)+(room.original.dimensions.x*4/2);
				y = (room.sourceNode.y*4)+(room.original.dimensions.y*4/2);
			}
			init.transform.position = new Vector3(x, 0, y);
			for (int i = 0; i < room.rotations; i++) {

			}
		}
	}

	//Make a random? room into either a start or end room
	bool AssignRandomRoom(int i, int quad) {
		int rX, rY;
			// We get a random position in the grid in the quadrant we want - if there's enough room for minimum distance to be taken into account, we use it
		switch (quad) {
		case 1:
			if (minDistance < gX) {
				rX = Random.Range((gX+minDistance)/2, gX);
			} else {rX = Random.Range(gX/2, gX);}
			if (minDistance < gY) {
				rY = Random.Range((gY+minDistance)/2, gY);
			} else {rY = Random.Range(gY/2, gY);}
		break;
		case 2:
			if (minDistance < gX) {
				rX = Random.Range(0, (gX-minDistance)/2);
			} else {rX = Random.Range(0, gX/2);}
			if (minDistance < gY) {
				rY = Random.Range((gY+minDistance)/2, gY);
			} else {rY = Random.Range(gY/2, gY);}
		break;
		case 3:
			if (minDistance < gX) {
				rX = Random.Range(0, (gX-minDistance)/2);
			} else {rX = Random.Range(0, gX/2);}
			if (minDistance < gY) {
				rY = Random.Range(0, (gY-minDistance)/2);
			} else {rY = Random.Range(0, gY/2);}
		break;
		case 4:
			if (minDistance < gX) {
				rX = Random.Range((gX+minDistance)/2, gX);
			} else {rX = Random.Range(gX/2, gX);}
			if (minDistance < gY) {
				rY = Random.Range(0, (gY-minDistance)/2);
			} else {rY = Random.Range(0, gY/2);}
		break;
		default:
			rX = Random.Range(0, gX);
			rY = Random.Range(0, gY);
		break;
		}
		if (grid[rX,rY].roomType == 0) {
			grid[rX,rY].roomType = i;
			if (i == 1) {
				start = grid[rX,rY];
			}
			if (i == 2) {
				end = grid[rX,rY];
			}
			return true;
		}
		return false;
	}

	//Set up the 2d grid array, 0 as default entry
	void InitializeGrid(int x, int y) {
		grid = new Node[x,y];
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				grid[i,j] = new Node();
				grid[i,j].roomType = 0;
				grid[i,j].x = i;
				grid[i,j].y = j;
			}
		}
	}
}
