using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;           // 引用 事件 API
using UnityEngine.UI;


public class ME : MonoBehaviour {

    [Header("移動速度"), Range(0, 1000)]
    public int Speed = 200;
    [Header("跳躍高度"), Range(0, 20000)]
    public float jump = 1000f;
    public string Name = "Hui";
    public bool pass = false;
    [Header("是否在地板上")]
    public bool isGround;

    [Header("吃東西事件")]
    public UnityEvent onEat;


    [Header("音效區域")]
    public AudioClip soundJum;
    public AudioClip soundProp;

    //private Transform tra;  原始寫法
    private Rigidbody2D foxyee;
    private AudioSource aud;
    private Animator ani;
    private float hpMax;

    [Header("血量"), Range(0, 200)]
    public float hp = 100;
    [Header("血量吧條")]
    public Image hpBar;
    [Header("結束畫面")]
    public GameObject final;

    public Text fin;

    private void Start() //開始事件：遊戲開始時執行一次
    {
        foxyee = GetComponent<Rigidbody2D>();
        ani = gameObject.GetComponent<Animator>();
        //tra = GetComponent<Transform>();
        ani.SetBool("run", false);
        aud = GetComponent<AudioSource>();

        hpMax = hp;
    }
    private void Update()
    {
        //if (Input.GetKey(KeyCode.D)) tra.eulerAngles = new Vector3(0, 0, 0);
        //if (Input.GetKey(KeyCode.A)) tra.eulerAngles = new Vector3(0, 180, 0);
        if (Input.GetKeyDown(KeyCode.D)) Turn(0);
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
        if(hp == 0 || this.transform.position.y <= -3.5)
        {
           final.SetActive(true);
            fin.text = "繼續咳QQ";
        }
    }
    //固定事件更新
    private void FixedUpdate()
    {
        Walk();
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            ani.SetBool("run", true);
        }
        else ani.SetBool("run", false);
        Jump();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        if (collision.gameObject.name == "地板")
        {
            isGround = true;
            //ani.SetBool("跳躍開關", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ppp")
        {
            aud.PlayOneShot(soundProp, 1.2f);
            Destroy(collision.gameObject);  // 刪除
            onEat.Invoke();                 // 呼叫事件
        }
    }

    //方法
    private void Walk()
    {
        if (foxyee.velocity.magnitude < 10)
            foxyee.AddForce(new Vector2(Speed * Input.GetAxisRaw("Horizontal"), 0));
        // foxyee.AddForce(new Vector2(Speed * Input.GetAxis("Horizontal"), 0));
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.W) && isGround == true)
        {
            isGround = false;
            foxyee.AddForce(new Vector2(0, jump));
            aud.PlayOneShot(soundJum, 1.5f);           // 播放一次音效(音效片段，音量)
        }
    }
    /// <summary>
    /// 轉彎
    /// </summary>
    /// <param name="diretion" >方向 ; 左轉180，右轉0 </param>
    private void Turn(int diretion)
    {
        transform.eulerAngles = new Vector3(0, diretion, 0);
    }
    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;

       
    }
}
