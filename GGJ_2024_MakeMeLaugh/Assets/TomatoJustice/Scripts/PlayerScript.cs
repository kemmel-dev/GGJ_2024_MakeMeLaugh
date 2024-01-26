using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject tomatoPrefab;
    private static int score = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnTomato();
        }
    }

    void SpawnTomato()
    {
        Instantiate(tomatoPrefab, transform.position, Quaternion.identity);
    }

    public static void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score); // Replace with actual UI update
    }
}
