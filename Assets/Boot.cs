using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour
{
    public GameObject target;
    private float speed = 3.0f;
    private Animator anim;
    private GameObject ActualTarget;
    // Update is called once per frame
    void Awake()
    {
        transform.position = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        ActualTarget = GameObject.FindWithTag("drop");
        if(ActualTarget == null)
            ActualTarget = target;
        Vector3 targetPoint = new Vector3(ActualTarget.transform.position.x, this.transform.position.y, ActualTarget.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPoint , speed * Time.deltaTime);
        if(transform.position == targetPoint)
        {
            anim.SetBool("Walk_Anim", false);
        }
        else
            anim.SetBool("Walk_Anim", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "drop")
        {
            Debug.Log("boom");
            Destroy(other.gameObject);
        }
    }
}
