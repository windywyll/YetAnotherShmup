using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Squads{
    public Spawn spawnName;
    public GameObject prefabSquad;
}

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private UIManager uIManager;

    [SerializeField]
    private Squads[] squadList;

    [SerializeField]
    private GameObject[] pathsList;

    [SerializeField]
    private LevelBehaviour levelList;
    
    private Player player;

    private static Money[] moneyList;

    private float beginWaitNextInstruction;
    private float nextTimeInstruction;
    private string newInstruction;
    
	void Awake () {
        nextTimeInstruction = 0;
        newInstruction = "";

        GameObject[] moneyGameobject = GameObject.FindGameObjectsWithTag("Money");
        moneyList = new Money[moneyGameobject.Length];
        int i = 0;

        foreach (GameObject g in moneyGameobject)
        {
            moneyList[i] = g.GetComponent<Money>();
            i++;
        }
    }

    private void Start()
    {
        beginWaitNextInstruction = Time.time;
        nextTimeInstruction = levelList.GetNextTimeInstruction();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
        if(uIManager.IsUpgradePopUpVisible())
        {
            return;
        }

        if(nextTimeInstruction != -1 && Time.time >= beginWaitNextInstruction + nextTimeInstruction)
        {
            newInstruction = levelList.GetNextInstruction();
            nextTimeInstruction = levelList.GetNextTimeInstruction();
            DecideNextInstruction();
        }
	}

    private void DecideNextInstruction()
    {
        if(newInstruction == "None")
        {
            return;
        }

        if(newInstruction == "End")
        {
            EndLevel();
            return;
        }

        string[] SpawnInstruction = newInstruction.Split('-');

        if(SpawnInstruction[0] == "Random")
        {
            SpawnRandomEnemy();
        }
        else if(SpawnInstruction[0] == "Obstacle")
        {
            //spawnObstacle
        }
        else
        {
            SpawnSpecificEnemy(SpawnInstruction[0], SpawnInstruction[1], SpawnInstruction[2]);
        }
    }

    private void SpawnSpecificEnemy(string enemyType, string enemySpawn, string enemyPath)
    {
        int squadToSpawn = 0;
        int path = 0;
        int pathVariation = int.Parse(enemyPath);

        switch (enemyType)
        {
            case "Assassin":
                squadToSpawn = 2;
                break;
            case "Laser":
                squadToSpawn = 1;
                break;
            case "Omni":
                break;
            case "DeathSquadron":
                break;
            case "Bullet":
            default:
                squadToSpawn = 0;
                break;
        }

        switch(enemySpawn)
        {
            case "Vertical":
                path = 1;
                break;
            case "Back":
                path = 2;
                break;
            case "Horizontal":
            default:
                path = 0;
                break;
        }

        InstantiateEnemyPrefab(squadToSpawn, path, pathVariation);
    }

    private void SpawnRandomEnemy()
    {
        int path = Random.Range(0, pathsList.Length);
        int pathVariation = Random.Range(0, pathsList[path].transform.childCount);

        Spawn squadDefaultAtPath = pathsList[path].GetComponent<AuthorizedSquad>().GetAuthorizedSpawn();

        int squadToSpawn = 0;

        for (int i = 0; i < squadList.Length; i++)
        {
            if (squadList[i].spawnName == squadDefaultAtPath)
            {
                squadToSpawn = i;
                break;
            }
        }
        
        InstantiateEnemyPrefab(squadToSpawn, path, pathVariation);
    }

    private void InstantiateEnemyPrefab(int squadToInstantiate, int pathToInstantiate, int pathVariation)
    {
        int _beginPoint = 0;
        Vector3 _positionOfSquad = pathsList[pathToInstantiate].transform.GetChild(pathVariation).GetChild(_beginPoint).position;

        int _endPoint = (_beginPoint + 1);
        Vector3 _destination = pathsList[pathToInstantiate].transform.GetChild(pathVariation).GetChild(_endPoint).position;

        Quaternion _rotationOfSquad = pathsList[pathToInstantiate].transform.GetChild(pathVariation).GetChild(_beginPoint).rotation;

        GameObject _squadToInstantiate = squadList[squadToInstantiate].prefabSquad;

        GameObject _newSquad = Instantiate(_squadToInstantiate, _positionOfSquad, _rotationOfSquad);

        _newSquad.GetComponent<AIMovementSquad>().SetDestination(_destination);
        _newSquad.GetComponent<AIMovementSquad>().SetWaitingAtDestination(false);
    }

    public void EndLevel()
    {
        player.StopShooting();
        player.ResetPosition();
        uIManager.launchUpgradePopUp();
        nextTimeInstruction = -1;
    }

    public void StartNextLevel()
    {
        player.StartShooting();
        levelList.PassToNextLevel();
        beginWaitNextInstruction = Time.time;
        nextTimeInstruction = levelList.GetNextTimeInstruction();
    }

    public static Money GetNextUnusedMoney()
    {
        foreach(Money m in moneyList)
        {
            if(!m.IsBeingUsed())
            {
                return m;
            }
        }

        return null;
    }
}
