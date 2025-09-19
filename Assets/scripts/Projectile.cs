using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 3f;
    private int damage;

    private GameObject damageVfxList;
    private int vfxLevel;

    public void SetDirection(Vector3 dir, int dmg, GameObject vfxList, int level)
    {
        direction = dir;
        damage = dmg;
        damageVfxList = vfxList;
        vfxLevel = level;

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
        if (other.CompareTag("Enemy"))
        {
            // Apply damage
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Spawn damage VFX
            if (damageVfxList != null )
            {
              
                GameObject vfx = Instantiate(damageVfxList, other.transform.position, Quaternion.identity);
                Destroy(vfx, 1.5f);
            }

            // Play DragonBones "Damage" animation, then return to "Idle"
            UnityArmatureComponent armature = other.GetComponent<UnityArmatureComponent>();
            if (armature != null)
            {
                Debug.Log("Playing 'Damage' animation...");
                armature.animation.Play("Damage", 1); // Play once

                // Remove existing COMPLETE listeners to avoid duplicates
                armature.RemoveEventListener(EventObject.COMPLETE, OnAnimationComplete);

                // Add listener for when "Damage" animation ends
                armature.AddEventListener(EventObject.COMPLETE, OnAnimationComplete);
            }

            Destroy(gameObject);
        }
    }

    private void OnAnimationComplete(string type, EventObject eventObject)
    {
        if (eventObject.animationState.name == "Damage")
        {
            GameObject go = eventObject.armature.display as GameObject;

            if (go != null)
            {
                UnityArmatureComponent armature = go.GetComponent<UnityArmatureComponent>();
                if (armature != null)
                {
                    Debug.Log("Returning to 'Idle' animation for " + go.name);
                    armature.animation.Play("Idle", 0);
                }
            }
            else
            {
                Debug.LogWarning("eventObject.armature.display is not a GameObject.");
            }
        }
    }


}
