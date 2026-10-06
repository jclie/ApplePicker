using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public float speed = 6f;
    public float leftAndRightEdge = 18f;

    public float minDirectionChangeTime = 0.75f;
    public float maxDirectionChangeTime = 2.25f;

    public GameObject applePrefab;
    public Transform appleSpawnPoint;
    public float secondsBetweenAppleDrops = 1f;

    private float nextDirectionChange;
    private float nextAppleDrop;

    void Start()
    {

        // keep the rest of your existing Start code here
        ScheduleDirectionChange();
        nextAppleDrop = Time.time + 1f;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        pos.x += speed * Time.deltaTime;

        if (pos.x < -leftAndRightEdge)
        {
            pos.x = -leftAndRightEdge;
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)
        {
            pos.x = leftAndRightEdge;
            speed = -Mathf.Abs(speed);
        }

        transform.position = pos;

        if (Time.time >= nextDirectionChange)
        {
            speed *= -1f;
            ScheduleDirectionChange();
        }

        if (Time.time >= nextAppleDrop)
        {
            DropApple();
            nextAppleDrop = Time.time + secondsBetweenAppleDrops;
        }
    }

    void ScheduleDirectionChange()
    {
        nextDirectionChange =
            Time.time +
            Random.Range(
                minDirectionChangeTime,
                maxDirectionChangeTime
            );
    }

    void DropApple()
    {
        Vector3 spawnPosition = transform.position;

        if (appleSpawnPoint != null)
        {
            spawnPosition = appleSpawnPoint.position;
        }

        Instantiate(
            applePrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}