using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private int life;
    private int score;
    private int money;
    private PlayerWeapon weaponManager;
    private float startDelayInvulnerability, delayInvulnerability;
    private float speed;
    private Rigidbody2D rBody;
    private Transform player;
    private SpriteRenderer playerSprite;
    private Vector3 originalPosition;
    private UIManager UIObserver;
    private bool canShoot;

    void OnTriggerEnter2D(Collider2D col)
    {
        Transform _root = col.transform.parent.parent;

        if(_root.tag == "Enemy" || _root.tag == "EnemyProjectile" || _root.tag == "Boss")
        {
            if (startDelayInvulnerability + delayInvulnerability > Time.time)
                return;

            Death();

            if (!_root.tag.StartsWith("Boss"))
            {
                Destroy(_root.gameObject);
            }
            else
            {
                //make 10 of damage to bosses;
            }
        }

        if(_root.tag == "Money")
        {
            _root.GetComponent<Money>().ReturnToOriginalPosition();
            money++;
            UIObserver.UpdateMoneyUI(money);
        }
    }

    // Use this for initialization
    void Awake () {
        life = 3;
        score = 0;
        money = 0;
        startDelayInvulnerability = -1;
        delayInvulnerability = 1.5f;
        speed = 3;
        rBody = GetComponent<Rigidbody2D>();
        player = transform;
        weaponManager = GetComponent<PlayerWeapon>();
        originalPosition = player.position;
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        canShoot = true;

        DontDestroyOnLoad(player.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (!canShoot)
        {
            return;
        }

        AutoFire();

        //Debug.Log(score);
	}

    public void ResetPosition()
    {
        player.position = originalPosition;
        rBody.velocity = Vector2.zero;
    }

    private void Death()
    {
        player.position = originalPosition;
        life--;
        startDelayInvulnerability = Time.time;

        UIObserver.UpdateLivesUI(life);

        if (life == 0)
        {
            player.position = new Vector2(1000, 1000);
            rBody.velocity = Vector2.zero;

            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        }
    }

    public void Move(Vector2 _direction)
    {
        if(life <= 0)
        {
            return;
        }

        if (player.position.x - playerSprite.bounds.extents.x < -8.4f && _direction.x < 0)
        {
            _direction.x = 0;
        }

        if (player.position.x + playerSprite.bounds.extents.x > 8.4f && _direction.x > 0)
        {
            _direction.x = 0;
        }

        if (player.position.y - playerSprite.bounds.extents.y < -4.6f && _direction.y < 0)
        {
            _direction.y = 0;
        }

        if (player.position.y + playerSprite.bounds.extents.y > 4.6f && _direction.y > 0)
        {
            _direction.y = 0;
        }

        rBody.velocity = _direction * speed;
    }

    public void StopShooting()
    {
        canShoot = false;
    }

    public void StartShooting()
    {
        canShoot = true;
    }

    private void AutoFire()
    {
        weaponManager.Shoot();
    }

    public void AddScore(int _points)
    {
        score += _points;
        UIObserver.UpdateScoreUI(score);
    }

    public void RemoveXMoney(int moneyToRemove)
    {
        money -= moneyToRemove;
    }

    public int GetMoneyCount()
    {
        return money;
    }

    public void SetUIListener(UIManager _UIObserver)
    {
        UIObserver = _UIObserver;

        UIObserver.UpdateScoreUI(score);
        UIObserver.UpdateLivesUI(life);
        UIObserver.UpdateMoneyUI(money);
    }

    public void UpgradeSpeed(int speedModifier)
    {
        speed += speed;
    }

    public void LifeUp()
    {
        life++;
    }

    public int GetLifeCount()
    {
        return life;
    }

    public int GetScore()
    {
        return score;
    }
}
