using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteController : MonoBehaviour
{
    [SerializeField]
    float songTempo = 120f;
    float arrowSpeed;

    public KeyCode arrow;

    [SerializeField]
    arrowColor aColor;

    [SerializeField]
    bool isEnabled = false;

    [SerializeField]
    int points;

    [SerializeField]
    GameObject noColorHitGO, normalHitGO, greatHitGO, perfectHitGO;

    [SerializeField]
    GameObject missHitText, noColorHitText, normalHitText, greatHitText, perfectHitText;
    // Start is called before the first frame update
    void Start()
    {
        arrowSpeed = songTempo / 60;


        switch(aColor)
        {
            case arrowColor.Blue:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.52f,0.52f,1);
                break;
            case arrowColor.Green:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case arrowColor.Purple:
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            && isEnabled)
        {
            if (!Input.GetKeyDown(arrow))
                missHit();
            else if (gameController.instance.GameColor != aColor)
                badColorHit();
            else if (Mathf.Abs(transform.position.y) < 0.05f)
                perfectHit();
            else if (Mathf.Abs(transform.position.y) < 0.5f)
                greatHit();
            else
                normalHit();

            Destroy(gameObject);
        }
        transform.position += new Vector3(0, arrowSpeed * Time.deltaTime, 0);

        if (transform.position.y > 0.8f)
        {
            missHit();
            Destroy(gameObject);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
            isEnabled = true;
    }

    void normalHit()
    {
        Instantiate(normalHitGO, transform.position, Quaternion.identity);
        Instantiate(normalHitText);
        gameController.instance.normal++;
        gameController.instance.addToCombo();

        gameController.instance.score += points * gameController.instance.multi;
    }

    void greatHit()
    {
        Instantiate(greatHitGO, transform.position, Quaternion.identity);
        Instantiate(greatHitText);
        if (Random.Range(0, 1f) > 0.8f) sonaVoiceController.instance.playVoices();
        gameController.instance.great++;
        gameController.instance.addToCombo();

        gameController.instance.score += (points * 2) * gameController.instance.multi;
    }

    void perfectHit()
    {
        Instantiate(perfectHitGO, transform.position, Quaternion.identity);
        Instantiate(perfectHitText);
        sonaVoiceController.instance.playPerfect();
        gameController.instance.perfect++;
        gameController.instance.addToCombo();

        gameController.instance.score += (points * 5) * gameController.instance.multi;
    }

    void badColorHit()
    {
        Instantiate(noColorHitGO, transform.position, Quaternion.identity);
        Instantiate(noColorHitText);
        gameController.instance.bad++;
        gameController.instance.combo = 0;
    }
    void missHit()
    {
        Instantiate(missHitText);
        gameController.instance.miss++;
        gameController.instance.combo = 0;
        gameController.instance.multi = 1;
    }
}

public enum arrowColor
{
    Blue,
    Green,
    Purple
}
