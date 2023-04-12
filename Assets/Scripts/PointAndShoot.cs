using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAndShoot : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject bullStart;
    public GameObject targetPrefab;
    public GameObject resultPanel;

    public Transform aim;

    public LayerMask targetLayer;

    public TextMeshProUGUI txtPoint;
    public TextMeshProUGUI txtQues;
    public TextMeshProUGUI txtFinalPoint;
    public TextMeshProUGUI txtComment;

    public TimeBar TimeBar;

    public float bulletSpeed = 80f;

    public AudioSource shootEffect;
    public AudioSource targetEffect;
    public AudioSource trueEffect;
    public AudioSource falseEffect;

    private Vector3 target;

    int point;
    int count;
    bool spawnTarget;
    bool checkOnTarget = false;
    bool canShoot;
    GameObject[] targetBoards;
    float time;
    float time2;
    float showTime;
    // Start is called before the first frame update
    void Start()
    {
        point = 0;
        count = 0;
        time = 10;
        time2 = 0;
        showTime = 2.5f;
        spawnTarget = true;
        canShoot = true;
        targetBoards = new GameObject[4];
        TimeBar.SetMaxTime(time);
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        showTime -= Time.deltaTime;
        if(showTime < 0)
        {
            time -= Time.deltaTime;
        }
        TimeBar.SetTime(time);
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(target.x, target.y);

        Vector3 diff = target - player.transform.position;
        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            shootEffect.Play();
            float distance = diff.magnitude;          
            Vector2 dir = diff / distance;
            dir.Normalize();
            Collider2D[] shootTargets = Physics2D.OverlapCircleAll(aim.position, 0.3f, targetLayer);
            foreach (Collider2D shoot in shootTargets)
            {
                targetEffect.Play();
                Destroy(shoot.gameObject, 0.5f);
                checkOnTarget = true;
                canShoot = false;
                if (shoot.GetComponent<TargetController>().check == true)
                {
                    trueEffect.Play();
                    point++;
                }
                else
                {
                    falseEffect.Play();
                }
            }
            fireBullet(dir, rotationZ, checkOnTarget);
        }
        if (spawnTarget && count < 10 && showTime < 0)
        {
            txtQues.text = "Question " + (count + 1).ToString() + ":"; 
            targetBoards[0] = Instantiate(targetPrefab, new Vector3(-6, Random.Range(0f, 1f), 0), Quaternion.Euler(Vector3.zero)) as GameObject;
            targetBoards[1] = Instantiate(targetPrefab, new Vector3(-2, Random.Range(0f, 1f), 0), Quaternion.Euler(Vector3.zero)) as GameObject;
            targetBoards[2] = Instantiate(targetPrefab, new Vector3(2, Random.Range(0f, 1f), 0), Quaternion.Euler(Vector3.zero)) as GameObject;
            targetBoards[3] = Instantiate(targetPrefab, new Vector3(6, Random.Range(0f, 1f), 0), Quaternion.Euler(Vector3.zero)) as GameObject;
            targetBoards[Random.Range(0,targetBoards.Length)].GetComponent<TargetController>().check = true;
            spawnTarget = false;
            checkOnTarget = false;
            canShoot = true;
            count++;
        }
        if(time < 0 || checkOnTarget)
        {
            foreach(GameObject target in targetBoards)
            {
                Destroy(target,0.5f);
            }
            time2 += Time.deltaTime;
            if(time2 > 1.5f)
            {
                spawnTarget = true;
                time = 10;
                time2 = 0;
                if (count == 10)
                {
                    resultPanel.SetActive(true);
                    txtFinalPoint.text = point.ToString();
                    switch (point)
                    {
                        case 0:
                            txtComment.text = "Very Bad!!!";
                            break;
                        case 1:
                        case 2:
                            txtComment.text = "Bad!!!";
                            break;
                        case 3:
                        case 4:
                            txtComment.text = "Not Bad!!!";
                            break;
                        case 5:
                            txtComment.text = "Normal!!!";
                            break;
                        case 6:
                        case 7:
                            txtComment.text = "Quite Good!!!";
                            break;
                        case 8:
                        case 9:
                            txtComment.text = "Good!!!";
                            break;
                        default:
                            txtComment.text = "Excellent!!!";
                            break;

                    }
                }
            }          
        }       
        txtPoint.text = point.ToString();
        
    }

    void fireBullet(Vector2 dir, float rotZ, bool check)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = bullStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0f,0f, rotZ);
        b.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
        b.GetComponent<ArrowController>().checkOnTarget = check;
    }

    public void GoNextScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
