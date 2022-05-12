using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float  scale = 20f;

    public float offsetX = 20f;
    public float offsetY = 20f;
    public float speed = 5f;

    Terrain terrain;

    [System.Serializable]
    public class SplatHeights
    {
        public int textureIndex;
        public int startingHeight;
    }

    public SplatHeights[] splatHeights;
    //float offset = 0f;


    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        offsetX = Random.Range(0f, 10000f);
        offsetY = Random.Range(0f, 10000f);
        terrain.terrainData = GenerateTerrain();
        paintMap();
    }

    // Update is called once per frame
    void Update()
    {
        terrain.terrainData = GenerateTerrain();
        offsetX += speed * Time.deltaTime;
    }

    TerrainData GenerateTerrain()
    {
        TerrainData newterrain = terrain.terrainData ;
        newterrain.heightmapResolution = width + 1;
        
        newterrain.size = new Vector3(width, depth, height);

        newterrain.SetHeights(0, 0, GenerateHeights());


        return newterrain;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int x=0; x< width; x++)
        {
            for (int y = 0; y< height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }

        }
        return heights;
    }

    float CalculateHeight(int x,int y)
    {
        float xcoord = (float)x / width * scale + offsetX;
        float ycoord = (float)y / width * scale + offsetY;

        return (Mathf.Clamp(Mathf.PerlinNoise(xcoord, ycoord), 0.1f, 0.8f));
    }

    void paintMap()
    {
        TerrainData terraindata = terrain.terrainData;
        float[,,] splatmapData = new float[terraindata.alphamapWidth, terraindata.alphamapHeight, terraindata.alphamapLayers];

        for (int y = 0; y < terraindata.alphamapHeight; y++)
        {
            for (int x = 0; x < terraindata.alphamapWidth; x++)
            {
                float terrainHeight = terraindata.GetHeight(y, x);

                float[] splat = new float[splatHeights.Length];

                for (int i = 0; i < splatHeights.Length; i++)
                {
                    if (terrainHeight >= splatHeights[i].startingHeight)
                        splat[i] = 1;
                }

                for (int j = 0; j < splatHeights.Length; j++)
                {
                    splatmapData[x, y, j] = splat[j];
                }
            }

            terraindata.SetAlphamaps(0, 0, splatmapData);
        }
    }
}
