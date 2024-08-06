using System.Collections;
using System.Collections.Generic;
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

    private Dictionary<TileEnum, int> tileCounter;

    public Dictionary<TileEnum, int> TileCounter => tileCounter;

    private Player player;
    private Bot bot;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.Log("Only 1 Battle is allowed to exist!");

        instance = this;
    }

    protected override void Start()
    {
        player = Game.Instance.Player;
        bot = Game.Instance.Bot;

        StartCoroutine(Fight());
    }

    private IEnumerator Fight()
    {
        yield return new WaitForSeconds(1);

        while(player.Stats.CurrentHP > 0 || bot.Stats.CurrentHP > 0)
        {
            if(pTurn)
            {
                countDownTurn = 90;
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

    public void NewTileCounter()
    {
        tileCounter = new()
        {
            { TileEnum.SWORD, 0 },
            { TileEnum.SLASH, 0 },
            { TileEnum.MANA, 0 },
            { TileEnum.SHEILD, 0 },
            { TileEnum.HEART, 0 },
            { TileEnum.VHEART, 0 }
        };
    }

    public void TurnChange()
    {
        if(opTurn) botPlayed = false;

        if(turnCount <= 0) endTurn = true;
        else
        {
            if(pTurn)
            {
                canDrag = true;
                countDownTurn = 90;
            }
            
            if(opTurn) botPlayed = false;
        }
    }

    public IEnumerator TileHandling()
    {
        foreach(KeyValuePair<TileEnum, int> keyValuePair in tileCounter)
        {
            Debug.Log($"{keyValuePair.Key} = {keyValuePair.Value}");
        }

        if(tileCounter[TileEnum.HEART] > 0)
        {
            if(pTurn) player.Stats.HPIns(2 * tileCounter[TileEnum.HEART]);
            if(opTurn) bot.Stats.HPIns(2 * tileCounter[TileEnum.HEART]);
        }

        if(tileCounter[TileEnum.VHEART] > 0)
        {
            if(pTurn) player.Stats.VHPIns(2 * tileCounter[TileEnum.VHEART]);
            if(opTurn) bot.Stats.VHPIns(2 * tileCounter[TileEnum.VHEART]);
        }

        if(tileCounter[TileEnum.MANA] > 0)
        {
            if(pTurn) player.Stats.ManaIns(tileCounter[TileEnum.MANA]);
            if(opTurn) bot.Stats.ManaIns(tileCounter[TileEnum.MANA]);
        }

        if(tileCounter[TileEnum.SHEILD] > 0)
        {
            if(pTurn) player.Stats.SheildStack(tileCounter[TileEnum.SHEILD]);
            if(opTurn) bot.Stats.SheildStack(tileCounter[TileEnum.SHEILD]);
        }

        if(tileCounter[TileEnum.SLASH] > 0)
        {
            if(pTurn)
            {
                int pSlashDamage = player.Stats.SlashDamage * tileCounter[TileEnum.SLASH];
                int botVHP = bot.Stats.VHP;
                int lostHP;

                if(pSlashDamage > botVHP)
                {
                    lostHP = pSlashDamage - botVHP;                  
                }
                else
                {
                    lostHP = 0;
                }

                yield return StartCoroutine(player.Moving.MoveToTarget());
                yield return StartCoroutine(player.Atack.MeleeAttack());

                bot.Stats.VHPDes(pSlashDamage);
                bot.Stats.HPDes(lostHP);

                yield return StartCoroutine(player.Moving.MoveBack());

                player.Anim.IdleAnim();
            }

            if(opTurn)
            {
                int opSlashDamage = bot.Stats.SlashDamage * tileCounter[TileEnum.SLASH];
                int pVHP = player.Stats.VHP;
                int lostHP;

                if(opSlashDamage > pVHP)
                {
                    lostHP = opSlashDamage - pVHP;
                }
                else
                {
                    lostHP = 0;
                }

                yield return StartCoroutine(bot.Moving.MoveToTarget());
                yield return StartCoroutine(bot.Atack.MeleeAttack());

                player.Stats.VHPDes(opSlashDamage);
                player.Stats.HPDes(lostHP);

                yield return StartCoroutine(bot.Moving.MoveBack());

                bot.Anim.IdleAnim();
            }
        }

        if(tileCounter[TileEnum.SWORD] > 0)
        {
            if(pTurn)
            {
                int pSlashDamage = tileCounter[TileEnum.SWORD] * player.Stats.SlashDamage;
                int botVHP = bot.Stats.VHP;
                int lostHP;

                if(pSlashDamage > botVHP)
                {
                    lostHP = pSlashDamage - botVHP;
                }
                else
                {                  
                    lostHP = 0;
                }

                StartCoroutine(player.ESwordrain.SpawnSword(tileCounter[TileEnum.SWORD]));

                yield return StartCoroutine(player.Moving.MoveToTarget());
                yield return StartCoroutine(player.Atack.MeleeAttack());

                bot.Stats.VHPDes(pSlashDamage);

                bot.Stats.HPDes(lostHP);

                yield return StartCoroutine(player.Moving.MoveBack());

                player.Anim.IdleAnim();
            }

            if(opTurn)
            {
                int opSlashDamage = tileCounter[TileEnum.SWORD] * bot.Stats.SlashDamage;
                int pVHP = player.Stats.VHP;
                int lostHP;

                if(opSlashDamage > pVHP)
                {
                    lostHP = opSlashDamage - pVHP;
                }
                else
                {
                    lostHP = 0;
                }

                player.Stats.VHPDes(opSlashDamage);

                player.Stats.HPDes(lostHP);
            }
        }

        PlayerText.Instance.HP.SetText($"{player.Stats.CurrentHP}/{player.Stats.MaxHP}");
        PlayerText.Instance.VHP.SetText($"{player.Stats.VHP}");
        PlayerText.Instance.MP.SetText($"{player.Stats.Mana}%");
        PlayerText.Instance.ShieldCount.SetText($"{player.Stats.ShieldCount}");
        PlayerText.Instance.ShieldStack.SetText($"{player.Stats.ShieldStack}");

        OpText.Instance.HP.SetText($"{bot.Stats.CurrentHP}/{bot.Stats.MaxHP}");
        OpText.Instance.VHP.SetText($"{bot.Stats.VHP}");
        OpText.Instance.MP.SetText($"{bot.Stats.Mana}%");
        OpText.Instance.ShieldCount.SetText($"{bot.Stats.ShieldCount}");
        OpText.Instance.ShieldStack.SetText($"{bot.Stats.ShieldStack}");

        yield return new WaitForSeconds(30);
    }

    public void DealSwordrainDamage(Entity dealer, Entity receiver)
    {
        int swordrainDamage = dealer.Stats.SwordrainDamage;
        int receiverVHP = receiver.Stats.VHP;
        int lostHP;

        if(swordrainDamage > receiverVHP)
        {
            lostHP = swordrainDamage - receiverVHP;
        }
        else
        {
            lostHP = 0;
        }

        receiver.Stats.VHPDes(swordrainDamage);

        receiver.Stats.HPDes(lostHP);

    }
}