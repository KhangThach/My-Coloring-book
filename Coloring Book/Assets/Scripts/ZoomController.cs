using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomController : MonoBehaviour
{
    [SerializeField] private float xMin;

    [SerializeField] private float xMax;

    [SerializeField] private float yMin;

    [SerializeField] private float yMax;

    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    private Vector3 initialPosition;

    private float indexZoom;

    private bool isZooming;

    private void Start()
    {
        initialPosition = Camera.main.transform.position;

        indexZoom = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.GamePlayManager.CanInteract && !IsPointerOverUIObject())
        {

            if (Input.touchCount == 2 && !IsPointerOverUIObject())
            {
                LevelManager.Instance.GamePlayManager.Detect.SetCanDetect(false);
                isZooming = true;

                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                switch (touchOne.phase)
                {
                    case TouchPhase.Ended:

                        touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        break;
                }

                switch (touchZero.phase)
                {
                    case TouchPhase.Ended:

                        touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        break;
                }

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * 0.01f);
            }
            else
            {
                isZooming = false;
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject() && !isZooming)
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }


            if (Input.GetMouseButton(0) && !IsPointerOverUIObject() && !isZooming)
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                float distance = Vector3.Distance(touchStart, Camera.main.ScreenToWorldPoint(Input.mousePosition));

                Debug.Log(distance);

                if (distance >= 0.08f)
                {
                    LevelManager.Instance.GamePlayManager.Detect.SetCanDetect(false);
                }

                Camera.main.transform.position = GetPositionClamp(Camera.main.transform.position, direction);
            }

            if(Input.GetMouseButtonUp(0) && !IsPointerOverUIObject())
            {
                LevelManager.Instance.GamePlayManager.Detect.SetCanDetect(true);
            }

            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void zoom(float increment)
    {
        if(increment == 0)
        {
            return;
        }

        indexZoom = LevelManager.Instance.GamePlayManager.PictureManager.OnZoom(-increment);

        if(indexZoom < 0)
        {
            indexZoom = 0;
        }

        Camera.main.transform.position = GetPositionClamp(Camera.main.transform.position, Vector3.zero);

        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    public void Init()
    {
        Camera.main.transform.position = initialPosition;
    }

    public Vector3 GetPositionClamp(Vector3 current, Vector3 direction)
    {
        float k = 5;

        current.x = Mathf.Clamp(current.x + direction.x, initialPosition.x - k * indexZoom, initialPosition.x + k * indexZoom);

        current.y = Mathf.Clamp(current.y + direction.y, initialPosition.y - k * indexZoom, initialPosition.y +  k * indexZoom);

        return current;
    }
}