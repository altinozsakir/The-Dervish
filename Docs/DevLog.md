















**2025-06-02**
 - State-Driven Cinemachine Camera added
 - Created Objects and Scripts:
   - Empty Object that keeps CameraControllerAnimator and CameraController Script.
   - State-Driven Camera that keeps two Cinemachine cameras one is Default, another is Shake.
     - Default will be default camera settings.
     - Shake Camera has same settings with default plus a noise profile for shaking
   - There are two triggers in CameraControllerAnimator for transition between states.
   -  Created CameraState Script was attached to States in the CameraControllerAnimator, it resets triggers.
   -  CameraContoller(Singleton) and CameraManager Scripts were added.
      -  CameraContoller keeps Animator to manage it.
      -  CameraManager keeps CameraContoller to manage it. For using camera in Scripts, CameraManager.Instance should be called.
