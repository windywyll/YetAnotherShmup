using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelStruct
{
    public float[] timeInstruction;
    public string[] instructions;
}

public class LevelBehaviour : MonoBehaviour
{
    private List<LevelStruct> levelStructs;
    private int currentLevel;

    private KeyValuePair<float, string>[] levelList;
    private int currentInstruction;

    private float beginTimeBeforeNextIntruction;
    // Start is called before the first frame update
    void Awake()
    {
        levelStructs = new List<LevelStruct>();

        currentInstruction = 0;
        currentLevel = 0;

        CreateLevelStructs();
        CreateLevelList();

        beginTimeBeforeNextIntruction = Time.time;

        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PassToNextLevel()
    {
        currentLevel++;
        currentInstruction = 0;
        CreateLevelList();
        beginTimeBeforeNextIntruction = Time.time;
    }

    public float GetNextTimeInstruction()
    {
        if (currentInstruction >= levelList.Length)
        {
            return -1;
        }

        return levelList[currentInstruction].Key;
    }

    public string GetNextInstruction()
    {
        if(currentInstruction >= levelList.Length)
        {
            return "EndLevel";
        }

        if(Time.time >= levelList[currentInstruction].Key + beginTimeBeforeNextIntruction)
        {
            currentInstruction++;
            return levelList[currentInstruction-1].Value;
        }

        return "None";
    }

    private void CreateLevelList()
    {
        if(currentLevel >= levelStructs.Count)
        {
            GenerateEndlessHarderLevels();
            return;
        }

        int newSize = levelStructs[currentLevel].instructions.Length;

        if(newSize > levelStructs[currentLevel].timeInstruction.Length)
        {
            newSize = levelStructs[currentLevel].timeInstruction.Length;
        }

        levelList = new KeyValuePair<float, string>[newSize];

        for(int i = 0; i < newSize; i++)
        {
            levelList[i] = new KeyValuePair<float, string>(levelStructs[currentLevel].timeInstruction[i], levelStructs[currentLevel].instructions[i]);
        }
    }

    private void CreateLevelStructs()
    {
        LevelStruct lvl1 = new LevelStruct
        {
            timeInstruction = new float[] { 3, 4, 5, 6, 7,
                                            15 },
            instructions = new string[] { "Assassin-Horizontal-0", "Assassin-Horizontal-1", "Assassin-Horizontal-2", "Assassin-Horizontal-1", "Assassin-Horizontal-0",
                                          "End" }
        };
        levelStructs.Add(lvl1);

        LevelStruct lvl2 = new LevelStruct
        {
            timeInstruction = new float[] { 3, 3.5f, 4.2f, 9, 13,
                                            13.2f, 13.4f, 13.5f, 14, 17,
                                            25 },
            instructions = new string[] { "Assassin-Horizontal-0", "Assassin-Horizontal-1", "Assassin-Horizontal-2", "Bullet-Horizontal-1", "Assassin-Horizontal-2",
                                          "Assassin-Horizontal-0", "Assassin-Horizontal-1", "Assassin-Horizontal-2", "Assassin-Horizontal-0", "Bullet-Horizontal-1",
                                          "End" }
        };
        levelStructs.Add(lvl2);

        LevelStruct lvl3 = new LevelStruct
        {
            timeInstruction = new float[] { 3, 3.5f, 3.5f, 7, 10,
                                            12, 15, 15, 18, 18.3f,
                                            18.3f, 18.3f, 19f, 19f, 19.3f,
                                            28 },
            instructions = new string[] { "Assassin-Horizontal-1", "Assassin-Horizontal-2", "Assassin-Horizontal-0", "Laser-Vertical-1", "Assassin-Horizontal-0",
                                          "Assassin-Back-0", "Assassin-Horizontal-0", "Assassin-Back-0", "Assassin-Horizontal-0", "Bullet-Horizontal-1",
                                          "Assassin-Horizontal-0", "Assassin-Horizontal-2", "Assassin-Horizontal-0", "Assassin-Back-0", "Assassin-Back-0",
                                          "End" }
        };
        levelStructs.Add(lvl3);

        LevelStruct lvl4 = new LevelStruct
        {
            timeInstruction = new float[] { 3, 3, 3, 3.5f, 5.5f,
                                            6, 6, 8, 8.3f, 9.5f,
                                            13, 13, 13.5f, 15, 16,
                                            17, 18, 21, 21.5f, 21.5f,
                                            30 },
            instructions = new string[] { "Assassin-Horizontal-0", "Assassin-Horizontal-1", "Assassin-Horizontal-2", "Assassin-Back-0", "Laser-Vertical-0",
                                          "Assassin-Horizontal-0", "Assassin-Horizontal-2", "Assassin-Horizontal-0", "Bullet-Back-0", "Bullet-Horizontal-1",
                                          "Bullet-Horizontal-0", "Bullet-Horizontal-2", "Assassin-Back-0", "Assassin-Horizontal-0", "Assassin-Horizontal-2",
                                          "Assassin-Horizontal-1", "Assassin-Horizontal-1", "Laser-Vertical-1", "Bullet-Horizontal-0", "Bullet-Horizontal-2",
                                          "End" }
        };
        levelStructs.Add(lvl4);
    }

    private void GenerateEndlessHarderLevels()
    {
        bool authorizeStupidFormations = false;

        if (currentLevel > 10)
            authorizeStupidFormations = true;

        float timeBetweenWaves = 5 - (currentLevel/10);

        string enemyInWave = "Bullet-Horizontal-1";

        string[] enemyToChooseFrom = { "Bullet", "Laser", "Assassin", "Random" };

        string[] pathsToChooseFrom = { "-Horizontal-0", "-Horizontal-1", "-Horizontal-2", "-Vertical-0", "-Vertical-1", "-Back-0" };
        
        int randEnemy, randPath;
        randEnemy = 0;
        randPath = 0;

        if( timeBetweenWaves < 0.5f)
        {
            timeBetweenWaves = 0.5f;
        }

        levelList = new KeyValuePair<float, string>[currentLevel * 5];

        for (int i = 0; i < (currentLevel * 5) - 1; i++)
        {
            randEnemy = Random.Range(0, enemyToChooseFrom.Length);

            enemyInWave = enemyToChooseFrom[randEnemy];

            if(randEnemy == 3)
            {
                enemyInWave += "-0-0";
                randPath = -1;
            }
            else if(randEnemy == 1 && !authorizeStupidFormations)
            {
                randPath = Random.Range(3, 5);
            }
            else
            {
                randPath = Random.Range(0, pathsToChooseFrom.Length);
            }

            if(randPath != -1)
            {
                enemyInWave += pathsToChooseFrom[randPath];
            }

            levelList[i] = new KeyValuePair<float, string>((0 + (i * timeBetweenWaves)) , enemyInWave);
        }

        levelList[((currentLevel * 5) - 1)] = new KeyValuePair<float, string>((currentLevel * 5 * timeBetweenWaves) + 5, "End");
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
