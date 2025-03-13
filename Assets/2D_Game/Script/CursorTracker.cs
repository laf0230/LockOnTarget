using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorTracker : MonoBehaviour
{
    Camera camera;
    Image prograssImg;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        prograssImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    public void StartPrograssUI(float value)
    {
        prograssImg.fillAmount = 1 / value;
        // StartCoroutine("PrograssingUI", value);
    }

    public void PrograssUpdate(float value)
    {
        // 5 == 100%
        // 1 == 20%
        prograssImg.fillAmount = value;
    }

    /*
    IEnumerator PrograssingUI(float value)
    {
        while (value < 0)
        {
            yield return null;
            value -= Time.deltaTime;
        }
        prograssImg.fillAmount = 1;
    }
    */
}
