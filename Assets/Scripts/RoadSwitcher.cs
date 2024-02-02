using UnityEngine;

public class RoadSwitcher : MonoBehaviour
{
    [SerializeField] private RoadManager roadManager;
    [SerializeField] private int currentRoad;
    [SerializeField] private float moveSpeed;

    private float[] roadsPosition;

    private Rigidbody2D rb;

    [SerializeField] private bool useKeyboard;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        InitRoadsPosition();
    }

    private void Update()
    {
        if (useKeyboard)
        {
            ChangeRoadViaKeyboard();
        }
        else
        {
            ChangeRoadViaMouse();
        }

        Move();
    }

    private void InitRoadsPosition()
    {
        roadsPosition = new float[roadManager.roadCount];
        for (int i = 0; i < roadManager.roadCount; i++)
        {
            roadsPosition[i] = (-roadManager.offset * 2) + (roadManager.offset * (i + 1));
        }
    }

    private void Move()
    {
        var position = new Vector2
        {
            x = roadsPosition[currentRoad],
            y = transform.position.y
        };

        rb.position = Vector2.MoveTowards(rb.position, position, moveSpeed * Time.deltaTime);
    }

    private void ChangeRoadViaKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentRoad++;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentRoad--;
        }

        currentRoad = Mathf.Clamp(currentRoad, 0, roadsPosition.Length - 1);
    }

    private void ChangeRoadViaMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var minDist = float.MaxValue;

        for (int i = 0; i < roadsPosition.Length; i++)
        {
            var dist = Mathf.Abs(mousePos.x - roadsPosition[i]);

            if (dist < minDist)
            {
                currentRoad = i;
                minDist = dist;
            }
        }
    }
}