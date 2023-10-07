using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hair : MonoBehaviour, ICollectable

{
    public void Collect()
    {
        HairCount.Instance.IncreaseHair();
        Destroy(gameObject);

    }

    
}
