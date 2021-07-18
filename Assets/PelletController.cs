using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    public Vector2 movement;

    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movement * Time.deltaTime * speed;
    }



    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
