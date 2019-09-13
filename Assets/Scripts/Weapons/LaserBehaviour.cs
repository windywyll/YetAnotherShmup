using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    private float damage;
    private int lifeSpan;
    private GameObject owner;
    private float startDeathTimer;

    // Use this for initialization
    void Start ()
    {
        damage = 0.1f;
        lifeSpan = 2;
        startDeathTimer = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (startDeathTimer + lifeSpan < Time.time)
        {
            if (owner.tag == "Player")
            {
                owner.GetComponent<PlayerWeapon>().StartCoolDown();
            }
            else if (owner.tag == "Enemy")
            {
                if (owner.name.StartsWith("EnemyLaser"))
                {
                    owner.GetComponent<EnemyLaserWeapon>().StartCoolDown();
                }
            }

            Destroy(gameObject);
        }
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner;

        if (owner.tag == "Player")
        {
            owner.GetComponent<PlayerWeapon>().DecreaseNbProjectile();
        }

        transform.SetParent(owner.transform);

        startDeathTimer = Time.time;
    }

    public float GetDamage()
    {
        return damage;
    }
}
