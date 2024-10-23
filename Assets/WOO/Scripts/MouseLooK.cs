using Unity.Mathematics;
using UnityEngine;


    public class MouseLooK : MonoBehaviour
    {
        #region

        public Transform player;

        // 마우스 움직임 보정값
        [SerializeField] private float sensivity = 100f;

        // 
        [SerializeField] private float maxX = 20f;
        [SerializeField] private float minX = -90f;
        private float rotateX = 0f;     // 카메라 x축 회전 값 

        #endregion

        void Start ()
        {
            // 마우스 커서 제어
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // 좌우입력받아 플에이어를 좌우로 회전 
            float mouseX = Input.GetAxis("Mouse X") * sensivity;
            player.Rotate(Vector3.up * mouseX * Time.deltaTime);

            // 위아래 입력받아 위아래로 회전
            float mouseY = Input.GetAxis("Mouse Y") * sensivity;

            rotateX -= mouseY * Time.deltaTime;
            rotateX = Mathf.Clamp(rotateX,minX,maxX);
            this.transform.localRotation = Quaternion.Euler(rotateX,0f, 0f);
        }





    }
