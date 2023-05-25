using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    Health hp;
    [SerializeField] GameObject[] entities;
    [SerializeField] GameObject deathFx;
    [SerializeField] GameObject popUp;
    [SerializeField] Sound weaponSfx;

    void Start() {
        hp = GetComponent<Health>();
    }

    void Update() {
        if (hp.isDead) {
            GameObject obj = Instantiate(entities[Random.Range(0,entities.Length)],transform.position,Quaternion.identity);
            if (obj.CompareTag("Gun") && GameObject.FindWithTag("Player")) {
                Transform holder = GameObject.Find("Anchor/GunHolder").transform;
                if (holder.childCount>0) {
                    for (int i = 0; i < holder.childCount; i++)
                    {
                        Destroy(holder.GetChild(i).gameObject);
                    }
                }
                obj.transform.parent = holder;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;

                AudioManager.Instance.PlaySFX(weaponSfx);

                GameObject popupPrefab = Instantiate(popUp,transform.position,Quaternion.identity);
                popupPrefab.GetComponentInChildren<TMP_Text>().text = "+ " + obj.GetComponent<Gun>().data.name;
            }
            Instantiate(deathFx,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
