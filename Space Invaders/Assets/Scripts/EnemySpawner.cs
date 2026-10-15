using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject invaderPrefab;
    [SerializeField] private float invaderInterval = 3.5f;
    
    void Start()
    {
        if (GameBehavior.Instance.State == Utilities.GameState.Play)
        StartCoroutine(spawnEnemy(invaderInterval, invaderPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehavior.Instance.State == Utilities.GameState.Pause)
            StopAllCoroutines();
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-2f, 2), 5, Random.Range(-10f, 10)),
            Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
