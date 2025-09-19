using UnityEngine;

public class Castleblast : MonoBehaviour
{
    public Castle castleRef;         // Reference to Castle.cs script
    public Animator animator;        // Animator with only one animation
    private bool hasPlayed = false;

    void Update()
    {
        if (castleRef != null && castleRef.isDead && !hasPlayed)
        {
            hasPlayed = true;
            animator.Play(0); // Play the first and only animation in the Animator
        }
    }
}
