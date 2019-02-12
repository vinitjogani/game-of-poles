using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Texture healthbarBackground;
    public Texture healthbarForeground;
    public float maxHealth = 100f;

    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnDestroy()
    {
        var manager = FindObjectOfType<MagnetManager>();
        if (manager)
        {
            manager.objects.RemoveAll(x => x.obj == gameObject);
        }
    }

    private void Update()
    {
        if (health <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var body = collision.gameObject.GetComponent<Rigidbody>();
        float mass = body ? body.mass : 1;

        // Kinetic energy = damage
        health -= 0.5f * mass * Mathf.Pow(collision.relativeVelocity.magnitude, 2);
    }

    // Display a health bar
    void OnGUI()
    {
        if (InSight())
        {
            var p = Camera.main.WorldToScreenPoint(transform.position);
            p.y = Screen.height - p.y;

            var dist = (transform.position - Camera.main.transform.position).magnitude;
            var size = Mathf.Max(5f, 20f - dist);

            GUI.DrawTexture(new Rect(p.x - size * 10, p.y - 100, size * 20, size),
                            healthbarBackground, ScaleMode.StretchToFill, true);
            // Draw Foreground
            GUI.DrawTexture(new Rect(p.x - size * 10, p.y - 100, size * 20 * health / maxHealth, size),
                            healthbarForeground, ScaleMode.StretchToFill, true,
                            ((healthbarForeground.width * health) / healthbarForeground.height));
        }
    }


    bool InSight()
    {
        var camPos = Camera.main.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(camPos, transform.position - camPos, out hit))
            return hit.collider.gameObject == gameObject;
        return false;
    }
}
