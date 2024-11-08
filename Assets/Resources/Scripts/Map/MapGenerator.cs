using UnityEngine;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour
{
    private IslandGenerator islandGenerator;
    private IslandConnector islandConnector;

    private void Start()
    {
        islandGenerator = GetComponent<IslandGenerator>();
        islandConnector = GetComponent<IslandConnector>();

        List<List<GameObject>> islandRows = islandGenerator.GenerateMap();
        GameObject bossIsland = islandGenerator.GetBossIsland();
        GameObject ship = islandGenerator.GetShip();
        islandConnector.ConnectIslands(islandRows, bossIsland, ship);
    }
}
