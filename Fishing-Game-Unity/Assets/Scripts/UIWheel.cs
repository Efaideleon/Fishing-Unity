using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIWheel : MonoBehaviour
{
    [SerializeField] RawImage wheelImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the wheel image base on touch controls
        // wheelImage.uvRect = new Rect(wheelImage.uvRect.x + Input.GetAxis("Horizontal") * Time.deltaTime, wheelImage.uvRect.y, wheelImage.uvRect.width, wheelImage.uvRect.height);
        wheelImage.rectTransform.rotation = Quaternion.Euler(0, 0, wheelImage.rectTransform.rotation.eulerAngles.z + Input.GetAxis("Horizontal") * Time.deltaTime * 100);
    }
}
