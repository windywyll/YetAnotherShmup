using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private bool isUsed;
    private Vector3 originalPos;
    private float lifespan;
    private float beginUsingTime;

    // Start is called before the first frame update
    void Start()
    {
        isUsed = false;
        originalPos = transform.position;
        lifespan = 7.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isUsed && beginUsingTime + lifespan < Time.time )
        {
            ReturnToOriginalPosition();
        }
    }

    public void ReturnToOriginalPosition()
    {
        transform.position = originalPos;
        isUsed = false;
    }

    public void SpawnMoney(Vector3 newPos)
    {
        transform.position = newPos;
        beginUsingTime = Time.time;
        isUsed = true;
    }

    public bool IsBeingUsed()
    {
        return isUsed;
    }
}
