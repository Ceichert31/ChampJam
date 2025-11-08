using UnityEngine;
using System.Collections;

public class RopeSwing : MonoBehaviour
{
    [Header("Rope Settings")]
    [SerializeField] private int ropeSegments = 10;
    [SerializeField] private float segmentLength = 0.5f;
    [SerializeField] private float ropeWidth = 0.1f;

    [Header("Physics Settings")]
    [SerializeField] private float segmentMass = 0.1f;
    [SerializeField] private float linearDamping = 0.5f;
    [SerializeField] private float angularDamping = 0.5f;

    [Header("Light Settings")]
    [SerializeField] private float lightSize = 0.5f;
    [SerializeField] private float lightMass = 1f;
    [SerializeField] private Color lightColor = Color.red;

    private GameObject[] ropeSegmentObjects;
    private GameObject lantern;
    private Camera mainCamera;
    private Vector2 mouseWorldPos;

    private void Start()
    {
        mainCamera = Camera.main;

        // much higher iterations for non-stretchy rope
        Physics2D.velocityIterations = 32;
        Physics2D.positionIterations = 16;

        CreateRope();
    }

    private void Update()
    {
        // mouse to world
        mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // anchor point follow mouse
        if (ropeSegmentObjects.Length > 0 && ropeSegmentObjects[0] != null)
        {
            ropeSegmentObjects[0].transform.position = mouseWorldPos;
        }
    }

    private void CreateRope()
    {
        ropeSegmentObjects = new GameObject[ropeSegments];

        // rope creation
        for (int i = 0; i < ropeSegments; i++)
        {
            GameObject segment = new GameObject($"RopeSegment_{i}");
            segment.transform.position = transform.position + Vector3.down * segmentLength * i;

            // line renderer visual init
            LineRenderer lr = segment.AddComponent<LineRenderer>();
            lr.startWidth = ropeWidth;
            lr.endWidth = ropeWidth;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = Color.white;
            lr.endColor = Color.white;
            lr.positionCount = 2;

            // component setup
            if (i > 0)
            {
                Rigidbody2D rb = segment.AddComponent<Rigidbody2D>();
                rb.mass = segmentMass;
                rb.linearDamping = linearDamping;
                rb.angularDamping = angularDamping;
                rb.interpolation = RigidbodyInterpolation2D.Interpolate;

                // hinge for rotation
                HingeJoint2D hinge = segment.AddComponent<HingeJoint2D>();
                hinge.connectedBody = ropeSegmentObjects[i - 1].GetComponent<Rigidbody2D>();
                hinge.autoConfigureConnectedAnchor = false;
                hinge.anchor = Vector2.up * (segmentLength / 2);
                hinge.connectedAnchor = Vector2.down * (segmentLength / 2);

                // distance to enforce length
                DistanceJoint2D distJoint = segment.AddComponent<DistanceJoint2D>();
                distJoint.connectedBody = ropeSegmentObjects[i - 1].GetComponent<Rigidbody2D>();
                distJoint.autoConfigureDistance = false;
                distJoint.distance = segmentLength;
                distJoint.maxDistanceOnly = false;
            }
            else
            {
                // first segment (da anchor) follow mouse
                Rigidbody2D rb = segment.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            ropeSegmentObjects[i] = segment;
        }

        // light
        CreateLight();
        // for line renderers
        StartCoroutine(UpdateRopeVisuals());
    }

    private void CreateLight()
    {
        lantern = new GameObject("Lantern");
        lantern.transform.position = transform.position + Vector3.down * (segmentLength * ropeSegments);

        // spriterender
        SpriteRenderer sr = lantern.AddComponent<SpriteRenderer>();
        sr.sprite = CreateSquareSprite();
        sr.color = lightColor;
        lantern.transform.localScale = Vector3.one * lightSize;

        // physics
        Rigidbody2D rb = lantern.AddComponent<Rigidbody2D>();
        rb.mass = lightMass;
        rb.linearDamping = linearDamping;
        rb.angularDamping = angularDamping;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        BoxCollider2D collider = lantern.AddComponent<BoxCollider2D>();

        // hinge for rotation
        HingeJoint2D hinge = lantern.AddComponent<HingeJoint2D>();
        hinge.connectedBody = ropeSegmentObjects[ropeSegments - 1].GetComponent<Rigidbody2D>();
        hinge.autoConfigureConnectedAnchor = false;
        hinge.anchor = Vector2.up * (lightSize / 2.0f);
        hinge.connectedAnchor = Vector2.down * (segmentLength / 2.0f);

        // distance to enforce length
        DistanceJoint2D distJoint = lantern.AddComponent<DistanceJoint2D>();
        distJoint.connectedBody = ropeSegmentObjects[ropeSegments - 1].GetComponent<Rigidbody2D>();
        distJoint.autoConfigureDistance = false;
        distJoint.distance = (segmentLength / 2.0f) + (lightSize / 2.0f);
        distJoint.maxDistanceOnly = false;
    }

    private Sprite CreateSquareSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
    }

    private IEnumerator UpdateRopeVisuals()
    {
        while (true)
        {
            // rope connection updates
            for (int i = 0; i < ropeSegments; i++)
            {
                LineRenderer lr = ropeSegmentObjects[i].GetComponent<LineRenderer>();
                lr.SetPosition(0, ropeSegmentObjects[i].transform.position);

                if (i < ropeSegments - 1)
                {
                    lr.SetPosition(1, ropeSegmentObjects[i + 1].transform.position);
                }
                else
                {
                    lr.SetPosition(1, lantern.transform.position);
                }
            }

            yield return null;
        }
    }
}