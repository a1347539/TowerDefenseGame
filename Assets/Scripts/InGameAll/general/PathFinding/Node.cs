using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Node
{
    public Point tilePosition { get; private set; }

    public Tile Tile { get; private set; }

    public Tile_Online Online_Tile { get; private set; }

    public Vector3 position { get; private set; }

    public int G { get; set; }

    public int H { get; set; }

    public int F { get; set; }

    private Node parent;

    public Node Parent { get => parent; set => parent = value; }

    public Node(Tile Tile)
    {
        this.Tile = Tile;
        this.tilePosition = Tile.GridPosition;
        this.position = Tile.CenterOfGridInWorldPosition;
    }

    public Node(Tile_Online Tile)
    {
        this.Online_Tile = Tile;
        this.tilePosition = Tile.GridPosition;
        this.position = Tile.CenterOfGridInWorldPosition;
    }
}
