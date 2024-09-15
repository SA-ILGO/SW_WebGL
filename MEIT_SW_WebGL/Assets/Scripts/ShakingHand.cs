using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingHand : MonoBehaviour
{
    public GameObject arm;
    private Quaternion armRotation;
    // Start is called before the first frame update
    void Start()
    {
        armRotation = arm.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
