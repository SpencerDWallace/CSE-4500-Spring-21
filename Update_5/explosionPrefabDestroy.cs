using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionPrefabDestroy : MonoBehaviour
{
        float timeSinceCreation;
        // Start is called before the first frame update
        void Start()
        {
            timeSinceCreation = 0;
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceCreation += Time.deltaTime;
            if (timeSinceCreation > 1)
                UnityEngine.Object.Destroy(gameObject);
        }
    
}
