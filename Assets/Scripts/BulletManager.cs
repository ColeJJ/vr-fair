using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public ColorType colorType = ColorType.None;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target") {
            Destroy(this.gameObject);
        }
    }

    public void SetColorType(ColorType colorType) {
        this.colorType = colorType;
        switch(colorType) {
            case ColorType.None:
                break;
            case ColorType.Green:
                Material greenMaterial = Resources.Load("Material/Color Type Green", typeof(Material)) as Material;
                GetComponent<Renderer>().material = greenMaterial;
                break;
            case ColorType.Red:
                Material redMaterial = Resources.Load("Material/Color Type Red", typeof(Material)) as Material;
                GetComponent<Renderer>().material = redMaterial;
                break;
        }
    }
}
