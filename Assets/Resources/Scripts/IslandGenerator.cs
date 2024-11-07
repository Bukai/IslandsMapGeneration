using UnityEngine;
using System.Collections.Generic;

public class IslandGenerator : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject islandPrefab;
    public GameObject bossIslandPrefab;

    public float horizontalSpacing = 7f;
    public float verticalSpacing = 4f;
    public float positionVariance = 1f;
    public float minDistance = 2.5f;

    private List<GameObject> nodes = new List<GameObject>();
    private GameObject bossIsland;

    public List<List<GameObject>> GenerateMap()
    {
        ClearMap();

        float xPosition = -15f;
        Vector2 shipPosition = new Vector2(xPosition, 0);
        GameObject ship = Instantiate(shipPrefab, shipPosition, Quaternion.identity, transform);
        nodes.Add(ship);

        int[] islandsPerRow = { 3, 4, 2 };
        List<List<GameObject>> allRows = new List<List<GameObject>>();
        List<GameObject> previousRowIslands = new List<GameObject> { ship };

        foreach (int islandCount in islandsPerRow)
        {
            xPosition += horizontalSpacing;
            List<GameObject> currentRow = CreateIslandRow(islandCount, xPosition);
            allRows.Add(currentRow);
            previousRowIslands = currentRow;
        }

        bossIsland = CreateBossIsland(xPosition, previousRowIslands);
        nodes.Add(bossIsland);

        return allRows;
    }

    public GameObject GetBossIsland()
    {
        return bossIsland;
    }

    public GameObject GetShip()
    {
        return nodes[0];
    }

    private void ClearMap()
    {
        foreach (GameObject node in nodes)
        {
            Destroy(node);
        }
        nodes.Clear();
    }

    private List<GameObject> CreateIslandRow(int islandCount, float xPosition)
    {
        List<GameObject> currentRowIslands = new List<GameObject>();
        float startY = -(islandCount - 1) * verticalSpacing / 2;

        for (int i = 0; i < islandCount; i++)
        {
            Vector2 position = FindIslandPosition(xPosition, startY + i * verticalSpacing);
            GameObject island = Instantiate(islandPrefab, position, Quaternion.identity, transform);
            nodes.Add(island);
            currentRowIslands.Add(island);
        }

        return currentRowIslands;
    }

    private Vector2 FindIslandPosition(float xPosition, float yPosition)
    {
        Vector2 position;
        bool positionIsValid;

        do
        {
            position = new Vector2(
                xPosition + Random.Range(-positionVariance, positionVariance),
                yPosition + Random.Range(-positionVariance, positionVariance)
            );

            positionIsValid = IsPositionValid(position);
        } while (!positionIsValid);

        return position;
    }

    private bool IsPositionValid(Vector2 position)
    {
        foreach (GameObject node in nodes)
        {
            if (Vector2.Distance(position, node.transform.position) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    private GameObject CreateBossIsland(float xPosition, List<GameObject> previousRowIslands)
    {
        xPosition += horizontalSpacing;
        Vector2 bossPosition = new Vector2(xPosition, Random.Range(-positionVariance, positionVariance));
        return Instantiate(bossIslandPrefab, bossPosition, Quaternion.identity, transform);
    }
}
