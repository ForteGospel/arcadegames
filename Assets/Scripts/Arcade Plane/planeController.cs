using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class planeController : MonoBehaviour, IDamagable
{
    [SerializeField]
    playerStatsSO playerStats;

    [SerializeField]
    float rotationSpeed = 1;

    [SerializeField]
    GameObject scope, scope2;

    [SerializeField]
    float scopeSpeed = 0.5f;


    [SerializeField]
    Transform[] shoot;

    [SerializeField]
    GameObject missile, honingMissle, bomb;

    [SerializeField]
    Transform honingTransform;

    int numOfShoots = 0;

    [SerializeField]
    ParticleSystem charge, spin;

    [SerializeField]
    float chargeTime;
    float time = 0;

    [SerializeField]
    LayerMask terrain;

    [SerializeField]
    LayerMask whatToHit;

    [SerializeField]
    bool backManuver = false;

    [SerializeField]
    GameObject pivot;

    [SerializeField]
    GameObject cameraObject;
    float manuverTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(backManuver)
        {
            backManuber();
            return;
        }

        RaycastHit hit;
        float verticalMovement = !Physics.Raycast(transform.position, Vector3.down, out hit, 5, terrain) ? Input.GetAxis("Vertical") : Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
        localRotation(verticalMovement);
        localMove(verticalMovement);
        
        if(playerStats.Boost <= playerStats.startingBoost)
            playerStats.Boost += (20 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootMissles();
            numOfShoots++;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            time += Time.deltaTime;

            if (time >= 0.2f && numOfShoots == 1)
            {
                shootMissles();
                numOfShoots++;
            }
            else if (time >= 0.4f && numOfShoots == 2)
            {
                shootMissles();
                numOfShoots++;
            }    
            else if (time >= 0.5f && numOfShoots == 3)
            {
                charge.Play();

                RaycastHit honinHit;
                if (Physics.BoxCast(shoot[2].position, Vector3.one * 5, scope.transform.position - shoot[2].position , out honinHit, shoot[2].rotation, 200f, whatToHit) && honingTransform == null)
                {
                    honinHit.transform.GetComponent<enemyController>().targetted();
                    honingTransform = honinHit.transform;
                }
            }    
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (time >= chargeTime)
                shootHoningMissle();

            charge.Stop();
            time = 0f;
            numOfShoots = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && playerStats.Boost >= 40)
        {
            playerStats.Boost -= 40;
            transform.DOLocalRotate(new Vector3(0, 0, 360 * 3), 0.8f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            spin.Play();
        }

        if (Input.GetKey(KeyCode.Q) && playerStats.Boost >= 60)
        {
            playerStats.Boost -= 60;
            backManuver = true;
            cameraObject.GetComponent<cameraController>().setCameraBackPosition();
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraObject.GetComponent<cameraController>().setCameraBackPosition();
            playerStats.Boost -= (50 * Time.deltaTime);
        }
            
        if (Input.GetKeyDown(KeyCode.LeftControl))
            cameraObject.GetComponent<cameraController>().setCameraFrontPosition();

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftShift))
            cameraObject.GetComponent<cameraController>().resetCameraPosition();
    }

    void backManuber()
    {
        transform.RotateAround(pivot.transform.position, -transform.right, 180 * Time.deltaTime);
        cameraObject.transform.LookAt(transform);
        manuverTimer += Time.deltaTime;

        if (manuverTimer >= 2f)
        {
            manuverTimer = 0;
            backManuver = false;
            cameraObject.GetComponent<cameraController>().resetCameraPosition();
            cameraObject.transform.DOLocalRotate(new Vector3(10, 0, 0), 0.5f);
        }
    }

    void localMove(float verticalMovement)
    {
        float positionY = (transform.localPosition.y + verticalMovement * scopeSpeed * Time.deltaTime);
        float positionX = (transform.localPosition.x + Input.GetAxis("Horizontal") * scopeSpeed * Time.deltaTime);
        transform.localPosition = new Vector3(positionX, positionY, transform.localPosition.z);
        //gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(positionX, positionY, transform.localPosition.z) * scopeSpeed * Time.deltaTime);
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
    }

    void localRotation(float verticalMovement)
    {
        transform.LookAt(scope.transform, transform.up);
        scope.transform.localPosition = Vector3.Lerp(scope.transform.localPosition, new Vector3(transform.localPosition.x + Input.GetAxis("Horizontal") * 50,transform.localPosition.y + verticalMovement * 50, 50), 10 * Time.deltaTime);
        scope2.transform.localPosition = Vector3.Lerp(scope2.transform.localPosition, new Vector3(transform.localPosition.x + Input.GetAxis("Horizontal") * 25, transform.localPosition.y + verticalMovement * 25, 25), 10 * Time.deltaTime);
        float zRotation;
        if (Input.GetAxis("Horizontal") == 0) zRotation = 0;
        else if (Input.GetAxis("Horizontal") > 0) zRotation = -30;
        else zRotation = 30;
        Vector3 zZeroRotation = new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, zRotation);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(zZeroRotation), rotationSpeed * Time.deltaTime);
    }

    void shootMissles()
    {
        Instantiate(missile, shoot[0].position, transform.rotation);
        Instantiate(missile, shoot[1].position, transform.rotation);
    }

    void shootHoningMissle()
    {
        GameObject go = Instantiate(honingMissle, shoot[2].position, shoot[2].rotation);
        go.GetComponent<honingMissleController>().destination = honingTransform;
        honingTransform = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide!");
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().isKinematic = false;
        getHit(20);
    }

    public void getHit(int damage)
    {
        playerStats.HealthPoints -= damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * 5);
        Gizmos.DrawRay(shoot[2].position, (scope.transform.position - shoot[2].position) * 100f);
    }
}
