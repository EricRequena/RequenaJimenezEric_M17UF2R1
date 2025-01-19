using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{
    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY,
        FLOOR2

    }

    // Variables
    public Grid [,] gridHandler;
    public List<WalkerObject> Walkers;
    public Tilemap tileMap;
    public Tilemap colisionMap;
    public Tilemap outMap;
    public Tile Floor;
    public Tile Wall;
    public Tile Floor2;
    public Tile Empty;
    public int MapWidth = 30;
    public int MapHeight = 30;

    public int MaximumWalkers = 10;
    public int TileCount = default;
    public float FillPercentage = 0.4f;
    public float WaitTime = 0.05f;

    void Start ()
    {
        InitializeGrid ();
    }

    void InitializeGrid()
{
    gridHandler = new Grid[MapWidth, MapHeight];

        for (int x = 0; x < gridHandler.GetLength (0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength (1); y++)
            {
                gridHandler [x, y] = Grid.EMPTY;
                Vector3Int tilePosition = new Vector3Int (x, y, 0);
                outMap.SetTile (tilePosition, Empty);
            }
        }


        Walkers = new List<WalkerObject>();

    // El centro del mapa en coordenadas (0, 0)
    Vector2Int start = new Vector2Int(0, 0);
    Vector3Int tileCenter = ConvertToTilemapCoords(start);

    WalkerObject curWalker = new WalkerObject(new Vector2(start.x, start.y), GetDirection(), 0.5f);
    gridHandler[tileCenter.x, tileCenter.y] = Grid.FLOOR;
    tileMap.SetTile(tileCenter, Floor);
    Walkers.Add(curWalker);

    TileCount++;

    StartCoroutine(CreateFloors());
}


    Vector2 GetDirection ()
    {
        int choice = Mathf.FloorToInt (UnityEngine.Random.value * 3.99f);

        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    IEnumerator CreateFloors ()
    {
        while ((float) TileCount / (float) gridHandler.Length < FillPercentage)
        {
            bool hasCreatedFloor = false;
            foreach (WalkerObject curWalker in Walkers)
            {
                Vector3Int curPos = ConvertToTilemapCoords (new Vector2Int ((int) curWalker.Position.x, (int) curWalker.Position.y));

                if ((gridHandler [curPos.x, curPos.y] != Grid.FLOOR) || gridHandler [curPos.x, curPos.y] != Grid.FLOOR2)
                {
                    
                    TileCount++;

                    if (TileCount % 4 == 0)
                    {
                        tileMap.SetTile (curPos, Floor2);
                        gridHandler [curPos.x, curPos.y] = Grid.FLOOR2;
                    }
                    else
                    {
                        tileMap.SetTile (curPos, Floor);
                        gridHandler [curPos.x, curPos.y] = Grid.FLOOR;
                    }



                    gridHandler [curPos.x, curPos.y] = Grid.FLOOR;
                    hasCreatedFloor = true;
                }
            }

            // Walker Methods
            ChanceToRemove ();
            ChanceToRedirect ();
            ChanceToCreate ();
            UpdatePosition ();

            if (hasCreatedFloor)
            {
                yield return new WaitForSeconds (WaitTime);
            }
        }

        StartCoroutine (CreateWalls ());
    }

    void ChanceToRemove ()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers [i].ChanceToChange && Walkers.Count > 1)
            {
                Walkers.RemoveAt (i);
                break;
            }
        }
    }

    void ChanceToRedirect ()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            if (UnityEngine.Random.value < Walkers [i].ChanceToChange)
            {
                WalkerObject curWalker = Walkers [i];
                curWalker.Direction = GetDirection ();
                Walkers [i] = curWalker;
            }
        }
    }

    void ChanceToCreate ()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers [i].ChanceToChange && Walkers.Count < MaximumWalkers)
            {
                Vector2 newDirection = GetDirection ();
                Vector2 newPosition = Walkers [i].Position;

                WalkerObject newWalker = new WalkerObject (newPosition, newDirection, 0.5f);
                Walkers.Add (newWalker);
            }
        }
    }

    void UpdatePosition ()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            WalkerObject FoundWalker = Walkers [i];
            FoundWalker.Position += FoundWalker.Direction;

            // Ajustar posiciones para que no salgan fuera del rango
            FoundWalker.Position.x = Mathf.Clamp (FoundWalker.Position.x, -MapWidth / 2 + 1, MapWidth / 2 - 2);
            FoundWalker.Position.y = Mathf.Clamp (FoundWalker.Position.y, -MapHeight / 2 + 1, MapHeight / 2 - 2);
            Walkers [i] = FoundWalker;
        }
    }

    IEnumerator CreateWalls ()
    {
        for (int x = 0; x < gridHandler.GetLength (0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength (1); y++)
            {
                if (gridHandler [x, y] == Grid.FLOOR)
                {
                    bool hasCreatedWall = false;

                    Vector3Int pos = new Vector3Int (x, y, 0);

                    if (CheckAndCreateWall (pos + Vector3Int.right))
                        hasCreatedWall = true;
                    if (CheckAndCreateWall (pos + Vector3Int.left))
                        hasCreatedWall = true;
                    if (CheckAndCreateWall (pos + Vector3Int.up))
                        hasCreatedWall = true;
                    if (CheckAndCreateWall (pos + Vector3Int.down))
                        hasCreatedWall = true;

                    if (hasCreatedWall)
                    {
                        yield return new WaitForSeconds (WaitTime);
                    }
                }
            }
        }
    }

    bool CheckAndCreateWall (Vector3Int pos)
    {
        if (pos.x < 0 || pos.x >= MapWidth || pos.y < 0 || pos.y >= MapHeight)
            return false;

        if (gridHandler [pos.x, pos.y] == Grid.EMPTY)
        {
            tileMap.SetTile (pos, Wall);
            colisionMap.SetTile (pos, Wall);
            gridHandler [pos.x, pos.y] = Grid.WALL;
            return true;
        }

        return false;
    }

    Vector3Int ConvertToTilemapCoords (Vector2Int position)
    {
        // Convierte las coordenadas centradas en (0,0) a índices de la matriz
        int x = position.x + MapWidth / 2;
        int y = position.y + MapHeight / 2;
        return new Vector3Int (x, y, 0);
    }
}
