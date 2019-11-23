using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChanger : MonoBehaviour
{
    [SerializeField] private Sprite normalSpeed;
    [SerializeField] private Sprite fastSpeed;

    public bool isSpedUp = false;

    public void ChangeSpeed()
    {
        // Flip the boolean to the opposite of itself
        isSpedUp = !isSpedUp;

        // If the isSpedUp is true
        if (isSpedUp)
        {
            // Set the image to be of the normal speed
            gameObject.GetComponent<Image>().sprite = normalSpeed;
            // Set the speed to be 7x normal
            Time.timeScale = 7f;
        }
        else
        {
            // Set the image to be of fast forward
            gameObject.GetComponent<Image>().sprite = fastSpeed;
            // Set the speed to be normal
            Time.timeScale = 1.0f;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSpeed();
        }
    }
}
