using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movedSpeed = 10.0f;

    private Transform _targetTransform;
    private int wavePointIndex = 0;

    private void Start() {
        _targetTransform = Waypoints.points[0];
    }

    private void Update() {
        Vector3 dir = _targetTransform.position - transform.position;
        transform.Translate(dir.normalized * _movedSpeed* Time.deltaTime,Space.World);

        // Waypoints distance check
        if (Vector3.Distance(transform.position,_targetTransform.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint(){
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavePointIndex++;
        _targetTransform = Waypoints.points[wavePointIndex];
    }
}
