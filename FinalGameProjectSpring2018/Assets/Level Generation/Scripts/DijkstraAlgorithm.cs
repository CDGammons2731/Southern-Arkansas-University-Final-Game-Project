using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DijkstraAlgorithm {
	
	public static List<Node> FindPath(Node start, Node end, Node[,] grid, int gX, int gY) {
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
				return BuildPath(current, start);
			}
			openSet.Remove(current);
			closedSet.Add(current);

			Node neighbor;
			if (current.x != 0) {
				neighbor = grid[current.x-1, current.y]; 
				if (!closedSet.Contains(neighbor) && !openSet.Contains(neighbor)) {
					openSet.Add(neighbor);
					CompareNeighbors(neighbor,current,end);
				}
			}
			if (current.x < gX-1) {
				neighbor = grid[current.x+1, current.y];
				if (!closedSet.Contains(neighbor) && !openSet.Contains(neighbor)) {
					openSet.Add(neighbor);
					CompareNeighbors(neighbor,current,end);
				}
			}
			if (current.y != 0) {
				neighbor = grid[current.x, current.y-1];
				if (!closedSet.Contains(neighbor) && !openSet.Contains(neighbor)) {
					openSet.Add(neighbor);
					CompareNeighbors(neighbor,current,end);
				}
			}
			if (current.y < gY - 1) {
				neighbor = grid[current.x, current.y+1];
				if (!closedSet.Contains(neighbor) && !openSet.Contains(neighbor)) {
					openSet.Add(neighbor);
					CompareNeighbors(neighbor,current,end);
				}
			}
		}
		return null;
	}

	static void CompareNeighbors(Node neighbor, Node current, Node end) {
		int tempS_Cost = current.s_Cost + Node.NodeDistance(current, neighbor) + neighbor.weight;
		if (tempS_Cost < neighbor.s_Cost) {
			neighbor.cameFrom = current;
			neighbor.s_Cost = tempS_Cost;
			neighbor.f_Cost = neighbor.s_Cost + Node.NodeDistance(neighbor, end);
		}
	}
		
	static List<Node> BuildPath(Node n, Node start) {
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
}
