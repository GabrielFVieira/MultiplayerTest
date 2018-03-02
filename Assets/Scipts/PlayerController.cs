using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawn;
    private float velY;
    private float velZ;
    private float velBullet;

    public GameObject canvas;
    // Use this for initialization
    void Start () {
        velY = 100f;
        velZ = 3f;
        velBullet = 50f;

        if(isLocalPlayer)
        {
            //Camera.main.transform.position = this.transform.position - this.transform.forward * 4 + this.transform.up * 1.5f;
            Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.412f, this.transform.position.z);
            //Camera.main.transform.LookAt(this.transform.position);
            Camera.main.transform.parent = this.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            return;
        }

        canvas.SetActive(true);

//float y;
        float y = Input.GetAxis("Horizontal") * Time.deltaTime * velY;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * velZ;

        /*
        if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x > 0.52f && Camera.main.ScreenToViewportPoint(Input.mousePosition).x <= 0.8f)
            y = 1 * Time.deltaTime * velY * 0.5f;

        else if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x > 0.8f)
            y = 1 * Time.deltaTime * velY;

        else if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x < 0.48f && Camera.main.ScreenToViewportPoint(Input.mousePosition).x >= 0.2f)
            y = -1 * Time.deltaTime * velY * 0.5f;

        else if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x < 0.2f)
            y = -1 * Time.deltaTime * velY;

        else
            y = 0;
            */

        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));

        transform.Rotate(0, y, 0);
        transform.Translate(0, 0, z);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * velBullet;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2);
    }

    public override void OnStartLocalPlayer()
    {
        //Cursor.visible = false;
        //GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
