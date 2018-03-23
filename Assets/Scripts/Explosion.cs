using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    private Animator animator;
    private int exitHash = 0;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        exitHash = Animator.StringToHash("Exit");
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Finished"))
        {
            Destroy(gameObject);
        }
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).fullPathHash);   
    }
}
