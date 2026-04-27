using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Trash Settings")]
    public Transform holdPoint;
    public Transform throwPoint;

    public Transform target;   // Organik
    public Transform target2;  // Plastik
    public Transform target3;  // B3

    private Transform currentTrash;

    bool isHolding=false;
    bool isThrowing=false;

    float t;

    [Header("Camera")]
    public Transform cam;
    Vector3 camRotation;

    public int minAngel=-30;
    public int maxAngel=45;
    public int sensitivity=200;



    void Update()
    {
        Rotate();
        Move();

        if(isHolding && Input.GetKeyDown(KeyCode.Space))
        {
            ThrowTrash();
        }

        if(isHolding && currentTrash != null)
        {
            currentTrash.position = holdPoint.position;
        }

        if(isThrowing)
        {
            MoveTrashToTarget();
        }
    }



    void Move()
    {
        float h=Input.GetAxis("Horizontal");
        float v=Input.GetAxis("Vertical");

        Vector3 move=
            transform.forward*v+
            transform.right*h;

        transform.position +=
            move.normalized*
            moveSpeed*
            Time.deltaTime;
    }



    void Rotate()
    {
        float mouseX=Input.GetAxis("Mouse X");
        float mouseY=Input.GetAxis("Mouse Y");

        transform.Rotate(
            Vector3.up*
            mouseX*
            sensitivity*
            Time.deltaTime
        );

        camRotation.x -=
            mouseY*
            sensitivity*
            Time.deltaTime;

        camRotation.x=
            Mathf.Clamp(
                camRotation.x,
                minAngel,
                maxAngel
            );

        cam.localRotation=
            Quaternion.Euler(
                camRotation.x,
                0,
                0
            );
    }



    private void OnTriggerEnter(Collider other)
    {
        if(!isHolding && !isThrowing)
        {
            if(other.CompareTag("Trash"))
            {
                Debug.Log("Ambil Sampah");

                currentTrash=other.transform;

                Rigidbody rb=
                    currentTrash.GetComponent<Rigidbody>();

                if(rb!=null)
                {
                    rb.isKinematic=false;
                    rb.useGravity=false;
                }

                isHolding=true;
            }
        }
    }



    void ThrowTrash()
    {
        if(currentTrash==null)
            return;

        isHolding=false;
        isThrowing=true;
        t=0;
    }



    void MoveTrashToTarget()
    {
        if(currentTrash==null)
            return;

        t += Time.deltaTime;

        float duration=0.8f;
        float t01=t/duration;

        Vector3 A=throwPoint.position;

        Trash trash=
            currentTrash.GetComponent<Trash>();

        Transform selectedTarget=target;

        if(trash!=null)
        {
            if(trash.type==TrashType.Organik)
                selectedTarget=target;

            if(trash.type==TrashType.Plastik)
                selectedTarget=target2;

            if(trash.type==TrashType.B3)
                selectedTarget=target3;
        }

        Vector3 B=selectedTarget.position;

        Vector3 pos=
            Vector3.Lerp(A,B,t01);

        Vector3 arc=
            Vector3.up*
            4f*
            Mathf.Sin(
                t01*Mathf.PI
            );

        currentTrash.position=
            pos+arc;


        if(t01>=1f)
        {
            Debug.Log("Masuk target");

            isThrowing=true;

            // paksa tepat masuk bin
            currentTrash.position=B;

            Rigidbody rb=
                currentTrash.GetComponent<Rigidbody>();

            if(rb!=null)
            {
                // biarkan trigger tong detect dulu
                rb.isKinematic=true;
                rb.useGravity=false;
            }

            currentTrash=null;
        }
    }
}