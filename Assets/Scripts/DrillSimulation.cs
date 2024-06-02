using UnityEngine;
using System.Collections;

public class DrillSimulation : MonoBehaviour
{
    public float linearVelocity = 0.25f;
    public float radius = 0.05f;
    public float maxRPM = 500f;
    public float maxRPMTime = 7.5f;
    
    private float currentRPM = 0f;
    private float elapsedTime = 0f;
    private float energyLossFactor = 0.98f; // 에너지 손실 비율

    void Update()
    {
        if (elapsedTime < maxRPMTime)
        {
            currentRPM = (maxRPM / maxRPMTime) * elapsedTime; // 선형적으로 증가
            elapsedTime += Time.deltaTime;
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
