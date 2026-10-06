using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -18f;

    private bool reportedMiss = false;

    void Update()
    {

        if (!reportedMiss &&
            transform.position.y < bottomY)
        {
            reportedMiss = true;

            ApplePicker picker =
                Camera.main.GetComponent<ApplePicker>();

            if (picker != null)
            {
                picker.AppleMissed();
            }
        }
    }
}