using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft = 90f;
    public TMP_Text timerText;

    bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        timeLeft -= Time.deltaTime;

        timerText.text = "Time: " + Mathf.Ceil(timeLeft);

        if (timeLeft <= 0)
        {
            isGameOver = true;
            Debug.Log("KALAH!");
            Time.timeScale = 0f;
        }
    }
}