using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public bool startExisting = true;

    public bool animated = false;

    public bool alwaysExist = false;

    public string introAnimation = "";
    public string outroAnimation = "";
    public void Exist()
    {
        gameObject.SetActive(true);
        //animator.wrapMode = WrapMode.Once;
        if (animated)
        {
            animator.Play(introAnimation);
        }
       
    }

    public void JumpTo (float time)
    {
        animator.Play(introAnimation, 0, time);
    }


    public void JumpTo(string clip)
    {
        animator.Play(clip);
    }


    public void StopExisting()
    {      
        if (animated)
        {
            StartCoroutine("exitAnimation");
        }
        else
        {
            if (!alwaysExist)
            {
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator exitAnimation ()
    {
        /*animator.PlayQueued("Something");
        yield WaitForAnimation(animator);*/
        animator.Play(outroAnimation);
        yield return new WaitForEndOfFrame();

        float timeForAnimation = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeForAnimation);
        if (!alwaysExist)
        {
            gameObject.SetActive(false);
        }
    }

    private Animator animator;

    void Start()
    {
        if (animated)
        {
            animator = GetComponent<Animator>() as Animator;
        }
        if (!startExisting)
        {
            gameObject.SetActive(false);
        }
	}
	


	// Update is called once per frame
	void Update () {
		
	}
}
