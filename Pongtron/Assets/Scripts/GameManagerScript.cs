using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public Text scoreText; 

    public Text instructionText; 
    public RawImage heart1;
    public RawImage heart2;
    public RawImage heart3; 
    public AudioSource soundtrackAudioSource;
    public Sprite soundEnabledSprite;
    public Sprite soundDisabledSprite;
    public AdScripts adScripts;

    public Button startButton;
    public Button soundButton;
    public BallSpawner spawner;
    private int lifes = 3;
    private int score = 0;

    public void StartGame() {
         lifes = 3;
         score = 0;
         scoreText.text = score.ToString();
         startButton.gameObject.SetActive(false);
         soundButton.gameObject.SetActive(false);
         heart3.gameObject.SetActive(true);
         heart2.gameObject.SetActive(true);
         heart1.gameObject.SetActive(true);
         spawner.StartSpawning();
         scoreText.color = Color.green;
    }

    public void Score() {
        score++;
        scoreText.text = score.ToString();
    }

    public void Damage() {
        lifes--;

        switch(lifes) {
            case 0:
                heart3.gameObject.SetActive(false);
                EndGame();
                break;
            case 1:
                heart2.gameObject.SetActive(false);
                break;
            case 2:
                heart1.gameObject.SetActive(false);
                break;
        }

        StartCoroutine(BlinkHeartColors());
    }

    private void EndGame() {
        spawner.StopSpawning();
        startButton.gameObject.SetActive(true);
        soundButton.gameObject.SetActive(true);
        StartCoroutine(BlinkScore());
    
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls) {
            if (ball == null) { continue; }    
            BallScripts ballScript = ball.GetComponent<BallScripts>();
            if (ballScript.Equals(null)) return;
            ballScript.ExplodeWithDelay(false);
        }

        adScripts.ShowInterstitial();
    }    

    public void ToggleMusic() {
        soundtrackAudioSource.mute = !soundtrackAudioSource.mute;
        Image buttonImage = soundButton.GetComponent<Image>();

        if (soundtrackAudioSource.mute) {
            buttonImage.sprite = soundDisabledSprite;
        } else {
            buttonImage.sprite = soundEnabledSprite;
        }
    }

    private IEnumerator BlinkScore() {
        scoreText.color = Color.red;

        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(0.5f); 
            scoreText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f); 
            scoreText.gameObject.SetActive(true);
        }
    }

    private IEnumerator BlinkHeartColors() {
        heart1.color = Color.red;
        heart2.color = Color.red;
        heart3.color = Color.red;
        yield return new WaitForSeconds(.8f); 
        heart1.color = Color.green;
        heart2.color = Color.green;
        heart3.color = Color.green;
    }

    public void FadeOutInstruction() {
        StartCoroutine(FadeOutInstructionEnumerator());
    }

    private IEnumerator FadeOutInstructionEnumerator() { 
        Color originalColor = instructionText.color;
        float fadeOutTime = 0.25f;
        for (float t = 0.001f; t < fadeOutTime; t += Time.deltaTime) {
            instructionText.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t/fadeOutTime));
            yield return null;
        }

        instructionText.color = Color.clear;
    }
}
