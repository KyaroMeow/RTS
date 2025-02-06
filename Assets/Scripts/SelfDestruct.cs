using Unity.VisualScripting;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destructionDelay = 2.0f; // Время задержки перед уничтожением в секундах
    
    void Start()
    {
        
        Destroy(gameObject, destructionDelay);
    }

}
