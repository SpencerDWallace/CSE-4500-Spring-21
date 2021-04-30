using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockAI : MonoBehaviour
{
    GameObject planet; GameObject freighter;
    Rigidbody direction;
    public GameObject enemyExplosion;
    bool leaving; bool turnAround; bool swarming;
    float distance; 
    float velocity;  float AngleDiff;
    Vector3 relativePos;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("planet");
        freighter = GameObject.Find("Freighter");
        direction = GetComponent<Rigidbody>();
        leaving = true;
        relativePos = freighter.transform.position - transform.position + new Vector3(Random.Range(-30F, 30F), Random.Range(-30F, 30F), Random.Range(-30F, 30F));
    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject collided = col.collider.gameObject;
        if (collided.tag == "planetMesh" || collided.tag == "Planet" || collided.tag == "Freighter")
        {
            Instantiate(enemyExplosion, transform.position, transform.rotation);
            UnityEngine.Object.Destroy(gameObject);
        }
    }

    void Update()
    {
        distance = Vector3.Distance(freighter.transform.position, transform.position);
        AI();

    }

    private void AI()
    {
       /* Vector3 planetAdjust = planet.transform.position;
        planetAdjust.z -= 800;
        //Debug.Log("" + Vector3.Angle(planet.transform.position, transform.forward));
        if (Vector3.Distance(transform.position, planetAdjust) < 260F || Vector3.Distance(transform.position, freighter.transform.position) < 13F)// && Vector3.Angle(planet.transform.position, transform.forward) > )
        {
            if (leaving == true || turnAround == true)
                relativePos = relativePos + new Vector3(Random.Range(30F, 45F), Random.Range(45F, 60F), Random.Range(25F, 45F));
            leaving = false;
            turnAround = false;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 145);
            direction.velocity = velocity * transform.forward;
        }
        else
        {
            if (!leaving && !turnAround)
            {
                relativePos = freighter.transform.position - transform.position + new Vector3(Random.Range(-10F, 10F), Random.Range(-10F, 10F), Random.Range(-10F, 10F));// = player.transform.position - transform.position;
                turnAround = true;
                timeAvoiding = 5.01F;
            }
        }
        */
        if (swarming)
        {
            Debug.Log("SWARMING");
            relativePos = freighter.transform.position - transform.position;
            direction.velocity = velocity * transform.forward;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360);
            if (Vector3.Distance(transform.position, freighter.transform.position) < 20F)
            {
                relativePos = relativePos + new Vector3(Random.Range(30F, 45F), Random.Range(45F, 60F), Random.Range(25F, 45F));
                leaving = true;
                swarming = false;
            }
        }

        else if (leaving)
        {
            Debug.Log("LEAVING");
            if (velocity < 25)
                velocity += 5 * Time.deltaTime;

            direction.velocity = velocity * transform.forward;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360);
            
            if (Vector3.Distance(transform.position, freighter.transform.position) > 100F)
            {
                leaving = false;
                turnAround = true;
            }
        }

        else if (turnAround)
        {
            Debug.Log("TURNING");
            //AngleDiff = Vector3.Angle(transform.forward * -1, freighter.transform.position);
            velocity -= 5 * Time.deltaTime;
            direction.velocity = velocity * transform.forward;
            relativePos = freighter.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, relativePos, Time.deltaTime * 60, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDirection);

            if (Vector3.Angle(relativePos, transform.forward) < 5F)
            {
                turnAround = false;
                swarming = true;
            }

        }

    }
}




