using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    public GameObject hazardPrefab;
    public float interval = 3f;
    public float scaleStep = 0.3f;
    public float floorY = 0.01f;

    private Transform target;
    private float timer;
    public Transform mapRoot;

    void Start()
    {
        target = FindFirstObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        GameManager gm = GameManager.Instance;
        int level = gm.GetLevel();

        if (gm.IsGameover() || level < 1) return;
        if (level == 2) interval = 2f;
        if (level > 4) interval = 1f;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;

            Vector3 pos = new Vector3(target.position.x, floorY, target.position.z);
            GameObject hazard = Instantiate(hazardPrefab, pos, Quaternion.identity, mapRoot);

            float scale = 1f + (level - 1) * scaleStep;
            hazard.transform.localScale = Vector3.one * scale;
        }
    }
}