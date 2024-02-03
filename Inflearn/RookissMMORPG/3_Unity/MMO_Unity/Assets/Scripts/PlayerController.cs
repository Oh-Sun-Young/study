using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed;

    void Start()
    {
        
    }

    /* GameObject (Player)
     * └ Transform
     * └ PlayerController (*)
     */
    void Update()
    {
        /* 1프레임마다 이동하는 작업을 한 경우 너무 빠르게 이동
         * 이전 프레임과 지금 프레임의 시간 차이를 알 수 있는 Time.deltaTime 추가
         if (Input.GetKey(KeyCode.W))
             transform.position += new Vector3(0.0f, 0.0f, 1.0f);
         if (Input.GetKey(KeyCode.S))
             transform.position -= new Vector3(0.0f, 0.0f, 1.0f);
         if (Input.GetKey(KeyCode.A))
             transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
         if (Input.GetKey(KeyCode.D))
             transform.position += new Vector3(1.0f, 0.0f, 0.0f);
         */

        /* 너무 느려지기 때문에 또 다른 상수의 값을 곱해야 함 → 속도
         if (Input.GetKey(KeyCode.W))
             transform.position += new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime;
         if (Input.GetKey(KeyCode.S))
             transform.position -= new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime;
         if (Input.GetKey(KeyCode.A))
             transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;
         if (Input.GetKey(KeyCode.D))
             transform.position += new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;
         */

        /* 코드 개선 → 예약어 사용 → 가독성 증가, 실수 확률 감소 
         if (Input.GetKey(KeyCode.W))
             transform.position += new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * _speed;
         if (Input.GetKey(KeyCode.S))
             transform.position -= new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * _speed;
         if (Input.GetKey(KeyCode.A))
             transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * _speed;
         if (Input.GetKey(KeyCode.D))
             transform.position += new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * _speed;
         */

        /* Vector3.forward → Word 좌표 기준으로 이동 → Local 좌표 기준으로 이동하도록 변경
         * TransformDirection : Local → Word
         * transform.InverseTransformDirection : Word → Local
         if (Input.GetKey(KeyCode.W))
             transform.position += Vector3.forward * Time.deltaTime * _speed;
         if (Input.GetKey(KeyCode.S))
             transform.position += Vector3.back * Time.deltaTime * _speed;
         if (Input.GetKey(KeyCode.A))
             transform.position += Vector3.left * Time.deltaTime * _speed;
         if (Input.GetKey(KeyCode.D))
             transform.position += Vector3.right * Time.deltaTime * _speed;
         */

        /* Local 좌표 기준으로 개선을 한 후 Word 좌표 기준으로 변해주는 과정이 번거로울 경우
         * Translate : Local 좌표로 작성해도 OK
         if (Input.GetKey(KeyCode.W))
            transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
         if (Input.GetKey(KeyCode.S))
            transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
         if (Input.GetKey(KeyCode.A))
            transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
         if (Input.GetKey(KeyCode.D))
            transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
         */

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
    }
}
