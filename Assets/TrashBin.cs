using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType acceptedType;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger kena");

        Trash trash =
            other.GetComponent<Trash>();

        if(trash!=null)
        {
            Debug.Log("Sampah masuk");

            if(trash.type==acceptedType)
            {
                Debug.Log("BENAR");
                ScoreManager.instance.AddScore(1);
            }

            else
            {
                Debug.Log("SALAH");
            }

            Destroy(other.gameObject);
        }
    }
}