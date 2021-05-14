using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMarker : MonoBehaviour
{
    Camera cam;
    GameObject hud;
    GameObject EM;
    GameObject offscreenEM;
    GameObject player;
    GameObject distanceMarker;
    Vector3 targetDirection;
    float x;
    float y;
    public GameObject center, top, bottom, right, left;
    float timePassed; float prevTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = player.GetComponentInChildren<Camera>();
        hud = GameObject.Find("HUD");
        EM = GameObject.Find("EM");
        distanceMarker = GameObject.Find("Distance");
        timePassed = 0;
        prevTime = 0;
        //offscreenEM = null;
        
    }

    void Update()
    {
        if( EM == null )
            EM = GameObject.FindGameObjectWithTag("enemyMarker");
        if(distanceMarker == null)
            distanceMarker = GameObject.FindGameObjectWithTag("distance");
        if (player != null && EM != null)
        {
            Vector3 viewPos = cam.WorldToViewportPoint(transform.position);

            //enemy is in view
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                float dot = Vector3.Dot(transform.forward, (player.transform.position - transform.position).normalized);
                float angle = Vector3.SignedAngle(player.transform.position, transform.position, Vector3.up);
                float distance = Vector3.Distance(player.transform.position, transform.position);
               // Debug.Log("distance is: " + distance);
                //show enemy marker until 10 units away
                if (distance > 10)
                    EM.transform.localScale = new Vector3(distance / 2.3F, distance / 2.3F, 0);
                //set size of enemy marker to 0 when close enough
                else
                    EM.transform.localScale = new Vector3(0, 0, 0);

                EM.transform.localPosition = new Vector3(0, distance / 2, 0);

                //only update distance four times second
                if (prevTime < timePassed - 0.25 && distance > 10)
                {
                    distanceMarker.GetComponent<TextMesh>().text = ("" + (int)distance + " M");
                    distanceMarker.GetComponent<TextMesh>().fontSize = (int)(distance * 2);
                    float remainder = 2.5F * (distanceMarker.GetComponent<TextMesh>().fontSize / 100);
                    if (dot > 0)
                    {
                        distanceMarker.transform.localPosition = new Vector3(remainder + distanceMarker.GetComponent<TextMesh>().fontSize / 10F, -1 * distance / 4, 0);
                        distanceMarker.transform.localEulerAngles = new Vector3(0, 180, 0);
                    }
                    else if (dot < 0)
                    {
                        distanceMarker.transform.localPosition = new Vector3((-1 * (remainder + distanceMarker.GetComponent<TextMesh>().fontSize / 10F)), -1 * distance / 4, 0);
                        distanceMarker.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    prevTime = timePassed;
                }
            }
            timePassed += Time.deltaTime;
        }
    }
}
