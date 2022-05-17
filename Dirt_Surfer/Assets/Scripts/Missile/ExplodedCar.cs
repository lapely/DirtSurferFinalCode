using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodedCar : MonoBehaviour
{
    public float explosionForce = 0f;
    public Rigidbody rb;
    public void Explosion() {
        Debug.Log("BOUM");
        rb.AddExplosionForce(explosionForce,transform.position, 2f, 0f, ForceMode.Impulse);
    }
}
