using UnityEngine;

public class fRocket : MonoBehaviour
{
    [SerializeField] private float delay = 3.0f;
    private HingeJoint hJ;
    [SerializeField]
    private Rigidbody rocketRb;

    private Vector3 storedVelocity;
    private Vector3 storedAngularVelocity;

    void Start()
    {
        // HingeJoint와 Rigidbody 컴포넌트를 가져옵니다.
        hJ = GetComponent<HingeJoint>();

        Invoke("StoreVelocity", delay - 0.01f);
        Invoke("DetachFromParent", delay);
    }

    void Update(){
        Debug.Log("V: " + rocketRb.velocity + "aV: "+ rocketRb.angularVelocity);
    }

    void StoreVelocity(){
        // 현재 속도와 각속도를 저장합니다.
        storedVelocity = rocketRb.velocity;
        storedAngularVelocity = rocketRb.angularVelocity;
        Debug.Log("stored V: " + storedVelocity);
        Debug.Log("stored aV:" + storedAngularVelocity);
    }

    void DetachFromParent()
    {
        // HingeJoint를 제거하여 로켓을 분리합니다.
        if (hJ != null)
        {
            Destroy(hJ);
        }

        // 로켓의 부모를 null로 설정하여 독립시킵니다.
        transform.parent = null;

        // 저장된 속도와 각속도를 로켓에 다시 적용합니다.
        rocketRb.velocity = storedVelocity;
        rocketRb.angularVelocity = storedAngularVelocity;
    }
}
