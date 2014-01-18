using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dota2CustomRealms
{
    enum HeroMode
    {
        Unknown = 0, All_Pick = 1, All_Random = 2, Draft = 3, NA = 4
    }

    enum SkillMode
    {
        Unknown = 0, All_Random = 1, Draft = 2, All_Pick = 3, NA = 4
    }

    enum GameMode
    {
        Unknown = 0, All_Pick = 1, Captains_Mode = 2, Random_Draft = 3, Single_Draft = 4, All_Random = 5, Diretide = 7, Reverse_Captains_Mode = 8, Greevilings = 9, Mid_Only = 11, New_Player_Pool = 13, OMG = 14, OMG_Balanced = 15, LOD = 16, OMG_Diretide = 17, OMG_Greevilings = 18, OMG_Mid_Only = 19
    }

    enum PlayerSide
    {
        Spectator = 0, Radiant = 1, Dire = 2
    }

    enum HeroSide
    {
        Radiant = 0, Dire = 1, Disabled = 2
    }

    enum HeroType
    {
        Strength = 0, Agility = 1, Intelligence = 2, Disabled = 3
    }
}
