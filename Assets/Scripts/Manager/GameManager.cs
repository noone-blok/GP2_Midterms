using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject[] spawnLocations;

    [SerializeField] private GameObject[] enemyArray;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        StartCoroutine(spawnEnemy());
    }
    private IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(3);

        int randEnemyIndex = Random.Range(0, 3);
        int randLocationIndex = Random.Range(0, 4);

        GameObject newEnemy = Instantiate(enemyArray[randEnemyIndex], spawnLocations[randLocationIndex].transform.position, transform.rotation);

        StartCoroutine(spawnEnemy());
    }

    public void EndGame() // when the player dies, it calls GM's EndGame function to cease everything
    {
        Debug.Log("End Game Now!");

        gameOverScreen.SetActive(true);

        StopAllCoroutines();
    }

}
