using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF.TotalMiner
{
    public struct ModItemDataXML
    {
        public string ItemID;

        public string Name;

        public string Desc;

        public bool? IsValid;

        public bool? IsEnabled;

        public bool? LockedDD;

        public bool? LockedCR;

        public bool? LockedSU;

        public int? MinCSPrice;

        public int? StackSize;

        public ushort? Durability;

        public float? StrikeDamage;

        public float? StrikeReach;

        public short? HealPower;

        public ushort? BurnTime;

        public float? SmeltTime;

        public byte? ParticleLight;

        public ItemSelectModeFlag? SelectFlag;

        public bool? CanDropIfLocked;

        public ushort? DropChance;

        public PluralType? Plural;
    }
}
