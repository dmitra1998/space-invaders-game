using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip destroySound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;    
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.5f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField]  float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 5f;
    Vector3 pos;

    Coroutine firingCorountine;

    float xMin, xMax;
    float yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        pos.z -= 0.1f;
        transform.position = pos;
        horizantalMove();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCorountine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCorountine);
        }
    }

    IEnumerator FireContinuously() {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            Audio(shootSound, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    } 

    private void horizantalMove()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damage = collision.GetComponent<DamageDealer>();
        if (!damage) { return; }
        ProcessHit(damage);
    }

    private void ProcessHit(DamageDealer damage)
    {
        health -= damage.GetDamage();
        damage.Hit();
        if (health <= 0)
        {
            FindObjectOfType<Level>().LoadGameOver();
            Destroy(gameObject);
            Audio(destroySound, deathSoundVolume);
        }
    }
    private void Audio(AudioClip sound, float volume)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
    }

    public int Health()
    {
        return health;
    }


}
