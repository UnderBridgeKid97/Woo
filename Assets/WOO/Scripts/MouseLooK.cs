using Unity.Mathematics;
using UnityEngine;


    public class MouseLooK : MonoBehaviour
    {
        #region

        public Transform player;

        // ���콺 ������ ������
        [SerializeField] private float sensivity = 100f;

        // 
        [SerializeField] private float maxX = 20f;
        [SerializeField] private float minX = -90f;
        private float rotateX = 0f;     // ī�޶� x�� ȸ�� �� 

        #endregion

        void Start ()
        {
            // ���콺 Ŀ�� ����
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // �¿��Է¹޾� �ÿ��̾ �¿�� ȸ�� 
            float mouseX = Input.GetAxis("Mouse X") * sensivity;
            player.Rotate(Vector3.up * mouseX * Time.deltaTime);

            // ���Ʒ� �Է¹޾� ���Ʒ��� ȸ��
            float mouseY = Input.GetAxis("Mouse Y") * sensivity;

            rotateX -= mouseY * Time.deltaTime;
            rotateX = Mathf.Clamp(rotateX,minX,maxX);
            this.transform.localRotation = Quaternion.Euler(rotateX,0f, 0f);
        }





    }
