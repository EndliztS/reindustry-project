using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    SpriteRenderer[] spriteRenderer;
    Material[] mats;

    Material mat;
    [ColorUsage(true, true)]
    [SerializeField] Color flashColor = Color.white;
    [SerializeField] float flashTime;
    [SerializeField] AnimationCurve flashCurve;
    Health hp;

    void Start() {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        
        mats = new Material[spriteRenderer.Length];
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = spriteRenderer[i].material;
        }

        hp = GetComponent<Health>();
    }

    void Update() {
        if (hp.isDamaged) {
            StartCoroutine(Flash());
            hp.isDamaged = false;
        }
    }

    private IEnumerator Flash() {
        SetFlashColor();

        float curFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime<flashTime) {
            elapsedTime += Time.deltaTime;

            curFlashAmount = Mathf.Lerp(1f, flashCurve.Evaluate(elapsedTime), elapsedTime/flashTime);
            SetFlashAmount(curFlashAmount);

            yield return null;
        }

    }

    void SetFlashColor() {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_FlashColor",flashColor);
        }
    }

    void SetFlashAmount(float amount) {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_FlashAmount",amount);
        }
    }
}
