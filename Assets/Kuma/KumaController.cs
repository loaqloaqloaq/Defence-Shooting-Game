using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class KumaController : MonoBehaviour
{
    public float speed;
    public int hp = 10;
    public Sprite[] number;
    public GameObject[] HPrender;
    public GameObject explode,player;    

    Animator animator;

    GameController gc;
    bool attackMode,firstAttk;
    // Start is called before the first frame update
    void Start()
    {
        //hp = 10;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        attackMode = false;
        firstAttk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.GameOver && !gc.pause)
        {
            animator.speed = 1;
            if (!attackMode)
            {                
                transform.Translate(-speed * Time.deltaTime, 0, 0);               
            }
            else {
                if (!firstAttk) {
                    Invoke("attackPlayer", 0.4f);
                    firstAttk = true;
                }
                if ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))) {
                    // Avoid any reload.
                    animator.Play("attack",0,0);                    
                    Invoke("attackPlayer", 0.4f);                   
                }
            }
            var hpString = hp.ToString().PadLeft(5, '0');
            for (int i = 0; i < HPrender.Length; i++)
            {
                HPrender[i].GetComponent<SpriteRenderer>().sprite = number[int.Parse(hpString[i].ToString())];
            }

            if (this.hp <= 0)
            {
                var ex = Instantiate(explode, transform.position, Quaternion.identity);
                ex.transform.localScale = new Vector3(5, 5, 1);
                Destroy(gameObject);
            }
        }
        else {
            animator.speed = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            attackMode=true;
            animator.SetBool("attackmode", true);
        }
    }

    private void attackPlayer() {
        if (!gc.GameOver)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.damage(hp);
        }
    }
}
