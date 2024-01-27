using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public GameObject spawnPoint;
    public void InitiateButtonMashSpawnpoints(ButtonMashGameController buttonMashGameController)
    {
        switch (buttonMashGameController.buttonMashPlayers.Count)
        {
            case 2:
                SpawnSpawnpoints(2);
                break;

            case 3:
                SpawnSpawnpoints(3);
                break;

            case 4:
                SpawnSpawnpoints(4);
                break;

            default:
                Debug.Log("NO PLAYERS IN LIST");
                break;
        }
    }

    public void SpawnSpawnpoints(int amountOfSpawnpoints)
    {
        for (int i = 0; i < amountOfSpawnpoints; i++)
        {
            Instantiate(spawnPoint, transform);

            // TODO add logic for positioning spawnpoints based on amount of players
        }
    }
}
