using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int collected = 0;
    public int targetAmount = 10;
    public TMP_Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
{
    collected += amount;

    if (collected < 0)
        collected = 0;

    Debug.Log("Target sekarang: " + collected);

    UpdateUI();

    if (collected >= targetAmount)
    {
        Debug.Log("MISSION COMPLETE!");
        Time.timeScale = 0f;
    }
}
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
"Target: " +
collected +
"/" +
targetAmount;
        }
        else
        {
            Debug.LogWarning("Score Text belum di assign!");
        }
    }
}