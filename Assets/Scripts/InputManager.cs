using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    public Slider powerSlider;
    bool blowing;
    public float minBlow = 0.5f;
    float blowStart;
    public float blowDuration = 5;


    int ringCount = 10;
    public float[] breaths;
    int breathCount;
    float pollRate = 30;

    float score = 0;

    void Start()
    {
        LevelStart();
    }

	void Update () {

        //  Get the blow force
        float h = Input.GetAxisRaw("Horizontal");
        // Get button state
        bool b = Input.GetButtonDown("Fire1");


        // blow start
        if(!blowing && h > minBlow )
        {
            blowing = true;
            blowStart = Time.time; 
        }

        if (blowing)
        {
            StartCoroutine(Blowing(h));
            
        }

        

       // Debug.Log(h);

        if (Input.GetButtonDown("Fire1"))
        {
            print("YAY");
        }
    }

    IEnumerator Blowing(float changeme)
    {
        int c = 0;
        float v = changeme;
        float b = 0;

        while (v > minBlow)
        {
            c++;
            v = Input.GetAxisRaw("Horizontal");
            b += v;
            powerSlider.value = v;
            yield return pollRate;
        }

        if(Time.time > blowStart + (blowDuration * 0.75f) && breaths[breathCount] == 0)
        {
            print("Successful breath!");
            
            breaths[breathCount] = b;
            float blowDelta = Mathf.Min( (blowStart + blowDuration - Time.time) / blowDuration, 1.5f);
            score += 5 * blowDelta;
            breathCount++;
        }

        powerSlider.value = 0;
        blowing = false;
    }

    void LevelStart()
    {
        breaths = new float[ringCount];
        breathCount = 0;
    }
}
