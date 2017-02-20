using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour {
    public GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehavior gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    private bool canPlaceMonster () {
        int cost = monsterPrefab.GetComponent<MonsterData>().levels[0].cost;
        return monster == null && gameManager.Gold >= cost;
    }

    void OnMouseUp()
    {
        if (canPlaceMonster())
        {
            monster = (GameObject)
                Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            AudioSource audioSoure = gameObject.GetComponent<AudioSource>();
            audioSoure.PlayOneShot(audioSoure.clip);

            //deduct gold
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        } else if (canUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().increaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        }
    }

    private bool canUpgradeMonster()
    {
        if (monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterLevel nextLevel = monsterData.getNextLevel();
            if (nextLevel != null && gameManager.Gold >= nextLevel.cost)
            {
                return true;
            }
        }
        return false;
    }
}
