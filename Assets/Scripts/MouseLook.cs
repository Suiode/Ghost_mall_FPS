using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseLook : MonoBehaviour
{
    [Header("UI tools")]
    [SerializeField] bool linkedXY = true;
    public Slider sensXSlider;
    public Slider sensYSlider;
    public TMP_InputField sensXInput;
    public TMP_InputField sensYInput;
    public Camera playerCam;
    [SerializeField] Button linkButton;
    [SerializeField] Sprite linkEnabledImg;
    [SerializeField] Sprite linkDisabledImg;



    [Header("Player objects")]
    public Transform playerBody;
    public Transform eyes;


    public float yMouseSens = 100f;
    public float xMouseSens = 100f;
    float xRotation;
    float yRotation;

    [SerializeField] GameManager gameManager;


    // Start is called before the first frame update
    public void Start()
    {
        if(playerBody != null)
        { Cursor.lockState = CursorLockMode.Locked; }

        gameManager = FindObjectOfType<GameManager>();

        yMouseSens = gameManager.mouseYSens;
        xMouseSens = gameManager.mouseXSens;

        sensXInput.text = xMouseSens.ToString();
        sensYInput.text = yMouseSens.ToString();
        UpdateValueFromInputX();
        UpdateValueFromInputY();
        Debug.Log("Did this reload on the scene change?");
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * (xMouseSens) / 10 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * (yMouseSens) / 10 * Time.deltaTime;


        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (playerBody != null && eyes != null)
        {
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
            eyes.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }


    }



    public void UpdateValueFromSliderX()
    {
        float newSens = Mathf.Round(sensXSlider.value * 10) / 10;
        sensXSlider.value = newSens;
        sensXInput.text = newSens.ToString();
        gameManager.mouseXSens = newSens;
        xMouseSens = newSens;

        if (linkedXY)
        {
            sensYSlider.value = newSens;
            sensYInput.text = newSens.ToString();
            yMouseSens = newSens;
        }
    }

    public void UpdateValueFromInputX()
    {
        string newSens = string.Format("{0:0.##}", sensXInput.text);
        bool canParse = float.TryParse(newSens, out float newSensFloat);

        if (canParse)
        {
            sensXSlider.value = newSensFloat;
            sensXInput.text = newSensFloat.ToString();
            gameManager.mouseXSens = newSensFloat;
            xMouseSens = newSensFloat;

            if (linkedXY)
            {
                sensYSlider.value = newSensFloat;
                sensYInput.text = newSensFloat.ToString();
                yMouseSens = newSensFloat;
            }
        }

        

    }


    public void UpdateValueFromSliderY()
    {
        float newSens = Mathf.Round(sensYSlider.value * 10) / 10;
        sensYSlider.value = newSens;
        sensYInput.text = newSens.ToString();
        gameManager.mouseYSens = newSens;
        yMouseSens = newSens;

        if (linkedXY)
        {
            sensXSlider.value = newSens;
            sensXInput.text = newSens.ToString();
            xMouseSens = newSens;
        }
    }

    public void UpdateValueFromInputY()
    {
        string newSens = string.Format("{0:0.##}", sensYInput.text);
        bool canParse = float.TryParse(newSens, out float newSensFloat);

        if (canParse)
        {
            sensYSlider.value = newSensFloat;
            sensYInput.text = newSensFloat.ToString();
            gameManager.mouseYSens = newSensFloat;
            yMouseSens = newSensFloat;


            if (linkedXY)
            {
                sensXSlider.value = newSensFloat;
                sensXInput.text = newSensFloat.ToString();
                xMouseSens = newSensFloat;
            }
        }
    }

    public void ToogleLink()
    {
        if(linkedXY)
        {
            linkedXY = false;
            linkButton.image.sprite = linkDisabledImg;
        }
        else
        {
            linkedXY = true;
            UpdateValueFromInputX();
            linkButton.image.sprite = linkEnabledImg;
        }
    }



    public void SyncMouseWithGameManager()
    {

        gameManager = FindObjectOfType<GameManager>();

        yMouseSens = gameManager.mouseYSens;
        xMouseSens = gameManager.mouseXSens;

        sensXInput.text = xMouseSens.ToString();
        sensYInput.text = yMouseSens.ToString();
        UpdateValueFromInputX();
        UpdateValueFromInputY();
    }

}


