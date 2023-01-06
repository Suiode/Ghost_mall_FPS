using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField] Transform gameplayMenuPosition;
    [SerializeField] MenuCameraMoving gameplayReturnScript;
    [SerializeField] Transform audioMenuPosition;
    [SerializeField] MenuCameraMoving audioReturnScript;
    [SerializeField] Transform videoMenuPosition;
    [SerializeField] MenuCameraMoving videoReturnScript;
    [SerializeField] Camera camera;


    

    public void BackToDefaultPos(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (camera.transform.position == videoMenuPosition.position)
            {
                videoReturnScript.MoveCameraToNewPos();
            }
            else if (camera.transform.position == gameplayMenuPosition.position)
            {
                gameplayReturnScript.MoveCameraToNewPos();
            }
            else if (camera.transform.position == audioMenuPosition.position)
            {
                audioReturnScript.MoveCameraToNewPos();
            }
        }
    }

    public void ButtonToGoBack()
    {
        if (camera.transform.position == videoMenuPosition.position)
        {
            videoReturnScript.MoveCameraToNewPos();
        }
        else if (camera.transform.position == gameplayMenuPosition.position)
        {
            gameplayReturnScript.MoveCameraToNewPos();
        }
        else if (camera.transform.position == audioMenuPosition.position)
        {
            audioReturnScript.MoveCameraToNewPos();
        }
    }
}
