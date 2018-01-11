using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF
{
    [Flags]
    public enum ItemSubType : ushort
    {
        None = 0,
        Bow = 1,
        Arrow = 2,
        Shield = 4,
        Edible = 8,
        TillTool = 16,
        HarvestTool = 32,
        Grenade = 64,
        GrenadeLauncher = 128,
        Key = 256,
        Door = 512,
        RangedWeapon = 1024,
        BlockCanBeOpened = 2048,
        Leaves = 4096,
        Gun = 8192,
        RapidSwing = 16384,
        Potion = 32768
    }
}
