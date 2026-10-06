using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public static int score = 0;

    private static HighScore instance;
    private TextMeshProUGUI highScoreText;

    void Awake()
    {
        instance = this;

        highScoreText = GetComponent<TextMeshProUGUI>();

        score = PlayerPrefs.GetInt(
            "ApplePickerHighScore",
            0
        );

        UpdateText();
    }

    public static void TrySet(int newScore)
    {
        if (newScore > score)
        {
            score = newScore;

            PlayerPrefs.SetInt(
                "ApplePickerHighScore",
                score
            );

            PlayerPrefs.Save();

            if (instance != null)
            {
                instance.UpdateText();
            }
        }
    }

    void UpdateText()
    {
        highScoreText.text =
            "High Score: " + score;
    }
}