using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class map : MonoBehaviour
{
    [Header("Grid Dimensions")]
    public int X = 20;
    public int Y = 20;

    [Header("Floor Prefabs")]
    public GameObject[] floorsPrefab;

    [Header("Boundary Wall Prefabs")]
    public GameObject[] topWallsPrefab;
    public GameObject[] bottomWallsPrefab;
    public GameObject[] leftWallsPrefab;
    public GameObject[] rightWallsPrefab;

    [Header("Set Transform Parents")]
    public Transform floorParent;
    public Transform wallParent;
    public Transform itemParent;

    [Header("Game Elements & Counts")]
    public GameObject[] itemPrefabs;
    public int totalItemCount = 10;
    public GameObject exitPrefab;

    private List<Vector2Int> availableFloorPositions;

    private void Awake()
    {
        availableFloorPositions = new List<Vector2Int>();
        CreateGrid();
        PlaceGameElements();
    }

    public void CreateGrid()
    {
        if (floorsPrefab == null || topWallsPrefab == null || bottomWallsPrefab == null ||
            leftWallsPrefab == null || rightWallsPrefab == null || floorParent == null || wallParent == null || itemParent == null)
        {
            Debug.LogError("Please assign all Prefabs and Parent Transforms in the Inspector.");
            return;
        }

        float offsetX = X / 2f;
        float offsetY = Y / 2f;

        for (int x = -1; x < X + 1; x++)
        {
            for (int y = -1; y < Y + 1; y++)
            {
                float worldX = x - offsetX;
                float worldY = y - offsetY;

                bool isBoundary = (x == -1 || x == X || y == -1 || y == Y);

                if (isBoundary)
                {
                    GameObject[] selectedWalls = null;
                    if (y == -1) selectedWalls = topWallsPrefab;
                    else if (y == Y) selectedWalls = bottomWallsPrefab;
                    else if (x == -1) selectedWalls = leftWallsPrefab;
                    else if (x == X) selectedWalls = rightWallsPrefab;

                    if (selectedWalls != null && selectedWalls.Length > 0)
                    {
                        int r = UnityEngine.Random.Range(0, selectedWalls.Length);
                        GameObject obj = Instantiate(selectedWalls[r], new Vector3(worldX, worldY, 0), Quaternion.identity);
                        obj.transform.parent = wallParent;
                    }
                }
                else
                {
                    if (floorsPrefab.Length > 0)
                    {
                        int r = UnityEngine.Random.Range(0, floorsPrefab.Length);
                        GameObject obj = Instantiate(floorsPrefab[r], new Vector3(worldX, worldY, 1), Quaternion.identity);
                        obj.transform.parent = floorParent;

                        availableFloorPositions.Add(new Vector2Int(x, y));
                    }
                }
            }
        }
    }

    private void PlaceGameElements()
    {
        if (availableFloorPositions.Count == 0)
        {
            Debug.LogWarning("No floor tiles were created to place items or exit.");
            return;
        }

        PlaceSingleElement(exitPrefab, "Exit");

        PlaceItems();
    }

    private void PlaceSingleElement(GameObject prefab, string name)
    {
        if (prefab == null || availableFloorPositions.Count == 0)
        {
            Debug.LogError($"{name} Prefab is null or no floor space available!");
            return;
        }

        int randIndex = UnityEngine.Random.Range(0, availableFloorPositions.Count);
        Vector2Int placementPos = availableFloorPositions[randIndex];

        availableFloorPositions.RemoveAt(randIndex);

        float offsetX = X / 2f;
        float offsetY = Y / 2f;

        float worldX = placementPos.x - offsetX;
        float worldY = placementPos.y - offsetY;

        GameObject obj = Instantiate(prefab, new Vector3(worldX, worldY, 0), Quaternion.identity);
        obj.transform.parent = itemParent;
        obj.name = $"{name}_{placementPos.x},{placementPos.y}";
    }

    private void PlaceItems()
    {
        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogWarning("Item Prefabs Array is empty. No items will be placed.");
            return;
        }

        int itemsToPlace = Mathf.Min(totalItemCount, availableFloorPositions.Count);
        int placedCount = 0;

        for (int i = 0; i < itemsToPlace; i++)
        {
            if (availableFloorPositions.Count == 0) break;

            int randIndex = UnityEngine.Random.Range(0, availableFloorPositions.Count);
            Vector2Int placementPos = availableFloorPositions[randIndex];

            availableFloorPositions.RemoveAt(randIndex);

            int r = UnityEngine.Random.Range(0, itemPrefabs.Length);
            GameObject selectedPrefab = itemPrefabs[r];

            float offsetX = X / 2f;
            float offsetY = Y / 2f;

            float worldX = placementPos.x - offsetX;
            float worldY = placementPos.y - offsetY;

            GameObject itemObj = Instantiate(selectedPrefab, new Vector3(worldX, worldY, 0), Quaternion.identity);
            itemObj.transform.parent = itemParent;
            itemObj.name = selectedPrefab.name + "_" + placementPos.x + "," + placementPos.y;

            placedCount++;
        }

        if (placedCount < totalItemCount)
        {
            Debug.LogWarning($"Only placed {placedCount} out of {totalItemCount} items. Ran out of floor space.");
        }
    }
}