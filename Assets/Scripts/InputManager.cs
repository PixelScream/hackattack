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
    public Color green;

    // make the sliders colour on a gradient
    public Gradient sliderGradient;
    public Image sliderFill;
    

    int ringCount = 10;
    public float[] breathsOut;
    int breathCount;
    float pollRate = 30;

    float score = 0;

    float breathSoFar;
    float currentBreath;

    int counter;

    float noise = 0.05f;
    bool simulateNoise = true;
    Transform currentRing;

    public LevelController levelController;

    void Start()
    {

        ResetBreath();
    }

	void Update () {

        if(breathCount == ringCount)
        {
            GameObject.Find("Player").GetComponent<Movement>().started = true;
           

            transform.GetComponentInChildren<Canvas>().enabled = false;
            return;

        }
        //  Get the blow force
        float h = PollHoizontal();
        // Get button state
        bool b = Input.GetButtonDown("Fire1");


        // blow start
        //if(!blowing && h < minSuck)
        if (!blowing && h > minBlow)
        {
            blowing = true;
            blowStart = Time.time;
            currentRing = levelController.rings[breathCount].transform;
            currentRing.position = Vector3.zero;
            currentRing.localPosition = Vector3.zero;
        }

        if(h > 0.1)
        {
            if (simulateNoise)
                powerSlider.value = h + (noise * Random.value);
            else
                powerSlider.value = h;

           
        }
        else
        {
            powerSlider.value = 0;
        }

        sliderFill.color = sliderGradient.Evaluate(h);

        // handle blowing
        if (blowing)
        {
            float breathComplete = (Time.time - blowStart) / blowDuration;
            timeSlider.value = breathComplete;
            if(h > minBlow)
            {
                breathSoFar += h ;
                counter++;
                currentRing.localScale = (Vector3.one * ( breathSoFar / counter)) * Mathf.Min( breathComplete, 1.3f);
            } 
            else
            {
                if(Time.time > blowStart + blowDuration)
                {
                    print("breath successful");
                    breathCount++;
                    levelController.PlaceRing(currentRing.gameObject, breathCount);
                } 
                else
                {
                    print("breath fail");
                    currentRing.localScale = Vector3.zero;
                }

                ResetBreath();
            }
        }
    }

    float PollHoizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    void ResetBreath()
    {
        blowing = false;
        holdText.color = Color.white;
        holdText.enabled = false;
        counter = 0;
        breathSoFar = 0;
        powerSlider.value = timeSlider.value = 0;
        holdText.text = "Blow!";
        holdText.enabled = true;
    }
}
