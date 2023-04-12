using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public bool checkOnTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(checkOnTarget)
        {
            if (collision.gameObject.CompareTag("target"))
            {
                collision.GetComponent<TargetController>().checkOnTarget = true;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                Destroy(this.gameObject, 0.4f);               
            }
        }
    }
}
