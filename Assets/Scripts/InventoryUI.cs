using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI paperText;

    // Start is called before the first frame update
    void Start()
    {
        paperText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdatePaperText(PlayerInventory playerInventory)
    {
        paperText.text = playerInventory.NumberOfPapers.ToString() + " / 3";
    }
}
