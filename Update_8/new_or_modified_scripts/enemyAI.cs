using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    GameObject player; GameObject planet; GameObject freighter;
    Rigidbody direction;
    public GameObject enemyExplosion;
    public GameObject redLaser;
    bool attacking; bool avoiding;
    float distance; Vector3 pointToShoot;
    float timeAwake; float timeSinceLastShot; float timeAvoiding;
    float velocity; Vector3 playerVelocity; float AngleDiff;
    Vector3 relativePos;
    Lasers laser;
    destroyEnemyShip destroyEnemy;
    // Start is called before the first frame update
    void Start()
    {
        destroyEnemy = new destroyEnemyShip();
        planet = GameObject.Find("planet");
        player = GameObject.Find("Player");
        freighter = GameObject.Find("Freighter");
        direction = GetComponent<Rigidbody>();
        attacking = true;
        timeAwake = 0;
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
        if (player != null)
        {
            if (freighter == null)
            {
                destroyEnemy.destroy(gameObject, enemyExplosion);
            }
            else
            {
                distance = Vector3.Distance(player.transform.position + ((player.transform.forward * (distance / (100F + velocity)))), transform.position);
                playerVelocity = player.GetComponent<Rigidbody>().velocity;

                if (laser != null)
                {
                    laser.laserMovement();
                }

                AI();

                timeAwake += Time.deltaTime;
            }
        }
    }

    private void shoot()
    {
        if (timeAwake > 2F)
        {
            timeSinceLastShot = 0;
            Vector3 missle = transform.position;
            Quaternion mAngle = transform.rotation;
            GameObject m = Instantiate(redLaser, missle, mAngle);
            Vector3 laserAngle = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            laser = new Lasers(m, 0F, -1 * (100F + velocity), missle + new Vector3(Random.Range(-3F, 3F), Random.Range(-3F, 3F), Random.Range(-3F, 3F)), new Vector3(Random.Range(-0.075F, 0.075F), Random.Range(-0.075F, 0.075F), Random.Range(-0.075F, 0.075F)));
        }
    }

    private void AI()
    {
        Vector3 planetAdjust = planet.transform.position;
        planetAdjust.z -= 800;
        //Debug.Log("" + Vector3.Angle(planet.transform.position, transform.forward));
        if (Vector3.Distance(transform.position, planetAdjust) < 260F || Vector3.Distance(transform.position, freighter.transform.position) < 20F)// && Vector3.Angle(planet.transform.position, transform.forward) > )
        {
            if (attacking == true || avoiding == true)
            relativePos = relativePos + new Vector3(Random.Range(30F, 45F), Random.Range(45F, 60F), Random.Range(25F, 45F));
            attacking = false;
            avoiding = false;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime*145);
            direction.velocity = velocity * transform.forward;
        }
        else
        {
            if(!attacking && !avoiding)
            {
                avoiding = true;
                timeAvoiding = 5.01F;
            }
        }

            if (attacking)
        {
            if (velocity < 15)
                velocity += 5 * Time.deltaTime;

            direction.velocity = velocity * transform.forward;
            relativePos = (player.transform.position + (playerVelocity * (distance / (100F + velocity)))) - transform.position;// = player.transform.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360);

            timeSinceLastShot += Time.deltaTime;
            //Debug.Log("Angle is" + Vector3.Angle(transform.forward, transform.forward - player.transform.forward));
            if (timeSinceLastShot > 0.5F && Vector3.Angle(transform.forward, transform.forward - player.transform.forward) < 25F)
            {
                shoot();
                
            }

            if (Vector3.Distance(transform.position, player.transform.position) < 30F)
            {
                attacking = false;
                avoiding = true;
                timeAvoiding = 0;
            }

        }

        else if (avoiding)
        {

            if (timeAvoiding <= 5F)
            {
                if (playerVelocity.magnitude < 25F)
                    direction.velocity = 25 * transform.forward;
                else
                    direction.velocity = (playerVelocity.magnitude + 7) * transform.forward;
                velocity = direction.velocity.magnitude;
                
            }
            if (timeAvoiding == 0F)
            {
                relativePos = (player.transform.position + (playerVelocity * (distance / (100F + velocity)))) - transform.position;
                relativePos = relativePos + new Vector3(Random.Range(-10F, 10F), Random.Range(-10F, 10F), Random.Range(-10F, 10F));
            }
            if (timeAvoiding < 1F)
            {
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * (1 / Time.deltaTime));
            }
            if (timeAvoiding > 5F)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 30F)
                {
                    timeAvoiding = 0F;
                }
                else
                {
                    AngleDiff = Vector3.Angle(transform.forward * -1, player.transform.forward);
                    velocity -= 5 * Time.deltaTime;
                    direction.velocity = velocity * transform.forward;
                    //Debug.Log("Angle is" + AngleDiff);
                    //float step = Mathf.Deg2Rad*(AngleDiff/2);
                    //Quaternion rotation = Quaternion.LookRotation(player.transform.forward, Vector3.up);
                    //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(player.transform.position), 120*Time.deltaTime);
                    relativePos = (player.transform.position + (playerVelocity * (distance / (100F + velocity)))) - transform.position;// = player.transform.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 120);
                    if (Vector3.Angle(transform.forward, transform.forward - player.transform.forward) < 5F) {
                        attacking = true;
                        timeSinceLastShot = 0;
                        shoot();
                    }
                }
            }

            if (timeAvoiding > 6.5F)
            {

                attacking = true;
                timeSinceLastShot = 0;
                //shoot();
            }

            timeAvoiding += Time.deltaTime;
        }
    }
}

