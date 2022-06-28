using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{

    public static gameController instance;

    [SerializeField]
    float delaySong = 2f;

    [SerializeField]
    arrowColor currGameColor;

    [SerializeField]
    TextMeshProUGUI scoreText, comboText, multiText, missText, badText, normalText, greatText, perfectText, maxComboText,finalScoreText;

    public arrowColor GameColor
    {
        get { return currGameColor; }
    }

    public int score, miss, bad, normal, great, perfect,combo, multi;
    int maxCombo;
    public int[] comboSteps = new int[3];

    [SerializeField]
    GameObject statsCanvas;

    bool ultiEnable = false;
    [SerializeField]
    GameObject ultiButton;
    // Start is called before the first frame update
    void Start()
    {
        startSong();
        instance = this;
    }

    // Update is called once per frame

    private void Update()
    {
        scoreText.text = "Score: " + score;

        if (combo > 1)
            comboText.text = "combo " + combo;
        else
            comboText.text = "";

        if (multi > 1)
            multiText.text = "X" + multi;
        else
            multiText.text = "";
    }

    public void startSong()
    {
        gameObject.GetComponent<AudioSource>().PlayDelayed(delaySong);
        Invoke("endSong", gameObject.GetComponent<AudioSource>().clip.length);
    }

    void endSong()
    {
        missText.text = miss.ToString();
        badText.text = bad.ToString();
        normalText.text = normal.ToString();
        greatText.text = great.ToString();
        perfectText.text = perfect.ToString();
        maxComboText.text = maxCombo.ToString();
        finalScoreText.text = score.ToString();
        statsCanvas.SetActive(true);
    }

    public void setColor(int color)
    {
        currGameColor = (arrowColor)color;
    }

    public void addToCombo()
    {
        combo++;

        if (combo == comboSteps[0])
            multi = 2;
        else if (combo == comboSteps[1])
            multi = 4;
        else if (combo == comboSteps[2])
            multi = 6;

        if (combo % (comboSteps[0] + comboSteps[1] + comboSteps[2]) == 0 && !ultiEnable)
            enableUlti();

        if (combo > maxCombo)
            maxCombo = combo;
    }

    void enableUlti()
    {
        ultiButton.GetComponent<Button>().interactable = true;

    }

    public void useUlti()
    {
        ultiButton.GetComponent<Button>().interactable = false;
    }
}
