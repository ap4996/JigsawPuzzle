using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumbledSectionRenderer : SectionRenderer
{
    protected override BoardRenderType BoardRenderType { get; set; } = BoardRenderType.JUMBLED;
}
