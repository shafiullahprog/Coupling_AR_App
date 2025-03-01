using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAnimation(string value)
    {
        animator.SetTrigger(value);
    }
}
