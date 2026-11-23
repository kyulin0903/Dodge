using UnityEngine;
using UnityEngine.UI;

public class CustomizeManager : MonoBehaviour
{
    public Renderer playerRenderer;
    public Camera targetCamera;

    public Color[] skinColors;
    public Color[] themeColors;

    public GameObject customizeButton;
    public GameObject panel;
    public Image skinSwatch;
    public Image themeSwatch;

    static int skinIndex;
    static int themeIndex;

    void Start()
    {
        Apply();
        customizeButton.SetActive(false);
        panel.SetActive(false);
    }

    public void ShowButton()
    {
        customizeButton.SetActive(true);
    }

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void NextSkin()
    {
        skinIndex = (skinIndex + 1) % skinColors.Length;
        Apply();
    }

    public void NextTheme()
    {
        themeIndex = (themeIndex + 1) % themeColors.Length;
        Apply();
    }

    void Apply()
    {
        Color skin = skinColors[skinIndex];
        Color theme = themeColors[themeIndex];

        playerRenderer.material.color = skin;
        targetCamera.backgroundColor = theme;
        skinSwatch.color = skin;
        themeSwatch.color = theme;
    }
}