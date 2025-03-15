using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantiateToggleButton : MonoBehaviour
{
    // public UnityEvent onButtonPressed, onButtonReleased;

    [SerializeField] private float buttonPressedDepth = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    private bool isPressed = false;
    private Vector3 initialPosition;
    private ConfigurableJoint joint;

    //
    public GameObject prefab;
    public GameObject parentObject;
    private GameObject spawnedObject;


    public void ToggleObject()
    {
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(prefab); // keep prefab position, rotation and scale
            spawnedObject.transform.SetParent(parentObject.transform, false);
        }
        else
        {
            Destroy(spawnedObject);
            spawnedObject = null; 
        }
    }

    void Pressed()
    {
        isPressed = true;
        // onButtonPressed.Invoke();
        Debug.Log("Button Pressed");
    }

    void Released()
    {
        isPressed = false;
        // onButtonReleased.Invoke();
        Debug.Log("Button Released");
        ToggleObject();
    }

    private float GetValue()
    {
        float value = Vector3.Distance(initialPosition, transform.localPosition) / joint.linearLimit.limit;
        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1, 1);
    }
    void Start()
    {
        initialPosition = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if (!isPressed && GetValue() + buttonPressedDepth >= 1)
        {
            Pressed();
        }
        else if (isPressed && GetValue() - buttonPressedDepth <= 0)
        {
            Released();
        }
    }

}


