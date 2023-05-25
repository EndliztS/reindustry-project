using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gameover : MonoBehaviour
{
    Animator anims;
    [SerializeField] GameObject deathPP;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text quoteText;
    [SerializeField] string[] quotes;
    bool canRestart = false, start=false;

    void Start() {
        anims = GetComponent<Animator>();
        anims.enabled = false;
        start = false;
    }

    void Update()
    {
        if (!GameObject.FindWithTag("Player")) {
            if (!start) {
                StartCoroutine(DeathPPEffect());
                Invoke("StartAnims",3f);
                start = true;
                AudioManager.Instance.musicSource.pitch = .85f;
            }
        }
        if (canRestart) {          
            if (Input.GetKeyDown(KeyCode.R)){
                AudioManager.Instance.musicSource.pitch = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                AudioManager.Instance.musicSource.pitch = 1;
                SceneManager.LoadScene(0);
            }
        }
    }

    void StartAnims() {
        anims.enabled = true;
        canRestart = true;
        int highScore = FindObjectOfType<Score>().score;
        if (highScore>PlayerPrefs.GetInt("highscore")) PlayerPrefs.SetInt("highscore",highScore);
        scoreText.text = "highscore: " + PlayerPrefs.GetInt("highscore").ToString();
        quoteText.text = quotes[Random.Range(0,quotes.Length)];
    }

    IEnumerator DeathPPEffect() {
        deathPP.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        deathPP.SetActive(false);
    }
}
