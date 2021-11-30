using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    //Config variables
    [Range(1,100)]
    public int width = 1;
    [Range(1,100)]
    public int height = 1;
    [Range(1,6)]
    public int numDivides = 1;

    //Internal variables
    private Node[,] map;

    private void Start()
    {
        InitializeMap();
        CreateDividers();
    }

    private void InitializeMap()
    {
        map = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Assign walls
                map[x, y] = new Node(x, y, Node.Type.Unwalkable);
            }
        }
    }

    private void CreateDividers()
    {
        
    }


    private void OnDrawGizmos()
    {
        if (map == null) return;

        foreach (var item in map)
        {
            switch (item.TileType)
            {
                case Node.Type.Unassigned:
                    Gizmos.color = Color.black;
                    break;
                case Node.Type.Unwalkable:
                    Gizmos.color = Color.red;
                    break;
                case Node.Type.Walkable:
                    Gizmos.color = Color.blue;
                    break;
                default:
                    break;
            }
            Gizmos.DrawCube(new Vector3(item.X, 0, item.Y), Vector3.one);
        }
    }
}

public class Node
{
    //Internal variables
    private int x, y;
    private Type tileType;

    //Accessor variables
    public int X => x;
    public int Y => y;
    public Vector3 worldPoint => new Vector3(x, 0, y);
    public Type TileType => tileType;

    //Constructors
    public Node(int x, int y, Type tileType)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
    }

    public Node()
    {
        x = 0;
        y = 0;
        tileType = Type.Unassigned;
    }

    //Enum Definitions
    public enum Type
    {
        Unassigned, Unwalkable, Walkable
    }
}

public class Divider
{
    int x1,x2,y1,y2;
    Divider right, left;
    Room roomWithin;

    void CreateRoomWithin()
    {
        int roomX1 = Range(x1 + 1, x2 - 1);
        int roomX2 = Range(roomX1, x2);
        int roomY1 = Range (y1 + 1, y1 - 1);
        int roomY2 = Range(roomY1, y2);
        roomWithin = new Room(roomX1, roomX2, roomY1, roomY2);
    }

    public Divider(int x1, int x2, int y1, int y2)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.y1 = y1;
        this.y2 = y2;
    }

}

public class Room
{
    int x1,x2,y1,y2;

    public Room(int x1, int x2, int y1, int y2)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.y1 = y1;
        this.y2 = y2;
    }
}