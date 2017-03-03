/* Shaun Yonkers
 * 1650692
 * CMPT430
 * Lab 3: A* pathfinding
 * 
 * Graph class is used to read in the xml graph and translate the xml into a workable graph to find the shortest path
 *
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class Graph
{

    public Dictionary<string, Node> nodes;
    public List<Edge> edge;

    public string beginningNode;
    public string endingNode;

    /* ******** Read Graph Function ***********
    *
    *Description: read the graph in from the xml file
    *
    *Inputs: fName: the filepath to the file
    *        isFile: bool check to see if the file exists
    */
    public void readGraph(string fName, bool isFile)
    {
        if (isFile)
        {
            using (XmlReader reader = XmlReader.Create(new StreamReader(fName)))
                parseXML(reader);
        }
        else {
            TextAsset data = Resources.Load(fName) as TextAsset;
            using (XmlReader reader = XmlReader.Create(new StringReader(data.text)))
                parseXML(reader);
        }
    }

    /* ******** Parse XML Function ***********
    *
    *Description: parse the xml file into a list of edges and dictionary of nodes
    *
    *Inputs: reader: the xmlreader to get the information from the xml file
    */
    private void parseXML(XmlReader reader)
    {
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    switch (reader.Name)
                    {
                        case "Graph":
                            edge = new List<Edge>();
                            nodes = new Dictionary<string, Node>();
                            break;

                        case "Node":
                            Node newNode = new Node(reader.GetAttribute("Name"),
                                new Vector3((float)System.Convert.ToDouble(reader.GetAttribute("X")),
                                (float)System.Convert.ToDouble(reader.GetAttribute("Y")),
                                (float)System.Convert.ToDouble(reader.GetAttribute("Z"))));
                            if (nodes.ContainsKey(newNode.id))
                                nodes[newNode.id] = newNode;
                            else
                                nodes.Add(newNode.id, newNode);
                            break;

                        case "Edge":
                            string start = reader.GetAttribute("Start");
                            string end = reader.GetAttribute("End");
                            float cost = (float)System.Convert.ToDouble(reader.GetAttribute("Cost"));
                            if (nodes.ContainsKey(start) && nodes.ContainsKey(end))
                            {
                                Edge fwdEdge = new Edge(nodes[end], cost);
                                Edge backEdge = new Edge(nodes[start], cost);
                                nodes[start].addEdge(fwdEdge);
                                nodes[end].addEdge(backEdge);
                            }
                            break;

                        case "Path":
                            beginningNode = reader.GetAttribute("Start");
                            endingNode = reader.GetAttribute("End");
                            break;

                    }
                    break;
            }
        }
    }
}
