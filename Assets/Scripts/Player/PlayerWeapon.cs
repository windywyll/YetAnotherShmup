using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    #region Properties
    [SerializeField]
    private GameObject[] projectile;

    private Weapons currentWeapon;
    private int lvlWeapon;
    private int lvlWeaponMax;
    private int nbProjectile;
    private int nbProjectileMax;
    private int bulletSpeed;
    private ShootDirection styleShoot;
    private float sizeModificator;
    private float startCDWeapon, cooldownWeapon;

    private Transform player;
    #endregion

    // Use this for initialization
    void Awake () {
        lvlWeaponMax = 3;

        lvlWeapon = 0;
        startCDWeapon = -1;
        cooldownWeapon = -1;
        nbProjectile = 0;
        nbProjectileMax = -1;
        sizeModificator = 0;
        bulletSpeed = 4;
        styleShoot = ShootDirection.MONO;
        currentWeapon = Weapons.NONE;

        changeWeapon(Weapons.BULLET);

        player = transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Changing & Lvling
    public void changeWeapon(Weapons _newWeapon)
    {
        if (currentWeapon == _newWeapon)
        {
            if(lvlWeapon < lvlWeaponMax)
                lvlWeapon++;
            lvlUpWeapon();
        }
        else
        {
            currentWeapon = _newWeapon;
            nbProjectile = 0;

            switch(currentWeapon)
            {
                case Weapons.LAZER:
                    lvlWeapon = 1;
                    startCDWeapon = -1;
                    cooldownWeapon = 1;
                    nbProjectileMax = 1;
                    sizeModificator = 1;
                    styleShoot = ShootDirection.MONO;
                    break;

                case Weapons.BULLET:
                    lvlWeapon = 1;
                    startCDWeapon = -1;
                    cooldownWeapon = 1.3f;
                    nbProjectileMax = -1;
                    sizeModificator = 1;
                    styleShoot = ShootDirection.MONO;
                    break;

                default:
                    currentWeapon = Weapons.NONE;
                    lvlWeapon = 0;
                    startCDWeapon = -1;
                    cooldownWeapon = -1;
                    nbProjectileMax = -1;
                    sizeModificator = 0;
                    styleShoot = ShootDirection.MONO;
                    break;
            }
        }
    }

    public void lvlUpWeapon()
    {
        switch (currentWeapon)
        {
            case Weapons.LAZER:
                lvlUpLaser();
                break;

            case Weapons.BULLET:
                lvlUpBullet();
                break;
        }
    }

    public void lvlUpLaser()
    {
        switch(lvlWeapon)
        {
            case 2:
                sizeModificator = 2.5f;
                break;
            case 3:
                cooldownWeapon = -1;
                break;
        }
    }

    public void lvlUpBullet()
    {
        switch (lvlWeapon)
        {
            case 2:
                styleShoot = ShootDirection.TRIPLE;
                break;
            case 3:
                styleShoot = ShootDirection.OMNI;
                break;
        }
    }

    public void UpgradeBulletSpeed(int speedModifier)
    {
        bulletSpeed += speedModifier;
    }

    public void UpgradeBulletFireRate(float fireRateModifier)
    {
        cooldownWeapon -= fireRateModifier;
    }
    #endregion

    #region Shooting
    public void Shoot()
    {
        if (nbProjectileMax > 0 && nbProjectile >= nbProjectileMax)
            return;

        if (cooldownWeapon > 0 && cooldownWeapon + startCDWeapon > Time.time)
            return;

        int _numberToInstantiate = SelectHowManyProjectilesToShoot();

        for(int i = 1; i <= _numberToInstantiate; i++)
        {
            Vector3 _position = positionOfProjectile(i);
            Quaternion _rotation = rotationOfProjectile(i);

            GameObject _projectile = Instantiate(projectile[(int)currentWeapon], _position, _rotation);


            Vector3 _resize = _projectile.transform.localScale;
            _resize.y *= sizeModificator;
            _projectile.transform.localScale = _resize;

            nbProjectile++;
            _projectile.tag = "PlayerProjectile";

            switch (currentWeapon)
            {
                case Weapons.LAZER:
                    _projectile.transform.SetParent(player);
                    _projectile.GetComponent<LaserBehaviour>().SetOwner(gameObject);
                    break;
                case Weapons.BULLET:
                    _projectile.GetComponent<BulletBehaviour>().SetOwner(gameObject);
                    _projectile.GetComponent<BulletBehaviour>().SetBulletSpeed(bulletSpeed);
                    break;
                default:
                    break;
            }
        }
    }

    private int SelectHowManyProjectilesToShoot()
    {
        switch (styleShoot)
        {
            case ShootDirection.MONO:
                return 1;

            case ShootDirection.TRIPLE:
                return 3;

            case ShootDirection.OMNI:
                return 8;

            default:
                return 0;
        }
    }

    private Vector3 positionOfProjectile(int _indexOfCreation)
    {
        Vector3 _position = Vector3.zero;

        if(_indexOfCreation == 1 || styleShoot == ShootDirection.MONO)
        {
            _position = player.position + (player.up * 0.8f);
        }
        else
        {
            switch (styleShoot)
            {
                case ShootDirection.TRIPLE:
                    _position = TripleShootPosition(_indexOfCreation);
                    break;

                case ShootDirection.OMNI:
                    _position = OmniShootPosition(_indexOfCreation);
                    break;
            }
        }

        return _position;
    }

    private Vector3 TripleShootPosition(int _indexOfCreation)
    {
        Vector3 _position = Vector3.zero;

        switch(_indexOfCreation)
        {
            case 2:
                break;
            case 3:
                break;
        }

        return _position;
    }

    private Vector3 OmniShootPosition(int _indexOfCreation)
    {
        Vector3 _position = Vector3.zero;

        switch (_indexOfCreation)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
        }

        return _position;
    }

    private Quaternion rotationOfProjectile(int _indexOfCreation)
    {
        Quaternion _rotation = new Quaternion();

        if (_indexOfCreation == 1 || styleShoot == ShootDirection.MONO)
        {
            _rotation = player.rotation;
        }
        else
        {
            switch (styleShoot)
            {
                case ShootDirection.TRIPLE:
                    _rotation = TripleShootRotation(_indexOfCreation);
                    break;

                case ShootDirection.OMNI:
                    _rotation = OmniShootRotation(_indexOfCreation);
                    break;
            }
        }

        return _rotation;
    }

    private Quaternion TripleShootRotation(int _indexOfCreation)
    {
        Quaternion _rotation = new Quaternion();

        switch (_indexOfCreation)
        {
            case 2:
                break;
            case 3:
                break;
        }

        return _rotation;
    }

    private Quaternion OmniShootRotation(int _indexOfCreation)
    {
        Quaternion _rotation = new Quaternion();

        switch (_indexOfCreation)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
        }

        return _rotation;
    }
    #endregion

    public void StartCoolDown()
    {
        startCDWeapon = Time.time;
    }

    public void DecreaseNbProjectile()
    {
        nbProjectile--;
    }
}
