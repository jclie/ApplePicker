using UnityEngine;
using TMPro;

public class Basket : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    public GameObject catchParticlePrefab;

    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        if (scoreGO != null)
        {
            scoreText =
                scoreGO.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;

        mousePos2D.z =
            -Camera.main.transform.position.z;

        Vector3 mousePos3D =
            Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = transform.position;
        pos.x = mousePos3D.x;

        transform.position = pos;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;

        if (collidedWith.CompareTag("Apple"))
        {
            // Make particles where the apple was caught
            if (catchParticlePrefab != null)
            {
                GameObject particles =
                    Instantiate(
                        catchParticlePrefab,
                        collidedWith.transform.position,
                        Quaternion.identity
                    );

                Destroy(particles, 2f);
            }

            Destroy(collidedWith);

            if (scoreText != null)
            {
                string currentText =
                    scoreText.text.Replace("Score: ", "");

                int score = int.Parse(currentText);

                score += 100;

                scoreText.text =
                    "Score: " + score;

                HighScore.TrySet(score);

            }
        }
    }
}