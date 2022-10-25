using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(AudioSource))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Target> targets;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startMenuPanel;

    [SerializeField] private AudioClip goodSound;
    [SerializeField] private AudioClip badSound;
    [SerializeField] private AudioClip gameOverSound;

    private AudioSource audioSource;
    private float spawnRate = 1.0f;
    private int score;
    Coroutine spawnCoroutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        StopCoroutine(spawnCoroutine);
        audioSource.PlayOneShot(gameOverSound);
    }

    public void StartGame(int difficulty)
    {
        score = 0;
        spawnRate /= difficulty;
        spawnCoroutine = StartCoroutine(SpawnObject(spawnRate));
        UpdateScore(0);
        startMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SpawnObject(float spawnDelay)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            int index = Random.Range(0, targets.Count);
            Target newTarget = Instantiate(targets[index]);
            newTarget.SetSpawner(this);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score\n{score}";
        if (scoreToAdd > 0)
        {
            audioSource.PlayOneShot(goodSound);
        }
        else if (scoreToAdd < 0)
        {
            audioSource.PlayOneShot(badSound);
        }
    }
}
