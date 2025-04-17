using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FuelManager : MonoBehaviour
{
    [Header("Fuel Settings")]
    public float maxFuel = 100f;
    public float currentFuel;
    public float fuelConsumptionRate = 10f;
    
    [Header("UI References")]
    public Slider fuelSlider;
    public Image fuelFillImage;
    public Color fullFuelColor = Color.green;
    public Color lowFuelColor = Color.red;
    public float lowFuelThreshold = 0.3f;
    
    [Header("Events")]
    public UnityEvent onFuelEmpty;
    
    private bool isFuelEmpty = false;
    
    void Start()
    {
        currentFuel = maxFuel;
        
        fuelSlider.maxValue = maxFuel;
        fuelSlider.value = currentFuel;
    }
    
    void Update()
    {
        fuelSlider.value = currentFuel;
        
        float fuelPercentage = currentFuel / maxFuel;
        fuelFillImage.color = Color.Lerp(lowFuelColor, fullFuelColor, fuelPercentage / lowFuelThreshold);
        
        if (currentFuel <= 0 && !isFuelEmpty)
        {
            isFuelEmpty = true;
            onFuelEmpty.Invoke();
        }
    }
    
    public void ConsumeFuel(float amount)
    {
        if (isFuelEmpty) return;
        
        currentFuel -= amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
    }
    
    public void RefillFuel(float amount)
    {
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
        
        if (currentFuel > 0)
        {
            isFuelEmpty = false;
        }
    }
    
    public bool IsFuelEmpty()
    {
        return isFuelEmpty;
    }
}
