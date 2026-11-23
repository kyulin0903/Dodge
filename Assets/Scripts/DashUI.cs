using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    public PlayerController player;
    public Image fillImage;
    public RectTransform icon;

    private bool wasReady = true;
    private float flashTimer;

    void Update()
    {
        float ratio = player.GetCooldownRatio();
        fillImage.fillAmount = 1f - ratio;

        bool ready = ratio <= 0f;
        if (ready && !wasReady) flashTimer = 0.15f;
        wasReady = ready;

        if (flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;
            icon.localScale = Vector3.one * (1f + flashTimer * 2f);
        }
        else
        {
            icon.localScale = Vector3.one;
        }
    }
}