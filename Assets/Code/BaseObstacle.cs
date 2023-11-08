using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player;

    [SerializeField]
    private float Speed;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        //Vector3 target = player.position;
        //transform.rotation = Quaternion.FromToRotation(transform.position, -player.position);
        transform.LookAt(player.position);
        rb.AddForce(transform.forward * Speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
