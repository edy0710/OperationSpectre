/*using UnityEngine;

public class Raycast : MonoBehaviour
{

    public float damage = 42f;
    public float range = 100f;
    public float fireRate = 1f;
    public float impactForce = 30; 

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;
    private float nextTimeToFire;

    public int maxAmmo = 7;
    public int currentAmmo;
    public float reloadTime = 0.9f;
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

            if (reloadTimer >= reloadTime)
            {
                FinishReloadEmpty();
            }
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }


    }
    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

        if (currentAmmo > 0)
        {
            Debug.Log("Disparando");
            currentAmmo--;

            Debug.Log("Municion restante: " + currentAmmo);
        }
        else
        {
            StartReloadEmpty();
        }
    }

    void StartReloadEmpty()
    {
        if (!isReloading)
        {
            Debug.Log("Recargando");
            isReloading = true;
            reloadTimer = 0f;
        }
    }

    void FinishReloadEmpty()
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
}*/

using UnityEngine;

public class A3500XRaycast : MonoBehaviour
{
    public float damage = 42f;
    public float range = 100f;
    public float fireRate = 1f;
    public float impactForce = 30;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    private float nextTimeToFire;
    public int maxAmmo = 7;
    public int currentAmmo;
    public float reloadTime = 0.9f;
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
        if (!isReloading && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
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
