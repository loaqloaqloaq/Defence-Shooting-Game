using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class explode_auto_destory : MonoBehaviour
{
    float timePassed;  
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
        source = GetComponent<AudioSource>();
        source.volume = (transform.localScale.x / 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 2.0f)
        {
           Destroy(gameObject);
        }
    }
}
