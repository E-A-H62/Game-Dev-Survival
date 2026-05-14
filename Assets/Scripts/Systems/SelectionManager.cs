using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance {get;set;}

    public bool onTarget;
 
    public TextMeshProUGUI interaction_Info_UI;
    TextMeshProUGUI interaction_text;
 
    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // can only pick up objects if it has second box collider
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();
 
            if (interactable && interactable.playerInRange)
            {
                onTarget = true;

                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.enabled = true;
            }
            else 
            { 
                onTarget = false;
                interaction_Info_UI.enabled = false;
            }
        }
        else
        {
            onTarget = false;
            interaction_Info_UI.enabled = false;
        }
    }
}