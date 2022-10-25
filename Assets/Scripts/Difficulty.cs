using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    [SerializeField] private int difficulty;
    private Button button;
    private Spawner spawner;

    private void Start()
    {
        button = GetComponent<Button>();
        spawner = FindObjectOfType<Spawner>();
        button.onClick.AddListener(SetDifficulty);

    }

    private void SetDifficulty()
    {
        spawner.StartGame(difficulty);
    }
}
