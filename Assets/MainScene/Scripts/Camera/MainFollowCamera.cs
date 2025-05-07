using UnityEngine;

namespace MainScene
{
    public class FollowCamera : MonoBehaviour
    {
        [Header("Target")]
        public Transform target;

        [Header("Zoom Settings")]
        public float zoomSpeed = 2f;
        public float minZoom = 2f;
        public float maxZoom = 10f;

        private float offsetX = 0f;
        private float offsetY = 0f;

        private bool initialized = false;
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;

            // target이 비어 있다면 Player 자동 할당
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    SetTarget(player.transform);
                }
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            RefreshOffset();
        }

        public void RefreshOffset()
        {
            if (target != null)
            {
                offsetX = transform.position.x - target.position.x;
                offsetY = transform.position.y - target.position.y;
                initialized = true;
            }
        }

        void Update()
        {

            if (target == null)
                return;

            Vector3 pos = transform.position;
            pos.x = target.position.x + offsetX;
            pos.y = target.position.y + offsetY;
            transform.position = pos;

            HandleZoom();
        }


        private void LateUpdate()
        {
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    SetTarget(player.transform);
                }
            }

            if (target == null || !initialized)
            {
                return;
            }

            Vector3 pos = transform.position;
            pos.x = target.position.x + offsetX;
            pos.y = target.position.y + offsetY;
            transform.position = pos;

            HandleZoom();
        }



        private void HandleZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                cam.orthographicSize -= scroll * zoomSpeed;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            }
        }
    }
}
