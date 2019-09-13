using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletWeapon : MonoBehaviour {
    
    [SerializeField]
    private GameObject projectile;

    private float startCDWeapon, cooldownWeapon;
    private int bulletSpeed;

    // Use this for initialization
    void Awake () {
        startCDWeapon = -1;
        cooldownWeapon = 1f;
        bulletSpeed = 7;
    }
	
	// Update is called once per frame
	void Update () {
        Shoot();
	}

    void Shoot()
    {
        if (cooldownWeapon + startCDWeapon > Time.time)
            return;

        Vector3 _position = transform.position + (transform.up * 0.8f);
        Quaternion _rotation = transform.rotation;

        GameObject _projectile = Instantiate(projectile, _position, _rotation);
        _projectile.tag = "EnemyProjectile";
        _projectile.GetComponent<BulletBehaviour>().SetOwner(gameObject);
        _projectile.GetComponent<BulletBehaviour>().SetBulletSpeed(bulletSpeed);
    }

    public void StartCoolDown()
    {
        startCDWeapon = Time.time;
    }
}
