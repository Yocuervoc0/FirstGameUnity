using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstanceLevelManager;

    public List<LevelBlock> allLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    public Transform levelStartposition;
    private void Awake()
    {
        if(sharedInstanceLevelManager == null)
        {
            sharedInstanceLevelManager = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialblocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Addlevelblock()
    {
        int randomIdX = Random.Range(1, allLevelBlocks.Count);
        LevelBlock block;

        Vector3 spawnPosition = Vector3.zero;

        if(currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allLevelBlocks[0]);
            spawnPosition = levelStartposition.localPosition;
        }
        else
        {
            block = Instantiate(allLevelBlocks[randomIdX]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
        }

        block.transform.SetParent(this.transform, false);

        Vector3 correction = new Vector3(
            spawnPosition.x - block.startPoint.localPosition.x,
            spawnPosition.y - block.startPoint.localPosition.y,
            0);

        block.transform.position = correction;
        currentLevelBlocks.Add(block);
    }

    public void RemoveLevelBlock()
    {
        LevelBlock oldLevelBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldLevelBlock);
        Destroy(oldLevelBlock.gameObject);
    }

    public void RemoveAllLevelBlocks()
    {
        while(currentLevelBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }
    public void GenerateInitialblocks()
    {
        for(int i=0; i<=2; i++)
        {
            Addlevelblock();
        }
    }
}
