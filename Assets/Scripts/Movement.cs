using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Movement : MonoBehaviour {
    //Parameters controlled by air input 
    public float moveAccel=30f;
    public Vector3 windDirection = new Vector3(0,0,0);
    public float windSpeed = 0f;
    private float forwardSpeed = 4f;
    public float levelWidth, levelHeight;
    

    public Canvas openScreen;

    public Text play;
    public Text scoreText;
    public float score=0f;

    private Vector3 airInput;
    private float xRotateMax=60f;
    private float xRotateMin=300f;
    private float yRotateMax=60f;
    private float yRotateMin=300f;
    private int targetScore = 3;
    private bool started = false;

    //Sets Game up at start
    public void GameStart()
    {
        started = true;
        score = 0;
        openScreen.enabled = false;
        transform.position = new Vector3(0, 5, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        scoreText.text = "Score: " + score.ToString();
    }

    // Use this for initialization
    void Start () {  
        transform.position = new Vector3(0, 5, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        scoreText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            airInput = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0);

            //Rotate player
            if (!((transform.eulerAngles.x >= xRotateMax && airInput.x > 0 && transform.eulerAngles.x < 180) || (transform.eulerAngles.x <= xRotateMin && airInput.x < 0 && transform.eulerAngles.x > 180)))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + (airInput.x * moveAccel / 10), transform.rotation.eulerAngles.y, 0);
            }
            if (!((transform.eulerAngles.y >= yRotateMax && airInput.y > 0 && transform.eulerAngles.y < 180) || (transform.eulerAngles.y <= yRotateMin && airInput.y < 0 && transform.eulerAngles.y > 180)))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (airInput.y * moveAccel / 10), 0);
            }
            //Move Player
            transform.position = transform.position + Camera.main.transform.forward * forwardSpeed * Time.deltaTime;
            
            //If acheive target score end game.
            if (score == targetScore)
            {
                GameEnd();
            }
        }


    }
    //Ends game
    void GameEnd()
    {
        play.text = "Play Again?";
        openScreen.enabled = true;
    }
    //When player collides with ring
    void OnTriggerEnter(Collider ring)
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 ringPos = new Vector2(ring.bounds.center.x, ring.bounds.center.y);
       
        float diffX = ring.bounds.size.x/2 - Mathf.Abs(transform.position.x - ring.bounds.center.x);
        float diffY = ring.bounds.size.y/2 - Mathf.Abs(transform.position.y - ring.bounds.center.y);

        //ring.bounds.Contains(transform.position)

        //if the player is inside the ring
        if (ring.CompareTag("Ring") && diffX > 0.3 && diffY > 0.3)
        {
            Debug.Log("Collide");
            score += 1;
            //ring.GetComponentInChildren<Renderer>().material.color = Color.cyan;
            scoreText.text = "Score: " + score.ToString();
        }
    }
    
}
/*

//
*/