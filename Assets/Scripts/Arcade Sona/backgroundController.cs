using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundController : MonoBehaviour
{
    [SerializeField]
    float colorChangeSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Color currColor = gameObject.GetComponent<RawImage>().color;
        Color gameColor = new Color(0.52f, 0.52f, 1);
        switch (gameController.instance.GameColor)
        {
            case arrowColor.Green:
                gameColor = Color.green;
                break;
            case arrowColor.Purple:
                gameColor = Color.magenta;
                break;
        }
        Color newcolor = Color.Lerp(currColor, gameColor, colorChangeSpeed * Time.deltaTime);
        gameObject.GetComponent<RawImage>().color = newcolor;
    }
}
