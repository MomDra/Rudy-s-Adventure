using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;
    [SerializeField]
    bool vertical;
    [SerializeField]
    float changeTime = 3f;

    [SerializeField]
    ParticleSystem smokeEffect;

    Rigidbody2D myRigid;
    Animator myAnimator;
    AudioSource myAudioSource;

    float timer;
    int direction = 1;

    bool broken = true;

    [SerializeField]
    AudioClip fixedSound;
    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        timer = changeTime;
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }   
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = myRigid.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            myAnimator.SetFloat("Move X", 0);
            myAnimator.SetFloat("Move Y", direction);

        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            myAnimator.SetFloat("Move X", direction);
            myAnimator.SetFloat("Move Y", 0);
        }

        myRigid.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        myRigid.simulated = false;
        myAnimator.SetTrigger("Fixed");
        smokeEffect.Stop();

        myAudioSource.PlayOneShot(fixedSound);
    }
}
