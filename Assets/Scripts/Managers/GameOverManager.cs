using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private Player player;
    private LevelBehaviour levelManager;

    private Text scoreText;
    private Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        levelManager = GameObject.Find("Managers").GetComponent<LevelBehaviour>();
        scoreText = GameObject.Find("DyingScore").GetComponent<Text>();
        levelText = GameObject.Find("DyingLevel").GetComponent<Text>();

        scoreText.text = "Score:" + player.GetScore();
        levelText.text = "Died on level:" + (levelManager.GetCurrentLevel() + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(player.gameObject);
            Destroy(levelManager.gameObject);
            SceneManager.LoadScene("GameScreen", LoadSceneMode.Single);
        }
    }
}
