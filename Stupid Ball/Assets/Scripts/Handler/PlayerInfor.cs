using System;

[Serializable]
public class PlayerInfor
{
    public string playerName;
    public int points;

    public PlayerInfor (string name, int points)
    {
        playerName = name;
        this.points = points;
    }
}
