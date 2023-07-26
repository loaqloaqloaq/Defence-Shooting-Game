using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class shooController : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public int power;
    float powerPre;
    public GameObject explode;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        powerPre = (float)power / 10.0f;

        mainCam = Camera.main;
        rb=GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * (force * powerPre + 3);
        if(power>=10) GetComponent<ParticleSystem>().Play();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, powerPre * 1000.0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("walls")) {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("kumas"))
        {
            var ex = Instantiate(explode, transform.position, Quaternion.identity);
            ex.transform.localScale = new Vector3(2, 2, 1);
            Destroy(gameObject);

            collision.GetComponent<KumaController>().hp -= this.power;
            if (collision.GetComponent<KumaController>().hp <= 0)
            {
                collision.GetComponent<KumaController>().hp = 0;
                PlayerController pc = player.GetComponent<PlayerController>();
                pc.addExp();
            }

            
        }
    }

}
