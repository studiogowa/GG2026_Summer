using UnityEngine;
using System;

public struct GameEventsStruct
{
    public Action preGameStarts;
    public Action duskStarts;
    public Action duskEnds;
    public Action dawnStarts;
    public Action dawnEnds;
    public Action dayStarts;
    public Action dayEnds;
    public Action performanceReviewStarts;
}
