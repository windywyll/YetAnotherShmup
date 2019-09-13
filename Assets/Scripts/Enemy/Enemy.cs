using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float life;
    private int score;

    [SerializeField]
    private bool elite;
    private Player player;

    private GameObject[] PowerUp;

    private float baseLuckMoney;
    private static int luckStep = -1;
    private static float[] luckModifiers = {0, 5, 10, 20, 30, 40, 65, 90 };

    void OnTriggerEnter2D(Collider2D col)
    {
        Transform _root = col.transform.parent.parent;

        if (_root.tag == "PlayerProjectile")
        {
            float _lifeLost = 0;

            if (_root.name.StartsWith("Laser"))
                _lifeLost = _root.GetComponent<LaserBehaviour>().GetDamage();

            if (_root.name.StartsWith("Bullet"))
            {
                _lifeLost = _root.GetComponent<BulletBehaviour>().GetDamage();
                Destroy(_root.gameObject);
            }

            LoseLife(_lifeLost);

            if (life <= 0)
            {
                Death();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Transform _root = col.transform.parent.parent;

        if (_root.tag == "PlayerProjectile")
        {
            float _lifeLost = 0;

            if (_root.name == "Laser")
            {
                _lifeLost = _root.GetComponent<LaserBehaviour>().GetDamage();
            }

            LoseLife(_lifeLost);
        }
    }

    // Use this for initialization
    void Start () {

        if(luckStep == -1)
        {
            luckStep = 0;
        }

        baseLuckMoney = 10;
        life = 2;
        score = 1000;

        if (elite)
        {
            life += 8;
            score += 2542;
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

    private void LoseLife(float _lifeLost)
    {
        life -= _lifeLost;

        if (life <= 0)
        {
            player.AddScore(score);
        }
    }

    private void Death()
    {
        float rand = Random.Range(0, 100);

        if( rand < baseLuckMoney + luckModifiers[luckStep])
        {
            luckStep = 0;
            (EnemySpawner.GetNextUnusedMoney()).SpawnMoney(transform.position);
        }
        else
        {
            luckStep++;
        }

        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
