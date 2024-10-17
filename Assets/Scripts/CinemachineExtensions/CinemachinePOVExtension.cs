using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    public float horizontalSpeed = 10f;
    public float verticalSpeed = 10f;
    public float clampAngle = 80f;
    public float cameraSmoothness = 0.9f;

    public InputManager inputManager;
    private Vector3 startingRotation;

    private bool isInRuntime;

    protected override void Awake()
    {
        startingRotation = transform.localEulerAngles;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!isInRuntime) return;

        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                var destRotation = startingRotation;
                Vector3 deltaInput = inputManager.GetLook();
                destRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                destRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;

                destRotation.y = Mathf.Clamp(destRotation.y, -clampAngle, clampAngle);

                startingRotation = Vector2.Lerp(startingRotation, destRotation, cameraSmoothness);

                if (startingRotation.x > 360)
                    startingRotation.x -= 360;
                if (startingRotation.x < -360)
                    startingRotation.x += 360;

                var rotation = new Vector3(-startingRotation.y, startingRotation.x, 0f);
                inputManager.UpdateCameraRotation(rotation);

                state.RawOrientation = Quaternion.Euler(rotation);
            }
        }
    }

    protected override void OnEnable()
    {
        isInRuntime = true;
    }

    public void OnDisable()
    {
        isInRuntime = false;
    }
}
