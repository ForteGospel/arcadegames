using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPaint : MonoBehaviour
{

    [System.Serializable]
    public class SplatHeights
    {
        public int textureIndex;
        public int startingHeight;
    }

    public SplatHeights[] splatHeights;
    float offset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        paintMap();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime;
        if (offset > 2f)
        {
            offset = 0;
            paintMap();
        }
    }

    void paintMap()
    {
        TerrainData terraindata = Terrain.activeTerrain.terrainData;
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
