using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;

    [SerializeField]
    int maxHealth = 5;
    int currentHealth;

    public int health { get => currentHealth; }
    public int maxhealth { get => maxHealth; }

    Rigidbody2D myRigid;
    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = myRigid.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        myRigid.MovePosition(position);
    }

    public void ChangeHelath(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
