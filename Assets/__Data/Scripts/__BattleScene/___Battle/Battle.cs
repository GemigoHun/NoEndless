using System.Collections;
using UnityEngine;

public class Battle : GMono
{
    private static Battle instance;

    public static Battle Instance => instance;

    [SerializeField] private bool pTurn = true;
    [SerializeField] private bool opTurn = false;
    [SerializeField] private bool endTurn = false;

    public bool EndTurn
    {
        get { return endTurn; }
        set { endTurn = value; }
    }

    [SerializeField] private bool canDrag = true;

    public bool CanDrag
    {
        get { return canDrag; }
        set { canDrag = value; }
    }

    [SerializeField] private float countDownTurn;

    public float CountDownTurn
    {
        get { return countDownTurn; }
        set { countDownTurn = value; }
    }

    [SerializeField] private int turnCount = 1;

    public int TurnCount
    {
        get { return turnCount; }
        set { turnCount = value; }
    }

    [SerializeField] private bool botPlayed = false;

    public bool BotPlayed
    {
        get { return botPlayed; }
        set { botPlayed = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.Log("Only 1 Battle is allowed to exist!");

        instance = this;
    }

    protected override void Start()
    {

        StartCoroutine(Fight());
    }

    private IEnumerator Fight()
    {
        yield return new WaitForSeconds(1);

        while(PlayerInBattle.Instance.CurrentHP > 0 || OpponentInBattle.Instance.CurrentHP > 0)
        {
            if(pTurn)
            {
                countDownTurn = 30;
                canDrag = true;

                while(countDownTurn > 0)
                {
                    if(endTurn) countDownTurn = 0;
                    countDownTurn -= Time.deltaTime;
                    CountDown.Instance.TextM.SetText($"{(int)countDownTurn}");
                    
                    yield return null;
                }              
            }

            pTurn = false;
            opTurn = true;
            endTurn = false;
            turnCount = 1;

            if(opTurn)
            {
                while(!endTurn)
                {
                    if(!botPlayed)
                    {
                        botPlayed = true;
                        Game.Instance.Bot.BotDestroyAndFill.DestroyAndFill();
                    }

                    yield return null;
                }
            }

            pTurn = true;
            opTurn = false;
            endTurn = false;
            turnCount = 1;

            yield return null;
        }
    }
}