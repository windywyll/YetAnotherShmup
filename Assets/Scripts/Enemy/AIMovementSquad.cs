using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementSquad : MonoBehaviour {

    private Vector3 destination;
    private Vector3 direction;
    private bool waitingAtDestination;
    private int speed = 3;
    private Rigidbody2D rBody;
    private bool stop;

    void OnTriggerEnter2D(Collider2D col)
    {

    }

	// Use this for initialization
	void Awake () {
        rBody = GetComponent<Rigidbody2D>();
        stop = false;
	}
	
	// Update is called once per frame
	void Update () {
        Move();

        CheckDeathOfSquad();
	}

    private void Move()
    {
        if (stop)
            return;

        direction = destination - transform.position + (transform.up * 10);
        direction.Normalize();

        rBody.velocity = direction * speed;
    }

    private void CheckDeathOfSquad()
    {
        if (transform.childCount == 0)
            Destroy(gameObject);

        if(Vector3.Distance(destination, transform.position + transform.up * -3) < 0.5f)
        {
            if (waitingAtDestination)
            {
                rBody.velocity = Vector2.zero;
                stop = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDestination(Vector3 _dest)
    {
        destination = _dest;
    }

    public void SetWaitingAtDestination(bool _wait)
    {
        waitingAtDestination = _wait;
    }
}
