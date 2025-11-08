using UnityEngine;

public class LanternControls : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Sprite lanternOn;
    [SerializeField] private Sprite lanternOff;

    public bool isLightOn = true;

    private GameObject mainLantern;
    private GameObject lanternLight;

    private void Start()
    {
        // please dont kill me chris
        mainLantern = GameObject.Find("LanternPrefab(Clone)");
        //lanternLight = lanternLight.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            isLightOn = !isLightOn;
        }

        UpdateLantern();
    }

    private void UpdateLantern()
    {
        if (isLightOn)
        {
            //lanternLight.SetActive(true);
            mainLantern.GetComponent<SpriteRenderer>().sprite = lanternOn;
        }
        else
        {
            //lanternLight.SetActive(false);
            mainLantern.GetComponent<SpriteRenderer>().sprite = lanternOff;
        }
    }
}
