using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 게임에서의 백터 
 * 1. 위치
 * 2. 방향 → 거리 크기와 실제 방향을 알 수 있음
 */
struct MyVector
{
    public float x;
    public float y;
    public float z;

    // 거리 크기(피타고라스 공식)
    public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
    // 실제 방향
    public MyVector normalized { get { return new MyVector(x / magnitude, y / magnitude, z / magnitude); } }

    public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

    // 두 백터를 이용해서 덧셈을 했을 때 어떤 행동이 일어나는 지 정의
    public static MyVector operator +(MyVector a, MyVector b)
    {
        return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    // 두 백터를 이용해서 빼기를 했을 때 어떤 행동이 일어나는 지 정의
    public static MyVector operator -(MyVector a, MyVector b)
    {
        return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    // 백터에 수치를 곱했을 때 어떤 행동이 일어나는 지 정의
    public static MyVector operator *(MyVector a, float d)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }
    public static MyVector operator *(float d, MyVector a)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed;

    // float _yAngle = 0.0f;

    void Start()
    {
        /* Vector3 Test
        // 위치로써의 Vector 확인
        MyVector pos = new MyVector(0.0f, 10.0f, 0.0f);
        pos += new MyVector(0.0f, 2.0f, 0.0f);

        // 방향으로써의 Vector 확인
        MyVector to = new MyVector(10.0f, 0.0f, 0.0f);
        MyVector from = new MyVector(5.0f, 0.0f, 0.0f);
        MyVector dir = to - from; // (5.0f, 0.0f, 0.0f)

        dir = dir.normalized; // (1.0f, 0.0f, 0.0f)

        MyVector newPos = from + dir * _speed;
        */

        // 구독
        Managers.input.KeyAction -= OnKeyboard; // 중복 체크 대비용
        Managers.input.KeyAction += OnKeyboard;
    }

    /* GameObject (Player)
     * └ Transform
     * └ PlayerController (*)
     */
    void Update()
    {
        /* Rotation Test
        _yAngle += Time.deltaTime * _speed;

        // transform.rotation

        // 절대 회전값 지정
        // _yAngle 이라는 변수를 따로 만들어서 넣어야 할까? → 공식 문서(360도 넘어가면 이상해지니 따로 사용 X)
        transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // +, -, delta 값 사용
        transform.Rotate(new Vector3(0.0f, Time.deltaTime * _speed, 0.0f));

        transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f)); // Vector3 → Quaternion
        
         */

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

        /* 움직이는 지점을 쳐다보도록 수정
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        */


        /* 부드럽게 처리
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        */

        /* 이동 위치가 안 맞음 → 방향 변경하는 중간에 이동 → World 위치값으로 지정하는 편이 어색한 현상을 개선할 수 있음
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        */

        /* 프레임마다 키보드를 체크하여 구현한 방식 → 큰 규모의 게임인 경우 성능 부하 발생할 수도 있음 → 이벤트 방식으로 변경/
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        */
    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
