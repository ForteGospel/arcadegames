using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonController : MonoBehaviour
{
    [SerializeField]
    KeyCode keyButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyButton))
        {
            gameObject.GetComponent<Button>().onClick.Invoke();
            fadeColor(gameObject.GetComponent<Button>().colors.pressedColor);
        }

        if(Input.GetKeyUp(keyButton))
            fadeColor(gameObject.GetComponent<Button>().colors.normalColor);

    }

    void fadeColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, gameObject.GetComponent<Button>().colors.fadeDuration, true, true);
    }
}
