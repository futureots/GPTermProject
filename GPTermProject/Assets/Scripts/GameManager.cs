using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class GameManager : Singleton<GameManager>
{
    public int point;
    public float time;
    public Cake cake;

    //적 오브젝트 풀
    public List<ObjectPool<Enemy>> enemyPool;
    public List<Enemy> activeEnemies;
    public Transform pool;

    

    [ContextMenuItem("SetSpawnPoint","SetSpawnPointList")]
    public List<GameObject> SpawnPoint;
    public List<Enemy> enemyPrefabs;
    public Enemy enemyPrefab;

    public Transform RespawnPoint;

    public UnityEvent OnGameOver;
    private void Start()
    {
        activeEnemies = new List<Enemy>();
        point = 0;
        time = 0;
        enemyPool = new List<ObjectPool<Enemy>>();
        for(int i = 0; i < 2; i++)
        {
            int t = i;
            var pool = new ObjectPool<Enemy>(
            createFunc: () => SpawnEnemy(t),
            actionOnGet: x => activeEnemies.Add(x),
            actionOnRelease: x => activeEnemies.Remove(x),
            actionOnDestroy: DestroyEnemy,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 100
            );
            enemyPool.Add(pool);
        }
        
        StartCoroutine(SpawnEnemyCoroutine());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        time += Time.deltaTime;
    }
    // 적 생성
    Enemy SpawnEnemy(int i)
    {

        var instance = Instantiate(enemyPrefabs[i]) as Enemy;
        
        instance.transform.SetParent(pool);
        instance.targetTr = cake.transform;
        return instance;
    }
    void DestroyEnemy(Enemy _enemy)
    {
        Destroy(_enemy.gameObject);
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        var spawnPool = new List<GameObject>();
        spawnPool.AddRange(SpawnPoint);
        float delay = 1f;
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(delay);
            count++;
            if(count > 30)
            {
                count = 0;
                delay *= 0.9f;
            }
            int enemyRand = UnityEngine.Random.Range(0, (int)time / 60+1);
            enemyRand = Mathf.Min(1, enemyRand);
            Enemy enemy = enemyPool[enemyRand].Get();

            
            if (spawnPool.Count < 5)
            {
                spawnPool.AddRange(SpawnPoint);
            }
            var rand = UnityEngine.Random.Range(0, spawnPool.Count);

            var point = spawnPool[rand];
            spawnPool.RemoveAt(rand);
           // Debug.Log("Spawn : " + SpawnPoint[rand].transform.position);
            enemy.agent.Warp(point.transform.position);
            enemy.Init();
            //Debug.Log("Enemy : " + enemy.transform.position);
        }
    }

    
    public void SetSpawnPointList()
    {
        SpawnPoint = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
        {
            SpawnPoint.Add(transform.GetChild(i).gameObject);
        }
    }
    public void GameOver()
    {
        Debug.Log("GameOver");

        StopAllCoroutines();
        //모든 적들 승리 애니메이션 트리거
        foreach (var item in activeEnemies)
        {
            item.anim.SetTrigger("Win");
            if (item.agent == null || !item.agent.enabled) continue;
            if (item.agent.isOnNavMesh)
            {
                item.agent.isStopped = true;
            }
            else
            {
                // NavMesh에 안 올라가 있을 경우 이동도 못 하니 그냥 Rigidbody 멈추기
                Rigidbody rb = item.agent.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }
                item.agent.enabled = false; // 더 이상 NavMesh 경로 계산 막기
            }
        }

        OnGameOver?.Invoke();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
