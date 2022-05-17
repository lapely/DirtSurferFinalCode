/*
 * Auteur: Emile Veillette
 * C'est ici que m'occupe de faire bouger mon missile
 * Note : Je m'occupe aussi de l'explosion du véhicule et
 * je m'occupe de garder le missile dans les airs en flottant ici.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileAi : MonoBehaviour
{
    //Floating in the air
    public Transform[] anchors = new Transform[4];
    public RaycastHit[] hits = new RaycastHit[4];
    float missileUpForce = 0f;
    public float upMultiplier = 0f;

    //Movement 
    private List<Vector3> movementList;
    public float speed = 0f;
    public float rotatingSpeed = 0f;

    //Explosion
    public float explosionForce = 0f;
    public float radius = 0f;
    public AudioSource explosionSound;

    //Objects Needed
    public GameObject target;
    public Rigidbody rb;
    private PathFinding pathFinding;
    List<Vector3> pathFollowed;

    //Missile Explosion
    public GameObject missile;
    public GameObject Explosionparticle;
    Vector3 originPosition = new Vector3(50, 0, 50);

    //Missile UI
    public RawImage missileAlertImage;
    public Text distanceFromCarText;




    // Start is called before the first frame update
    void Start()
    {
        pathFinding = new PathFinding(200, 15, 200, 50, originPosition);

    }

    // Update is called once per frames
    void FixedUpdate()
    {
        //Making the missile float by applying the force to each anchor
        for (int i = 0; i < 4; i++)
            ApplyForce(anchors[i], hits[i]);

        pathFollowed = PathFinding.Instance.FindPath(rb.position, target.transform.position);

        if (pathFollowed != null)
            Movement();

    }

    private void Movement()
    {
        //Vector3 targetVector = movementList[i];
        Vector3 direction = (rb.position - target.transform.position).normalized;
        transform.position = transform.position + direction * speed * Time.deltaTime;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotatingSpeed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        ExplodedCar();
        explosionSound.Play();
        missile.SetActive(false);
        Explosionparticle.transform.position = rb.transform.position;
        Explosionparticle.SetActive(true);
        Destroy(missileAlertImage);
        Destroy(distanceFromCarText);
        rb.AddExplosionForce(explosionForce, transform.position, 50f, 0f, ForceMode.Impulse);
        Destroy(Explosionparticle, 4);
        //target.GetComponent<ExplodedCar>().Explosion();
        Destroy(missile, 6);


    }
    void ExplodedCar()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody otherRb = nearby.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                Debug.Log("I'm in!");
                otherRb.AddExplosionForce(explosionForce, transform.position, radius, 10f);
            }
        }
    }

    //Je fais flotter mon missile
    void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit))
        {
            missileUpForce = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            rb.AddForceAtPosition(transform.up * missileUpForce * upMultiplier, anchor.position, ForceMode.Acceleration);
        }

    }

    //Je dessine l'explosion du missile
    private void OnDrawGizmos()
    {
        //Draw the radius of the explosion
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
