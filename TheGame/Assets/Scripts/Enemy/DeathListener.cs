using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathListener : MonoBehaviour
{
    public List<GameObject> enemies;

    private void Update()
    {
        int active = enemies.Count;
        foreach(var enemy in enemies) {
            if (!enemy || !enemy.activeInHierarchy) active--;
        }
        if (active == 0) SendMessage("OnDeath");
    }
}
