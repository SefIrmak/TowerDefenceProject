using UnityEngine;
using PathCreation; 

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float movedSpeed = 5f;
    float distanceTravalled;

    // Update is called once per frame
    void Update()
    {
        distanceTravalled += movedSpeed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravalled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravalled);
    }
}
