using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    //Movemant and camControl
    [Header("Movemant and CamControl")]
    [SerializeField] private float speed;
    private CharacterController characterController;
    [SerializeField] private Camera _camera_main;
    [SerializeField] private Transform target;
    [SerializeField] private float sensitivityCamY;
    [SerializeField] private float sensitivityCamX;
    private float rotationY;
    private float rotationX;
    //CameraShake
    [SerializeField] private Animator cameraShake;
    

    //Shooting
    [Header("Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioSource gunshotSound;
    [SerializeField] private Animator animator;
    private float lastShootTime = 2f;

    //children_sSwing
    public children_sSwing children_SSwing;

    //catapilt
    public Сatapult catapult;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _camera_main = Camera.main;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastShootTime >= 2f)
            {
                Shoot();
                lastShootTime = Time.time;
            }

        }

        if(Input.GetKeyDown(KeyCode.K)) //activate children_sSwing
        {
            children_SSwing.allowed = true;
        }

        if (Input.GetKeyDown(KeyCode.L)) //activate catapult
        {
            catapult.allowed = true;
        }
    }

    private void Move()
    {
        //Movemant
        Vector3 forward = _camera_main.transform.forward;
        Vector3 right = _camera_main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward = forward.normalized;
        right = right.normalized;

        Vector3 move = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;

        move = new Vector3(move.x * speed, -9.81f, move.z * speed);

        characterController.Move(move * Time.deltaTime);


        //CamControl
        rotationX += Input.GetAxis("Mouse Y") * sensitivityCamY;
        rotationX = Mathf.Clamp(rotationX, -89f, 89f); // Ограничиваем поворот
        rotationY += Input.GetAxis("Mouse X") * sensitivityCamX;

        _camera_main.transform.rotation = Quaternion.Euler(-rotationX, rotationY, 0);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, transform.localEulerAngles.z);

        _camera_main.transform.position = target.position;
    }

    private void Shoot()
    {
        Ray ray = _camera_main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            gunshotSound.Play();
            cameraShake.SetTrigger("Shake");
            animator.SetTrigger("Shoot");

            Vector3 direction = (hit.point - firePoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            rotation *= Quaternion.Euler(90f, 0f, 0f);

            bullet.transform.rotation = rotation;

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * 200f;
            }
            else
            {
                Debug.LogError("Пуля должна иметь компонент Rigidbody!");
            }
        }
        else
        {
            Debug.Log("Рейкаст не пересекает ничего.");
        }
    }
}
