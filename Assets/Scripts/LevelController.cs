using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public GameObject ring;

    [Header("Level Variables")]
    LevelInfo defaultLevelInfo;
    public float ringDistance = 5;
    public float levelWidth, levelHeight;
    public GameObject[] rings;

    public struct LevelInfo
    {
        public int ringCount;
        public float[] ringSize;
    }

	void Start () {

        defaultLevelInfo.ringCount = 10;
        defaultLevelInfo.ringSize = new float[10] ;
        for(int i = 0; i < defaultLevelInfo.ringCount; i++)
        {
            defaultLevelInfo.ringSize[i] = Mathf.Max ( Random.value, 0.3f);
        }
        GenerateRings(defaultLevelInfo.ringCount);
        //BuildLevel(defaultLevelInfo);
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BuildLevel(defaultLevelInfo);
        }


    }

    public void BuildLevel(LevelInfo l)
    {
        // check for rings being in existance


        Debug.Log("Placing Rings");
        for (int i = 0; i < l.ringCount; i++)
        {
            print(i);
            Vector3 offset = new Vector3(Random.value * levelWidth, Random.value * levelHeight,15);
            rings[i].transform.position = Vector3.forward * ringDistance * i + offset;
            rings[i].transform.localScale = Vector3.one * l.ringSize[i];

        }
    }

    public void GenerateRings(int count)
    {

        Debug.Log("Making Rings");
        rings = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            rings[i] = Instantiate(ring, Vector3.forward * -10, Quaternion.identity) as GameObject;
            rings[i].transform.parent = this.transform;
            rings[i].tag = "Ring";
        }
        
    }

    public void PlaceRing(GameObject r, int i)
    {
        Vector3 offset = new Vector3(Random.value * levelWidth, Random.value * levelHeight, 15);
        r.transform.position = Vector3.forward * ringDistance * i + offset;
    }
}
