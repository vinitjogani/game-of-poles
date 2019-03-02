using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pole
{
    NORTH,
    SOUTH
}

public class BulletCollide : MonoBehaviour
{
    public Pole type = Pole.NORTH;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.name != "Body")
        {
            Destroy(gameObject);

            MagnetObject magnetObject = new MagnetObject(other.gameObject, type, MagnetManager.magnetizeTime);
            var manager = FindObjectOfType<MagnetManager>();

            MagnetObject search = manager.objects.Find(x => x.obj.name == magnetObject.obj.name);
            if (search != null)
            {
                search.time = MagnetManager.magnetizeTime;
                search.strength = Mathf.Min(MagnetManager.maxStrength, search.strength * MagnetManager.strengthCompound);
                search.pole = type;
            }
            else
            {
                manager.objects.Add(magnetObject);
            }

            AudioSource laudio = gameObject.AddComponent<AudioSource>();
            laudio.PlayOneShot((AudioClip)Resources.Load("MagneticON"));
        }
    }
}
