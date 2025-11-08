using UnityEngine;

public class LanternControls : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Sprite lanternOn;
    [SerializeField] private Sprite lanternOff;

    [Header("Lantern Settings")]

    [SerializeField] private float lightRadius = 2f;

    public bool isLightOn = true;

    private GameObject mainLantern;
    private GameObject lanternLight;
    private GameObject detection;

    private void Start()
    {
        // please dont kill me chris
        mainLantern = GameObject.Find("LanternPrefab(Clone)");
        lanternLight = mainLantern.transform.GetChild(0).gameObject;
        detection = mainLantern.transform.GetChild(1).gameObject;

        // set light size
        lanternLight.transform.localScale = lanternLight.transform.localScale * lightRadius;
        detection.transform.localScale = detection.transform.localScale * lightRadius;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLightOn = !isLightOn;
        }

        UpdateLanternState();
    }

    private void UpdateLanternState()
    {
        if (isLightOn)
        {
            lanternLight.SetActive(true);
            mainLantern.GetComponent<SpriteRenderer>().sprite = lanternOn;
        }
        else
        {
            lanternLight.SetActive(false);
            mainLantern.GetComponent<SpriteRenderer>().sprite = lanternOff;
        }
    }

    private void OnDrawGizmos()
    {
        // draw light radius
        Gizmos.color = Color.yellow;

        if (detection != null)
        {
            Gizmos.DrawWireSphere(detection.transform.position, lightRadius);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bug"))
        {
            GameObject bug = collision.gameObject;
            AIAgent bugAgent = bug.GetComponent<AIAgent>();

            bugAgent.inRadius = true;

            bugAgent.litWhenInRadius = isLightOn;

            bugAgent.latestLightPos = transform;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bug"))
        {
            GameObject bug = collision.gameObject;
            AIAgent bugAgent = bug.GetComponent<AIAgent>();

            bugAgent.inRadius = false;
        }
    }
}
