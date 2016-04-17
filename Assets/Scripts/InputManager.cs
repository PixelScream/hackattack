using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    public Slider powerSlider, timeSlider;
    bool blowing;
    public float minBlow, minSuck;
    float blowStart;
    public float blowDuration, restDuration, suckDuration;

    // make the sliders colour on a gradient
    // public Gradient sliderGradient;

    int ringCount = 10;
    public float[] breathsOut, breathsIn;
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
        float timeLeft = Time.time - (blowStart + blowDuration);



        // handle Blowing
        while (v > minBlow)
        {
            c++;
            v = Input.GetAxisRaw("Horizontal");
            b += v;
            powerSlider.value = v;
            timeLeft = 1 - (Time.time - blowStart) / blowDuration;
            timeSlider.value = timeLeft;
            yield return pollRate;
        }

        if (Time.time > blowStart + (blowDuration * 0.75f))
        {
            print("Successful breath!");

            breathsOut[breathCount] = b / c;
            float blowDelta = Mathf.Min((blowStart + blowDuration - Time.time) / blowDuration, 1.5f);
            score += 5 * blowDelta;
            breathCount++;
        }
        
        if(breathCount == ringCount)
        {
            Debug.Log("End Breathing Phase");
        }

        powerSlider.value = 0;
        blowing = false;
        
    }



    void LevelStart()
    {
        breathsOut = new float[ringCount];
        breathsIn = new float[ringCount];
        breathCount = 0;
    }
}
