using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody2D RB;
    public float speed;
 
    void Start()
    {
        RB.velocity = transform.right * speed;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }
}
