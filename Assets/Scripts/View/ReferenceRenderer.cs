using System.Collections.Generic;
using UnityEngine;

public class ReferenceRenderer : SectionRenderer
{
    protected override BoardRenderType BoardRenderType { get; set; } = BoardRenderType.REFERENCE;

    public Dictionary<int, Vector3> GetIdAndPositionMapping()
    {
        Dictionary<int, Vector3> dict = new Dictionary<int, Vector3>();
        for(int i = 0; i < transform.childCount; ++i)
        {
            dict.Add(i + 1, transform.GetChild(i).position);
        }
        return dict;
    }
}
