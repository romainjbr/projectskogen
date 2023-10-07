using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    Player player;

    private int needleCount;
    [SerializeField] private TMP_Text needleText;

    private static UIManager instance;

    public static UIManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

            }
            return instance;
        }
    }

    [SerializeField] private Transform weaponsBar; 
    [SerializeField] private Transform[] weaponsBoxes;

    private Stack<GameObject> newWeapon = new Stack<GameObject>();

    [SerializeField] private WEAPON[] weaponTypes;
    [SerializeField] private GameObject[] weaponSprites;
    [SerializeField] private GameObject[] weaponSpritesSelected;

    private Dictionary<WEAPON, GameObject> weaponObjects = new Dictionary<WEAPON, GameObject>(); // Declare the weaponObjects dictionary



    public void AddNeedle()
    {
        needleCount++;
        needleText.text = needleCount.ToString();
    }

    public void AddWeaponInBar(WEAPON weapon)
    {
        int weaponIndex = (int)weapon;
        weaponSpritesSelected[weaponIndex].SetActive(false);
        weaponSprites[weaponIndex].SetActive(true);
    }

    public void UpdateWeapon(WEAPON weapon, bool selected)
    {
        int weaponIndex = (int)weapon;
        
        if (!selected && (weaponSpritesSelected[weaponIndex].activeSelf || weaponSprites[weaponIndex].activeSelf))
        {
            AddWeaponInBar(weapon);
        }
        else if (selected)
        {
            SwapWeaponInBar(weapon);
        }
    }

    public void SwapWeaponInBar(WEAPON weapon)
    {
        int weaponIndex = (int)weapon;
        weaponSprites[weaponIndex].SetActive(false);
        weaponSpritesSelected[weaponIndex].SetActive(true);
    }



}
