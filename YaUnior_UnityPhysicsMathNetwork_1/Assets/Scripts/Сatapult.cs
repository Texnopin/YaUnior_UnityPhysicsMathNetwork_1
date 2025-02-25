using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
[RequireComponent(typeof(Rigidbody))]
public class Сatapult : MonoBehaviour
{
    [SerializeField] private SpringJoint joint;
    [SerializeField] private Transform PointSpawnSpheres;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject SpherePrefab;
    [SerializeField] private Transform COM;
    [HideInInspector]public bool allowed = false;

    private float timer;
    private GameObject spawnedObject;

    private void Start()
    {
        Vector3 localPosition = transform.InverseTransformPoint(COM.position);
        rb.centerOfMass = localPosition;
        timer = 0f;
        joint.spring = 1f;
    }

    private void Update()
    {
        if(allowed)
        {
            timer += Time.deltaTime;
            print(timer + "До первого if");
            if((timer >= 0.2f) && (joint.spring<200f) && (spawnedObject==null))
            {
                spawnedObject = Instantiate(SpherePrefab, PointSpawnSpheres.position, PointSpawnSpheres.rotation);
                joint.spring = 200f;
                print(timer + "Внутри первого if" + joint.spring);
            }
            if(timer >= 0.8f && joint.spring==200f)
            {
                allowed = false;
                joint.spring = 1f;
                timer= 0f;
                print(timer + "Внутри второго if");
            }
        }

        /*if(spawnedObject == null)
        {
            allowed = true;
        }*/

    }


}
