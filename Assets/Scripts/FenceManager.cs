using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Terrain terrain;
    Bounds terrainBounds;

    Vector3 fenceSize;

    // Start is called before the first frame update
    void Start()
    {
        terrainBounds = terrain.terrainData.bounds;

        float height = terrainBounds.size.x;
        float width = terrainBounds.size.y;

        fenceSize = prefab.GetComponent<BoxCollider>().size;

        Debug.Log(fenceSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
