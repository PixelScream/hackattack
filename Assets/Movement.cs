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
    public GameObject ring;
    public float levelDistance=55f;
   


    public Text scoreText;
    public float score=0f;

    private Vector3 airInput;
    private Vector3 direction;
    private float xRotateMax=60f;
    private float xRotateMin=300f;
    private float yRotateMax=60f;
    private float yRotateMin=300f;
 

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 5, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        scoreText.text = "Score: " + score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        airInput = new Vector3( Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0);
        //transform.Translate(airInput * moveAccel/100);
        if (!((transform.eulerAngles.x >= xRotateMax && airInput.x > 0 && transform.eulerAngles.x<180) || (transform.eulerAngles.x <= xRotateMin && airInput.x < 0 && transform.eulerAngles.x > 180)))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + (airInput.x * moveAccel / 10), transform.rotation.eulerAngles.y, 0);
            //transform.Rotate((airInput.x * moveAccel / 10), 0, 0);
        }

        if (!((transform.eulerAngles.y >= yRotateMax && airInput.y > 0 && transform.eulerAngles.y < 180) || (transform.eulerAngles.y <= yRotateMin && airInput.y < 0 && transform.eulerAngles.y > 180)))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (airInput.y * moveAccel / 10), 0);
           // transform.Rotate(0,(airInput.y * moveAccel / 10),0);
        }
        //transform.Rotate((airInput * moveAccel / 10));
        //transform.eulerAngles=new Vector3(Mathf.Min(transform.eulerAngles.x, xRotateMax), Mathf.Min(transform.eulerAngles.y, yRotateMax),0);
        // transform.eulerAngles = new Vector3(Mathf.Max(transform.eulerAngles.x, xRotateMin), Mathf.Max(transform.eulerAngles.y, yRotateMin),0);

        direction = new Vector3(Mathf.Cos( transform.eulerAngles.x), Mathf.Cos(transform.eulerAngles.y), Mathf.Cos(transform.eulerAngles.z));
        transform.position = transform.position + Camera.main.transform.forward * forwardSpeed * Time.deltaTime;
        //transform.Translate(forwardSpeed / 10)
        //transform.Translate(direction.normalized * forwardSpeed/10);

        if(transform.position.z> levelDistance)
        {
            GameEnd();
        }


    }

    void GameEnd()
    {
       // Debug.Log("GameEnd");
    }
    
    void OnTriggerEnter(Collider ring)
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 ringPos = new Vector2(ring.bounds.center.x, ring.bounds.center.y);
        float diff = ring.bounds.size.x  - (playerPos-ringPos).magnitude;
        float diffX = ring.bounds.size.x/2 - Mathf.Abs(transform.position.x - ring.bounds.center.x);
        float diffY = ring.bounds.size.y/2 - Mathf.Abs(transform.position.y - ring.bounds.center.y);

        
        
        //ring.bounds.Contains(transform.position)
        if (ring.CompareTag("Ring") && diffX>0 && diffY > 0)
        {
            Debug.Log("Collide");
            score += 1;
            scoreText.text = "Score: " + score.ToString();
            
        }else
        {
            Debug.Log("meh");
        }
        Debug.Log("Diffx, diffy, ringboundx, posx, centerx" + diffX + " " + diffY + " " + ring.bounds.size.x + " " + transform.position.x + " " + ring.bounds.center.x);
        Debug.Log(ring.bounds.size.x);
        Debug.Log(ring.bounds.size.y);
    }
    
}
/*

//
*/