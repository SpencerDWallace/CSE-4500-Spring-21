using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentEnemyMarker : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = transform.parent.position;
        transform.position = new Vector3(0, 10, 0);
    }
}
