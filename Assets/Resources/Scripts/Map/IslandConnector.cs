using UnityEngine;
using System.Collections.Generic;

public class IslandConnector : MonoBehaviour
{
    public GameObject lineRendererPrefab;

    public void ConnectIslands(List<List<GameObject>> islandRows, GameObject bossIsland, GameObject ship)
    {   
        ConnectShipToFirstRow(ship, islandRows[0]);

        for (int i = 0; i < islandRows.Count; i++)
        {
            List<GameObject> currentRow = islandRows[i];
            List<GameObject> nextRow = (i < islandRows.Count - 1) ? islandRows[i + 1] : new List<GameObject> { bossIsland };
            ConnectRows(currentRow, nextRow);
        }
    }

    private void ConnectShipToFirstRow(GameObject ship, List<GameObject> firstRow)
    {
        foreach (GameObject island in firstRow)
        {
            DrawPath(ship.transform.position, island.transform.position);
        }
    }

    private void ConnectRows(List<GameObject> currentRow, List<GameObject> nextRow)
    {
        HashSet<GameObject> connectedFromCurrent = new HashSet<GameObject>();
        HashSet<GameObject> connectedToNext = new HashSet<GameObject>();

        foreach (GameObject currentNode in currentRow)
        {
            GameObject nextNode = nextRow[Random.Range(0, nextRow.Count)];
            DrawPath(currentNode.transform.position, nextNode.transform.position);

            connectedFromCurrent.Add(currentNode);
            connectedToNext.Add(nextNode);
        }

        EnsureRowConnectivity(currentRow, connectedFromCurrent, nextRow, connectedToNext);
    }

    private void EnsureRowConnectivity(List<GameObject> currentRow, HashSet<GameObject> connectedFromCurrent,
                                       List<GameObject> nextRow, HashSet<GameObject> connectedToNext)
    {
        foreach (GameObject currentNode in currentRow)
        {
            if (!connectedFromCurrent.Contains(currentNode))
            {
                GameObject fallbackNode = nextRow[Random.Range(0, nextRow.Count)];
                DrawPath(currentNode.transform.position, fallbackNode.transform.position);
            }
        }

        foreach (GameObject nextNode in nextRow)
        {
            if (!connectedToNext.Contains(nextNode))
            {
                GameObject fallbackNode = currentRow[Random.Range(0, currentRow.Count)];
                DrawPath(fallbackNode.transform.position, nextNode.transform.position);
            }
        }
    }

    private void DrawPath(Vector3 start, Vector3 end)
    {
        int segmentCount = 10;
        Vector3[] linePoints = new Vector3[segmentCount + 1];

        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            linePoints[i] = Vector3.Lerp(start, end, t);
        }

        GameObject lineObj = Instantiate(lineRendererPrefab, transform);
        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        Material dashedMaterial = Resources.Load<Material>("Graphics/Materials/RedLine");
        lineRenderer.material = dashedMaterial;
        lineRenderer.textureMode = LineTextureMode.Tile;
        lineRenderer.material.mainTextureScale = new Vector2(1f, 1f);
    }
}
