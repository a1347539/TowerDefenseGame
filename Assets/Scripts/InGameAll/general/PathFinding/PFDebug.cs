using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class PFDebug : MonoBehaviour
{
    
    private Tile start, end;

    private List<Node> path = new List<Node>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ClickTile();

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (path.Count != 0)
            {
                start = end =  null;
                path.Clear();
            }
            else 
            {
                if (PathFinding.FindPath(start.GridPosition, end.GridPosition)) {
                    path = PathFinding.Path;
                    //DrawPath(); 
                }
                else 
                {
                    start = end = null;
                    UnityEngine.Debug.Log("Not found"); 
                }
                
            }
        }
    }

    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Tile tmp = hit.collider.GetComponent<Tile>();

                if (tmp != null)
                {
                    if (start == null)
                    {
                        start = tmp;
                        start.debugging = true;
                        start.spriteRenderer.color = new Color32(255, 132, 0, 255);
                    }
                    else if (end == null)
                    {
                        end = tmp;
                        end.debugging = true;
                        end.spriteRenderer.color = new Color32(255, 0, 0, 255);
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList, HashSet<Node> closeList)
    { 
        foreach(Node node in openList)
        {
            if (node.tilePosition != start.GridPosition || node.tilePosition != end.GridPosition)
            {
                node.Tile.spriteRenderer.color = Color.cyan;
                node.Tile.debugging = true;
            }
        }
    }

    public void DrawPath()
    {
        foreach (Node node in path)
        {
            print(node.tilePosition.x);
            if (node.tilePosition != start.GridPosition && node.tilePosition != end.GridPosition)
            {
                node.Tile.spriteRenderer.color = Color.cyan;
            }
        }
    }
}
