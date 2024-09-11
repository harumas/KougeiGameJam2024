using UnityEngine;

public class GameStateData
{
    public static GameStateData Instance
    {
        get
        {
            instance ??= new GameStateData();
            return instance;
        }
    }

    private static GameStateData instance;
    private float maxHp;
    public bool IsFirstRound => round == 0;
    public bool IsGameEnd => LPlayerHp == 0f || RPlayerHp == 0f;
    public int Round => round;
    private int round;

    public void Initialize(float maxHp)
    {
        this.maxHp = maxHp;
        lPlayerHp = maxHp;
        rPlayerHp = maxHp;

        round = 0;
    }

    public void Reset()
    {
        lPlayerHp = maxHp;
        rPlayerHp = maxHp;
        round = 0;
    }

    public void IncrementRound()
    {
        round++;
    }

    public float LPlayerHp
    {
        get { return lPlayerHp; }
        set { lPlayerHp = Mathf.Clamp(value, 0f, maxHp); }
    }

    public float RPlayerHp
    {
        get { return rPlayerHp; }
        set { rPlayerHp = Mathf.Clamp(value, 0f, maxHp); }
    }

    private float lPlayerHp;
    private float rPlayerHp;
}