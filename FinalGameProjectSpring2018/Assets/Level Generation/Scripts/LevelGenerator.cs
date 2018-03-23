using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public int gX = 24, gY = 24;
	public int minDistance = 5;
	public int borderDistance = 5;
	public float gridScale = 4f;
	public int sideRooms = 100;

	public bool debugging = false;

	public Node[,] grid;
	Node start;
	Node end;
	List<Node> path = new List<Node>(0);
	List<Arrangement> roomsToPlace;
	List<DoorInfo> openDoors = new List<DoorInfo>(0);
	public Material connectedDoor;
	public Material requiredDoor;

	List<GameObject> rooms = new List<GameObject>(0);

	public GameObject gridVisual;
	public GameObject navRemover;

	public GameObject unlockedDoor;
	public GameObject lockedDoor;

	public bool loaded = false;

	public LevelGenCategory[] categories;
	/*
	// 0  - Normal Room
	// 1  - Start Room
	// 2  - End Room
	// 3  - Locked Room
	*/

	void Start() {
		StartCoroutine (GenerateLevel());
	}

	IEnumerator GenerateLevel() {
		while (!loaded) {
			if (gX * gY > 1) {
				InitializeGrid (gX, gY);
				yield return null;
				int r1Q = Random.Range (1, 4);
				int r2Q = Random.Range (1, 4);
				if (r1Q == r2Q) {
					r2Q++;
					if (r2Q == 5)
						r2Q = 1;
				}
				bool r1Assigned = false;
				do {
					r1Assigned = AssignRandomRoom (1, r1Q);
				} while (!r1Assigned);
				bool r2Assigned = false;
				do {
					r2Assigned = AssignRandomRoom (2, r2Q);
				} while (!r2Assigned);
				end.occupied = false;
				yield return null;
				for (int i = 0; i < gX; i++) {
					for (int j = 0; j < gY; j++) {
						grid [i, j].weight = Random.Range (0, 50);
					}
				}
				yield return null;
				start.weight = 0;
				path = DijkstraAlgorithm.FindPath(start, end, grid, gX, gY);
				roomsToPlace = new List<Arrangement> (0);
				List<Node> pathOrig = new List<Node> (0);
				foreach (Node n in path) {
					pathOrig.Add (n);
				}
				for (int i = 0; i < pathOrig.Count - 1; i++) {
					if (path.Contains (pathOrig [i])) {
						if (roomsToPlace.Count == 0) {
							roomsToPlace.Add (PlaceRoom (path [0], Arrangement.pathFailure, true));
						}
						roomsToPlace.Add (PlaceRoom (path [0], roomsToPlace [roomsToPlace.Count - 1].pathExitPt, true));
						yield return null;
					}
				}
				roomsToPlace.Add (PlaceRoom (end, roomsToPlace [roomsToPlace.Count - 1].pathExitPt, false));
				yield return null;
				InstantiateRooms (roomsToPlace);
				yield return null;
				roomsToPlace.Clear ();
				bool rLAssigned = false;
				Node lockRoom;
				if (openDoors.Count > 0) {
					do {
						lockRoom =  grid[Random.Range(0, gX), Random.Range(0,gY)];
						if (!lockRoom.occupied) {
							rLAssigned = true;
						}
					} while (!rLAssigned);
					lockRoom.weight = 0;
					lockRoom.roomType = 3;
					DoorInfo d = openDoors [Random.Range (0, openDoors.Count)];
					Node hallRoom = grid[(int)d.loc.x, (int)d.loc.y];
					path = DijkstraAlgorithm.FindPath(hallRoom, lockRoom, grid, gX, gY);
					roomsToPlace = new List<Arrangement> (0);
					pathOrig = new List<Node> (0);
					foreach (Node n in path) {
						pathOrig.Add (n);
					}
					for (int i = 0; i < pathOrig.Count - 1; i++) {
						if (path.Contains (pathOrig [i])) {
							if (roomsToPlace.Count == 0) {
								roomsToPlace.Add (PlaceRoom (path [0], Arrangement.pathFailure, true));
							}
							roomsToPlace.Add (PlaceRoom (path [0], roomsToPlace [roomsToPlace.Count - 1].pathExitPt, true));
							yield return null;
						}
					}
					roomsToPlace.Add (PlaceRoom (lockRoom, roomsToPlace [roomsToPlace.Count - 1].pathExitPt, false));
					yield return null;
					InstantiateRooms (roomsToPlace);
					yield return null;
					roomsToPlace.Clear ();
				}
				for (int i = 0; i < sideRooms; i++) {
					if (openDoors.Count > 0) {
						DoorInfo door = openDoors [Random.Range (0, openDoors.Count)];
						if (InBounds((int)door.loc.x, (int)door.loc.y)) {
							Arrangement r = PlaceRoom (grid [(int)(door.loc.x), (int)(door.loc.y)], door.loc - door.face, false);
							if (r != null) {
								roomsToPlace.Add (r);
								InstantiateRooms (roomsToPlace);
								roomsToPlace.Clear ();
							} else {
								openDoors.Remove (door);
								i--;
							}
							yield return null;
						}
					} else
						break;
				}
				RemoveExcessNavmesh ();
				DoorInfo[] dGrp = FindObjectsOfType<DoorInfo> ();
				for (int i = dGrp.Length - 1; i >= 0; i--) {
					if (dGrp [i].pair != null && dGrp[i] != null) {
						dGrp [i].pair.GetComponent<RoomInfo> ().AddToNeighborList (dGrp [i].gameObject);
						dGrp [i].GetComponent<RoomInfo> ().AddToNeighborList (dGrp [i].pair.gameObject);
						if (!dGrp [i].MarkForRemoval) {
							if (!dGrp[i].locked) {
								dGrp [i].PlaceDoorObject (unlockedDoor, gridScale);
							} else {
								dGrp [i].PlaceDoorObject (lockedDoor, gridScale);
							}
						}
						yield return null;
					}
				}
				for (int i = dGrp.Length - 1; i >= 0; i--) {
					if (dGrp [i].MarkForRemoval) {
						Destroy (dGrp [i]);
					}
				}
			} 
			loaded = true;
		}
		StopCoroutine (GenerateLevel ());
	}
		
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
		
	bool AssignRandomRoom(int i, int quad) {
		int rX, rY;
		switch (quad) {
		case 1:
			if (minDistance < gX) {
				rX = Random.Range((gX+minDistance)/2, gX-borderDistance);
			} else {rX = Random.Range(gX/2, gX);}
			if (minDistance < gY) {
				rY = Random.Range((gY+minDistance)/2, gY-borderDistance);
			} else {rY = Random.Range(gY/2, gY);}
			break;
		case 2:
			if (minDistance < gX) {
				rX = Random.Range(0+borderDistance, (gX-minDistance)/2);
			} else {rX = Random.Range(0, gX/2);}
			if (minDistance < gY) {
				rY = Random.Range((gY+minDistance)/2, gY-borderDistance);
			} else {rY = Random.Range(gY/2, gY);}
			break;
		case 3:
			if (minDistance < gX) {
				rX = Random.Range(0+borderDistance, (gX-minDistance)/2);
			} else {rX = Random.Range(0, gX/2);}
			if (minDistance < gY) {
				rY = Random.Range(0+borderDistance, (gY-minDistance)/2);
			} else {rY = Random.Range(0, gY/2);}
			break;
		case 4:
			if (minDistance < gX) {
				rX = Random.Range((gX+minDistance)/2, gX-borderDistance);
			} else {rX = Random.Range(gX/2, gX);}
			if (minDistance < gY) {
				rY = Random.Range(0+borderDistance, (gY-minDistance)/2);
			} else {rY = Random.Range(0, gY/2);}
			break;
		default:
			rX = Random.Range(0, gX);
			rY = Random.Range(0, gY);
			break;
		}
		if (grid[rX,rY].roomType == 0 && !grid[rX,rY].occupied) {
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

	Arrangement PlaceRoom(Node source, Vector2 prev, bool isPathed) {
		List<Arrangement> potential = new List<Arrangement>();
		/*/
		/// Step 1: Get all the initial arrangements that can be in that spot, assuming nothing's there and we don't have to connect to anything else
		/*/
		foreach (LevelGenPrefab pref in categories[source.roomType].components) {
			if (prev==Arrangement.pathFailure) {
				for (int i = 0; i < pref.dimensions.x; i++) {
					for (int j = 0; j < pref.dimensions.y; j++) {
						Arrangement arr = new Arrangement();
						arr.offset = new Vector2(i,j);
						arr.sourceNode = source;
						arr.original = pref;
						potential.Add(arr);
					}
				}
			} else {
				for (int i = 0; i < pref.dimensions.x; i++) {
					for (int j = 0; j < pref.dimensions.y; j++) {
						foreach (Vector2 door in pref.doorLocations) {
							if (prev == source.Location()-new Vector2(i,j)+door-Vector2.one) {
								Arrangement arr = new Arrangement();
								arr.offset = new Vector2(i,j);
								arr.sourceNode = source;
								arr.doorEntry = door;
								arr.original = pref;
								potential.Add(arr);
							}
						}
					}
				}
			}
		}
		/*/
		/// Step 2: Check every door for every arrangement, see which door leads the furthest along the path, if there aren't any, we don't use that arrangement
		/// This is purely for rooms that are on generated paths
		/*/
		if (isPathed) {
			foreach (Arrangement pot in potential) {
				for (int i = path.IndexOf(pot.sourceNode); i < path.Count; i++) {
					foreach (Vector2 door in pot.original.doorLocations) {	
						if (path[i].Location() == pot.sourceNode.Location()-Vector2.one-pot.offset+door) {
							pot.pathExitPt = path[i].Location();
							//Door will never be on a corner
							if (door.x > pot.original.dimensions.x) { //Door is on the right
								pot.pathExitPt+=new Vector2(-1,0);
							} else if (door.x < 1) { //Door is on the left
								pot.pathExitPt+=new Vector2(1,0);
							} else if (door.y > pot.original.dimensions.y) { //Door is up
								pot.pathExitPt+=new Vector2(0,-1);
							} else if (door.y < 1) { //Door is down
								pot.pathExitPt+=new Vector2(0,1);
							}
							pot.pathExitNode = path[i];
						}
					}
				}
				if (pot.pathExitPt == Arrangement.pathFailure) {
					pot.markForRemoval=true;
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
			for (int i = (int)(pot.sourceNode.x-pot.offset.x); i < (int)(pot.sourceNode.x-pot.offset.x+pot.original.dimensions.x); i++) {
				for (int j = (int)(pot.sourceNode.y-pot.offset.y); j < (int)(pot.sourceNode.y-pot.offset.y+pot.original.dimensions.y); j++) {
					if (InBounds(i,j)) {
						if ((isPathed && path.Contains(grid[i,j]) && path.IndexOf(grid[i,j]) > path.IndexOf(pot.pathExitNode))) {
							pot.markForRemoval=true;
						}
						if (grid[i,j].occupied == true) {
							pot.markForRemoval=true;
						}
					} else {
						pot.markForRemoval=true;
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
		/*/
		/// Step 4: Clean up eaten path, mark occupied grid spots as occupied, get door information
		/*/
		if (potential.Count == 0) {
			InstantiateRooms(roomsToPlace);
			return null;
		}
		Arrangement use = potential[Random.Range(0,potential.Count)];
		if (isPathed) {
			path.RemoveRange(0, path.IndexOf(use.pathExitNode));
		}
		for (int i = (int)(use.sourceNode.x-use.offset.x); i < (int)(use.sourceNode.x-use.offset.x+use.original.dimensions.x); i++) {
			for (int j = (int)(use.sourceNode.y-use.offset.y); j < (int)(use.sourceNode.y-use.offset.y+use.original.dimensions.y); j++) {
				if (i < gX && j < gY) {
					grid[i,j].occupied=true;
				}
			}
		}
		return use;
	}

	bool InBounds(int x, int y) {
		if (x < gX && y < gY && x >= 0 && y >= 0) return true;
		else return false;
	}
		
	void InstantiateRooms(List<Arrangement> roomsToCreate) {
		foreach (Arrangement room in roomsToCreate) {
			GameObject init = Instantiate(room.original.prefab);
			float x = room.original.dimensions.x/2;
			float y = room.original.dimensions.y/2;
			init.transform.position = new Vector3(x+room.sourceNode.x-room.offset.x, 0, y+room.sourceNode.y-room.offset.y)*gridScale;
			init.name = "Room " + roomsToCreate.IndexOf (room) + " " + room.original.name;
			rooms.Add(init);
			RoomInfo ri = init.AddComponent<RoomInfo>();
			ri.SetInfo(this, init.transform.position/gridScale, room.original.dimensions.x, room.original.dimensions.y);
			for (int j = 0; j < room.original.doorLocations.Length; j++) {
				DoorInfo doorInf = init.AddComponent<DoorInfo>();
				if (room.original.LockedRoom) {
					doorInf.locked = true;
				}
				Vector2 d = room.original.doorLocations [j];
				doorInf.loc = room.sourceNode.Location () - room.offset - Vector2.one + d;
				//Door will never be on a corner
				if (d.x > room.original.dimensions.x) { //Door is on the right
					doorInf.face = Vector2.right;
				} else if (d.x < 1) { //Door is on the left
					doorInf.face = Vector2.left;
				} else if (d.y > room.original.dimensions.y) { //Door is up
					doorInf.face = Vector2.up;
				} else if (d.y < 1) { //Door is down
					doorInf.face = Vector2.down;
				}
				foreach (DoorInfo other in openDoors) {
					Material mat;
					if (doorInf.loc == room.sourceNode.Location () - room.offset - Vector2.one + room.doorEntry || doorInf.loc == room.sourceNode.Location () - room.offset - Vector2.one + room.pathExitPt && room.needsReqDoor) {
						room.needsReqDoor = false;
					}
					bool con = doorInf.PairDoors(other);
					if (con) {
						other.MarkForRemoval = true;
					}
				}
				if (doorInf.pair == null) {
					openDoors.Add(doorInf);
				}
			}
		}
		for (int i = 0; i < openDoors.Count; i++) {
			if (openDoors [i].MarkForRemoval) {
				openDoors.Remove (openDoors [i]);
				i--;
			}
		}
	}

	void RemoveExcessNavmesh() {
		for (int i = 0; i < gX; i++) {
			bool start = false;
			Vector2 stNode = Vector2.zero;
			Vector2 enNode = Vector2.zero;
			for (int j = 0; j < gY; j++) {
				if (grid[i,j].occupied == false && !start) {
					stNode = new Vector2 (i,j);
					start = true;
				}
				if ((grid[i,j].occupied == true) && start) {
					enNode = new Vector2 (i,j);
					start = false;
					GameObject cut = Instantiate(navRemover);
					cut.transform.position = new Vector3(gridScale/2,0,0)+new Vector3 (i, 0, (stNode.y + enNode.y)/2) * gridScale;
					cut.transform.localScale = new Vector3(1, 1, (enNode.y-stNode.y))*gridScale;
				}
				if (j == gY-1 && start) {
					enNode = new Vector2 (i,j);
					start = false;
					GameObject cut = Instantiate(navRemover);
					cut.transform.position = new Vector3(gridScale/2,0,0)+new Vector3 (i, 0, (1+(stNode.y + enNode.y))/2) * gridScale;
					cut.transform.localScale = new Vector3(1, 1, 1+(enNode.y-stNode.y))*gridScale;
				}
			}
		}
	}
}
