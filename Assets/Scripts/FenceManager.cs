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

    [Range(1, 15)]
    public int amountOffset = 1;

    Vector3 terrianSize;
    Vector3 fenceSize;

    float offset;
    int fenceAmount;

    // Start is called before the first frame update
    void Start()
    {   
        terrainBounds = terrain.terrainData.bounds;
        terrianSize = terrain.terrainData.size;

        fenceSize = prefab.GetComponent<BoxCollider>().size;

        fenceAmount = GetFenceAmount(terrianSize.x, fenceSize.x, amountOffset);

        offset = GetOffset(terrianSize.x, fenceAmount, fenceSize.x);

        InstantiateFenceBorder(fenceAmount);
    }

    int GetFenceAmount(float terrianLength, float fenceLength, int amountOffset)
    {
        return (int)((terrianLength / fenceLength) - amountOffset);
    }

    float GetOffset(float terrianLength, int fenceAmount, float fenceLength)
    {
        return (terrianLength - (fenceAmount * fenceLength)) / 2;
    }

    void InstantiateFenceBorder(int fenceAmount)
    {
        // ((X0 -> Xn), Y0, Z0)
        Vector3 minHor = new Vector3(terrainBounds.min.x + offset + (fenceSize.x / 2), 0, terrainBounds.min.z + offset);
        // ((X0 -> Xn), Y0, Zn)
        Vector3 maxHor = new Vector3(terrainBounds.min.x + offset + (fenceSize.x / 2), 0, terrainBounds.max.z - offset);

        // ((X0, Y0, (Z0 -> Zn))
        Vector3 minVer = new Vector3(terrainBounds.min.x + offset, 0, terrainBounds.min.z + offset + (fenceSize.x / 2));
        // (Xn, Y0, (Z0 -> Zn))
        Vector3 maxVer = new Vector3(terrainBounds.max.x - offset, 0, terrainBounds.min.z + offset + (fenceSize.x / 2));

        for (int i = 0; i < fenceAmount; i++)
        {
            GameObject minobj = Instantiate(prefab, minHor, Quaternion.identity);
            Instantiate(prefab, maxHor, minobj.transform.rotation);

            minHor.x += fenceSize.x;
            maxHor.x += fenceSize.x;

            GameObject minObj = Instantiate(prefab, minVer, Quaternion.Euler(0, 90f, 0));
            Instantiate(prefab, maxVer, minObj.transform.rotation);

            minVer.z += fenceSize.x;
            maxVer.z += fenceSize.x;
        }        
    }
}
