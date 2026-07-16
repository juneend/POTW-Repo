using UnityEngine;

[RequireComponent(typeof(LineRenderer))] // Ensures the Guard always has a Line Renderer
public class FOVSensor : MonoBehaviour
{
    public Vector2 lookDir = Vector2.down;

    public float fovAngle = 60f;
    public float fovRange = 5f;
    public Transform rayPoint;

    [SerializeField] Transform target;
    public GameObject alertIcon;

    [SerializeField] GameManager gM;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Safe target finding
        var controller = GameObject.FindObjectOfType<CharacterController>();
        if (controller != null)
        {
            target = controller.gameObject.transform;
            if (target.childCount > 0) target = target.GetChild(0);
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }

        if (transform.childCount > 0)
            rayPoint = transform.GetChild(0);
        else
            rayPoint = transform;

        gM = GameManager.Inst();
    }

    private void Start()
    {
        // Force the line renderer to use 3 points for our triangle
        lineRenderer.positionCount = 3;
        DrawFOVVisual();
    }

    private void FixedUpdate()
    {
        if (target == null || rayPoint == null) return;

        Vector2 targetDir = target.position - rayPoint.position;
        float distanceToTarget = targetDir.magnitude;
        Vector2 targetDirNormalized = targetDir.normalized;

        float angleOfDir = Vector2.Angle(targetDirNormalized, lookDir);

        // Always redraw the visual cone in case you change settings in real-time
        DrawFOVVisual();

        if (angleOfDir < (fovAngle / 2f) && distanceToTarget <= fovRange)
        {
            RaycastHit2D ray = Physics2D.Raycast(rayPoint.position, targetDirNormalized, fovRange);

            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    if (alertIcon != null) alertIcon.SetActive(true);
                    if (gM != null) gM.Alerted();
                }
                else
                {
                    if (alertIcon != null) alertIcon.SetActive(false);
                }
            }
        }
        else
        {
            if (alertIcon != null) alertIcon.SetActive(false);
        }
    }

    // This function calculates the triangle vertices and updates the Line Renderer
    private void DrawFOVVisual()
    {
        if (lineRenderer == null) return;

        // 1. Point A: The origin (local 0,0,0)
        Vector3 origin = Vector3.zero;

        // 2. Calculate local direction angles
        float baseAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        float halfFov = fovAngle / 2f;

        // Calculate left boundary of the cone
        float leftAngleRad = (baseAngle + halfFov) * Mathf.Deg2Rad;
        Vector3 leftPoint = new Vector3(Mathf.Cos(leftAngleRad), Mathf.Sin(leftAngleRad), 0) * fovRange;

        // Calculate right boundary of the cone
        float rightAngleRad = (baseAngle - halfFov) * Mathf.Deg2Rad;
        Vector3 rightPoint = new Vector3(Mathf.Cos(rightAngleRad), Mathf.Sin(rightAngleRad), 0) * fovRange;

        // 3. Assign local points to our Line Renderer
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, leftPoint);
        lineRenderer.SetPosition(2, rightPoint);
    }
}