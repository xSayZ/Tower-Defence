using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    #region Singleton
    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("There are more than one instance of GameManager in the scene");
        }

        _instance = this;
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }
    #endregion

    [Header("Round Settings")]
    [SerializeField] private List<RoundScriptableObject> rounds;
    [SerializeField] private Transform spawnPosition;

    private int currentRoundIndex = 0;
    private bool isRoundActive = false;

    #region Enemy Management
    private List<EnemyBehaviour> activeEnemies = new List<EnemyBehaviour>();

    public int TotalEnemies => activeEnemies.Count;

    public void RegisterEnemy(EnemyBehaviour enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(EnemyBehaviour enemy)
    {
        activeEnemies.Remove(enemy);
    }
    #endregion

    #region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {

        if (isRoundActive)
        {
            if (AllEnemiesDefeated())
            {
                EndRound();
            }
        }
    }
    #endregion

    #region Round Management
    // Start Round
    private void StartRound()
    {
        if (currentRoundIndex < rounds.Count)
        {
            isRoundActive = true;
            StartCoroutine(SpawnRoundEnemies(rounds[currentRoundIndex]));
        }
    }

    // End Round
    private void EndRound()
    {
        isRoundActive = false;
        currentRoundIndex++;

        if (currentRoundIndex < rounds.Count)
        {
            StartRound();
        }
    }

    private IEnumerator SpawnRoundEnemies(RoundScriptableObject round)
    {
        for (int i = 0; i < round.enemies.Count; i++)
        {
            GameObject enemy = Instantiate(round.enemies[i], spawnPosition);
            yield return new WaitForSeconds(round.spawnTime);
        }
    }
    #endregion

    #region Helper Methods
    bool AllEnemiesDefeated()
    {
        return TotalEnemies == 0;
    }
    #endregion
}
