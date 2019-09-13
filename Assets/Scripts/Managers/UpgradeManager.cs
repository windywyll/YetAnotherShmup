using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private Player player;
    private PlayerWeapon playerWeapon;

    private int fireRateStep;
    private float[] fireRateModifier;
    private int[] fireRateCost;

    private int moveSpeedStep;
    private int[] moveSpeedModifier;
    private int[] moveSpeedCost;

    private int bulletSpeedStep;
    private int[] bulletSpeedModifier;
    private int[] bulletSpeedCost;

    private int lifeBuyCount;
    private int lifeBuyStepCost;
    private int lifeBuyBaseCost;
    
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerWeapon = player.gameObject.GetComponent<PlayerWeapon>();

        fireRateStep = 0;
        fireRateModifier = new float[] { 0.3f, 0.2f, 0.2f, 0.2f, 0.2f };
        fireRateCost = new int[] { 2, 5, 9, 15, 21 };

        moveSpeedStep = 0;
        moveSpeedModifier = new int[] { 1, 2, 2 };
        moveSpeedCost = new int[] { 3, 7, 15};

        bulletSpeedStep = 0;
        bulletSpeedModifier = new int[] { 3, 3, 2 };
        bulletSpeedCost = new int[] { 3, 6, 12 };

        lifeBuyCount = 0;
        lifeBuyStepCost = 2;
        lifeBuyBaseCost = 3;
    }
    
    public int GetCurrentFireRateCost()
    {
        if (fireRateStep > fireRateCost.Length)
            return -1;

        return fireRateCost[fireRateStep];
    }

    public float GetCurrentFireRateModifier()
    {
        if (fireRateStep > fireRateModifier.Length)
            return 0;

        return fireRateModifier[fireRateStep];
    }

    public void BuyFireRateUpgrade()
    {
        if (fireRateStep > fireRateModifier.Length)
            return;

        playerWeapon.UpgradeBulletFireRate(fireRateModifier[fireRateStep]);
        player.RemoveXMoney(fireRateCost[fireRateStep]);
        fireRateStep++;
    }

    public int GetCurrentMoveSpeedCost()
    {
        if (moveSpeedStep > moveSpeedCost.Length)
            return -1;

        return moveSpeedCost[moveSpeedStep];
    }

    public int GetCurrentMoveSpeedModifier()
    {
        if (moveSpeedStep > moveSpeedModifier.Length)
            return 0;

        return moveSpeedModifier[moveSpeedStep];
    }

    public void BuyMoveSpeedUpgrade()
    {
        if (moveSpeedStep > moveSpeedModifier.Length)
            return;

        player.UpgradeSpeed(moveSpeedModifier[moveSpeedStep]);
        player.RemoveXMoney(moveSpeedCost[moveSpeedStep]);
        moveSpeedStep++;
    }

    public int GetCurrentBulletSpeedCost()
    {
        if (bulletSpeedStep > bulletSpeedCost.Length)
            return -1;

        return bulletSpeedCost[bulletSpeedStep];
    }

    public int GetCurrentBulletSpeedeModifier()
    {
        if (bulletSpeedStep > bulletSpeedModifier.Length)
            return 0;

        return bulletSpeedModifier[bulletSpeedStep];
    }

    public void BuyBulletSpeedUpgrade()
    {
        if (bulletSpeedStep > bulletSpeedModifier.Length)
            return;

        playerWeapon.UpgradeBulletSpeed(bulletSpeedModifier[bulletSpeedStep]);
        player.RemoveXMoney(bulletSpeedCost[bulletSpeedStep]);
        bulletSpeedStep++;
    }

    public int GetCurrentLifeCost()
    {
        return (lifeBuyBaseCost + ( lifeBuyCount*lifeBuyStepCost ) ) ;
    }

    public void BuyLifeUpgrade()
    {
        if(player.GetLifeCount() >= 3)
        {
            return;
        }

        player.LifeUp();
        player.RemoveXMoney((lifeBuyBaseCost + (lifeBuyCount * lifeBuyStepCost)));
        lifeBuyCount++;
    }
}
