using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageUI : MonoBehaviour
{

    public static ManageUI Instance;

    [SerializeField]
    private Button button1, button2;

    [SerializeField]
    private GameObject imageObj;

    public GameObject dialogueObj, todolistObj;

    [SerializeField]
    Sprite[] dialogueSprites, todoListSprites;

    private Image img;
    private int clickCount;

    private void Awake()
    {
        /// Singleton
        if (Instance != null)
            Debug.Log("Creating another instance (shouldn't happen)");
        else
        {
            Instance = this;
        }

        // Hide cursor
        clickCount = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        button1.interactable = false;
        button2.interactable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueObj.SetActive(false);
        img = imageObj.GetComponent<Image>();
        img.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse click on any dialogue except the one with 2 buttons
        if (Input.GetMouseButtonDown(0) && dialogueObj.activeSelf) OnClickButtonOne();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            dialogueObj.SetActive(true);
            img.sprite = dialogueSprites[0];
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            ManageActivities.Instance.ResetProductPositions();
            clickCount = 0;
            ManageActivities.Instance.cookingStep = 0;
            ManageActivities.Instance.ResetCamera();
            ManageActivities.Instance.ResetProductPositions();
            RemoveImage();
            dialogueObj.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    // Functions for top and bottom button clicks
    public void OnClickButtonOne() {
        clickCount++;
        switch (clickCount) {
            case 1:
                img.sprite = dialogueSprites[1];
                button1.interactable = true;
                button2.interactable = true;
                break;
            case 2:
                img.sprite = dialogueSprites[2];
                button1.interactable = false;
                button2.interactable = false;
                break;
            case 3:
                ManageActivities.Instance.RunCookingStep();
                RemoveImage();
                dialogueObj.SetActive(false);
                break;
        }
    }
    public void OnClickButtonTwo()
    {
        RemoveImage();
        dialogueObj.SetActive(false);
        clickCount = 0;
    }

    // Remove image
    private void RemoveImage() {
        img.sprite = null;
    }

    public void SetFourthImage() {
        print("fourth image setting");
        dialogueObj.SetActive(true);
        img.sprite = dialogueSprites[3];
    }
}
