using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  playerProperties
{
    static int playerPoint;
    public static int getPlayerPoint()
    {
        return playerPoint;
    }
    public static void setPlayerPoint(int _point)
    {
        playerPoint = _point;
    }
}
