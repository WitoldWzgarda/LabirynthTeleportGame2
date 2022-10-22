using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Camera myCamera;
    public GameObject player;
    public Transform myRenderPlane;
    public Transform myCollidPlane;
    public Portal otherPortal;

    PortalCamera portalCamera;
    PortalTeleport portalTeleport;

    public Material material;
    float myAngle;

    private void Awake()
    {
        portalCamera = myCamera.GetComponent<PortalCamera>();
        portalTeleport = myCollidPlane.gameObject.GetComponent<PortalTeleport>();
        player = GameObject.FindGameObjectWithTag("Player");

        portalCamera.playerCamera = player.gameObject.transform.GetChild(0);
        portalCamera.otherPortal = otherPortal.transform;
        portalCamera.portal = this.transform;

        portalTeleport.player = player.transform;
        portalTeleport.reciever = otherPortal.transform;

        myRenderPlane.gameObject.GetComponent<Renderer>().material = Instantiate(material);

        if (myCamera.targetTexture != null)
        {
            myCamera.targetTexture.Release();
        }

        myAngle = transform.localEulerAngles.y % 360;
        portalCamera.SetMyAngle(myAngle);
    }

    // Start is called before the first frame update
    void Start()
    {
        myRenderPlane.gameObject.GetComponent<Renderer>().material.mainTexture = otherPortal.myCamera.targetTexture;
        CheckAngle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckAngle()
    {
        if (Mathf.Abs(otherPortal.ReturnMyAngle() - ReturnMyAngle()) != 180)
        {
            Debug.LogWarning("Portale nie s¹ odpowiednio ustawione: " + gameObject.name);
            Debug.Log("Angle: " + (otherPortal.ReturnMyAngle() - ReturnMyAngle()));
        }
    }

    public float ReturnMyAngle()
    {
        return myAngle;
    }
}
