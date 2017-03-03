/* Shaun Yonkers
 * 1650692
 * CMPT430
 * Lab 3: A* pathfinding
 * 
 * Main script that creates the map and draws the path from start to end. Blue nodes and edges are apart of the shortest path while red are not.
 * */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

public class Controller : MonoBehaviour {

    Graph g = new Graph();
    public Transform point;
    public Transform connector;

    private Transform clone;

    // Use this for initialization
    void Start () 
	{
        drawGraph();
        findAStarPath();
	}


    /* ********  Draw Graph Function *********
    *
    * Description: draws the initial graph by instantiating the point prefab
    *               for nodes and the connector prefab for edges.
    ******************************************/
    private void drawGraph()
    {
        string filename = Application.persistentDataPath + "/Save/Graph.xml";
        g.readGraph(filename, File.Exists(filename));
        
        //create all the nodes
        foreach (KeyValuePair<string, Node> n in g.nodes)
        {
            clone = Instantiate(point, new Vector3(n.Value.position.x, n.Value.position.y, n.Value.position.z), Quaternion.identity) as Transform;
			clone.gameObject.name = n.Value.id;
            clone.gameObject.GetComponent<ColorChanger>().start = n.Key;
            if ((n.Key == g.beginningNode) || (n.Key == g.endingNode))
            {
                clone.gameObject.GetComponent<ColorChanger>().addToPath();
            }

            //create all the edges from node to node
            foreach (Edge e in n.Value.edge)
            {
				clone = (Transform)Instantiate (connector, new Vector3 (0, 0, 0), Quaternion.identity);
				clone.gameObject.name = n.Value.id + " to " + e.end.id + " connector";
			
				clone.gameObject.GetComponent<ColorChanger> ().start = n.Key;
				clone.gameObject.GetComponent<ColorChanger> ().end = e.end.id;
			
				float temp = Vector3.Distance (n.Value.position, e.end.position) / 2;
				clone.localScale = new Vector3 (clone.localScale.x, temp, clone.localScale.z);
				clone.position = Vector3.Lerp (n.Value.position, e.end.position, (float)0.5);
				clone.transform.up = e.end.position - n.Value.position;
            }
        }
    }

    /*  ***** find A* Path Function ******
    *
    * Description: find the shortest path between the start and end nodes created from the graph using the A* algorithm
    *           nodes created from the graph using the A* algorithm
    *
    *
    ***************************************/

    private void findAStarPath()
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        Node current;

        //add starting node to open list
        foreach (KeyValuePair<string, Node> n in g.nodes)
        {
            if (n.Key == g.beginningNode)
            {
                openList.Add(n.Value);
            }

        }

        //while we havent found the shortest path
        while (openList.Count > 0)
        {

            //find node with lowest cost in open list and add to closed & remove from open list
            current = openList[0];
            foreach (Node n in openList)
            {
                if (n.net < current.net)
                {
                    current = n;
                }
            }
            openList.Remove(current);
            closedList.Add(current);

            //if we found the end node
            if (current.id == g.endingNode)
            {
                drawPath(current);
            }

            //for each neighbor of current node
            foreach (Edge e in current.edge)
            {
                //if not found in closed list
                if (!closedList.Contains(e.end))
                {
                    //if not found in the open list, add to open list
                    if  (!openList.Contains(e.end))
                    {
                        Node newNode = e.end;
                        newNode.net = newNode.net + current.net + e.cost;
                        foreach (Edge ee in newNode.edge)
                        {
                            ee.back = current;
                        }
                        openList.Add(newNode);
                    }
                    // if we find a shorter path to a node that is in the open list, update it with the new cost & parent
                   else
                   {
                        foreach (Node n in openList)
                        {
                            if ((e.end.id == n.id) && (e.cost + current.net < n.net))
                            {
                                n.net = e.cost + current.net;
                                foreach (Edge ee in n.edge)
                                {
                                    ee.back = current;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /* *************** Draw Path Function ********************
    *
    *   Description: Changes the color of all nodes and edges from the ending node to the starting node
                    to the color blue to indicate the shortest path
    *
    *   Inputs: currentNode: the node found the be the end node
    *
    ***********************************************/

    private void drawPath(Node currentNode)
    {
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("point");
		GameObject[] connectorObjects = GameObject.FindGameObjectsWithTag("connector");

        //while we haven't got back to the starting node
        while (currentNode.id != g.beginningNode)
        {
            //find the edge between the current node and the next
            foreach (GameObject obj in connectorObjects)
            {
                //change color of edges that are between the two nodes
                ColorChanger cc = obj.GetComponent<ColorChanger>();
                if ((cc.start == currentNode.id) && (cc.end == currentNode.edge[0].back.id))
                {
                    cc.addToPath();
                }
                else if ((cc.end == currentNode.id) && (cc.start == currentNode.edge[0].back.id))
                {
                    cc.addToPath();
                }
            }
            currentNode = currentNode.edge[0].back;

            //find new currentNode gameobject and change its color
            foreach (GameObject obj in nodeObjects)
            {
                if (obj.GetComponent<ColorChanger>().start == currentNode.id)
                {
                    obj.GetComponent<ColorChanger>().addToPath();
                }
            }
        }
    }
}
