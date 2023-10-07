using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageBoxScript : MonoBehaviour, ICollisionHandler
{

    public Animator animator;

    [SerializeField] private TextMeshProUGUI BoxText;

    public bool BoxShowing = false;

    private static MessageBoxScript instance;
    public static MessageBoxScript Instance  
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MessageBoxScript>();

            }
            return instance;
        }
    }


    public void QuitBox(){
        if (BoxShowing == true) {
            BoxShowing = false;
            animator.SetBool("Box_Appear", false);
            animator.SetBool("QuitBox", true);
            }
        }

    public void BoxActive(){
        BoxShowing = true;
        animator.SetBool("Box_Appear", false);

    }

    public void BoxFallingActive(){
        animator.SetBool("QuitBox", false);
    }

    public void WhichBox(string boxName)
    {
        if (boxName == "NeedleBox")
        {
            BoxText.text = "A needle... A witch must have dropped it. They must be around...";
        }

        if (boxName == "BreakBox")
            {
                BoxText.text = "Let's get the Stick by pressing 2, and hit the box by pressing K";
            }

        if (boxName == "TreeBox")
            {
                BoxText.text = "This tree looks quite unstable... let's try and push it by pressing P";

            }
        
        if (boxName == "PushBox")
            {
                BoxText.text = "Maybe I could reach this platform up there by pushing the wagon a tad closer...";
            }
        animator.SetBool("Box_Appear", true);      
    }
}
