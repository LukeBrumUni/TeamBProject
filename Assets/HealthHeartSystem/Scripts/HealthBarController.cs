/*

 *  Author: ariel oliveira [o.arielg@gmail.com]


using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        // Should I use lists? Maybe :)
       // old code - heartContainers = new GameObject[(int)TestPlayerStats.Instance.MaxTotalHealth];
       // old code - heartFills = new Image[(int)TestPlayerStats.Instance.MaxTotalHealth];

       heartContainers = new GameObject[(int)PlayerStats.Instance.MaxHealth];
       heartFills = new Image[(int)PlayerStats.Instance.MaxHealth]; // possibly use the max health variable

        PlayerStats.Instance.onHealthChangedCallback += UpdateHeartsHUD;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < PlayerStats.Instance.MaxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < PlayerStats.Instance.currentHealth)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (PlayerStats.Instance.currentHealth % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(PlayerStats.Instance.currentHealth);
            heartFills[lastPos].fillAmount = PlayerStats.Instance.currentHealth % 1;
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < TestPlayerStats.Instance.MaxTotalHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
*/