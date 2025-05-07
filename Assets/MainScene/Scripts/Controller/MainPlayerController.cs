using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainScene
{
    public class MainPlayerController : MainBaseController
    {
        private Camera cam;
        private Rigidbody2D rb;

        protected override void Start()
        {
            base.Start();
            cam = Camera.main;
            rb = GetComponent<Rigidbody2D>();
        }

        protected override void HandleAction()
        {
            // --- 1) 이동 입력 ---
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            movementDirection = new Vector2(horizontal, vertical).normalized;

            // --- 2) 회전 : 이동 방향으로 ---
            if (movementDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                rb.MoveRotation(angle);     // 오른쪽을 기본으로 보는 스프라이트이므로 보정 각도 필요 없음
            }

            // --- 3) lookDirection은 선택 사항 (필요 없으면 제거) ---
            // 이동방향을 그대로 사용하거나, 정말 필요 없으면 아예 제거하세요
            lookDirection = movementDirection;
        }
    }

}