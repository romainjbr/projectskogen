using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HairCount : MonoBehaviour, ICollisionHandler
{

    public Animator animator;
    public Animator animator2;

    [SerializeField] private TextMeshProUGUI displayedCount;

    private int HCount = 0;

    private static HairCount instance;
    public static HairCount Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HairCount>();

            }
            return instance;
        }
    }

    public void IncreaseHair()
    {
        HCount++;
        displayedCount.text = HCount.ToString();
        displayedCount.text = HCount + " / 3";
        animator.SetBool("Collected", true);
        animator2.SetBool("moving", true);

    }

    public void SetFalse()
    {
        animator.SetBool("Collected", false);
        animator2.SetBool("moving", false);

    }


}
