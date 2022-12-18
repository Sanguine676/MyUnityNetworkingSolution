using UnityEngine;
//

//
public static class TimeInfo
{
    //---
    public static float deltaTimeMult = 1.0f,
        fixedDeltaTimeMult = 1.0f,
        timeMult = 1.0f,
        unscaledGlobalTimeMult = 1.0f;
    //---

    //
    public static float DeltaTime => Time.deltaTime * deltaTimeMult;

    //
    public static float FixedDeltaTime => Time.fixedDeltaTime * fixedDeltaTimeMult;

    //
    public static float GlobalTime => Time.time * timeMult;

    //
    public static float UnscaledGlobalTime => Time.unscaledTime * unscaledGlobalTimeMult;
}
