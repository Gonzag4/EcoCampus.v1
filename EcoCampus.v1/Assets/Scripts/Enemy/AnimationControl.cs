using UnityEngine;

public class AnimationControl : MonoBehaviour
{

    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
