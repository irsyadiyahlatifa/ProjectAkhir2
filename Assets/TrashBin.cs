using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType acceptedType;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger kena");

        Trash trash = other.GetComponent<Trash>();

        if (trash != null)
        {
            Debug.Log("Ini sampah");

            if (trash.type == acceptedType)
            {
                Debug.Log("BENAR");
                ScoreManager.instance.AddScore(10);
            }
            else
            {
                Debug.Log("SALAH");
                ScoreManager.instance.AddScore(-5);
            }

            Destroy(other.gameObject);
        }
    }
}