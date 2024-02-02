using UnityEngine;

public class RoadSwitcher : MonoBehaviour
{
    [SerializeField] private RoadManager roadManager;
    [SerializeField] private int currentRoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentRoad += 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentRoad -= 1;
        }

        currentRoad = Mathf.Clamp(currentRoad, 1, roadManager.roadCount);

        var position = new Vector2
        {
            x = (-roadManager.offset * 2) + (roadManager.offset * currentRoad),
            y = transform.position.y
        };

        transform.position = position;
    }
}