using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraPosition;
    public Vector2 center;
    public Vector2 mapSize;

    public float cameraMoveSpeed;
    private float height;
    private float width;

    public float shakeTime = 0.5f;
    public float shakeSpeed = 2.0f;
    public float shakeAmount = 5.0f;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
            transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);

        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }

    public IEnumerator Shake()
    {
        float elapsedTime = 0.0f;

        while(elapsedTime < shakeTime){
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * shakeAmount;
            transform.localPosition = Vector3.Lerp(transform.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
    } 
}