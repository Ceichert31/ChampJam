using UnityEngine;
using System.Collections.Generic;

public class RopeSwing : MonoBehaviour
{
    [Header("Rope Settings")]

    [SerializeField] private int ropeSegments = 5;
    [SerializeField] private float segmentLength = 0.15f;
    [SerializeField] private float ropeWidth = 0.05f;
    [SerializeField] private Color ropeColor = Color.white;

    [Header("Lantern Settings")]

    [SerializeField] private Sprite lanternSprite;
    [SerializeField] private float lanternSize = 0.5f;
    [SerializeField] private float lanternMass = 2f;
    [SerializeField] private Color lanternColor = Color.white;

    [Header("Physics Settings")]

    [SerializeField] private float ropeMass = 0.05f;
    [SerializeField] private float drag = 0.1f;
    [SerializeField] private float angularDrag = 0.05f;
    [SerializeField] private float solverMultiplier = 2f;

    [Header("Movement Settings")]

    [SerializeField] private float maxMoveSpeed = 10f;
    [SerializeField] private float moveSmoothing = 0.1f;

    private List<Rigidbody2D> segments = new List<Rigidbody2D>();
    private GameObject lantern;
    private LineRenderer ropeRenderer;
    private Camera mainCam;
    private Vector2 targetPosition;
    private Vector2 velocity = Vector2.zero;

    private void Start()
    {
        mainCam = Camera.main;
        Physics2D.velocityIterations = Mathf.RoundToInt(60 * solverMultiplier);
        Physics2D.positionIterations = Mathf.RoundToInt(30 * solverMultiplier);
        targetPosition = transform.position;
        buildRope();
    }

    private void Update()
    {
        Vector2 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = mouseWorld;
    }

    private void FixedUpdate()
    {
        if (segments.Count > 0)
        {
            Vector2 currentPos = segments[0].position;

            // move towards target with velocity cap
            Vector2 newPos = Vector2.SmoothDamp(currentPos, targetPosition, ref velocity, moveSmoothing, maxMoveSpeed);

            segments[0].MovePosition(newPos);
        }
        DrawRope();
    }

    private void buildRope()
    {
        ropeRenderer = gameObject.AddComponent<LineRenderer>();
        ropeRenderer.material = new Material(Shader.Find("Sprites/Default"));
        ropeRenderer.startWidth = ropeWidth;
        ropeRenderer.endWidth = ropeWidth;
        ropeRenderer.startColor = ropeColor;
        ropeRenderer.endColor = ropeColor;
        ropeRenderer.positionCount = ropeSegments + 2;
        ropeRenderer.sortingOrder = 0;

        Vector2 startPos = transform.position;
        Rigidbody2D prevBody = null;

        for (int i = 0; i < ropeSegments; i++)
        {
            GameObject seg = new GameObject("RopeSeg_" + i);
            seg.transform.position = startPos + Vector2.down * segmentLength * i;

            Rigidbody2D rb = seg.AddComponent<Rigidbody2D>();
            rb.mass = ropeMass;
            rb.linearDamping = drag;
            rb.angularDamping = angularDrag;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            if (i == 0)
            {
                // anchor segment that follows the mouse
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                DistanceJoint2D dist = seg.AddComponent<DistanceJoint2D>();
                dist.connectedBody = prevBody;
                dist.autoConfigureDistance = false;
                dist.distance = segmentLength;
                dist.maxDistanceOnly = false;
                dist.enableCollision = false;
            }

            prevBody = rb;
            segments.Add(rb);
        }

        CreateLantern(prevBody);
    }

    private void CreateLantern(Rigidbody2D bottomBody)
    {
        lantern = new GameObject("Lantern");
        lantern.transform.position = bottomBody.transform.position + Vector3.down * segmentLength;

        SpriteRenderer sr = lantern.AddComponent<SpriteRenderer>();
        sr.sprite = lanternSprite != null ? lanternSprite : CreateSquareSprite();
        sr.color = lanternColor;
        // lantern above rope
        sr.sortingOrder = 1;
        lantern.transform.localScale = Vector3.one * lanternSize;

        Rigidbody2D rb = lantern.AddComponent<Rigidbody2D>();
        rb.mass = lanternMass;
        rb.linearDamping = drag;
        rb.angularDamping = angularDrag;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        BoxCollider2D col = lantern.AddComponent<BoxCollider2D>();
        col.size = new Vector2(lanternSize, lanternSize);

        DistanceJoint2D dist = lantern.AddComponent<DistanceJoint2D>();
        dist.connectedBody = bottomBody;
        dist.autoConfigureDistance = false;
        dist.distance = segmentLength + (lanternSize * 0.4f);
        dist.maxDistanceOnly = false;
        dist.enableCollision = false;
    }

    private void DrawRope()
    {
        ropeRenderer.positionCount = segments.Count + 1;
        for (int i = 0; i < segments.Count; i++)
        {
            ropeRenderer.SetPosition(i, segments[i].transform.position);
        }
        ropeRenderer.SetPosition(segments.Count, lantern.transform.position);
    }

    private Sprite CreateSquareSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 1f), 1);
    }
}