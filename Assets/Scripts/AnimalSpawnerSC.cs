using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnerSC : MonoBehaviour
{
    [SerializeField] List<GameObject> animalPreyToSpawn = new List<GameObject>();
    [SerializeField] List<GameObject> animalPredatorToSpawn = new List<GameObject>();
    [SerializeField] NoahSC player;
    public int curPreyOnScreen, curPredatorOnScreen;
    public int preyOnScreenCap, pretadorOnScreenCap;
    void Start()
    {
        preyOnScreenCap = 30;
        pretadorOnScreenCap = 10;
        curPreyOnScreen = 0;
        curPredatorOnScreen = 0;
        Invoke(nameof(WaitToAssign), 1f);
        InvokeRepeating(nameof(OnHandleInitAnimal), 1f, 10f);
    }

    public void OnHandleInitAnimal()
    {
        int randAnimalInitChance;
        randAnimalInitChance = Random.Range(1, 100);
        if (randAnimalInitChance < 70)
        {
            //Spawn prey
            int randAnimalToSpawn;
            randAnimalToSpawn = Random.Range(0, animalPreyToSpawn.Count);
            Vector3 randPos;
            randPos.x = Random.Range(player.transform.position.x - 2, player.transform.position.x + 2);
            randPos.y = Random.Range(player.transform.position.y - 2, player.transform.position.y + 2);
            if (curPreyOnScreen <= preyOnScreenCap)
            {
                curPreyOnScreen++;
                Instantiate(animalPreyToSpawn[randAnimalToSpawn], new Vector3(randPos.x, randPos.y, 0), Quaternion.identity);
            }
        }
        else if (randAnimalInitChance >= 70)
        {
            //Spawn Predator
            int randAnimalToSpawn;
            randAnimalToSpawn = Random.Range(0, animalPredatorToSpawn.Count);
            Vector3 randPos;
            randPos.x = Random.Range(player.transform.position.x - 2, player.transform.position.x + 2);
            randPos.y = Random.Range(player.transform.position.y - 2, player.transform.position.y + 2);
            if (curPreyOnScreen <= preyOnScreenCap)
            {
                curPreyOnScreen++;
                Instantiate(animalPredatorToSpawn[randAnimalToSpawn], new Vector3(randPos.x, randPos.y, 0), Quaternion.identity);
            }
        }
    }
    private void WaitToAssign()
    {
        player = GameObject.Find("OBJ_Noah(Clone)").GetComponent<NoahSC>();
    }
}
