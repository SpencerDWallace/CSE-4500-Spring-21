/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockUnit : MonoBehaviour
{
    [SerializeField] private float FOVAngle;
    [SerializeField] private float smoothDamp;

    private List<flockUnit> cohesionNeighbors = new List<flockUnit>();
    private flock assignedFlock;
    private Vector3 currentVelocity;
    private float speed;

    public Transform MyProperty { get; set; }

    public void AssignFlock (flock f)
    {
        assignedFlock = f;
    }

    public void InitializeSpeed(float speed)
    {
        this.speed = speed;
    }


    public void Move()
    {
        FindNeighbors();
        var cohesionVector = CalculateCohesionVector();
        var moveVector = Vector3.SmoothDamp(transform.forward, cohesionVector, ref currentVelocity, smoothDamp);
        moveVector = moveVector.normalized * speed;
        transform.forward = moveVector;
        transform.position += moveVector * Time.deltaTime;
    }

public void FindNeighbors()
    {
        cohesionNeighbors.Clear();
        var allUnits = assignedFlock.allUnits;
        for (int i = 0; i < allUnits.Length; i++)
        {
            var currentUnit = allUnits[i];
            if(currentUnit != this)
            {
                float currentNDistSqr = Vector3.SqrMagnitude(currentUnit.transform.position - transform.position);
                if(currentNDistSqr <= assignedFlock.cohesionDistance * assignedFlock.cohesionDistance)
                {
                    cohesionNeighbors.Add(currentUnit);
                }
            }
        }
    }

    private Vector3 CalculateCohesionVector()
    {
        var cohesionVector = Vector3.zero;
        if (cohesionNeighbors.Count == 0)
        {
            return cohesionVector;
        }
        int neighboursInFov = 0;
        for (int i = 0; i < cohesionNeighbors.Count; i++)
        {
            if (IsInFOV(cohesionNeighbors[i].transform.position))
            {
                neighboursInFov++;
                cohesionVector += cohesionNeighbors[i].transform.position;
            }
            
        }
        if (neighboursInFov == 0)
            return cohesionVector;
        cohesionVector /= neighboursInFov;
        cohesionVector -= transform.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    private bool IsInFOV(Vector3 position)
    {
        return Vector3.Angle(transform.forward, position - transform.position) <= FOVAngle;
    }

}
*/