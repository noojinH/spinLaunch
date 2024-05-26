using UnityEngine;

public class DrillSimulation : MonoBehaviour
{
    public float linearVelocity = 1.0f;
    public float radius = 0.05f;
    public float maxRPM = 3000f;
    public float maxRPMTime = 2.0f;
    
    private float currentRPM = 0f;
    private float elapsedTime = 0f;
    private float energyLossFactor = 0.98f; // 에너지 손실 비율
    
    void Update()
    {
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

        currentRPM *= energyLossFactor; // 에너지 손실 적용

        float angle = currentRPM * 360f / 60f * Time.deltaTime;
        transform.Rotate(Vector3.up, angle);
    }
}
