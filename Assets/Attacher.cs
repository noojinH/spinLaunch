using UnityEngine;
using TMPro;

public class Attacher : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] public float delay = 3.0f;
    [SerializeField] public int launch = 0;
    private Rigidbody rocketRb;
    private Vector3 previousPosition;
    private Vector3 velocity;
    private Vector3 storedVelocity;
    [SerializeField]
    private GameObject camera0;
    [SerializeField]
    private GameObject ruler0;
    private float initY;

    void Start()
    {
        if (camera0 == null){
            Debug.Log("rocket: Untargeted cineMachine.");
        }
        if (ruler0 == null){
            Debug.Log("rocket: Untargeted indicator.");
        } else {
            ruler0.SetActive(false);
        }
        
        rocketRb = GetComponent<Rigidbody>();
        rocketRb.drag = 0.1f; // 공기 저항 추가
        previousPosition = transform.position;

        Invoke("StoreVelocity", delay);
        Invoke("Launch", delay);
        Invoke("fp0", delay - 0.45f);
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        velocity = (currentPosition - previousPosition) / Time.deltaTime;
        previousPosition = currentPosition;

        if (target != null){
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        //Debug.Log("V: " + velocity.magnitude);

        if (launch == 1){
            ruler0.GetComponent<TextMeshProUGUI>().text = (transform.position.y - initY).ToString() + "m";
        }

        if (Input.GetKeyDown(KeyCode.L)){
            //StoreVelocity();
            fp0();
            Launch();
            launch = 1;
        }
    }

    void StoreVelocity()
    {
        storedVelocity = velocity * 0.1f; // 속도를 현실적으로 조정
        Debug.Log("stored V: " + storedVelocity);
    }

    public void DetachFromParent()
    {
        target = null;
        rocketRb.velocity = storedVelocity;
        ruler0.SetActive(true);
    }

    public void Launch()
    {
        if (launch != 1){
            launch = 1;
            initY = transform.position.y;
            DetachFromParent();
        }
    }

    void fp0()
    {
        if (launch != 1){
            camera0.GetComponent<cineMachine>().fixFp();
        }
    }
}
