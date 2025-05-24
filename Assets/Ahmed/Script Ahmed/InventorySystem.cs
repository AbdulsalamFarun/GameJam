using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]

    private bool isNotCollected = true;
    public List<InventoryItem> InventoryItems = new List<InventoryItem>(4);
    public List<Image> inventorySlotImages = new List<Image>();
    public TextMeshProUGUI take;
    public TextMeshProUGUI TotalText;
    public TextMeshProUGUI CurrentText;
    public GameObject pickupPanel;
    public int Currentpoints;
    public int Totalpoints;
    public float startTime = 180f;
    private float currentTime;
    private float countTime;
    public TextMeshProUGUI timerText;

    private bool timerRunning = true;
    SoundManager SoundManager;










    private void Start()
    {
        SoundManager = SoundManager.Instance;

        if (SoundManager ==null)
        {
            Debug.Log("No Sound");
        }

        currentTime = startTime;
        TotalText.text = $"Total: {Totalpoints}";
        CurrentText.text = $"Point: {Currentpoints}";


    }

    private void Update()
    {
        UpdateInventoryUI();
        CheckForItems();

        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                timerRunning = false;
                TimerEnded();
            }

            UpdateTimerDisplay();
        }
    } 






    void UpdateTimerDisplay()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
    }

    void TimerEnded()
    {
        Debug.Log("الوقت انتهى!");
    }

    public void countPoints(ItemData data) {
        InventoryItem newItem = new InventoryItem(data);

        Currentpoints += newItem.itemData.value;





    }

    public void TotalPoints(ItemData data)
    {
        InventoryItem newItem = new InventoryItem(data);

        Totalpoints += Currentpoints;




    }


    IEnumerator incrasePointsOneByOne()
    {
        int Currentval = Currentpoints;
        while (Currentval >= 0)
        {
            Currentval--;
           Totalpoints++;
            if (Currentval % 5 == 0)
            {
                SoundManager.PlaySFX(SoundManager.Point);
            }
            TotalText.text = "Total: " + Totalpoints;
            yield return new WaitForSeconds(0.01f);
            

        }

        

    }


    IEnumerator ReducePointsOneByOne()
    {
        while (Currentpoints > 0)
        {
            Currentpoints--;
            Debug.Log(Currentpoints);
            CurrentText.text = "Points: " + Currentpoints.ToString(); 
            yield return new WaitForSeconds(0.01f); 
        }
    }




    public void AddToInventory(ItemData data)
    {
        InventoryItem newItem = new InventoryItem(data);



        if (data == null || string.IsNullOrEmpty(data.itemName) || data.icon == null)
        {
            Debug.LogError("ItemData is missing values!");
            return;
        }
        InventoryItems.Add(newItem);

        Debug.Log($"{data.itemName} added to inventory.");
        UpdateInventoryUI();
        pickupPanel.SetActive(false);
        take.text = "";

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    ItemData data = other.GetComponent<ItemData>();

    //    if (other.CompareTag("Item"))
    //    {

    //        if (isNotCollected)
    //        {
    //            if (InventoryItems.Count < 5)
    //            {
    //                take.color = Color.white;

    //                take.text = $"Press E to take {data.itemName}";
    //                pickupPanel.SetActive(true);
    //                Debug.Log("i");
    //            }
    //            else
    //            {
    //                take.color = new Color32(255, 0, 0, 255);

    //                take.text = "Inventory is full";
    //                pickupPanel.SetActive(true);

    //            }
    //        }
    //        Debug.LogWarning($"Press E to take {data.itemName}");

    //        if (Input.GetKeyDown(KeyCode.E) && isNotCollected)
    //        {
    //            if (InventoryItems.Count < 5)
    //            {
    //                isNotCollected = false;

    //                Debug.Log(data);
    //                if (data != null)
    //                {
    //                    Debug.Log(data);

    //                    AddToInventory(data);

    //                }
    //                else
    //                {
    //                    Debug.LogWarning("No ItemData found on the object.");
    //                }
    //                countPoints(data);
    //                CurrentText.text = $"Point: {Currentpoints}";

    //                countTime += data.time;
    //                pickupPanel.SetActive(false);
    //                take.text = "";

    //                Destroy(other.gameObject);

    //                Invoke("ResetIsNotCollected", 1f);
    //            }
    //            else
    //            {
    //                Debug.Log("no item");
    //            }
    //        }

    //    }
    //    if (other.CompareTag("RatHome"))
    //    {
    //        take.color = Color.white;

    //        take.text = $"Press E to Store";
    //        pickupPanel.SetActive(true);
    //        if (Input.GetKeyDown(KeyCode.E) && InventoryItems.Count != 0)
    //        {
    //            currentTime += countTime;
    //            Debug.Log(InventoryItems.Count);
    //            Debug.Log(InventoryItems);
    //            SoundManager.Instance.SFXSource.pitch = 1;
    //            StartCoroutine(ReducePointsOneByOne());
    //            StartCoroutine(incrasePointsOneByOne());

    //            for (int i = InventoryItems.Count; i >= 0; i--)
    //            {
    //                InventoryItems.RemoveAt(0);
    //            }

    //        }
    //    }
    //}
    void CheckForItems()
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4f)) 
        {
            if (hit.collider.CompareTag("Item"))
            {
                ItemData data = hit.collider.GetComponent<ItemData>();

                if (isNotCollected)
                {
                    if (InventoryItems.Count < 5)
                    {
                        take.color = Color.white;

                        take.text = $"Press E to take {data.itemName}";
                        pickupPanel.SetActive(true);
                        Debug.Log("i");
                    }
                    else
                    {
                        take.color = new Color32(255, 0, 0, 255);

                        take.text = "Inventory is full";
                        pickupPanel.SetActive(true);

                    }
                }
                Debug.LogWarning($"Press E to take {data.itemName}");

                if (Input.GetKeyDown(KeyCode.E) && isNotCollected)
                {
                    if (InventoryItems.Count < 5)
                    {
                        isNotCollected = false;

                        Debug.Log(data);
                        if (data != null)
                        {
                            Debug.Log(data);

                            AddToInventory(data);

                        }
                        else
                        {
                            Debug.LogWarning("No ItemData found on the object.");
                        }
                        countPoints(data);
                        CurrentText.text = $"Point: {Currentpoints}";

                        countTime += data.time;
                        pickupPanel.SetActive(false);
                        take.text = "";

                        Destroy(hit.collider.gameObject);

                        Invoke("ResetIsNotCollected", 1f);
                    }
                    else
                    {
                        Debug.Log("no item");
                    }
                }

            }
            else if (hit.collider.CompareTag("RatHome"))
            {

                take.color = Color.white;

                take.text = $"Press E to Store";
                pickupPanel.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && InventoryItems.Count != 0)
                {
                    currentTime += countTime;
                    Debug.Log(InventoryItems.Count);
                    Debug.Log(InventoryItems);
                    SoundManager.Instance.SFXSource.pitch = 1;
                    StartCoroutine(ReducePointsOneByOne());
                    StartCoroutine(incrasePointsOneByOne());

                    for (int i = InventoryItems.Count; i >= 0; i--)
                    {
                        InventoryItems.RemoveAt(0);
                    }

                }
            }






            else
            {
                pickupPanel.SetActive(false);
            }
        }
        else
        {
            pickupPanel.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pickupPanel.SetActive(false);
        take.text = "";

    }


    private void ResetIsNotCollected()
    {
        isNotCollected = true;
        Debug.Log("isNotCollected reset to true");
    }
    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlotImages.Count; i++)
        {
            if (i < InventoryItems.Count && InventoryItems[i] != null)
            {
                inventorySlotImages[i].sprite = InventoryItems[i].itemData.icon;
                inventorySlotImages[i].enabled = true;
            }
            else
            {
                inventorySlotImages[i].sprite = null;
                inventorySlotImages[i].enabled = false;
            }
        }
        


    }



}

