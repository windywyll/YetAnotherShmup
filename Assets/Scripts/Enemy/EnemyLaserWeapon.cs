using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserWeapon : MonoBehaviour
{

    [SerializeField]
    private GameObject projectile;

    private float startCDWeapon, cooldownWeapon;
    private int nbMaxLaser;
    private int nbLaser;

    // Use this for initialization
    void Awake()
    {
        startCDWeapon = -2;
        cooldownWeapon = 3f;
        nbMaxLaser = 1;
        nbLaser = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (nbMaxLaser == nbLaser)
            return;

        if (cooldownWeapon + startCDWeapon > Time.time)
            return;

        Vector3 _position = transform.position + (transform.up * 0.8f);
        Quaternion _rotation = transform.rotation;

        GameObject _projectile = Instantiate(projectile, _position, _rotation);
        _projectile.tag = "EnemyProjectile";
        _projectile.GetComponent<LaserBehaviour>().SetOwner(gameObject);

        nbLaser++;
    }

    public void StartCoolDown()
    {
        startCDWeapon = Time.time;
        nbLaser--;
    }
}
