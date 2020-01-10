using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public List<Transform> Positions;
    public float Speed;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Positions[index].position, Speed);
        if(Vector2.Distance(transform.position, Positions[index].position) < 0.5f)
        {
            index++;
            index = index % Positions.Count;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
