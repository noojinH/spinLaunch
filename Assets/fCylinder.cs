using UnityEngine;

public class fCylinder : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float radius = 1f;
    public float mass = 1f;

    void Update()
    {
        // Increase rotation speed over time
        rotationSpeed += Time.deltaTime * 5f;

        // Calculate linear velocity
        float linearVelocity = rotationSpeed * Mathf.Deg2Rad * radius;

        // Calculate centripetal force
        float centripetalForce = (mass * linearVelocity * linearVelocity) / radius;

        Debug.Log("Centripetal force: " + centripetalForce + " N");

        // Rotate the object
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
