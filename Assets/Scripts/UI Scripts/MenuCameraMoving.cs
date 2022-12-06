using System.Collections;
using UnityEngine;

public class MenuCameraMoving : MonoBehaviour
{
    public Camera camera;
    public Transform[] newCamPosList;
    public float defaultChangeTime = 1f;
    [SerializeField] private bool cameraMoving = false;
    private float changeTime;
    

    public void MoveCameraToNewPos()
    {
        if(newCamPosList.Length != 0)
        StartCoroutine(MovingTheCam());
    }

    private IEnumerator MovingTheCam()
    {
        for (int i =0; i < newCamPosList.Length; i++)
        {
            Transform oldCamTransform = camera.transform;
            Vector3 oldCamPos = oldCamTransform.position;

            //float oldX = UnityEditor.TransformUtils.GetInspectorRotation(oldCamTransform).x;
            //float oldY = UnityEditor.TransformUtils.GetInspectorRotation(oldCamTransform).y;
            //float oldZ = UnityEditor.TransformUtils.GetInspectorRotation(oldCamTransform).z;

            //float newX = UnityEditor.TransformUtils.GetInspectorRotation(newCamPosList[i].transform).x;
            //float newY = UnityEditor.TransformUtils.GetInspectorRotation(newCamPosList[i].transform).y;
            //float newZ = UnityEditor.TransformUtils.GetInspectorRotation(newCamPosList[i].transform).z;



            float oldX = oldCamTransform.transform.localEulerAngles.x % 360;
            float oldY = oldCamTransform.transform.localEulerAngles.y % 360;
            float oldZ = oldCamTransform.transform.localEulerAngles.z % 360;

            float newX = newCamPosList[i].transform.localEulerAngles.x % 360;
            float newY = newCamPosList[i].transform.localEulerAngles.y % 360;
            float newZ = newCamPosList[i].transform.localEulerAngles.z % 360;
            Debug.Log("New rotation for camera: " + newCamPosList[i].localRotation);



            Vector3 oldCamRot = new Vector3(oldX, oldY, oldZ);
            Vector3 newCamRot = new Vector3(newX, newY, newZ);

            cameraMoving = true;
            changeTime = 0;


            while (cameraMoving)
            {
                yield return null;
                changeTime += Time.deltaTime;
                camera.transform.position = Vector3.Lerp(oldCamPos, newCamPosList[i].transform.position, (changeTime / (defaultChangeTime / newCamPosList.Length)));


                //Max time is divided by the number of positions in newCamPosList. Then current time is divided by that to make sure no matter how many positions we have, it always takes the same amount of time
                //camera.transform.eulerAngles = Vector3.Lerp(oldCamRot, newCamRot, (changeTime / (defaultChangeTime / newCamPosList.Length)));
                camera.transform.rotation = Quaternion.Lerp( Quaternion.Euler(oldCamRot), Quaternion.Euler(newCamRot), (changeTime / (defaultChangeTime / newCamPosList.Length)));
                



                if (changeTime >= (defaultChangeTime / newCamPosList.Length))
                {
                    cameraMoving = false;
                    changeTime = 0;
                    //Debug.Log("We're at: " + (i+1) + " out of: " + (newCamPosList.Length) + " and the camera rotation is: " + newCamRot);
                }
            }
        }

    }
}
