using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    /* Collision 발동 조건
     * 1) 나한테 RigidBody가 있어야 한다 (IsKinematic : Off)
     * 2) 나한테 Collider가 있어야 한다 (IsTrigger : Off)
     * 3) 상대한테 Collider가 있어야 한다 (IsTrigger : Off)
     */
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name}!");
    }

    /* Trigger 발동 조건
     * 1) 둘 다 Collider가 있어야 한다
     * 2) 둘 중 하나는 IsTrigger : On
     * 3) 둘 중 하나는 RigidBody가 있어야 한다
     */
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger @ {other.gameObject.name}!");
    }

    private void Update()
    {
        /* 투영의 개념
         * Local(특정 물체 좌표계) <-> World(게임 세상 좌표계) <-> Viewport <-> Screen(화면/2D)

        Debug.Log(Input.mousePosition); // Screen 좌표(px)
        Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // Viewport 좌표(비율)
         */
        if (Input.GetMouseButtonDown(0))
        {
            /* 코드 단축
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = mousePos - Camera.main.transform.position;
            dir = dir.normalized;

            Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
             */

            /* Layer Mask
             * Raycast는 과부화주는 작업이기에 최적화 작업 필요
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
             */
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
            // int mask = (1 << 8) | (1 << 9); // 비트 플래그

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }

        /* Raycast Test
        Vector3 look = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        // 단일
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
        {
            Debug.Log($"Raycast {hit.collider.gameObject.name}!");
        }

        // 복수
        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);
        foreach(RaycastHit hit in hits)
        {
            Debug.Log($"Raycast {hit.collider.gameObject.name}!");
        }
        */
    }
    }
