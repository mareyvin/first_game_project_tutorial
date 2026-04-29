using UnityEngine;


namespace Player {

    public class Player : MonoBehaviour {

        private Rigidbody2D rb;
        [SerializeField] private float movingSpeed = 5f;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();

        }
        private void FixedUpdate() {

            Vector2 inputVector = Vector2.zero;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                inputVector.y = 1f;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                inputVector.y = -1f;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                inputVector.x = -1f;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                inputVector.x = 1f;
            }

            inputVector = inputVector.normalized;

            rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
        }
    }
}