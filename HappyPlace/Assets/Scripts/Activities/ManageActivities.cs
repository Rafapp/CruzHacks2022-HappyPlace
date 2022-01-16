using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ManageActivities : MonoBehaviour
{
    public static ManageActivities Instance;

    [SerializeField]
    private CinemachineFreeLook gameCamera;

    [SerializeField]
    private GameObject cameraAngleObject, player, lettuceObject, tomatoObject, tomatoSliceObj, lettuceLeafObj;

    [SerializeField]
    private Texture2D knifeUp, knifeDown, handOpen, handClosed, lettuceTx, lettuceleafTx, tomatoTx, tomatosliceTx;

    [SerializeField]
    private int cookingStepsToDo;

    public int cookingStep;

    private void Awake()
    {
        
        if (Instance != null)
            Debug.Log("Creating another instance (shouldn't happen)");
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (cookingStep > 0 && cookingStep <= cookingStepsToDo)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RunCookingStep();
            }
            
        }
    }

    // A step is added, and the function runs
    public void RunCookingStep() {
        print($"Current step: {cookingStep}");
        cookingStep++;
        ManageUI.Instance.todolistObj.SetActive(true);
        switch (cookingStep)
        {
            // The game starts, we place tomato, give knife
            case 2:
                Cursor.SetCursor(knifeUp, new Vector2(0, 0), CursorMode.Auto);
                tomatoObject.transform.localPosition = new Vector3(4.11600018f, -0.416999996f, 2.78399992f);
                SetCameraAngle();
                break;
            // First click, knife chops tomato
            case 3:
                Cursor.SetCursor(knifeDown, new Vector2(0, 0), CursorMode.Auto);
                tomatoObject.SetActive(false);
                tomatoSliceObj.SetActive(true);
                Invoke("KnifeUp", .25f);
                Invoke("HandOpen", .35f);
                break;
            // Third click, tomato slice moves to bowl, lettuce spawns, give knife again
            case 4:
                tomatoSliceObj.transform.localPosition = new Vector3(2.602f, -0.426999986f, 3.04299998f);
                lettuceObject.transform.localPosition = new Vector3(4.12799978f, -0.180999994f, 2.88700008f);
                Cursor.SetCursor(knifeUp, new Vector2(0, 0), CursorMode.Auto);
                break;
            // Fourth click, knife chops lettuce, we give hand
            case 5:
                lettuceObject.SetActive(false);
                lettuceLeafObj.SetActive(true);
                Cursor.SetCursor(knifeDown, new Vector2(0, 0), CursorMode.Auto);
                Invoke("KnifeUp", .25f);
                Invoke("HandOpen", .35f);
                break;
            // Fifth click, Lettuce goes to bowl, we move camera to bowl, enable winscreen
            case 6:
                ManageUI.Instance.SetFourthImage();
                lettuceLeafObj.transform.localPosition = new Vector3(2.77999997f, -0.25999999f, 3.15100002f);
                Camera.main.transform.position = new Vector3(-3.8599999f, 2.27999997f, 0.0500000007f);
                Camera.main.transform.rotation = new Quaternion(0, 0.923879564f, -0.382683426f, 0);
                break;
            // Sixth click, we reset camera and we reset the game
            case 7:
                ManageUI.Instance.todolistObj.SetActive(false);
                tomatoSliceObj.SetActive(false);
                lettuceLeafObj.SetActive(false);
                ResetCamera();
                ResetCursor();
                ResetProductPositions();
                ManageUI.Instance.dialogueObj.SetActive(false);
                break;
        }

    }

    private void HandOpen() { Cursor.SetCursor(handOpen, new Vector2(0, 0), CursorMode.Auto); }
    private void KnifeUp() { Cursor.SetCursor(knifeUp, new Vector2(0, 0), CursorMode.Auto); }

    public void ResetProductPositions()
    {
        tomatoSliceObj.transform.localPosition = new Vector3(4.30200005f, -0.426999986f, 2.64199996f);
        lettuceLeafObj.transform.localPosition = new Vector3(4.33500004f, -0.25999999f, 2.65100002f);
        tomatoObject.transform.localPosition = new Vector3(3.19700003f, -0.416999996f, 3.00300002f);
        lettuceObject.transform.localPosition = new Vector3(3.07399988f, -0.180999994f, 2.75900006f);
    }

    private void SetCameraAngle() {
        print("setting cam angle");
        gameCamera.enabled = false;
        Camera.main.transform.position = new Vector3(-2.88000011f, 2.41000009f, 0.389999986f);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(22.0599976f, 180, 0));
    }
    public void ResetCamera() {
        gameCamera.enabled = true;
    }

    private void SetKnifeToCursor() {
        Cursor.SetCursor(knifeUp, new Vector2(0,0), CursorMode.Auto);
    }

    private void AnimateKnife() {
        Cursor.SetCursor(knifeDown, new Vector2(0, 0), CursorMode.Auto);
        Cursor.SetCursor(knifeUp, new Vector2(0, 0), CursorMode.Auto);
    }


    private void AnimateHand() {
        Cursor.SetCursor(handClosed, new Vector2(0, 0), CursorMode.Auto);
        Cursor.SetCursor(handOpen, new Vector2(0, 0), CursorMode.Auto);
    }

    private void ResetCursor() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
