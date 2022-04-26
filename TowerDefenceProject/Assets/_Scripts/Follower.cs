using UnityEngine;
using PathCreation; 

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float movedSpeed = 5f;
    float distanceTravalled;
    
    void Awake()
    {
        pathCreator = GameObject.Find("Path").GetComponent<PathCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravalled += movedSpeed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravalled,end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravalled,end);       
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndLine"))
        {
             Destroy(this.gameObject);
        }
    }
}
