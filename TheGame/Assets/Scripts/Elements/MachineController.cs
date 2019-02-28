using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineController : MonoBehaviour
{
    private int collidedKeys = 0;
    public int numberOfUniqueKeys = 4;
    public float rotationDegreesPerSecond = 3f;

    // Start is called before the first frame update
    private void Update()
    {
        if (collidedKeys == numberOfUniqueKeys)
        {
            Destroy(transform.parent);
        }

        rotate();
    }

    public void uniqueKeyCollided()
    {
        collidedKeys += 1;

        if (collidedKeys == numberOfUniqueKeys)
        {
            SceneManager.LoadScene("Over");
        }
    }

    void rotate()
    {
        float currentAngle = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(currentAngle + (Time.deltaTime * rotationDegreesPerSecond), Vector3.up);
    }
}
