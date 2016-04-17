using UnityEngine;
using System.Collections;

public class RingController : MonoBehaviour {

    public float spinTime, spinSpeed;
    Vector3 startPos, endPos;
    public float speed = 5;
    bool go = false;
    /*
    void Start()
    {
        StartCoroutine(Spin());
    }
    */

    public void Collected()
    {
        StartCoroutine(Spin());

    }
    IEnumerator Spin()
    {
        float beginTime = Time.time;

        while(beginTime + spinTime > Time.time)
        {
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }


        //Destroy(this.gameObject);
    }

    public void Go(Vector3 p)
    {
        endPos = p;
        go = true;
    }

    void Update()
    {
        if (go)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * speed);

            
            if(Vector3.Distance(transform.position , endPos) < 1)
            {
                go = false;
            }
            
        }
    }
}
