using System.Collections;
using UnityEngine;


namespace the_dervish
{
    public class CameraManager : SingletonMonoBehaviour<CameraManager>
    {
        private CameraController cameraController;

        private Coroutine routine;
        public CameraController CAM_CONTROLLER
        {
            get
            {
                if (cameraController == null)
                {
                    cameraController = GameObject.FindFirstObjectByType<CameraController>();

                }
                return cameraController;
            }


        }

        IEnumerator _CamShake(float sec)
        {

            CAM_CONTROLLER.TriggerCamera(CameraTrigger.Shake);
            yield return new WaitForSeconds(sec);
            CAM_CONTROLLER.TriggerCamera(CameraTrigger.Default);

        }

        public void ShakeCamera(float sec)
        {

            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(_CamShake(sec));
        }


    }

}

