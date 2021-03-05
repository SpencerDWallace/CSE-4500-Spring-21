using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    Vector3 Vec;
    float playerAngle;
    Vector3 to;
    int n;
    float rad;
    //new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        // camera = Camera;

    }

    // Update is called once per frame
    void Update()
    {
        //GameObject ball = GameObject.Find("Ball");
        playerAngle = transform.rotation.eulerAngles.y;
        Vec = transform.localPosition;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            playerAngle += 90;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            playerAngle -= 90;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rad = Mathf.Deg2Rad * playerAngle;
            // Debug.Log("Player angle = " + playerAngle.ToString());
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.UpArrow))
                n = 1;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                n = -1;

            Vec.z += n * Mathf.Cos(rad) * Time.deltaTime * 20;
            Vec.x += n * Mathf.Sin(rad) * Time.deltaTime * 20;
        }

            //Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 20;
            to = new Vector3(0, playerAngle, 0);
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
            transform.localPosition = Vec;
            // GameObject.Find("Camera").transform.localPosition = new Vector3((float)Vec.x, (float)(Vec.y + 3), (float)(Vec.x -.5));
        
    }
}
