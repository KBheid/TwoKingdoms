using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    public string cardDescription;

    public int attack;
    public int health;
    public int cost;
    public Sprite cardImage;
    public Color backgroundColor;

    //public List<Effect> effects;
    public Effect[] effects;

    [Header("Playability")]
    public bool playableInFrontline;
    public bool playableInBackline;
    public bool playableInBuilding;
    public bool draggable = true;
    public bool played = false;
    public bool active = false;

    [Header("References")]
    public CardUI _cardUI;
    public CardHolder _cardHolder;

    private void Start()
    {
        _cardUI.SetCard(this);
	}

    public void PlayWithoutEvent(CardHolder ch)
    {
        if (_cardUI == null)
            Start();

        // Do play effects
        _cardHolder = ch;
        _cardUI.SetCard(this);

        CheckAndDoEffects(Effect.EffectType.Battlecry);

        _cardUI.UpdateUI();
    }

	public void Play(CardHolder ch, bool playerAction)
	{
        // TEST: Send card played command
        Command comm = (Command) ScriptableObject.CreateInstance(nameof(MinionPlayedCommand));
        MinionPlayedCommand mpc = (MinionPlayedCommand)comm.CreateInstance();
        mpc.SetOwner(this).SetCardHolder(ch).Do(playerAction);

    }
    
    public void Play(CardHolder ch)
	{
        // TEST: Send card played command
        Command comm = (Command) ScriptableObject.CreateInstance(nameof(MinionPlayedCommand));
        MinionPlayedCommand mpc = (MinionPlayedCommand)comm.CreateInstance();
        mpc.SetOwner(this).SetCardHolder(ch).Do(true);

    }

    public void Damage(int damage)
	{
        health -= damage;
        _cardUI.UpdateUI();

        // Do damage effects
        CheckAndDoEffects(Effect.EffectType.OnDamage);

        if (health <= 0)
            Die();
	}

    public void SetAttack(int attack) { this.attack = attack; _cardUI.UpdateUI(); }
    public void SetHealth(int health) { this.health = health; _cardUI.UpdateUI(); }

    public void Die()
	{
        // TEST: Send minion destroyed command
        Command comm = (Command) ScriptableObject.CreateInstance(nameof(MinionDeathCommand));
        MinionDeathCommand mdc = (MinionDeathCommand) comm.CreateInstance();
        mdc.SetOwner(this).Do(false);

        // Send death effects
        CheckAndDoEffects(Effect.EffectType.Deathrattle);

	}

    private void CheckAndDoEffects(Effect.EffectType type)
	{
        foreach (Effect e in effects)
        {
            if (e.effectType == type)
            {
                Effect effInst = (Effect) e.CreateInstance();

                effInst.SetOwner(this).Do(false);
            }
        }
    }

}
