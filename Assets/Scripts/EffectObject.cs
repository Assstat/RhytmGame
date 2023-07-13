using UnityEngine;

public class EffectObject : MonoBehaviour
{

    public float lifetime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
