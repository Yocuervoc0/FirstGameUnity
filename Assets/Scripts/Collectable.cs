using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public CollectableType type = CollectableType.coin;
    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;
    bool hasbeeCollected = false;
    public int value = 1;
    GameObject player;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
        

    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            if (!hasbeeCollected)
            {
                Collect();
            }
        }

    }

    void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasbeeCollected = false;
    }

    void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;
        hasbeeCollected = true;
    }

    void Collect()
    {
        Hide();
        switch (this.type)
        {
            case CollectableType.coin:
                GameManager.sharedInstance.CollectObject(this);
                GetComponent<AudioSource>().Play();
                break;
            case CollectableType.healthPotion:
                player.GetComponent<PlayerController>().CollectHealth(this.value);
                break;
            case CollectableType.manaPotion:
                player.GetComponent<PlayerController>().CollectMana(this.value);
                break;
        }
    }
}

public enum CollectableType{
    healthPotion,
    manaPotion,
    coin
}

