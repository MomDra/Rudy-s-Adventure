using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;

    [SerializeField]
    int maxHealth = 5;
    [SerializeField]
    int currentHealth;

    public int health { get => currentHealth; }
    public int maxhealth { get => maxHealth; }

    [SerializeField]
    float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D myRigid;
    Animator myAnimator;

    Vector2 lookDirection = new Vector2(1, 0);
    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }
    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        myAnimator.SetFloat("Look X", lookDirection.x);
        myAnimator.SetFloat("Look Y", lookDirection.y);
        myAnimator.SetFloat("Speed", move.magnitude);

        Vector2 position = myRigid.position;

        position = position + move * speed * Time.deltaTime;

        myRigid.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            myAnimator.SetTrigger("Hit");

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
