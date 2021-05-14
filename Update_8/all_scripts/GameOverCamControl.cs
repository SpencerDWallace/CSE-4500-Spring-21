using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCamControl : MonoBehaviour
{
    GameObject HUD;
    public GameObject gameoverHUD;
    float t;
    float dist;
    float AngleDiff;
    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.Find("HUD");
        HUD.SetActive(false);
        Instantiate(gameoverHUD);
        t = 0;
        dist = 100000;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (t<1)
        t += Time.deltaTime;
        else
        {
            if(dist == 100000)
            dist = Vector3.Distance(new Vector3(100, 80, 600), transform.position);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(100,80,600), 30*Time.deltaTime);
            AngleDiff = Quaternion.Angle(Quaternion.Euler(18, -40, 0), transform.rotation);
            if (AngleDiff != 0F)
            {                
                float step = Mathf.Deg2Rad*(AngleDiff * (dist/30)*Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(18, -140, 0), step);
            }
        }
    }
}
