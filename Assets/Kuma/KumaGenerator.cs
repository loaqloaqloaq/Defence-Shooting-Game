using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KumaGenerator : MonoBehaviour
{
    float genMin,genMax;
    float yMin,yMax;
    float timePassed;
    float rnd;

    public float genOffset;
    public int genHP;
    public GameObject kuma;

    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
        genMin = 1.5f;
        genMax = 2.5f;        

        yMin =-3.1f;
        yMax=1.2f;

        genHP = 10;
        genOffset = 1f;
        rnd = Random.Range(genMin, genMax);

        gc= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.GameOver && !gc.pause)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= rnd)
            {
                Vector3 pos = new Vector3(13.0f, Random.Range(yMin, yMax), 0);
                var ku = Instantiate(kuma, pos, Quaternion.identity);
                int minHP = Mathf.Max(10, genHP - 3);
                int randHP = Random.Range(minHP, genHP+1);               
                ku.GetComponent<KumaController>().hp = randHP;

                timePassed = 0;

                Debug.Log(genOffset);
                float offsetedGenMin = genMin * genOffset;
                float offsetedGenMax = genMax * genOffset;
                rnd = Random.Range(offsetedGenMin, offsetedGenMax);
            }                       
        }
    }
}
