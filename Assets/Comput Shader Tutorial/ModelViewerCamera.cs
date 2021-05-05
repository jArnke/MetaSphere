using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ModelViewerCamera : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float radius;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform target;
    private float angle;

    private float xOffset;
    private float yOffset;
    private float zOffset;
    

    private void Awake()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Warning no targetObject assigned to ModelViewCamera: ", this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        angle = 0;
        radius = 5;
    }

    // Update is called once per frame
    void Update()
    {
        float prevAngle = angle;
        float prevRadius = radius;
        
        //Get New Radius and Angle:
        getInput();
        
        //Update Position:
        updatePosition();

        //update Rotation
        var transformTemp = transform;
        transform.rotation = Quaternion.Slerp(transformTemp.rotation, Quaternion.LookRotation(targetObject.position - transformTemp.position), rotationSpeed*Time.deltaTime);
        
    }

    void getInput()
    {
        float xHorizontal = Input.GetAxisRaw("Vertical");

        if (xHorizontal < 0)
        {
            xOffset -= cameraSpeed * Time.deltaTime;
        }
        else if (xHorizontal > 0)
        {
            xOffset += cameraSpeed * Time.deltaTime;
        }
    }

    void updatePosition()
    {
        Vector3 targetDir = new Vector3(radius, 0, 0);
        
        
        this.transform.position = Vector3.Slerp(transform.position, targetDir.normalized*radius, Time.deltaTime);
    }

    Vector3 AddScalar(Vector3 vector, float scalar)
    {
        return new Vector3(vector.x + scalar, vector.y + scalar, vector.z + scalar);
    }
    
    
}
