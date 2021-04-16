using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers
{
    public GameObject Player;
    public GameObject RedLaser;
    Rigidbody direction;
    public GameObject laser;
    float timeSinceShot;
    float velocity;
    Vector3 position;
    Vector3 angle;
    
    

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
        if (timeSinceShot == 0F)
        {
            //Debug.Log("made it." );
            laser.transform.Rotate(angle.x, angle.y, angle.z, Space.World);
            direction.velocity = -1 * velocity * laser.transform.forward;
        }
        timeSinceShot += Time.deltaTime;

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

