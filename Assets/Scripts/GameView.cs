using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
   public Text scoreText, maxScoreText, cointNumberText;
    PlayerController player;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.Find("Player")
                .GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int coin = GameManager.sharedInstance.collectedObjects;
            float score = player.GertravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0);

            scoreText.text = "Score  " + score.ToString("f1");
            maxScoreText.text = "MaxScore  " + maxScore.ToString("f1");
            cointNumberText.text = coin.ToString();
        }
    }


}
