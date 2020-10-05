using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalInformation
{
    // Constants
    public const int MAX_SCENES = 7;
    
    
    // Global information sharing
    public static int currentScene = 5;
    public static int saturnScore = 0;
    public static int currentCollectibles = 0;

    /// <summary>
    /// 0 is RED, 1 is BLUE, 2 is GREEN, 3 is YELLOW
    /// </summary>
    public static int CharacterSkinIndex = 0;
}
