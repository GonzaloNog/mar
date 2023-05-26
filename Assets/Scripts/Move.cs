using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController controller;

    public float playerSpeedWalk = 3.0f;
    public float playerSpeedRun = 6.0f;
    public float gravity = 20.0f;
    public GameObject bull;
    public GameObject misil;
    public GameObject SpawnBullwalk;
    public GameObject SpawnBullidle;
    public float speedShoot = 10;
    private bool shoot = true;
    public float cooldownShoot = 0.5f;
    private float time = 0;
    private Animator anim;
    private bool idle = false;
    private float speed = 0;
    private Vector3 velocity;
    public Shotgun escopeta;
    public armaTres uzi;
    public GameObject pistola;
    private string []armas = {"pistola","escopeta","Lanzacoetes"};
    private int armasIndex = 0;
    private AudioSource audio;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        speed = playerSpeedWalk;
        anim = GetComponent<Animator>();
        idle = true;
        escopeta.gameObject.SetActive(false);
        uzi.gameObject.SetActive(false);
        pistola.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Vector3 move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;

        if(move == Vector3.zero)
        {
            speed = 0;
            idle = false;
            escopeta.move = false;
            uzi.move = false;
        }
        else
        {
            speed = playerSpeedWalk;
            escopeta.move = true;
            uzi.move = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = playerSpeedRun;
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeWapon();
        }
            

        anim.SetFloat("speed", speed);
        anim.SetBool("idle", idle);
        controller.Move(move * Time.deltaTime * speed);

        if (!shoot)
        {
            time += Time.deltaTime;
            if (time > cooldownShoot)
            {
                time = 0;
                shoot = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && shoot)
        {
            shoot = false;
            shootWapon();
        }
    }

    public void shootWapon()
    {
        GameObject SpawnBull;
        audio.Play();
        if (speed > 0)
        {
            SpawnBull = SpawnBullwalk;
        }
        else
            SpawnBull = SpawnBullidle;

        GameObject TemporalSkill = Instantiate(bull, SpawnBull.transform.position, SpawnBull.transform.rotation);

        if(armas[armasIndex] == "escopeta")
            TemporalSkill.transform.localScale = new Vector3(TemporalSkill.transform.localScale.x + 0.1f, TemporalSkill.transform.localScale.y + 0.1f, TemporalSkill.transform.localScale.z + 0.1f);

        //TemporalSkill.transform.forward = new Vector3(TemporalSkill.transform.forward.x, TemporalSkill.transform.forward.y, TemporalSkill.transform.forward.z );
        Rigidbody rb = TemporalSkill.GetComponent<Rigidbody>();

        transform.forward = new Vector3(transform.forward.x,transform.forward.y,transform.forward.z);

        rb.AddForce(transform.forward * speedShoot);

        Destroy(TemporalSkill, 5.0f);
    }

    public void ChangeWapon()
    {
        armasIndex += 1;
        if(armasIndex  == armas.Length)
        {
            armasIndex = 0;
        }
        switch (armas[armasIndex])
        {
            case "pistola":
                pistola.SetActive(true);
                escopeta.gameObject.SetActive(false);
                uzi.gameObject.SetActive(false);
                GameManager.instance.wapon = "pistola";
                cooldownShoot = 0.4f;
                break;
            case "escopeta":
                pistola.SetActive(false);
                escopeta.gameObject.SetActive(true);
                uzi.gameObject.SetActive(false);
                GameManager.instance.wapon = "escopeta";
                cooldownShoot = 0.8f;
                break;
            case "Lanzacoetes":
                pistola.SetActive(false);
                escopeta.gameObject.SetActive(false);
                uzi.gameObject.SetActive(true);
                GameManager.instance.wapon = "lanzacoetes";
                cooldownShoot = 0.05f;
                break;
        }
    }
}
