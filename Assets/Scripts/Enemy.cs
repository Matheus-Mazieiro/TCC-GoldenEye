using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    int myPoint = 0;
    Rigidbody myRb;
    [SerializeField] Transform[] myTargets;
    [SerializeField] float distanceTrashhold;
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    [Range(0, 180)]
    [SerializeField] float viewOpening = 45;
    [Range(0, 100)]
    [SerializeField] float viewRange;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.LookAt(myRb.position + myRb.velocity);
        Vector2 playerPos = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z);
        Vector2 myPos = myRb.velocity;

        if (Vector3.Angle(playerPos, myPos) <= viewOpening)
        {
            RaycastHit hit;
            if (Physics.Raycast(myRb.position, player.transform.position - myRb.position, out hit))
            {
                if (hit.collider.gameObject.layer == 9)
                    Debug.Log("Te vi");
            }
        }

        myRb.velocity = new Vector3(myRb.position.x - myTargets[myPoint].position.x, myRb.velocity.y, myRb.position.z - myTargets[myPoint].position.z).normalized * -speed;

        if (Vector3.Distance(new Vector3(myRb.position.x, 0, myRb.position.z), new Vector3(myTargets[myPoint].position.x, 0 ,myTargets[myPoint].position.z)) <= distanceTrashhold)
        {
            myPoint++;
            if (myPoint >= myTargets.Length)
                myPoint = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (myTargets.Length > 1)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < myTargets.Length; i++)
            {
                Gizmos.DrawLine(myTargets[i].position, myTargets[i + 1 < myTargets.Length ? i + 1 : 0].position);
            }
        }
    }
}
