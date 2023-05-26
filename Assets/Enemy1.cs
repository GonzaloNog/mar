using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    public int lives = 1;
    public Slider vidaBar;
    public GameObject drop;
    public bool dropOn;

    private bool live = true;
    public GameObject target;
    private bool atacando = false;
    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        vidaBar.maxValue = lives;
        vidaBar.value = lives;
    }

    // Update is called once per frame
    void Update()
    {
        if(live)
            Comportamiento_Enemigo();
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position,target.transform.position) > 8) {
            ani.SetBool("run", false);
            Final_Ani();
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando) {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                ani.SetBool("walk", false);

                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                ani.SetBool("atack", false);
            }
            /*
            else if(Vector3.Distance(transform.position, target.transform.position) > 1)
            {
                Final_Ani();
            }*/
            else
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("atack", true);
                atacando = true;
            }
            if (Vector3.Distance(transform.position, target.transform.position) > 2 && atacando)
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("atack", false);
                atacando = false;
            }
        }
    }

    public void Final_Ani()
    {
        ani.SetBool("atack", false);
        atacando = false;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "bull")
        {
            if (GameManager.instance.wapon == "pistola")
                lives -= 1;
            else if (GameManager.instance.wapon == "escopeta")
                lives -= 4;
            else
                lives -= 1;
            vidaBar.value = lives;
            if (lives <= 0)
            {
                GameObject TemporalSkill;
                Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                if(dropOn)
                    TemporalSkill = Instantiate(drop, position, transform.rotation);
                live = false;
                ani.SetBool("dead", true);
                vidaBar.gameObject.SetActive(false);
                Destroy(other.gameObject, 0.5f);
            }
        }
    }

    public bool GetAtack()
    {
        return atacando;
    }
}
