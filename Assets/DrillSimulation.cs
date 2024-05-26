using UnityEngine;

public class DrillSimulation : MonoBehaviour
{
    // Linear velocity of the drill bit (in meters per second)
    public float linearVelocity = 1.0f;
    
    // Radius of the drill bit (in meters)
    public float radius = 0.05f;
    
    // Maximum RPM of the drill
    public float maxRPM = 3000f;
    
    // Time taken to reach max RPM (in seconds)
    public float maxRPMTime = 2.0f;
    
    private float currentRPM = 0f;
    private float elapsedTime = 0f;
    
    // Update is called once per frame
    void Update()
    {
        // Calculate RPM based on linear velocity and drill bit radius
        if (elapsedTime < maxRPMTime)
        {
            currentRPM = Mathf.Lerp(0f, maxRPM, elapsedTime / maxRPMTime);
            elapsedTime += Time.deltaTime;
            //Debug.Log("RPM: " + currentRPM);
        }
        else
        {
            currentRPM = maxRPM;
        }
        
        // Print RPM to console
        
        // Rotate the drill based on calculated RPM
        float angle = currentRPM * 360f / 60f * Time.deltaTime;
        transform.Rotate(Vector3.up, angle);
    }
}
