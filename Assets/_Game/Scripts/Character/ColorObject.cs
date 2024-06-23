using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : GameUnit
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer renderer;

    public ColorType colorType;

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = colorData.GetColorMat(colorType);
    }

    //public override void OnInit()
    //{

    //}

    //public override void OnDespawn()
    //{

    //}
}
