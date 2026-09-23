using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Inimigo")]
    public GameObject enemyPrefab;

    [Header("Local de Spawn")]
    public Transform spawnPoint;

    [Header("Waypoints do Caminho")]
    public Transform waypointsParent;

    [Header("Configuração das Ondas")]
    public int enemiesPerWave = 5;
    public float timeBetweenEnemies = 2f;
    public float timeBetweenWaves = 5f;

    [Header("Progressão")]
    public int extraEnemiesPerWave = 2;

    [Header("Limite da Fase")]
    public int maxWaves = 10;

    private int currentWave = 0;

    private bool phaseComplete = false;

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public int GetMaxWaves()
    {
        return maxWaves;
    }

    public bool IsPhaseComplete()
    {
        return phaseComplete;
    }

    void Start()
    {
        // As ondas não começam automaticamente.
        // O TutorialStartUI vai iniciar quando o jogador clicar em COMEÇAR.
    }

    public void StartWaves()
    {
        StartCoroutine(WaveSystem());
    }

    IEnumerator WaveSystem()
    {
        while (currentWave < maxWaves)
        {
            currentWave++;

            Debug.Log(
                "===== ONDA " + currentWave +
                "/" + maxWaves + " ====="
            );

            yield return StartCoroutine(SpawnWave());

            Debug.Log(
                "Onda " + currentWave +
                " terminou de spawnar!"
            );

            if (currentWave >= maxWaves)
            {
                Debug.Log(
                    "===== TODAS AS 10 WAVES FORAM SPAWNADAS! ====="
                );

                Debug.Log(
                    "Aguardando todos os inimigos serem derrotados..."
                );

                yield return StartCoroutine(
                    WaitForAllEnemiesDefeated()
                );

                phaseComplete = true;

                Debug.Log(
                    "================================="
                );

                Debug.Log(
                    "🏆 FASE TUTORIAL CONCLUÍDA!"
                );

                Debug.Log(
                    "Todos os inimigos foram derrotados!"
                );

                Debug.Log(
                    "================================="
                );

                yield break;
            }

            yield return new WaitForSeconds(timeBetweenWaves);

            enemiesPerWave += extraEnemiesPerWave;
        }
    }

    IEnumerator SpawnWave()
    {
        int enemiesSpawned = 0;

        while (enemiesSpawned < enemiesPerWave)
        {
            SpawnEnemy();

            enemiesSpawned++;

            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    IEnumerator WaitForAllEnemiesDefeated()
    {
        while (true)
        {
            EnemyHealth[] enemies =
                FindObjectsByType<EnemyHealth>(
                    FindObjectsSortMode.None
                );

            if (enemies.Length == 0)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        EnemyMovement enemyMovement =
            enemy.GetComponent<EnemyMovement>();

        if (enemyMovement == null)
        {
            Debug.LogError(
                "O prefab do inimigo não possui EnemyMovement!"
            );

            return;
        }

        Transform[] waypoints =
            new Transform[waypointsParent.childCount];

        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }

        enemyMovement.waypoints = waypoints;

        Debug.Log(
            "Inimigo criado: " + enemy.name
        );
    }
}