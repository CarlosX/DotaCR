using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dota2CustomRealms
{
    /// <summary>
    /// Provides drafting functionality
    /// </summary>
    /// <typeparam name="DraftType">Type of a pick in the draft</typeparam>
    /// <typeparam name="DraftPickStorageType">Either same type as DraftType for singular pick drafts, or a list of them for multiple pick drafts</typeparam>
    abstract class Draft<DraftType, DraftPickStorageType>
    {

        /// <summary>
        /// List of choices in draft pool
        /// </summary>
        public List<DraftType> AvailableDraftChoices;

        /// <summary>
        /// List of choices each player has made - Game class might need to manipulate this also
        /// </summary>
        public Dictionary<Player, DraftPickStorageType> PlayerDraftPicks = new Dictionary<Player, DraftPickStorageType>();

        /// <summary>
        /// List of turns in which players pick
        /// </summary>
        public List<Player> Turns = new List<Player>();

        /// <summary>
        /// Index in Turns list of current picker
        /// </summary>
        protected int CurrentTurn = 0;


        protected Stopwatch Timer = new Stopwatch();

        public bool IsPlayerTurn(Player Player)
        {
            if (CurrentTurn >= 0 && CurrentTurn < Turns.Count)
            {
                return (Turns[CurrentTurn] == Player);
            }
            return false;
            
        }

        public string GetCurrentPickersString()
        {
            /*if (CurrentTurn >= 0 && CurrentTurn < Turns.Count)
            {
                return Turns[CurrentTurn].FriendlyName;
            }
            return "";*/
            string Players = "";
            foreach (Player Player in GetCurrentPickers())
            {
                if (Players != "") Players += " + ";
                Players += Player.FriendlyName;
            }
            return Players;
        }

        public List<Player> GetCurrentPickers()
        {
            List<Player> Players = new List<Player>(2);
            int i = 0;
            while(CurrentTurn + i >= 0 && CurrentTurn + i < Turns.Count)
            {
                if (Turns[CurrentTurn].Side == Turns[CurrentTurn + i].Side)
                {
                    Players.Add(Turns[CurrentTurn + i]);
                }
                i++;
            }
            return Players;
        }

        public Player GetCurrentPicker()
        {
            if (CurrentTurn >= 0 && CurrentTurn < Turns.Count)
            {
                return Turns[CurrentTurn];
            }
            return null;
        }

        protected void NextTurn()
        {
            CurrentTurn++;
        }


        public int GetCurrentTurn()
        {
            return CurrentTurn;
        }

        public Draft(List<Player> Players, List<DraftType> Pool)
        {
            List<PlayerSide> SidePickOrder = new List<PlayerSide>(10);
            SidePickOrder.Add(PlayerSide.Radiant);
            SidePickOrder.Add(PlayerSide.Dire);
            SidePickOrder.Add(PlayerSide.Dire);
            SidePickOrder.Add(PlayerSide.Radiant);
            SidePickOrder.Add(PlayerSide.Radiant);
            SidePickOrder.Add(PlayerSide.Dire);
            SidePickOrder.Add(PlayerSide.Dire);
            SidePickOrder.Add(PlayerSide.Radiant);
            SidePickOrder.Add(PlayerSide.Radiant);
            SidePickOrder.Add(PlayerSide.Dire);

            List<Player> Radiant = new List<Player>(), Dire = new List<Player>();

            for(int i = 0; i < Players.Count ; i++)
            {
                if (Players[i].Side == PlayerSide.Radiant)
                {
                    Radiant.Add(Players[i]);
                }
                else if (Players[i].Side == PlayerSide.Dire)
                {
                    Dire.Add(Players[i]);
                }
            }

            int SideIndex = 0;

            while (Radiant.Count > 0 || Dire.Count > 0)
            {
                if (SidePickOrder[SideIndex] == PlayerSide.Radiant && Radiant.Count > 0)
                {
                    Turns.Add(Radiant[0]);
                    Radiant.RemoveAt(0);
                }
                else if (SidePickOrder[SideIndex] == PlayerSide.Dire && Dire.Count > 0)
                {
                    Turns.Add(Dire[0]);
                    Dire.RemoveAt(0);
                }
                SideIndex = (SideIndex + 1) % SidePickOrder.Count;
            }

            AvailableDraftChoices = new List<DraftType>(Pool.Count);
            AvailableDraftChoices.AddRange(Pool);

            Timer.Start();
        }

        /// <summary>
        /// Fires when something is picked by a player
        /// </summary>
        public event EventHandler<DraftPickEventArgs<DraftType>> DraftPlayerPick;

        public class DraftPickEventArgs<T> : EventArgs
        {

            public DraftPickEventArgs(Player player, T pick)
            {
                Player = player;
                Pick = pick;
            }

            public Player Player;
            public T Pick;
        }

        public abstract void UpdateUI();

        public virtual void Pick(Player Player, DraftType Pick)
        {

            int PickerIndex = CurrentTurn;
            Player temp;

            while(Turns[PickerIndex] != Player && PickerIndex < Turns.Count)
            {
                PickerIndex++;
            }

            if (PickerIndex != CurrentTurn)
            {
                temp = Turns[PickerIndex];
                Turns[PickerIndex] = Turns[CurrentTurn];
                Turns[CurrentTurn] = Turns[PickerIndex];
            }

            NextTurn();



            Timer.Stop();
            Timer.Reset();
            Timer.Start();
            if (DraftPlayerPick != null)
            {
                DraftPlayerPick(this, new DraftPickEventArgs<DraftType>(Player, Pick));
            }
        }

        public int GetTimeRemaining()
        {
            return (int)(30 - Timer.Elapsed.TotalSeconds);
        }

    }
}
