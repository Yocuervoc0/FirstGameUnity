using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManger : MonoBehaviour
{

    public Canvas menuCanvas;
    public Canvas gameOverCanvas;
    public Canvas inGameCanvas;

    public static MenuManger sharedInstance;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
        HideGameOverCanvas();
        HideInGameCanvas();
    }
    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }

    public void ShowGameOverCanvas()
    {
        gameOverCanvas.enabled = true;
        HideInGameCanvas();
        HideMainMenu();
    }
    public void HideGameOverCanvas()
    {
        gameOverCanvas.enabled = false;
    }

    public void ShowInGameCanvas()
    {
        inGameCanvas.enabled = true;
        HideGameOverCanvas();
        HideMainMenu();
    }
    public void HideInGameCanvas()
    {
        inGameCanvas.enabled = false;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Appliation.Quit();
#endif
    }
}
