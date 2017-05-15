using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour {

    public Transform target;
    public float speed = 20f;
    public float stoppingDistance = 0;
    public List<Node> path;

    private Graph graph;
    private float remainingDistance = 0;

	// Use this for initialization
	void Start () {
        // Find graph in the scene
        graph = FindObjectOfType<Graph>();

        // Check for errors
        if (graph == null)
        {
            // Log an error
            Debug.LogError("Error: There is no generated graph in the scene!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Node> RunAstar(Vector3 startPos, Vector3 targetPos)
    {
        // The set of nodes to be evaluated
        List<Node> openList = new List<Node>();
        // The set of nodes that are already evaluated
        List<Node> closedList = new List<Node>();
        // Get the startNode and targetNode
        Node startNode = graph.GetNodeFromPosition(startPos);
        Node targetNode = graph.GetNodeFromPosition(targetPos);
        // Add the start node to openList
        openList.Add(startNode);
        // While not all nodes have been checked
        while (openList.Count > 0)
        {
            // Set currentNde to node in openList with lowest fCost
            Node currentNode = FindShortedNode(openList);
            // Remove currentNode from Open
            openList.Remove(currentNode);
            // Add currentNode to Closed
            closedList.Add(currentNode);
            // Check if the currentNode is our targetNode
            if (currentNode == targetNode)
            {
                // We have found our path!
                path = RetracePath(startNode, targetNode);
                // Return the path
                return path;
            }

            // Loop through each neighbour of the currentNode
            foreach (Node neighbour in graph.GetNeighbours(currentNode))
            {
                
            }

        }

        return path;

    }

    Node FindShortedNode(List<Node> nodeList)
    {
        // Get current node
        Node shortestNode = null;
        // Check if the fCost is less than minFCost AND
        // Check if the hCost is less than minHcost
        float minFCost = float.MaxValue;
        float minHCost = float.MaxValue;
        for (int i = 0; i < nodeList.Count; i++)
        {
            // Set the shortest node and nosts to currentNode's
            Node currentNode = nodeList[i];
            if (currentNode.fCost <= minFCost && currentNode.hCost <= minHCost)
            {
                minFCost = currentNode.fCost;
                minHCost = currentNode.hCost;
                shortestNode = currentNode;
            }
        }
        return shortestNode;
    }

    List<Node> RetracePath(Node start, Node end)
    {
        // Create a list to store the path
        List<Node> path = new List<Node>();
        // Set the currentNode to end node
        Node currentNode = end;
        // Retrace the path back to the start node
        while (currentNode != start)
        {
            // Add the curent node to the path
            path.Add(currentNode);
            // Traverse up the parent tree
            currentNode = currentNode.parent;
        }

        // The path is in reverse order so reverse the path before returning
        path.Reverse();
        return path;
    }
}
