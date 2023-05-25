using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float startTime, minTime, maxTime;
    [SerializeField] GameObject entity;

    [Header("FXs")]
    [SerializeField] GameObject deathFx;
    [SerializeField] GameObject spawnFx;

    [Header("SFXs")]
    [SerializeField] Sound spawnSfx;
    [SerializeField] Sound[] deathSfx;
    Health hp;

    void Start() {
        float time = Random.Range(minTime,maxTime);

        Invoke("StartSpawn",startTime);

        hp = GetComponent<Health>();
        Instantiate(spawnFx,transform.position,Quaternion.identity);
        AudioManager.Instance.PlaySFX(spawnSfx);
    }

    void StartSpawn() {
        float time = Random.Range(minTime,maxTime);
        StartCoroutine(Spawn(entity,time));
    }

    void Update() {
        if (hp.isDead) {
            AudioManager.Instance.PlaySFX(deathSfx[Random.Range(0,deathSfx.Length)]);
            Instantiate(deathFx,transform.position,Quaternion.identity);
            FindObjectOfType<Score>().score++;
            Destroy(gameObject);
        }
    }
    
    IEnumerator Spawn(GameObject obj, float time) {
        if (!GameObject.FindWithTag("Player")) yield break;
        Instantiate(obj,transform.position,Quaternion.identity);
        time = Random.Range(minTime,maxTime);
        yield return new WaitForSeconds(time);
        StartCoroutine(Spawn(obj,time));
    }
}
