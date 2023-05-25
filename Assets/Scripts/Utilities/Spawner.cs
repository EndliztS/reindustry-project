using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool local = false;
    [SerializeField] float yMin, yMax;
    [SerializeField] float xMin, xMax;
    [SerializeField] float startTime,minTime,maxTime;
    [SerializeField] GameObject entity;

    void Start() {
        Invoke("StartSpawn",startTime);
    }

    void StartSpawn() {
        float time = Random.Range(minTime,maxTime);
        StartCoroutine(Spawn(entity,time));
    }

    IEnumerator Spawn(GameObject obj, float time) {
        if (!GameObject.FindWithTag("Player")) yield break;
        var randomX = Random.Range(xMin,xMax);
        var randomY = Random.Range(yMin,yMax);
        if (local) {
            randomX += transform.position.x;
            randomY += transform.position.y;
        }
        Instantiate(obj,new Vector2(randomX,randomY),Quaternion.identity);
        time = Random.Range(minTime,maxTime);
        yield return new WaitForSeconds(time);
        StartCoroutine(Spawn(obj,time));
    }
}
