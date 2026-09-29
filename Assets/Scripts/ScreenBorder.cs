using UnityEngine;

public class ScreenBorder : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera targetCamera;

    [Header("Border Settings")]
    [SerializeField] private float thickness = 0.5f;

    private BoxCollider2D topBorder;
    private BoxCollider2D bottomBorder;
    private BoxCollider2D leftBorder;
    private BoxCollider2D rightBorder;

    private int lastScreenWidth;
    private int lastScreenHeight;
    private Vector2 lastCameraPosition;
    private float lastOrthographicSize;
    private float lastCameraAspect;

    private void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        if (targetCamera == null)
        {
            Debug.LogError(
                "ScreenBorder: No camera found."
            );

            return;
        }

        if (!targetCamera.orthographic)
        {
            Debug.LogError(
                "ScreenBorder requires an Orthographic Camera."
            );

            return;
        }

        CreateBorders();
        UpdateBorders();

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        CacheCameraSettings();
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = targetCamera.transform.position;

        if (Screen.width != lastScreenWidth ||
            Screen.height != lastScreenHeight ||
            (Vector2)cameraPosition != lastCameraPosition ||
            targetCamera.orthographicSize != lastOrthographicSize ||
            targetCamera.aspect != lastCameraAspect)
        {
            UpdateBorders();

            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            CacheCameraSettings();
        }
    }

    private void CacheCameraSettings()
    {
        lastCameraPosition = targetCamera.transform.position;
        lastOrthographicSize = targetCamera.orthographicSize;
        lastCameraAspect = targetCamera.aspect;
    }

    private void CreateBorders()
    {
        topBorder = CreateBorder("Top Border");
        bottomBorder = CreateBorder("Bottom Border");
        leftBorder = CreateBorder("Left Border");
        rightBorder = CreateBorder("Right Border");
    }

    private BoxCollider2D CreateBorder(string borderName)
    {
        GameObject border = new GameObject(borderName);

        border.transform.SetParent(transform, false);

        BoxCollider2D collider =
            border.AddComponent<BoxCollider2D>();

        // These are physical walls.
        collider.isTrigger = false;

        return collider;
    }

    private void UpdateBorders()
    {
        if (targetCamera == null)
            return;

        if (!targetCamera.orthographic)
            return;

        // ==================================================
        // GET EXACT CAMERA VIEWPORT SIZE
        // ==================================================

        float height =
            targetCamera.orthographicSize * 2f;

        float width =
            height * targetCamera.aspect;

        Vector3 cameraPosition =
            targetCamera.transform.position;

        float left =
            cameraPosition.x - width * 0.5f;

        float right =
            cameraPosition.x + width * 0.5f;

        float bottom =
            cameraPosition.y - height * 0.5f;

        float top =
            cameraPosition.y + height * 0.5f;

        // ==================================================
        // TOP
        // Exactly at the top edge of the camera.
        // ==================================================

        topBorder.transform.position = new Vector3(
            cameraPosition.x,
            top + thickness * 0.5f,
            0f
        );

        topBorder.size = new Vector2(
            width + thickness * 2f,
            thickness
        );

        // ==================================================
        // BOTTOM
        // Exactly at the bottom edge of the camera.
        // ==================================================

        bottomBorder.transform.position = new Vector3(
            cameraPosition.x,
            bottom - thickness * 0.5f,
            0f
        );

        bottomBorder.size = new Vector2(
            width + thickness * 2f,
            thickness
        );

        // ==================================================
        // LEFT
        // Exactly at the left edge of the camera.
        // ==================================================

        leftBorder.transform.position = new Vector3(
            left - thickness * 0.5f,
            cameraPosition.y,
            0f
        );

        leftBorder.size = new Vector2(
            thickness,
            height + thickness * 2f
        );

        // ==================================================
        // RIGHT
        // Exactly at the right edge of the camera.
        // ==================================================

        rightBorder.transform.position = new Vector3(
            right + thickness * 0.5f,
            cameraPosition.y,
            0f
        );

        rightBorder.size = new Vector2(
            thickness,
            height + thickness * 2f
        );
    }
}