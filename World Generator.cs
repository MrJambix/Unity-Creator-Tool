
public class WorldGenerator : MonoBehaviour
{
    public void Generate(BiomeType biome, int size, float complexity, bool vegetation)
    {
        // Create terrain data
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = size;
        terrainData.size = new Vector3(size, size / 4, size);

        // Generate terrain based on biome
        float[,] heights = GenerateHeightmap(size, complexity, biome);
        terrainData.SetHeights(0, 0, heights);

        // Create terrain
        Terrain terrain = Terrain.CreateTerrainGameObject(terrainData).GetComponent<Terrain>();
        terrain.transform.parent = transform;

        if (vegetation)
        {
            GenerateVegetation(terrain, biome);
        }
    }

    private float[,] GenerateHeightmap(int size, float complexity, BiomeType biome)
    {
        float[,] heights = new float[size, size];
        // Implementation of procedural terrain generation
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                heights[x, z] = Mathf.PerlinNoise(x * 0.1f, z * 0.1f) * complexity;
            }
        }
        return heights;
    }

    private void GenerateVegetation(Terrain terrain, BiomeType biome)
    {
        // Define vegetation types and their spawn rates
        Dictionary<string, float> vegetationTypes = new Dictionary<string, float>();
        switch (biome)
        {
            case BiomeType.Forest:
                vegetationTypes.Add("Tree", 0.5f);
                vegetationTypes.Add("Bush", 0.3f);
                vegetationTypes.Add("Flower", 0.2f);
                break;
            case BiomeType.Desert:
                vegetationTypes.Add("Cactus", 0.5f);
                vegetationTypes.Add("Rock", 0.3f);
                vegetationTypes.Add("Sand", 0.2f);
                break;
            case BiomeType.Mountain:
                vegetationTypes.Add("Tree", 0.3f);
                vegetationTypes.Add("Rock", 0.5f);
                vegetationTypes.Add("Snow", 0.2f);
                break;
            case BiomeType.Tundra:
                vegetationTypes.Add("Bush", 0.5f);
                vegetationTypes.Add("Rock", 0.3f);
                vegetationTypes.Add("Snow", 0.2f);
                break;
            case BiomeType.Tropical:
                vegetationTypes.Add("Tree", 0.5f);
                vegetationTypes.Add("Flower", 0.3f);
                vegetationTypes.Add("Vine", 0.2f);
                break;
        }

        // Spawn vegetation
        foreach (var type in vegetationTypes)
        {
            for (int i = 0; i < terrain.terrainData.heightmapWidth * terrain.terrainData.heightmapHeight * type.Value; i++)
            {
                // Randomly select a position on the terrain
                int x = Random.Range(0, terrain.terrainData.heightmapWidth);
                int z = Random.Range(0, terrain.terrainData.heightmapHeight);

                // Check if the position is valid (not too steep or too high)
                if (terrain.terrainData.GetHeight(x, z) < 0.5f && terrain.terrainData.GetSteepness(x, z) < 45f)
                {
                    // Spawn the vegetation
                    GameObject vegetationObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    vegetationObject.name = type.Key;
                    vegetationObject.transform.position = new Vector3(x, terrain.terrainData.GetHeight(x, z), z);
                    vegetationObject.transform.parent = terrain.transform;
                }
            }
        }
    }
}
    