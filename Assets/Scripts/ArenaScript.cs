using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class ArenaScript : MonoBehaviour
{
    ProBuilderMesh mesh;
    IList<Edge> edges;
    public Material arenaMaterial;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshRenderer>().sharedMaterial = arenaMaterial;
        edges = new List<Edge>();
        mesh = this.GetComponent<ProBuilderMesh>();
        foreach(Face f in mesh.faces)
        {
            foreach(Edge e in f.edges)
            {
                edges.Add(e);
            }
        }
        //Bevel.BevelEdges(mesh, edges, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
