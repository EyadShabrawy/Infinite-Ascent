using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody2D RB;
    public float speed;
    private UserManager userManager;
    private int damage;
 
    void Start()
    {
        userManager = UserManager.Instance;
        SetDamageBasedOnLevel();
        RB.velocity = transform.right * speed;
    }
    
    void SetDamageBasedOnLevel()
    {
        switch (userManager.DamageLevel)
        {
            case 1:
                damage = 2;
                break;
            case 2:
                damage = 5;
                break;
            case 3:
                damage = 10;
                break;
            case 4:
                damage = 15;
                break;
            case 5:
                damage = 20;
                break;
            default:
                damage = 2;
                break;
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
