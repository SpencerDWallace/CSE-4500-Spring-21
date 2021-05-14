using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLaserDestroy : MonoBehaviour
{
    float timeAwake;
    // Start is called before the first frame update
    void Start()
    {
        timeAwake = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAwake > 8F)
            UnityEngine.Object.Destroy(gameObject);
        timeAwake += Time.deltaTime;
        
    }
}
