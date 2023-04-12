using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public TextMeshPro txt;
    public bool checkOnTarget = false;
    public bool check = false;

    //adjust this to change speed
    float speed = 3f;
    //adjust this to change how high it goes
    float height = 0.7f;
    // Start is called before the first frame update

    Vector3 pos;
    float[] ran = new float[2] { 1, -1 };
    float random;
    void Start()
    {      
        pos = transform.position;

        random = ran[Random.Range(0, ran.Length)];

    }

    // Update is called once per frame
    void Update()
    {
        float newY = transform.position.y;
        if (!checkOnTarget)
        {
            //calculate what the new Y position will be
            newY = Mathf.Sin(random * Time.time * speed);
            //set the object's Y to the new calculated Y
            transform.position = new Vector3(pos.x, pos.y + newY * height, pos.z);
        }
        if(check )
        {
            txt.text = "T";
        }
        else
        {
            txt.text = "F";
        }
    }
}
