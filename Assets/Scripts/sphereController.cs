using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereController : MonoBehaviour
{

    [SerializeField]
    GameObject cube;

    [SerializeField]
    float offset;
    float timer = 0f;

    [SerializeField]
    float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        timer += Time.deltaTime;

        if (timer > offset )
        {
            timer = 0;
            instantiateCubes();
        }
    }

    void instantiateCubes()
    {
        int numOfCubes = Random.Range(5, 10);

        for(int i = 0; i < numOfCubes; i++)
        {
            float x = Random.Range(80f, 160);
            float y = Random.Range(-5f, 4);
            float z = 170;
            Vector3 pos = new Vector3(x, y,z);
            GameObject go = Instantiate(cube, pos, Quaternion.identity);
            go.transform.parent = transform;
            //go.transform.LookAt(transform);
        }
    }
}
