using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public GameObject ring;
    [Header("Level Variables")]
    LevelInfo defaultLevelInfo;
    public float ringDistance = 5;
    public float levelWidth, levelHeight;


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

        BuildLevel(defaultLevelInfo);
	}


    public void BuildLevel(LevelInfo l)
    {
        for (int i = 0; i < l.ringCount; i++)
        {
            Vector3 offset = new Vector3(Random.value * levelWidth, Random.value * levelHeight);
            GameObject g = Instantiate(ring, Vector3.forward * ringDistance * i + offset, Quaternion.identity) as GameObject;
            g.transform.localScale = Vector3.one * l.ringSize[i];
            g.transform.parent = this.transform;
        }
    }
}
