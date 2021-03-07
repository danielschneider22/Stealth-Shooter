using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowStatsManager : MonoBehaviour
{
    public GameObject gunStatsUI;
    public GameObject regView;
    public GameObject detailView;

    // for player gun
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI damageLow;
    public TextMeshProUGUI damageHigh;
    public TextMeshProUGUI clipSize;
    public TextMeshProUGUI reloadTime;

    public GameObject gunDropPrefab;

    private GameObject player;

    private GunStatsManager gunStatsManager;
    private bool showingPopup;

    public ButtonManager swapGunsButtonManager;
    public ButtonManager detailsButtonManager;

    private void Awake()
    {
        gunStatsManager = gameObject.transform.parent.gameObject.GetComponent<GunStatsManager>();
        swapGunsButtonManager.OnButtonClicked += swapGuns;
        detailsButtonManager.OnButtonClicked += toggleDetails;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gunStatsUI.SetActive(true);

            Gun playerGun = collision.gameObject.GetComponent<PlayerGeneralShootController>().currGun;

            gunStatsManager.statsContainerSolo.GetComponent<InitializeWithGun>().Initialize(gunStatsManager.gun, playerGun);
            gunStatsManager.statsContainerSolo2.GetComponent<InitializeWithGun>().Initialize(gunStatsManager.gun, playerGun);
            gunStatsManager.statsContainerPlayerGun.GetComponent<InitializeWithGun>().Initialize(playerGun, gunStatsManager.gun);

            showingPopup = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gunStatsUI.SetActive(false);
            showingPopup = false;
            regView.SetActive(true);
            detailView.SetActive(false);
        }
    }

    private void Update()
    {
        if(showingPopup && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            if(regView.activeSelf)
            {
                regView.SetActive(false);
                detailView.SetActive(true);
            } else
            {
                regView.SetActive(true);
                detailView.SetActive(false);
            }
        }
        if (showingPopup && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newGunDrop = Instantiate(gunDropPrefab);
            Gun currGun = player.GetComponent<PlayerGeneralShootController>().currGun;
            newGunDrop.GetComponent<GunStatsManager>().InitializeGunDrop(currGun);
            newGunDrop.transform.position = player.transform.position;
            player.GetComponent<PlayerGeneralShootController>().SetNewGun(gunStatsManager.gun);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void toggleDetails()
    {
        if (regView.activeSelf)
        {
            regView.SetActive(false);
            detailView.SetActive(true);
        }
        else
        {
            regView.SetActive(true);
            detailView.SetActive(false);
        }
    }

    private void swapGuns()
    {
        GameObject newGunDrop = Instantiate(gunDropPrefab);
        Gun currGun = player.GetComponent<PlayerGeneralShootController>().currGun;
        newGunDrop.GetComponent<GunStatsManager>().InitializeGunDrop(currGun);
        newGunDrop.transform.position = player.transform.position;
        player.GetComponent<PlayerGeneralShootController>().SetNewGun(gunStatsManager.gun);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
