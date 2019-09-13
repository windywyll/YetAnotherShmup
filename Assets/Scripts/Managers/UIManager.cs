using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner enemyManager;
    [SerializeField]
    private UpgradeManager upgradeManager;

    private Player player;

    private bool isUpgradePopUpVisible;

    //GameplayCanvas
    private GameObject CanvasGame;

    private Text ScoreTextUI;
    private string baseScoreText;
    
    private Text MoneyTextUI;
    private string baseMoneyText;
    
    private Text LivesTextUI;
    private string baseLivesText;
    

    //Upgrade Menu Canvas
    private GameObject CanvasUpgrade;

    private Text fireRateNameText;
    private Text fireRateCostText;
    private Text moveSpeedNameText;
    private Text moveSpeedCostText;
    private Text bulletSpeedNameText;
    private Text bulletSpeedCostText;
    private Text livesOptionNameText;
    private Text livesOptionCostText;

    private Text finishUpgradeText;

    private int currentlySelectedUpgradeMenu;

    private Text playerMoneyText;
    private string basePlayerMoneyText;

    private Color hoveringColor;
    private Color baseColor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        CanvasGame = GameObject.Find("CanvasGame");

        ScoreTextUI = GameObject.Find("ScoreUI").GetComponent<Text>();
        MoneyTextUI = GameObject.Find("MoneyUI").GetComponent<Text>();
        LivesTextUI = GameObject.Find("LifeUI").GetComponent<Text>();

        baseScoreText = "Score:";
        baseMoneyText = "Money:";
        baseLivesText = "Lives x";

        CanvasUpgrade = GameObject.Find("CanvasUpgrade");

        fireRateNameText = GameObject.Find("FireRateName").GetComponent<Text>();
        fireRateCostText = GameObject.Find("FireRateCost").GetComponent<Text>();
        fireRateCostText.text = upgradeManager.GetCurrentFireRateCost().ToString();

        moveSpeedNameText = GameObject.Find("MoveSpeedName").GetComponent<Text>();
        moveSpeedCostText = GameObject.Find("MoveSpeedCost").GetComponent<Text>();
        moveSpeedCostText.text = upgradeManager.GetCurrentMoveSpeedCost().ToString();

        bulletSpeedNameText = GameObject.Find("BulletSpeedName").GetComponent<Text>();
        bulletSpeedCostText = GameObject.Find("BulletSpeedCost").GetComponent<Text>();
        bulletSpeedCostText.text = upgradeManager.GetCurrentBulletSpeedCost().ToString();

        livesOptionNameText = GameObject.Find("LivesName").GetComponent<Text>();
        livesOptionCostText = GameObject.Find("LivesCost").GetComponent<Text>();
        livesOptionCostText.text = upgradeManager.GetCurrentLifeCost().ToString();

        finishUpgradeText = GameObject.Find("FinishOptionText").GetComponent<Text>();

        playerMoneyText = GameObject.Find("PlayerMoneyText").GetComponent<Text>();
        playerMoneyText.text = player.GetMoneyCount().ToString();

        basePlayerMoneyText = "Current Money:";

        currentlySelectedUpgradeMenu = 0;

        hoveringColor = new Color(255, 219, 0);
        baseColor = new Color(255, 255, 255);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetUIListener(this);

        CanvasUpgrade.SetActive(false);

        isUpgradePopUpVisible = false;
    }
    
    public void UpdateScoreUI(int newScore)
    {
        ScoreTextUI.text = baseScoreText + newScore.ToString();
    }

    public void UpdateMoneyUI(int newMoney)
    {
        MoneyTextUI.text = baseMoneyText + newMoney.ToString();
    }

    public void UpdateLivesUI(int newLivesCount)
    {
        LivesTextUI.text = baseLivesText + newLivesCount.ToString();
    }

    public void launchUpgradePopUp()
    {
        CanvasGame.SetActive(false);
        CanvasUpgrade.SetActive(true);

        ResetAllTextColorUpgradeMenu();
        currentlySelectedUpgradeMenu = 0;
        fireRateCostText.color = hoveringColor;
        fireRateNameText.color = hoveringColor;
        playerMoneyText.text = player.GetMoneyCount().ToString();

        isUpgradePopUpVisible = true;
    }

    public void closeUpgradePopUp()
    {
        ResetAllTextColorUpgradeMenu();

        CanvasGame.SetActive(true);
        CanvasUpgrade.SetActive(false);

        enemyManager.StartNextLevel();

        isUpgradePopUpVisible = false;
    }

    public bool IsUpgradePopUpVisible()
    {
        return isUpgradePopUpVisible;
    }

    public void ChangeSelectedOptionUpgradeMenu(int changeSelect)
    {
        ResetAllTextColorUpgradeMenu();

        currentlySelectedUpgradeMenu += changeSelect;

        if(currentlySelectedUpgradeMenu > 4)
        {
            currentlySelectedUpgradeMenu = 0;
        }

        if(currentlySelectedUpgradeMenu < 0)
        {
            currentlySelectedUpgradeMenu = 4;
        }

        switch(currentlySelectedUpgradeMenu)
        {
            case 0:
                fireRateCostText.color = hoveringColor;
                fireRateNameText.color = hoveringColor;
                break;
            case 1:
                moveSpeedCostText.color = hoveringColor;
                moveSpeedNameText.color = hoveringColor;
                break;
            case 2:
                bulletSpeedCostText.color = hoveringColor;
                bulletSpeedNameText.color = hoveringColor;
                break;
            case 3:
                livesOptionCostText.color = hoveringColor;
                livesOptionNameText.color = hoveringColor;
                break;
            case 4:
                finishUpgradeText.color = hoveringColor;
                break;
        }

    }

    public void  ResetAllTextColorUpgradeMenu()
    {
        fireRateCostText.color = baseColor;
        fireRateNameText.color = baseColor;
        moveSpeedCostText.color = baseColor;
        moveSpeedNameText.color = baseColor;
        bulletSpeedCostText.color = baseColor;
        bulletSpeedNameText.color = baseColor;
        livesOptionCostText.color = baseColor;
        livesOptionNameText.color = baseColor;
        finishUpgradeText.color = baseColor;
    }

    public void BuyCurrentlySelectedOptionUpgradeMenu()
    {
        switch (currentlySelectedUpgradeMenu)
        {
            case 0:
                upgradeManager.BuyFireRateUpgrade();
                fireRateCostText.text = ReturnCorrectPrice(upgradeManager.GetCurrentFireRateCost());
                playerMoneyText.text = player.GetMoneyCount().ToString();
                break;
            case 1:
                upgradeManager.BuyMoveSpeedUpgrade();
                moveSpeedCostText.text = ReturnCorrectPrice(upgradeManager.GetCurrentMoveSpeedCost());
                playerMoneyText.text = player.GetMoneyCount().ToString();
                break;
            case 2:
                upgradeManager.BuyBulletSpeedUpgrade();
                bulletSpeedCostText.text = ReturnCorrectPrice(upgradeManager.GetCurrentBulletSpeedCost());
                playerMoneyText.text = player.GetMoneyCount().ToString();
                break;
            case 3:
                upgradeManager.BuyLifeUpgrade();
                livesOptionCostText.text = upgradeManager.GetCurrentLifeCost().ToString();
                playerMoneyText.text = player.GetMoneyCount().ToString();
                break;
            case 4:
                closeUpgradePopUp();
                break;
        }
    }

    public string ReturnCorrectPrice(int price)
    {
        if(price == -1)
        {
            return "MAX";
        }

        return price.ToString();
    }
}
