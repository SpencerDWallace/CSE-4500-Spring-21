 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMain : MonoBehaviour
{

    
    Vector3 Vec; Vector3 prevpos; Vector3 fwd;
    Rigidbody player;
    float acc = 0; float xAcc, yAcc, zAcc; float accMax = 3;
    float velo, xVelo, yVelo, zVelo;
    float yAngle, xAngle, zAngle;
    Vector3 to;
    public Lasers[] lasers;
    //HUD hud;
    public GameObject zero; public GameObject one; public GameObject two; public GameObject three; public GameObject four; public GameObject five; public GameObject six; public GameObject seven; public GameObject eight; public GameObject nine; public GameObject ten;
    public int numOfLasers = 0; int previousNumOfLasers = 0;
    public GameObject blueLaser; public GameObject enemy; public GameObject victoryCam; public GameObject explosion;
    GameObject hud; GameObject crosshair; GameObject playerHealth;
    GameObject ammo;
    int n; float camX = 0; float camY = 0; 
    float rad; float width = Screen.width; float height = Screen.height; float screenW2; float screenH2; float mouseX; float mouseY;
    bool moveRight; bool moveLeft; bool moveUp; bool moveDown; bool tiltRight; bool tiltLeft; bool ammoChanged = false;
    public Camera cam; public GameObject gameoverCam;  Health Health;
    float t, timeSinceFreighterDestroyed, victoryStart;
    GameObject HUD; GameObject freighter; bool Victory;
    //new Camera camera;

    
    // Start is called before the first frame update
    void Start()
    {
        // camera = Camera;
        victoryStart = 4f;
        Victory = false;
        HUD = GameObject.Find("HUD");
        freighter = GameObject.FindGameObjectWithTag("Freighter");
        Instantiate(enemy);
        player = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        lasers = new Lasers[10];
        hud = GameObject.Find("HUD");
        crosshair = hud.transform.GetChild(0).gameObject;
        xAngle = transform.localEulerAngles.x;
        yAngle = transform.localEulerAngles.y;
        zAngle = transform.localEulerAngles.z;
        crosshair.transform.localPosition = new Vector3(0,(Screen.height / 6.75F),0);
        Health = GetComponent<Health>();
        playerHealth = GameObject.Find("Player_healthbar");
        t = 0;
    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject collided = col.collider.gameObject;
        if (collided.tag == "Freighter" || collided.tag == "turret")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            gameOver();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (Health.health > 0)
        {
            if (!Victory)
            {
                if (Health.health <= 45F && t > 10F)
                {
                    Health.health += 5;
                    t = 0;
                    playerHealth.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Health.health * 4);
                }
                // Debug.Log(1.0f / Time.deltaTime);
                

                if (height != Screen.height)
                {
                    crosshair.transform.localPosition = new Vector3(0, (Screen.height / 8), 0);
                    height = Screen.height;
                }

                movement();
                rotateCamera();


                if (Input.GetKeyDown(KeyCode.Space) && numOfLasers < 10)
                {
                    fire();
                    ammoChanged = true;
                }

                verifyLasers();

                if (ammoChanged || numOfLasers == 0)
                {

                    updateAmmo(numOfLasers);
                    ammoChanged = false;
                }
                if (freighter == null)
                {
                    timeSinceFreighterDestroyed += Time.deltaTime;
                    if (timeSinceFreighterDestroyed > victoryStart)
                        Victory = true;
                }
            }
            else
            {
                victory();
            }
        }
        else
        {
            gameOver();
            //gameObject is effectively destroyed upon gameOver, script no longer runs
        }
        t += Time.deltaTime;
    }

    public void gameOver()
    {
        gameObject.SetActive(false);
        Instantiate(gameoverCam, cam.transform.position, cam.transform.rotation);
       // gameoverCam.enabled = true;
    }

    public void victory()
    {
        HUD.SetActive(false);
        Instantiate(victoryCam);
        player.velocity = new Vector3(0f, 0f, 0f);
    }

    public void verifyLasers()
    {
        for (int s = 0; s < numOfLasers; s++)
        {
            if (lasers[s] != null)
            {
                int t = lasers[s].getTime();
                if (t >= 4)
                {
                    lasers[s].destroyLaser(ref numOfLasers);
                    lasers[s] = lasers[numOfLasers];
                    lasers[numOfLasers] = null;
                    ammoChanged = true;
                }
                else
                {
                    lasers[s].laserMovement();
                }
            }
        }
    }

    public void updateAmmo(int remainingAmmo)
    {
        //if(remainingAmmo == 0 && remainingAmmo != previousNumOfLasers)
        //  UnityEngine.Object.Destroy(ammo);
        if (remainingAmmo != previousNumOfLasers)
        {
            UnityEngine.Object.Destroy(ammo);
            ammo = null;
        }
        previousNumOfLasers = remainingAmmo;
        remainingAmmo = 10 - remainingAmmo;
        if(ammo == null)
        {
            switch (remainingAmmo)
            {
                case 0: ammo = GameObject.Instantiate(zero) as GameObject; break;
                case 1: ammo = GameObject.Instantiate(one) as GameObject; break;
                case 2: ammo = GameObject.Instantiate(two) as GameObject; break;
                case 3: ammo = GameObject.Instantiate(three) as GameObject; break;
                case 4: ammo = GameObject.Instantiate(four) as GameObject; break;
                case 5: ammo = GameObject.Instantiate(five) as GameObject; break;
                case 6: ammo = GameObject.Instantiate(six) as GameObject; break;
                case 7: ammo = GameObject.Instantiate(seven) as GameObject; break;
                case 8: ammo = GameObject.Instantiate(eight) as GameObject; break;
                case 9: ammo = GameObject.Instantiate(nine) as GameObject; break;
                case 10: ammo = GameObject.Instantiate(ten) as GameObject; break;
                default: break;
            }
        }
        ammo.transform.SetParent(hud.transform, false);
        
    }

    void fire()
    {
        Vector3 missle = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Quaternion mAngle = Quaternion.Euler(transform.localEulerAngles);
        mAngle.x += 90;

        GameObject m = Instantiate(blueLaser, missle, mAngle);
        
        Vector3 laserAngle = new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        //Debug.Log("angle x is: " + laserAngle.x + "  angle y is: " + laserAngle.y + " angle z is:" + laserAngle.z);
        
        lasers[numOfLasers] = new Lasers(m, 0F, (velo + 100F), missle, laserAngle);
        numOfLasers++;
    }
 
    void movement()
    {
        height = Screen.height;
        width = Screen.width;
        moveRight = moveLeft = moveUp = moveDown = tiltRight = tiltLeft = false;
        screenH2 = height / 2;
        screenW2 = width / 2;
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        Vec = transform.localPosition;

        // adjusting xAngle
        if (mouseY > screenH2)
        {
            xAngle = Mathf.Pow((((mouseY - screenH2) / screenH2) * 0.6F),2) * -1;
            moveUp = true;
        }
        else if (mouseY < screenH2)
        {
            xAngle = Mathf.Pow(((1 - (mouseY / screenH2)) * 0.6F),2);
            moveDown = true;
        }
        // adjusting zAngle
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            tiltRight = true;
            zAngle = -1.2F;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            tiltLeft = true;
            zAngle = 1.2F;
        }
        // adjusting yAngle
        if (mouseX > screenW2)
        {
            yAngle = (((mouseX - screenW2) / screenW2) * 1.2F);
            moveRight = true;
        }
        else if (mouseX < screenW2)
        {
            moveLeft = true;
            yAngle = (1 - (mouseX / screenW2)) * -1.2F;
        }
        rad = Mathf.Deg2Rad * transform.localEulerAngles.y;
        // moving the object
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

            if (acc < 0) acc = 0;
            acc += 0.12f * Time.deltaTime;
            if (acc > accMax)
                {
                    acc = accMax;
                }
                velo += acc;
            if (velo > 30)
                velo = 30;
                
            
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (acc > 0)
                acc = 0;
                acc -= 0.35F * Time.deltaTime;
            /*if (acc < 0)
            {
                acc = 0;// -50*Time.deltaTime;
            }
            */
            velo += acc;
            if (velo < 0)
                velo = 0;
         

        }
        
        zVelo = velo * Mathf.Cos(rad);
        xVelo = Mathf.Sin(rad) * velo;
        yVelo = Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.x) * -velo;

        player.AddForce(acc * transform.forward, ForceMode.VelocityChange);
        if (player.velocity.magnitude > 35)
        {
            player.velocity = player.velocity.normalized * 35f;
        }
        

        if(Vector3.Dot(fwd, (transform.position - prevpos)) < 0 && acc < 0)
        {
            player.velocity = player.velocity.normalized * 4f;
        }

      //  Debug.Log("" + Vector3.Dot(fwd, (transform.position - prevpos)));
        

       //player.velocity = velo*transform.forward;
       // Vec.x += xVelo*Time.deltaTime;
       // Vec.y += yVelo*Time.deltaTime;

       // Debug.Log(transform.localEulerAngles.y + "   "   + velo);
        
            // translating the adjustments to the object local transform rotation
        to = new Vector3(xAngle, yAngle, zAngle);
        transform.Rotate(to, Space.Self);// Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
        //transform.localPosition = Vec;
        yAngle = 0; zAngle = 0; xAngle = 0;
    }

    void rotateCamera()
    {
        Vec.x = 0;//2;// transform.localPosition.x;
        Vec.y = 5;
        //  Vec.z = 3;
        if (!moveLeft && !moveRight && !moveUp && !moveDown && !tiltLeft && !tiltRight)
        {
            camY = 0;
            cam.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        else
        {
            if (moveRight)
            {
                Vec.x += (((mouseX - screenW2) / screenW2) * 6);
                camY = (((mouseX - screenW2) / screenW2) * 6);
            }
            else if (moveLeft)
            {
                Vec.x += (1 - (mouseX / screenW2)) * -6;
                camY = (1 - (mouseX / screenW2)) * -6;
            }

            if (moveDown)
            {
                Vec.y += Mathf.Pow((1 - (mouseY / screenH2)), 2) * -3;
                camX = Mathf.Pow((1 - (mouseY / screenH2)), 2) * -3;
            }
            else if (moveUp)
            {
                Vec.y += Mathf.Pow(((mouseY - screenH2) / screenH2), 2) * 3;
                camX = Mathf.Pow(((mouseY - screenH2) / screenH2), 2) * 3;
            }

            Vec.z = cam.transform.localPosition.z;
            Quaternion to = Quaternion.Euler(transform.localEulerAngles.x + camX + 6, transform.localEulerAngles.y + camY, transform.localEulerAngles.z);
            cam.transform.localPosition = Vec;
        }
    }
}