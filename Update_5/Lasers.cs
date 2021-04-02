using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers
{
    public GameObject Player;
    public GameObject RedLaser;
    Rigidbody direction;
    GameObject laser;
    float timeSinceShot;
    float velocity;
    Vector3 position;
    Vector3 angle;
    
    void Start()
    {
        
    }

    public Lasers(GameObject laser, float timeSinceShot, float velocity, Vector3 position, Vector3 angle)
    {
        this.laser = laser;
        this.timeSinceShot = timeSinceShot;
        this.velocity = velocity;
        this.position = position;
        this.angle = angle;
        direction = laser.GetComponent<Rigidbody>();
    }

    public int getTime() { return (int)(timeSinceShot); }

    public void laserMovement()
    {
        
        if (timeSinceShot == 0)
        {
            //Debug.Log("made it." );
            laser.transform.Rotate(angle.x, angle.y, angle.z, Space.World);
            direction.velocity = -1 * velocity * laser.transform.forward;

        }

        //Vector3 laserLoc = laser.transform.localPosition;
        /*
        laserLoc.x += velocity * Mathf.Sin(Mathf.Deg2Rad*angle.y) * Time.deltaTime;
        laserLoc.y -= velocity * Mathf.Sin(Mathf.Deg2Rad*angle.x) * Time.deltaTime;
        laserLoc.z += velocity * Mathf.Cos(Mathf.Deg2Rad*angle.y) * Time.deltaTime;
        */
        //laser.transform.localPosition = laserLoc;// laser.velocity = lasers[s].laser.transform.forward * 5;
        timeSinceShot += Time.deltaTime; ;
        

    }

    public void destroyLaser(ref int numOfLasers)
    {
        if(laser != null)
            UnityEngine.Object.Destroy(laser);
        numOfLasers--;
    }

    public void destroyLaser()
    {
        if (laser != null)
            UnityEngine.Object.Destroy(laser);
    }

}

