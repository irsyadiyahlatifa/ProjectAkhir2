using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Trash Settings")]
    public Transform holdPoint;
    public Transform throwPoint;
    public Transform target;
    public Transform target2;
    public Transform target3;

    private Transform currentTrash;
    private bool isHolding = false;
    private bool isThrowing = false;

    private float t = 0;

    void Update()
    {
        Move();

        // lempar pakai SPACE
        if (isHolding && Input.GetKeyDown(KeyCode.Space))
        {
            ThrowTrash();
        }

        if (isThrowing)
        {
            MoveTrashToTarget();
        }

        // kalau lagi pegang → ikut tangan
        if (isHolding && currentTrash != null)
        {
            currentTrash.position = holdPoint.position;
        }
    }

    // ================= MOVE =================
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        transform.position += dir * moveSpeed * Time.deltaTime;

        if (dir != Vector3.zero)
        {
            transform.LookAt(transform.position + dir);
        }
    }

    // ================= AUTO PICK =================
    private void OnTriggerEnter(Collider other)
{
    Debug.Log("Nabrak: " + other.name);

    if (!isHolding && !isThrowing)
    {
        if (other.CompareTag("Trash"))
        {
            Debug.Log("Ambil sampah!");

            currentTrash = other.transform;

            Rigidbody rb = currentTrash.GetComponent<Rigidbody>();
            rb.isKinematic = true;

            isHolding = true;
        }
    }
}

    // ================= THROW =================
    void ThrowTrash()
    {
        isHolding = false;
        isThrowing = true;
        t = 0;
    }

    // ================= ARC THROW =================
    void MoveTrashToTarget()
{
    if (currentTrash == null) return;

    t += Time.deltaTime;
    float duration = 0.7f;
    float t01 = t / duration;

    Vector3 A = throwPoint.position;

    // 🔥 tentukan target sesuai jenis sampah
    Trash trash = currentTrash.GetComponent<Trash>();
    Transform selectedTarget = target;

    if (trash != null)
    {
        if (trash.type == TrashType.Organik)
        {
            selectedTarget = target;
        }
        else if (trash.type == TrashType.Plastik)
        {
            selectedTarget = target2;
        }
        else if (trash.type == TrashType.B3)
        {
            selectedTarget = target3;
        }
    }

    Vector3 B = selectedTarget.position;

    Vector3 pos = Vector3.Lerp(A, B, t01);
    Vector3 arc = Vector3.up * 3 * Mathf.Sin(t01 * Mathf.PI);

    currentTrash.position = pos + arc;

    if (t01 >= 1)
    {
        isThrowing = false;

        Rigidbody rb = currentTrash.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        currentTrash = null;
    }
}
    }
