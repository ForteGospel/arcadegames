using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scopeController : MonoBehaviour
{
    [SerializeField]
    float scopeSpeed = 5;

    [SerializeField]
    Transform plane;

    //[SerializeField]
    //Vector2 yLimits, xLimits;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scopeY = (transform.localPosition.y + Input.GetAxis("Vertical") * scopeSpeed * Time.deltaTime);
        float scopeX = (transform.localPosition.x + Input.GetAxis("Horizontal") * scopeSpeed * Time.deltaTime);

        ////Debug.Log(scopeX + " " + scopeY);
        if (Input.GetAxis("Vertical") == 0)
        //    scopeY = Mathf.Clamp(scopeY, yLimits.x - 2, yLimits.y - 2);
        //else
            scopeY = Mathf.Lerp(scopeY, plane.localPosition.y, (scopeSpeed / 40) * Time.deltaTime);

        if (Input.GetAxis("Horizontal") == 0)
        //    scopeX = Mathf.Clamp(scopeX, xLimits.x - 2, xLimits.y - 2);
        //else
            scopeX = Mathf.Lerp(scopeX, plane.localPosition.x, (scopeSpeed / 40) * Time.deltaTime);

        transform.localPosition = new Vector3(scopeX, scopeY, transform.localPosition.z);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 50f);

        transform.LookAt(Camera.main.transform);
    }
}
