using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.HOMMExample;
using TbsFramework;

public class UIRotatePointer : MonoBehaviour
{
    public Transform rotationCenter;
    public Transform character;
    public Transform destination;

    public float rotationSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationCenter != null && character != null && destination != null)
        {
            Vector3 direction = destination.position - character.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotationCenter.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
