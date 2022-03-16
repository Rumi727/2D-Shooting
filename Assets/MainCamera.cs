using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MainCamera : MonoBehaviour
{
    [SerializeField] new Camera camera;
    [SerializeField] Slider cameraModeSlider;

    public static float cameraZoom = 1;
    public static byte cameraMode = 0;
    static Vector3 tempPosLerp = Vector3.zero;
    void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            camera.orthographicSize = Screen.height * 0.5f;
            return;
        }
#endif

        if (Time.timeScale <= 0)
            return;

        cameraMode = (byte)cameraModeSlider.slider.value;

        if (cameraMode == 1)
        {
            tempPosLerp = Vector3.Lerp(tempPosLerp, Player.instance.transform.position, 0.125f * GameManager.fpsDeltaTime);
            transform.position = Player.instance.transform.position - tempPosLerp + Player.instance.transform.position + (Vector3.back * 14);
        }
        else if (cameraMode == 2)
        {
            transform.position = Vector3.Lerp(transform.position, ScreenMove(1 / cameraZoom * 0.5f) + (Vector3.back * 14), 0.125f * GameManager.fpsDeltaTime);
            tempPosLerp = transform.position;
        }
        else if (cameraMode == 3)
        {
            transform.position = Vector3.Lerp(transform.position, ScreenMove(1 / cameraZoom) + (Vector3.back * 14), 0.125f * GameManager.fpsDeltaTime);
            tempPosLerp = transform.position;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position + (Vector3.back * 14), 0.125f * GameManager.fpsDeltaTime);
            tempPosLerp = transform.position;
        }

        if (Player.instance.hp < 0)
            camera.orthographicSize = Screen.height * 0.5f * (cameraZoom = Mathf.Lerp(cameraZoom, Player.speed * 0.6666666667f, 0.125f * GameManager.fpsDeltaTime));
        else
            camera.orthographicSize = Screen.height * 0.5f * (cameraZoom = Mathf.Lerp(cameraZoom, Player.speed, 0.125f * GameManager.fpsDeltaTime));
    }

    public static Vector3 ScreenMove(float moveSize)
    {
        Vector2 playerPos = Player.instance.transform.position;
        Vector2 temp = Vector2.zero;

        if (((playerPos.x / moveSize) - (Screen.width * 0.5f)) / Screen.width > 0)
        {
            for (int i = 0; i < Mathf.Abs(Mathf.Floor(((playerPos.x / moveSize) + (Screen.width * 0.5f)) / Screen.width)); i++)
                temp.x += Screen.width * moveSize;
        }
        else
        {
            for (int i = 0; i < Mathf.Abs(Mathf.Ceil(((playerPos.x / moveSize) - (Screen.width * 0.5f)) / Screen.width)); i++)
                temp.x += -Screen.width * moveSize;
        }

        if (((playerPos.y / moveSize) - (Screen.height * 0.5f)) / Screen.height > 0)
        {
            for (int i = 0; i < Mathf.Abs(Mathf.Floor(((playerPos.y / moveSize) + (Screen.height * 0.5f)) / Screen.height)); i++)
                temp.y += Screen.height * moveSize;
        }
        else
        {
            for (int i = 0; i < Mathf.Abs(Mathf.Ceil(((playerPos.y / moveSize) - (Screen.height * 0.5f)) / Screen.height)); i++)
                temp.y += -Screen.height * moveSize;
        }

        return temp;
    }
}
