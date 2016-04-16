using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    public Slider powerSlider;

	void Update () {

        float h = Input.GetAxis("Horizontal");

        
        powerSlider.value = h;

        Debug.Log(h);

        if (Input.GetButtonDown("Fire1"))
        {
            print("YAY");
        }
    }
}
