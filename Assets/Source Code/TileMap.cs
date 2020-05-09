using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMap : MonoBehaviour
{
    GameObject Pointer;
    public TileType[] tileTypes;

    int[,] tiles;
    Node[,] graph;

    List<Node> currentPath = null;

    int mapSizeR = 10;
    int mapSizeC = 10;

    void Start()
    {
        Pointer = GameObject.Find("Pointer2");
        GenerateMapData();
        GeneratePathFindingGraph();
        GenerateMapVisual();
    }

    void GenerateMapData()
    {
        //Allocate map tiles
        tiles = new int[mapSizeR, mapSizeC];
        int row, column;

        //Initiate map tiles
        for (row = 0; row < mapSizeR; row++)
        {
            for (column = 0; column < mapSizeC; column++)
            {
                tiles[row, column] = 0;
            }
        }

        //for (int x=0; x < 20; x++)
        //{
        //    int m, n;
        //    m = (int)Random.Range(2.0f, 7.0f);
        //    n = (int)Random.Range(2.0f, 7.0f);
        //    tiles[m,n] = 1;
        //}

        tiles[2, 2] = 1;
        tiles[3, 2] = 1;
        tiles[3, 3] = 1;
        tiles[3, 4] = 1;
        tiles[2, 4] = 1;
        tiles[2, 5] = 1;
        tiles[4, 4] = 1;
        tiles[4, 5] = 1;
        tiles[5, 5] = 1;
        tiles[4, 3] = 1;
        tiles[5, 3] = 1;
        tiles[5, 2] = 1;

    }

    public class Node
    {
        public List<Node> edges;

        public Node()
        {
            edges = new List<Node>();
        }
    }
    
    void GeneratePathFindingGraph()
    {
        graph = new Node[mapSizeR, mapSizeC];
        int row, column;

        for (row = 0; row < mapSizeR; row++)
        {
            for (column = 0; column < mapSizeC; column++)
            {
                graph[row, column] = new Node();
            }
        }

        for (row=0;row<mapSizeR;row++)
        {
            for(column=0;column<mapSizeC;column++)
            {
                if (row > 0)
                    graph[row, column].edges.Add(graph[row - 1, column]);
                if (row < mapSizeR - 1f)
                    graph[row, column].edges.Add(graph[row + 1, column]);
                if (column > 0)
                    graph[row, column].edges.Add(graph[row, column - 1]);
                if (column < mapSizeC - 1f)
                    graph[row, column].edges.Add(graph[row, column + 1]);
            }
        }
    }

    void GenerateMapVisual()
    {
        for (int row = 0; row < mapSizeR; row++)
        {
            for (int column = 0; column < mapSizeC; column++)
            {
                TileType tiletype = tileTypes[tiles[row, column]];
                Instantiate(tiletype.tileVisualPrefab, new Vector3(row -4.5f, 0.5f, column -4.5f), Quaternion.identity);
                if(tiletype.name == "Wall")
                {
                    Instantiate(tiletype.tileVisualPrefab, new Vector3(row - 4.5f, 1.5f, column - 4.5f), Quaternion.identity);
                }
                switch(tiletype.name)
                {
                    case "Wall":
                        Instantiate(tiletype.tileVisualPrefab, new Vector3(row - 4.5f, 1.5f, column - 4.5f), Quaternion.identity);
                        break;
                    case "Grass":
                        TileType tiletypeMI = tileTypes[2];
                        Instantiate(tiletypeMI.tileVisualPrefab, new Vector3(row - 4.5f, 1.05f, column - 4.5f), Quaternion.identity);
                        break;
                };
            }
        }
    }

    public void GeneratePathTo(int x, int y)
    {
        SelectPlayer sp = Pointer.GetComponent<SelectPlayer>();
        GameObject player = sp.GetSelectedObject();

        player.GetComponent<PlayerMovement>().currentPath = null;

        Dictionary<Node, float> distance = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[
            (int)(player.transform.position.x + 4.5f), 
            (int)(player.transform.position.z + 4.5f)];

        Node target = graph[
            x,
            y];

        distance[source] = 0;
        prev[source] = null;

        foreach(Node vertex in graph)
        {
            if(vertex != source)
            {
                distance[vertex] = Mathf.Infinity;
                prev[vertex] = null;
            }

            unvisited.Add(vertex);
        }

        while(unvisited.Count > 0)
        {
            Node u = null;

            foreach(Node possibleU in unvisited)
            {
                if(u == null || distance[possibleU] < distance[u])
                {
                    u = possibleU;
                }
            }

            if(u == target)
            {
                break;
            }

            unvisited.Remove(u);
            
            foreach(Node v in u.edges)
            {
                float alt = distance[u] + 1;
                if(alt < distance[v])
                {
                    distance[v] = alt;
                    prev[v] = u;
                }
            }   
        }

        if (prev[target] == null)
        {
            return;
        }

        currentPath = new List<Node>();
        Node curr = target;

        while(curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        player.GetComponent<PlayerMovement>().currentPath = currentPath;
    }

}
