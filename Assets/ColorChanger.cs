/* Shaun Yonkers
 * 1650692
 * CMPT430
 * Lab 3: A* pathfinding
 * 
 * Color changer script holds the variables that identify each instantiated object and allows the object to change color if it is apart of the shortest path
 * */

using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

    public Color startingColor;
    public Color pathColor;
    public string start;
    public string end;

    private Material mat;
    private bool partOfPath = false;

	// Use this for initialization
	void Start () {
        mat = new Material(Shader.Find("Diffuse"));
        mat.color = startingColor;
        this.GetComponent<Renderer>().material = mat;
    }
	
	// Update is called once per frame
	void Update () {
        if (partOfPath)
        {
            mat = new Material(Shader.Find("Diffuse"));
            mat.color = pathColor;
            this.GetComponent<Renderer>().material = mat;
        }
    }

    public void addToPath()
    {
        partOfPath = true;
    }
}
