using UnityEngine;

public class fNrocket : MonoBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private float delay = 3.0f;
    private Rigidbody rocketRb;
    private Vector3 previousPosition;
    private Vector3 velocity;
    private Quaternion previousRotation;
    private Vector3 angularVelocity;

    private Vector3 storedVelocity;
    private Vector3 storedAngularVelocity;
    void Start()
    {
        rocketRb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
        previousRotation = transform.rotation;

        Invoke("StoreVelocity", delay - 0.01f);
        Invoke("DetachFromParent", delay);
    }

    void Update(){
        // 현재 위치
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;
        
        // 이전 위치와 현재 위치의 차이를 계산하여 속도를 구합니다.
        velocity = (currentPosition - previousPosition) / Time.deltaTime;

        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(previousRotation);
        float angle;
        Vector3 axis;
        deltaRotation.ToAngleAxis(out angle, out axis);
        angularVelocity = axis * angle * Mathf.Deg2Rad / Time.deltaTime;

        // 현재 위치를 이전 위치로 업데이트합니다.
        previousPosition = currentPosition;
        previousRotation = currentRotation;

        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        Debug.Log("V: " + velocity+ "aV: "+ rocketRb.angularVelocity);
    }

    void StoreVelocity(){
        // 현재 속도와 각속도를 저장합니다.
        storedVelocity = velocity;
        storedAngularVelocity = rocketRb.angularVelocity;
        //Debug.Log("stored V: " + storedVelocity + "stored w" + storedAngularVelocity);
    }

    void DetachFromParent()
    {
        target = null;

        // 저장된 속도와 각속도를 로켓에 다시 적용합니다.
        rocketRb.velocity = storedVelocity;
        rocketRb.angularVelocity = storedAngularVelocity;
    }
}
