using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 3f;
    private int damage;

    private GameObject damageVfxList1;
   
   

   
    public void SetDirection(Vector3 dir, int dmg, GameObject vfxList)
    {
        direction = dir;
        damage = dmg;
        damageVfxList1 = vfxList;
       

        // Rotate projectile to face movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            HeroStats data = other.GetComponent<HeroStats>();
            if (data != null)
            {
                data.TakeDamage(damage);
            }

            // Spawn damage VFX
            if (damageVfxList1 != null)
            {
               
                GameObject vfx = Instantiate(damageVfxList1, other.transform.position, Quaternion.identity);
                Destroy(vfx, 1f);
            }

            Destroy(gameObject);
        }
    }

    


}
