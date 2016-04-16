using UnityEngine;
using System.Collections;

public class RingController : MonoBehaviour {

    public float spinTime, spinSpeed;

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
}
