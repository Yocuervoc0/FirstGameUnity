using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager sharedInstance;
    
    public GameState currentGameState = GameState.menu;

    private PlayerController playerController;

    public int collectedObjects = 0;


    void Awake(){
        if(sharedInstance == null){
            sharedInstance = this;
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        MenuManger.sharedInstance.ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame){
            StartGame();
        }
    }

    public void StartGame(){
        SetGameState(GameState.inGame);
        MenuManger.sharedInstance.HideMainMenu();
    }

    public void GameOver(){
        SetGameState(GameState.gameOver);
       
    }

    public void BackToMenu(){
        
    }

    private void SetGameState(GameState newGameState){
        if(newGameState == GameState.menu) {
            //TODO
            MenuManger.sharedInstance.ShowMainMenu();
        }
        if(newGameState == GameState.inGame) {
            //TODO
            LevelManager.sharedInstanceLevelManager.RemoveAllLevelBlocks();
            MenuManger.sharedInstance.ShowInGameCanvas();
            playerController.StartGame();
            LevelManager.sharedInstanceLevelManager.GenerateInitialblocks();
        }
        if(newGameState == GameState.gameOver) {
            MenuManger.sharedInstance.ShowGameOverCanvas();
            
        }
        this.currentGameState = newGameState;
    }
    public void CollectObject(Collectable collectable)
    {
        collectedObjects += collectable.value;
    }


}

public enum GameState{
    menu,
    inGame,
    gameOver
}