using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float Seconds;
    
	void Start ()
    {
        Invoke("Destroy", Seconds);
	}

    void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
