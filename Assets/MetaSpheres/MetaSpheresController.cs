using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class MetaSpheresController : MonoBehaviour
{
    
    private static int sphereCount = 50;
    
    private Vector3[] spheres = new Vector3[sphereCount];
    private Vector2[] Velocities = new Vector2[sphereCount];
    
    [SerializeField] private float gravity;
    [SerializeField] private float particleSpeed;
    [SerializeField] private float offset;

    private const int SCALE = 20;
    private const int WIDTH = SCALE/2;
    private const int HEIGHT = SCALE/2;
    private Material _material;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
       /* 
        // Define spheres
        Vector3 s1 = new Vector3(0.0f, 0.0f, 1.0f);
        Vector3 s2 = new Vector3(0.0f, 0.0f, 1.5f);
        Vector3 s3 = new Vector3(-6.0f, -3.0f, 2.0f);
        Vector3 s4 = new Vector3(-6.0f, -3.0f, 2.0f);
        Vector3 s5 = new Vector3(-6.0f, -3.0f, 2.0f);

        spheres[0] = s1;
        spheres[1] = s2;
        spheres[2] = s3;   
        spheres[3] = s4;   
        spheres[4] = s5;   
*/
      /* for (int i = 0; i < sphereCount/4; i++)
       {
           emitSphere(i,0);
       }

       for (int i = sphereCount / 4; i < sphereCount/2; i++)
       {
           emitSphere(i, 2);
       }
       for (int i = sphereCount / 2; i < sphereCount*3/4; i++)
       {
           emitSphere(i, 4);
       }

       for (int i = sphereCount * 3 / 4; i < sphereCount; i++)
       {
           emitSphere(i, 6);
       }
       */

      for (int i = 0; i < sphereCount; i++)
      {
          emitSphere(i);
      }
        _material = this.GetComponent<MeshRenderer>().material;

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        float dt = Time.deltaTime;
        //PHYSICS STEP:
        for (int i = 0; i < spheres.Length; i++)
        {
            
            //Get location to use for collision check
            Vector3 spher = spheres[i];
            Vector2 vel = Velocities[i];
            vel.y = vel.y - (gravity * dt);
            Vector3 targetPos = addVelocityVector(spher, vel, dt);
          
            //Detect and Resolve Collision with walls:
            if (targetPos.x < -WIDTH || targetPos.x > WIDTH)
            {
                vel.x = -vel.x;
            }
            /*if (targetPos.y < -HEIGHT || targetPos.y > HEIGHT)
            {
                vel.y = -vel.y;
            }*/
            
            
            //Apply Velocity to Pos Vector:
            spheres[i] = addVelocityVector(spher, vel, dt);
            //Apply Accelerations to Velocity Vector:
            Velocities[i] = vel;

            if (targetPos.y < -(HEIGHT+offset))
            {
                emitSphere(i);
            }

        }
        
        //Send updated Position to shader:
        _material.SetFloatArray("spheres", ConvertToRawArray(spheres));
        _material.SetInt("sphereCount", spheres.Length);
        
    }

    private void emitSphere(int i)
    {
        spheres[i] = new Vector3(Random.Range(-WIDTH,WIDTH), HEIGHT+offset, Random.Range(1f, 3));
        Velocities[i] = new Vector2(Random.Range(-WIDTH/2, WIDTH/2), Random.Range(-particleSpeed,particleSpeed));
    }

    private void emitSphere(int i, float additionalOffset)
    {
        spheres[i] = new Vector3(Random.Range(-WIDTH,WIDTH), HEIGHT+offset+additionalOffset, Random.Range(1f, 3));
        Velocities[i] = new Vector2(Random.Range(-WIDTH/2, WIDTH/2), Random.Range(particleSpeed/2f,particleSpeed));
    }

    Vector3 addVelocityVector(Vector3 pos, Vector2 vel, float dt)
    {
        return new Vector3(pos.x + (vel.x * dt), pos.y + (vel.y * dt), pos.z);
    }

    float[] ConvertToRawArray(Vector3[] baseArray)
    {
        float[] outputArray = new float[baseArray.Length*3];
        for(int i = 0; i < outputArray.Length; i+=3)
        {
            Vector3 currElement = baseArray[i/3];
            outputArray[i] = currElement.x;
            outputArray[i + 1] = currElement.y;
            outputArray[i + 2] = currElement.z;
        }

        return outputArray;
    } 
    
}
