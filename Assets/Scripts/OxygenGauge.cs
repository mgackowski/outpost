using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenGauge : MonoBehaviour
{
    private RectTransform rectTransform;
    public float maxOxygenLevel = 600f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDisplayedValue(float oxygenLevel)
    {
        float zeroOxygenValue = 86f;
        float fullOxygenValue = -70f;
        float percentage = oxygenLevel / maxOxygenLevel;
        float targetMaxRange = fullOxygenValue - zeroOxygenValue;
        float finalAngle = zeroOxygenValue + (percentage * targetMaxRange);

        rectTransform.rotation = Quaternion.Euler(0, 0, finalAngle);
    }
}
