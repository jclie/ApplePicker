using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Baskets")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -4f;
    public float basketSpacingY = 1f;

    public List<GameObject> basketList;

    [Header("Game Over")]
    public GameObject gameOverText;

    private bool handlingMiss = false;
    private bool gameIsOver = false;

    void Start()
    {
        // Remove any baskets that somehow already exist
        GameObject[] oldBaskets =
            GameObject.FindGameObjectsWithTag("Basket");

        foreach (GameObject oldBasket in oldBaskets)
        {
            Destroy(oldBasket);
        }

        basketList = new List<GameObject>();

        // Create 3 fresh baskets
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject basket = Instantiate(basketPrefab);

            basket.transform.position = new Vector3(
                0f,
                basketBottomY + (basketSpacingY * i),
                0f
            );

            basketList.Add(basket);
        }

        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
    }

    public void AppleMissed()
    {
        // Don't process more misses while one is already happening
        if (handlingMiss || gameIsOver)
        {
            return;
        }

        handlingMiss = true;

        // Destroy all apples currently falling
        GameObject[] apples =
            GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject apple in apples)
        {
            Destroy(apple);
        }

        // Remove one basket
        if (basketList.Count > 0)
        {
            int lastBasket =
                basketList.Count - 1;

            GameObject basket =
                basketList[lastBasket];

            basketList.RemoveAt(lastBasket);

            Destroy(basket);
        }

        // No baskets left = Game Over
        if (basketList.Count == 0)
        {
            GameOver();
        }
        else
        {
            // Allow another miss after a short delay
            Invoke(nameof(AllowAnotherMiss), 0.25f);
        }
    }

    void AllowAnotherMiss()
    {
        handlingMiss = false;
    }

    void GameOver()
    {
        gameIsOver = true;

        // Show Game Over text
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }

        Debug.Log("GAME OVER");

        // Restart after 2 seconds
        Invoke(nameof(RestartGame), 2f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}