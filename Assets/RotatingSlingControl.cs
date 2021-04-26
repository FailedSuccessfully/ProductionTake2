using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSlingControl : MonoBehaviour
{
    public Transform SlingObject;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = Mouse - (Vector2)SlingObject.position;

        SlingObject.right = dir;
    }
}
