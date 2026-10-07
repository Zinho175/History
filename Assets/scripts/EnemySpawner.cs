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

    // Indica que a partida terminou por derrota
    private bool spawningStopped = false;

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
        StartWaves();
    }

    public void StartWaves()
    {
        StartCoroutine(WaveSystem());
    }

    IEnumerator WaveSystem()
    {
        while (currentWave < maxWaves)
        {
            // Se a base foi destruída, para tudo
            if (spawningStopped)
                yield break;

            currentWave++;

            Debug.Log(
                "===== ONDA " + currentWave +
                "/" + maxWaves + " ====="
            );

            yield return StartCoroutine(SpawnWave());

            // Se a base morreu durante a onda
            if (spawningStopped)
                yield break;

            Debug.Log(
                "Onda " + currentWave + " terminou de spawnar!"
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

                // Se a base morreu enquanto aguardávamos
                if (spawningStopped)
                    yield break;

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

            if (spawningStopped)
                yield break;

            enemiesPerWave += extraEnemiesPerWave;
        }
    }

    IEnumerator SpawnWave()
    {
        int enemiesSpawned = 0;

        while (enemiesSpawned < enemiesPerWave)
        {
            // Para de spawnar imediatamente se houver derrota
            if (spawningStopped)
                yield break;

            SpawnEnemy();

            enemiesSpawned++;

            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    IEnumerator WaitForAllEnemiesDefeated()
    {
        while (true)
        {
            // Se houve derrota, abandona a espera
            if (spawningStopped)
                yield break;

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
        // Segurança extra
        if (spawningStopped)
            return;

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

    // =====================================================
    // DERROTA
    // =====================================================

    public void StopSpawningAndKillEnemies()
    {
        // Evita executar duas vezes
        if (spawningStopped)
            return;

        spawningStopped = true;

        Debug.Log("🛑 SPAWN DE INIMIGOS INTERROMPIDO!");

        // Para todas as coroutines deste EnemySpawner
        StopAllCoroutines();

        // Encontra todos os inimigos vivos
        EnemyHealth[] enemies =
            FindObjectsByType<EnemyHealth>(
                FindObjectsSortMode.None
            );

        Debug.Log(
            "💀 Eliminando " + enemies.Length +
            " inimigos restantes..."
        );

        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }

        Debug.Log("💀 Todos os inimigos foram eliminados!");
    }
}