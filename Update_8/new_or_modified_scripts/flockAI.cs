using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockAI : MonoBehaviour
{
    GameObject planet; GameObject freighter;
    Rigidbody direction;
    public GameObject enemyExplosion;
    bool turnAround; bool swarming; bool detected;
    float distance; 
    float velocity;  float AngleDiff;
    Vector3 relativePos; Quaternion rotation;
    enum Direction {UP, DOWN, LEFT, RIGHT};
    Direction avoid;
    public float maxAvoidDistance;
    destroyEnemyShip destroyFlockling;
    private float timeSinceFreighterDestroyed, maxTimeBeforeExplode;
    // Start is called before the first frame update
    void Start()
    {
        destroyFlockling = new destroyEnemyShip();
        timeSinceFreighterDestroyed = 0;
        maxTimeBeforeExplode = Random.Range(0.5F, 3F);
        planet = GameObject.Find("planet");
        freighter = GameObject.Find("Freighter");
        direction = GetComponent<Rigidbody>();
        turnAround = true;
        detected = false;
        avoid = (Direction)((int)(Random.Range(0F, 3.99F)));
        maxAvoidDistance = Random.Range(50F, 100F);
        relativePos = freighter.transform.position - transform.position + new Vector3(Random.Range(-30F, 30F), Random.Range(-30F, 30F), Random.Range(-30F, 30F));
    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject collided = col.collider.gameObject;
        if (collided.tag == "planetMesh" || collided.tag == "Planet" || collided.tag == "Freighter" || collided.tag == "turret")
        {
            Instantiate(enemyExplosion, transform.position, transform.rotation);
            UnityEngine.Object.Destroy(gameObject);
        }
    }

    void Update()
    {
        if (freighter == null)
            timeSinceFreighterDestroyed += Time.deltaTime;
        if(timeSinceFreighterDestroyed > maxTimeBeforeExplode)
        {
            destroyFlockling.destroy(gameObject, enemyExplosion);
        }

        if (gameObject != null && freighter != null)
        {
            distance = Vector3.Distance(freighter.transform.position, transform.position);
            AI();
        }

    }

    private void AI()
    {

        if (swarming)
        {
            var dir = (freighter.transform.position - transform.position).normalized;
            RaycastHit hit;


            for (int i = -3; i < 4; i++)
            {
                var posX = transform.position; var posY = transform.position;

                posX.x += i;
                posY.y += i;

                if ((Physics.Raycast(posX, transform.forward, out hit, 50) || Physics.Raycast(posY, transform.forward, out hit, 50)) && !detected)
                {
                    //Debug.Log("Hit!");
                    if (hit.collider.gameObject.tag == "Freighter")
                    {
                       
                        relativePos = transform.forward * -400;// = player.transform.position - transform.position;
                        detected = true;
                    }
                }
                i = i + 2;
            }
            if (detected && Vector3.Distance(transform.position, relativePos) < 10F)
            {
                relativePos = transform.forward * 100;
                detected = false;
            }

            switch (avoid)
            {
                case Direction.UP: rotation = Quaternion.LookRotation(relativePos, Vector3.right);
                    break;
                case Direction.DOWN: rotation = Quaternion.LookRotation(relativePos, Vector3.left);
                    break;
                case Direction.LEFT: rotation = Quaternion.LookRotation(relativePos, Vector3.left);
                    break;
                case Direction.RIGHT: rotation = Quaternion.LookRotation(relativePos, Vector3.right);
                    break;
            }
            rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 50);
            if (velocity < 25)
                velocity += 5 * Time.deltaTime;
            direction.velocity = velocity * transform.forward;

            if (Vector3.Distance(transform.position, freighter.transform.position) > maxAvoidDistance)
            {
                swarming = false;
                turnAround = true;
            }
        }

        else if (turnAround)
        {
           // Debug.Log("TURNING");
            //AngleDiff = Vector3.Angle(transform.forward * -1, freighter.transform.position);
            if (velocity < 25)
                velocity += 5 * Time.deltaTime;
            direction.velocity = velocity * transform.forward;
            relativePos = freighter.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, relativePos, Time.deltaTime * 30, 0.0F);
            rotation = Quaternion.LookRotation(newDirection);

            //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 120);

            if (Vector3.Angle(relativePos, transform.forward) < 5F)
            {
                
                turnAround = false;
                //relativePos = relativePos + new Vector3(Random.Range(-20F, 20F), Random.Range(-60F, 60F), Random.Range(-50F, 50F));
                swarming = true;
                detected = false;
            }

        }

    }
}




