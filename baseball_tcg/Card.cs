using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


static class Const
{
    public const int LEFT = 1;
    public const int RIGHT = 2;
    public const int BOTH = 3;

    public const int P = 1;
    public const int C = 2;
    public const int FB = 3;
    public const int SB = 4;
    public const int TB = 5;
    public const int SS = 6;
    public const int LF = 7;
    public const int CF = 8;
    public const int RF = 9;
    public const int DH = 0;

    public const int FSEAM = 0;
    public const int CUTTER = 1;
    public const int SPLITTER = 2;
    public const int TSEAM = 3;
    public const int SLIDER = 4;
    public const int CURVE = 5;
    public const int CHANGEUP = 6;
    public const int SINKER = 7;
    public const int FORK = 8;
    public const int KNUCKLE = 9;
}

public class Card {
    public string card_name { get; protected set; }
    public int card_id { get; protected set; }
    public string card_team { get; protected set; }
}

public class Manager : Card
{
    public int card_ability { get; protected set; }

    public Manager(string name, string team, int id, int ability)
    {    
        card_id = id; card_name = name; card_team = team; card_ability = ability;
    }
}

public class Pitcher : Card
{
    public string card_pos { get; protected set; }
    public int card_throws { get; protected set; }
    public bool[] card_arsenal = new bool[10];
    public int card_pp { get; protected set; }
    public int card_ability { get; protected set; }
    public int card_stamina { get; protected set; }

    public Pitcher(){}

    public Pitcher(string name, int id, string team, int throws, bool[] arsenal, int pp, int stamina, int ability)
    {
        card_name = name; card_id = id; card_team = team; card_throws = throws;
        card_arsenal = arsenal; card_pp = pp; card_ability = ability; card_stamina = stamina;
    }
}

public class Fielder : Card
{
    public int card_bats { get; protected set; }
    public bool[] card_pos = new bool[10];
    public int card_bp { get; protected set; }
    public int card_con  { get; protected set; }
    public int card_def { get; protected set; }
    public int card_ability { get; protected set; }

    public Fielder(){}

    public Fielder(string name, int id, string team, int bats, bool[] pos, int bp, int con, int def, int ability)
    {
        card_name = name; card_id = id; card_team = team; card_bats = bats;
        card_pos = pos; card_bp = bp; card_con = con; card_def = def; card_ability = ability;
    }
}

public class Pitch : Card
{
    public int card_power { get; set; }
    public int card_effect { get; protected set; }

    public Pitch(){}

    public Pitch(string name, int id, int power)
    {
        card_name = name; card_id = id; card_power = power;
    }
}


