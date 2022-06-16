using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    
    private Rigidbody2D rigidBody;
    public LayerMask groundMask;
    Animator animator;
    private const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    public int healthPoints { get; set;  }
    public int manaPoints { get; set; }

    public const int INITIAL_HEALTH = 100;
    public const int INITIAL_MANA= 15;

    public const int MAX_HEALTH = 200;
    public const int MAX_MANA = 30;

    public const int MIN_HEALTH = 10;
    public const int MIN_MANA = 0;

    public float jumpForce = 8f;
    public float runningSpeed = 2f;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.2f;

    Vector3 startPosition;

    public PlayerController SharedPlayerController;

    void Awake()
    {
        // if(SharedPlayerController == null){
        //     SharedPlayerController = this;
        // }
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = this.transform.position;
        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

    }
    void Start()
    {

    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
        Invoke("ReStartPosition", 0.3f);
        
    }

    void ReStartPosition()
    {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Jump(true);
        }
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down * 1.3f, Color.black);
    }

    void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
            if(rigidBody.velocity.x < runningSpeed){
            rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            }
        }else{
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        
    }    

    void Jump(bool superJump)
    {
        float jumpForceFactor = jumpForce;
        if (superJump && manaPoints >= SUPERJUMP_COST)
        {
            jumpForceFactor *= SUPERJUMP_FORCE;
            manaPoints -= SUPERJUMP_COST;
        }
        if (IsTouchingTheGround()){
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            if(jumpForceFactor > jumpForce)
                rigidBody.AddForce(Vector2.right * 4f, ForceMode2D.Impulse);
            GetComponent<AudioSource>().Play();
        }
            
    }

    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 2f, groundMask)){
            //GameManager.sharedInstance.currentGameState = GameState.inGame;
            return true;
        }
            
        return false;
        
    }

    public void Die()
    {
        float travelledDistance = GertravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0);
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
        this.animator.SetBool(STATE_ALIVE, false);
        //instead of this  is posiblee call the function Gameover()
        //GameManager.sharedInstance.currentGameState = GameState.gameOver; 
        GameManager.sharedInstance.GameOver();
        
    }

    public void CollectHealth(int health)
    {
        this.healthPoints += health;
        if(this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
        if (healthPoints <= 0)
            Die();
    }

    public void CollectMana(int mana)
    {
        this.manaPoints += mana;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public float GertravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }

}