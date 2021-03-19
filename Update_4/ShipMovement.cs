using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    Vector3 Vec;
    float acc = 0; float xAcc, yAcc, zAcc; float accMax = 4;
    float velo, xVelo, yVelo, zVelo;
    float yAngle, xAngle, zAngle;
    Vector3 to;
    int n; float camX = 0; float camY = 0; float camZ = 0;
    float rad; float width = Screen.width; float height = Screen.height; float screenW2; float screenH2; float mouseX; float mouseY;
    bool moveRight; bool moveLeft; bool moveUp; bool moveDown; bool tiltRight; bool tiltLeft;
    Camera cam;
    //new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        // camera = Camera;
        cam = GetComponentInChildren<Camera>();
        xAngle = transform.localEulerAngles.x;
        yAngle = transform.localEulerAngles.y;
        zAngle = transform.localEulerAngles.z;
    }
      
    // Update is called once per frame
    void Update()
    {
        movement();
        rotateCamera();

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
        if (mouseY > screenH2 && mouseY < height)
        {
            xAngle = Mathf.Pow((((mouseY - screenH2) / screenH2) * -1),2) * -1;
            moveUp = true;
        }
        else if (mouseY < screenH2 && mouseY > 0)
        {
            xAngle = Mathf.Pow(((1 - (mouseY / screenH2)) * 1),2);
            moveDown = true;
        }
        // adjusting zAngle
        if (Input.GetKey(KeyCode.D))
        {
            tiltRight = true;
            zAngle = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            tiltLeft = true;
            zAngle = 1;
        }
        // adjusting yAngle
        if (mouseX > screenW2 && mouseX < width)
        {
            yAngle = (((mouseX - screenW2) / screenW2) * 1);
            moveRight = true;
        }
        else if (mouseX < screenW2 && mouseX > 0)
        {
            moveLeft = true;
            yAngle = (1 - (mouseX / screenW2)) * -1;
        }
        rad = Mathf.Deg2Rad * transform.localEulerAngles.y;
        // moving the object
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            
           
            acc += 0.8f * Time.deltaTime;
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
                acc -= 5 * Time.deltaTime;
            if (acc < -10*Time.deltaTime)
            {
                acc = -10*Time.deltaTime;
            }
            velo += acc;
            if (velo < 0)
                velo = 0;
         

        }
/*        else
        {
           
            acc = -3 * Time.deltaTime;
            if (acc < -3 * Time.deltaTime)
            {
                acc = -3 * Time.deltaTime;
            }
            
            velo += acc;
            if (velo < 0)
                velo = 0;
            

        }*/
        zVelo = velo * Mathf.Cos(rad);
        xVelo = Mathf.Sin(rad) * velo;
        yVelo = Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.x) * -velo;

        Vec.z += zVelo*Time.deltaTime;
        Vec.x += xVelo*Time.deltaTime;
        Vec.y += yVelo*Time.deltaTime;

       // Debug.Log(transform.localEulerAngles.y + "   "   + velo);
        
            // translating the adjustments to the object local transform rotation
        to = new Vector3(xAngle, yAngle, zAngle);
        transform.Rotate(to, Space.Self);// Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
        transform.localPosition = Vec;
        yAngle = 0; zAngle = 0; xAngle = 0;
    }

    void rotateCamera()
    {
        Vec.x = 0;// transform.localPosition.x;
        Vec.y = 10;
        Vec.z = 3;
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
                Vec.y += Mathf.Pow(((1 - (mouseY / screenH2)) * 1), 2) * 5;
                camX = Mathf.Pow(((1 - (mouseY / screenH2)) * 1), 2) * -5;
            }
            else if (moveUp)
            {
                Vec.y += Mathf.Pow((((mouseY - screenH2) / screenH2) * -1), 2) * -5;
                camX = Mathf.Pow((((mouseY - screenH2) / screenH2) * -1), 2) * 5;
            }
            Vec.z = cam.transform.localPosition.z;
            Quaternion to = Quaternion.Euler(transform.localEulerAngles.x + camX, transform.localEulerAngles.y + camY, transform.localEulerAngles.z);
            cam.transform.rotation = to;
            cam.transform.localPosition = Vec;
        }
    }
}