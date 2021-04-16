using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelMovement : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject turret; GameObject temp;
    GameObject player;
    public GameObject redLaser;
    Lasers laser;
    float timeSinceLastShot; Vector3 playerVelocity;
    void Start()
    {
        turret = gameObject.transform.parent.gameObject;
        player = GameObject.Find("Player");
        temp = gameObject;
        timeSinceLastShot = 0;
        playerVelocity = player.GetComponent<Rigidbody>().velocity;
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            adjustBarrel();
        }
        
    }

    private void adjustBarrel()
    {
        
        bool inSight = false;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 relativePos = player.transform.position + ((player.transform.forward * (distance / 50F))) - transform.position;// = player.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        float testX = rotation.eulerAngles.x;
        float testY = rotation.eulerAngles.y;
        
        if (testX > 180)
            testX -= 360F;

        if (gameObject.tag == "positiveBarrel")
        {
            if (Mathf.Sin(Mathf.Deg2Rad * testY) < Mathf.Sin(Mathf.Deg2Rad * 191.5F))
                inSight = true;
        }
        else if(gameObject.tag == "negativeBarrel")
        {
            if (Mathf.Sin(Mathf.Deg2Rad * testY) > Mathf.Sin(Mathf.Deg2Rad * 168.5F))
                inSight = true;
        }
       
        if (testX <= 10F && testX >= -90F && inSight)
        {
            
            transform.rotation = rotation;
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, turret.transform.localEulerAngles.y, turret.transform.localEulerAngles.z);
            if (timeSinceLastShot >= 0.6F)
                shoot();
            turret.transform.rotation = rotation;
            turret.transform.rotation = Quaternion.Euler(0, turret.transform.localEulerAngles.y, 0);
        }
        
        timeSinceLastShot += Time.deltaTime;

    }

    private void shoot()
    {
        timeSinceLastShot = 0;
        Vector3 missle = transform.position;
        Quaternion mAngle = transform.rotation;
        GameObject m = Instantiate(redLaser, missle, mAngle);
        Vector3 laserAngle = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        laser = new Lasers(m, 0F, -1 * (50F), missle, new Vector3(Random.Range(-0.1F, 0.1F), Random.Range(-0.1F, 0.1F), Random.Range(-0.1F, 0.1F)));
        laser.laserMovement();
    }

}
