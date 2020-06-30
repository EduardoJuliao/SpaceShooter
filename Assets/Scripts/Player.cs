using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime converts to real human time
        // Equivalent of incorporating real time
        transform.Translate(Vector3.right * (_speed * Time.deltaTime));
    }
}