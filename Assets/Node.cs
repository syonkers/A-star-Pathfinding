/* Shaun Yonkers
 * 1650692
 * CMPT430
 * Lab 3: A* pathfinding
 * 
 * Node class used to handle nodes in the XML graph
 *
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    private Vector3 _position;
    private string _id;
    private List<Edge> _edge;
    private float _net;

    public Vector3 position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }

    public string id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public List<Edge> edge
    {
        get
        {
            return _edge;
        }
        set
        {
            _edge = value;
        }
    }

    public float net
    {
        get
        {
            return _net;
        }
        set
        {
            _net = value;
        }
    }

    public Node(string name, Vector3 where)
    {
        id = name;
        position = where;
        edge = new List<Edge>();
        net = 0;
    }

    public void addEdge(Edge newEdge)
    {
        edge.Add(newEdge);
    }

}
