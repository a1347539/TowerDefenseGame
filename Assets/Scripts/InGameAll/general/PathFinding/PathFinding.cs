using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using System.Linq;

public static class PathFinding
{
   
    public static Dictionary<Point, Node> nodes { get; private set; }

    public static List<Node> Path;

    private static void CreateNodes(int gameMode)
    {
        nodes = new Dictionary<Point, Node>();

        if (gameMode == 0)
        {

            foreach (Tile tile in LevelManager.Instance.Tiles.Values)
            {
                nodes.Add(tile.GridPosition, new Node(tile));
            }
        }
        else if (gameMode == 1)
        {

            foreach (Tile_Online tile in LevelManager_Online.Instance.Tiles.Values)
            {
                nodes.Add(tile.GridPosition, new Node(tile));
            }
        } 

    }
    public static bool FindPath(Point startPosition, Point endPosition)
    {
        return FindPathMain(startPosition, endPosition, 0);
    }

    public static bool FindPath(Point startPosition, Point endPosition, int gameMode)
    {
        return FindPathMain(startPosition, endPosition, gameMode);
    }

    public static bool FindPathMain(Point startPosition, Point endPosition, int gameMode)
    {
        if (nodes == null)
        {
            CreateNodes(gameMode);
        }

        int endState = 0;

        Path = new List<Node>();

        HashSet<Node> openList = new HashSet<Node>();

        HashSet<Node> closeList = new HashSet<Node>();

        Node current_node = nodes[startPosition];

        nodes[startPosition].G = nodes[startPosition].H = nodes[startPosition].F = 0;

        openList.Add(current_node);

        while (openList.Count > 0)
        {
            current_node = openList.OrderBy(x => x.F).First();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighbourPosition = new Point(current_node.tilePosition.x - x, current_node.tilePosition.y - y);
                    
                    if (Math.Abs(x) == Math.Abs(y))
                    { 
                        continue; 
                    }

                    if (InBound(neighbourPosition, gameMode))
                    {
                        if (TileIsValid(neighbourPosition, current_node.tilePosition, gameMode))
                        {

                            Node neighbour = nodes[neighbourPosition];

                            if (!openList.Contains(neighbour) && !closeList.Contains(neighbour))
                            {
                                openList.Add(neighbour);
                                cost(current_node.tilePosition, neighbour.tilePosition, endPosition);
                                neighbour.Parent = current_node;
                            }
                            else if (openList.Contains(neighbour))
                            {
                                if (current_node.G + 10 < neighbour.G)
                                {
                                    cost(current_node.tilePosition, neighbour.tilePosition, endPosition);
                                    neighbour.Parent = current_node;
                                }
                            }
                        }
                    }
                }
            }

            openList.Remove(current_node);
            closeList.Add(current_node);
            
            if (current_node == nodes[endPosition])
            {
                endState = 1;
                GetPath(current_node, nodes[startPosition]);
                break;
            }
        }
        if (endState != 1) { return false; }
        else { return true; }
    }


    private static bool InBound(Point neighbourPosition, int gameMode)
    {
        Point mapSize = new Point(0, 0);

        if (gameMode == 0)
        { mapSize = LevelManager.Instance.mapSize; }
        else
        { mapSize = LevelManager_Online.Instance.mapSize; }

        return ((neighbourPosition.x >= 0 && neighbourPosition.y >= 0) && (neighbourPosition.x < mapSize.x && neighbourPosition.y < mapSize.y));
    }

    private static bool TileIsValid(Point neighbourPosition, Point current_node, int gameMode)
    {
        if (gameMode == 0)
        { 
            return ((neighbourPosition != current_node) && (LevelManager.Instance.Tiles[neighbourPosition].IsWalkable)); 
        }
        else
        {
            return ((neighbourPosition != current_node) && (LevelManager_Online.Instance.Tiles[neighbourPosition].IsWalkable));
        }
    }

    private static void cost(Point origin, Point neighbour, Point endPosition)
    {

        nodes[neighbour].G = 10 + nodes[origin].G;

        nodes[neighbour].H = (Math.Abs(neighbour.x - endPosition.x) + Math.Abs(neighbour.y - endPosition.y)) * 10;

        nodes[neighbour].F = nodes[neighbour].G + nodes[neighbour].H;
    }

    private static void GetPath(Node current_node, Node start_node)
    {
        while (current_node != start_node)
        {
            Path.Insert(0, current_node);
            current_node = current_node.Parent;
        }
        Path.Insert(0, current_node);
    }
}