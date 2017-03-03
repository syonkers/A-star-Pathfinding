/* Shaun Yonkers
 * 1650692
 * CMPT430
 * Lab 3: A* pathfinding
 * 
 * Edge class used to handle edges in the XML graph
 *
 * */

using UnityEngine;
using System.Collections;

    public class Edge
    {

    private float _cost;
    private Node _end;
    private Node _back;

        public float cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
            }
        }

        public Node end
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        public Node back
        {
            get
            {
                return _back;
            }
            set
            {
                _back = value;
            }
        }

        public Edge(Node target, float c)
        {
            end = target;
            cost = c;
            back = null;
        }
    }

