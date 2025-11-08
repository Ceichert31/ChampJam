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

    private void Start()
    {
        // please dont kill me chris
        mainLantern = GameObject.Find("LanternPrefab(Clone)");
        lanternLight = mainLantern.transform.GetChild(0).gameObject;

        // set light size
        lanternLight.transform.localScale = Vector3.one * lightRadius;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLightOn = !isLightOn;
        }

        UpdateLantern();

        // keep light scale synced
        lanternLight.transform.localScale = Vector3.one * lightRadius;
    }

    private void UpdateLantern()
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
        Gizmos.DrawWireSphere(transform.position, lightRadius);
    }
}
