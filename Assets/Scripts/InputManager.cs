using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    public Slider powerSlider, timeSlider;
    public Text holdText;
    bool blowing;
    public float minBlow, minSuck;
    float blowStart;
    public float blowDuration, holdDuration, suckDuration;

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
        if(!blowing && h < minSuck)
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
        // counter
        int c = 0;
        float v = changeme;
        float b = 0;
        float timeLeft;

        holdText.enabled = true;
        holdText.text = "Inhale";
        // handle Suck

        while (v < minSuck)
        {
            v = Input.GetAxisRaw("Horizontal");
            powerSlider.value = -v;

            timeLeft = 1 - (Time.time - blowStart) / holdDuration;
            timeSlider.value = timeLeft;

            yield return pollRate;
        }

        float suckedFor = Time.time - blowStart;
        if(suckedFor <  suckDuration)
        {
            print("suck failed");
            yield return null;
        }

        // handle Hold

        holdText.text = "Hold!";

        float heldFor = Time.time - blowStart - suckedFor;
        while (heldFor < holdDuration )
        {
            heldFor = Time.time - blowStart - suckedFor;
            timeLeft = 1 - heldFor/ holdDuration;
            timeSlider.value = timeLeft;
            v = Input.GetAxisRaw("Horizontal");

            if(v > minBlow)
            {
                print("rest failed");
                yield return null;
            }

            
            yield return pollRate;
            
        }

        holdText.text = "Blow";
        while (v < minBlow)
        {
            v = Input.GetAxisRaw("Horizontal");
            yield return pollRate;
        }
       

        // handle Blowing
        c = 0;
        while (v > minBlow)
        {
            c++;
            v = 
            b += v;
            powerSlider.value = v;
            timeLeft = 1 - (Time.time - blowStart - heldFor - suckedFor) / blowDuration;
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
        holdText.enabled = false;

    }



    void LevelStart()
    {
        breathsOut = new float[ringCount];
        breathsIn = new float[ringCount];
        breathCount = 0;
    }

    float PollHorzontal()
    {
        float f = Input.GetAxisRaw("Horizontal");
        powerSlider.value = f;
        return f;
    }
}
