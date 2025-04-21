using UnityEngine;

public class Ak74Raycast : MonoBehaviour
{
    public float damage = 30f;
    public float range = 100f;
    public float fireRate = 5f;
    public float impactForce = 30;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    private float nextTimeToFire;
    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 1.24f;
    public float emptyReloadTime = 1.4f;
    public bool isReloading = false;
    private float reloadTimer = 0;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            float currentReloadTime = currentAmmo == 0 ? emptyReloadTime : reloadTime;

            if (reloadTimer >= currentReloadTime)
            {
                FinishReload();
            }
        }

        // Recargar manualmente con la tecla R
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartReload();
        }

        // Disparar si no está recargando
        if (!isReloading && Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (isReloading)
        {
            return;
        }

        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)damage, this.gameObject); // this.gameObject es el jugador (o el arma)
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

        currentAmmo--;
        Debug.Log("Municion restante: " + currentAmmo);

        if (currentAmmo <= 0)
        {
            StartReload();
        }
    }

    void StartReload()
    {
        if (!isReloading)
        {
            Debug.Log("Recargando...");
            isReloading = true;
            reloadTimer = 0f;
        }
    }

    void FinishReload()
    {
        Debug.Log("Recarga completada");
        isReloading = false;
        currentAmmo = maxAmmo;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 50), "Municion: " + currentAmmo);
        if (isReloading)
        {
            GUI.Label(new Rect(10, 30, 200, 50), "Recargando... Espere");
        }
    }
}
