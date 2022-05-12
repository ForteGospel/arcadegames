using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyerController : MonoBehaviour
{
    [SerializeField]
    float destroyAfter = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (destroyAfter != 0)
            Destroy(gameObject, destroyAfter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
