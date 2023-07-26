using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sunController : MonoBehaviour
{
    float timeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        timeSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-timeSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x >= 28.0f) transform.position = new Vector3(-28f, 9.27f, 0);
    }
}
