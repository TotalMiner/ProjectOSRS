using ProjectOSRS.Sets;
using StudioForge.TotalMiner;
using StudioForge.TotalMiner.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectOSRS
{
    class ProjectOSRS : ITMPlugin
    {
        private ITMGame game;
        private ItemDictionary itemDictionary;
        private SetRegistry setRegistry;

        public void Draw(ITMPlayer player, ITMPlayer virtualPlayer)
        {
        }

        public bool HandleInput(ITMPlayer player)
        {
            return false;
        }

        public void Initialize(ITMPluginManager mgr, string path)
        {
            try
            {
                _Initialize(mgr, path);
            } catch (Exception e)
            {
                Logger.LogErr($"Failed to Initialize\n{e}");
                throw new Exception("Failed to Initialize");
            }
        }

        private void _Initialize(ITMPluginManager mgr, string path)
        {
            this.itemDictionary = new ItemDictionary(path, (Item)mgr.Offsets.ItemID);
            this.setRegistry = new SetRegistry();
            this.setRegistry.Register(new PartyhatSet());
            this.setRegistry.Register(new HalloweenMaskSet());
            this.setRegistry.Register(new BronzeLgSet());
            this.setRegistry.Register(new IronLgSet());
            this.setRegistry.Register(new SteelLgSet());
            this.setRegistry.Register(new BlackLgSet());
            this.setRegistry.Register(new MithrilLgSet());
            this.setRegistry.Register(new AdamantLgSet());
            this.setRegistry.Register(new RuneArmourLgSet());
            this.setRegistry.Register(new DragonArmourLgSet());
            this.setRegistry.Register(new GildedArmourLgSet());
        }

        public void InitializeGame(ITMGame game)
        {
            try
            {
                _InitializeGame(game);
            }
            catch (Exception e)
            {
                Logger.LogErr($"Failed to InitializeGame\n{e}");
                throw new Exception("Failed to InitializeGame");
            }
        }

        private void _InitializeGame(ITMGame game)
        {
            this.game = game;
            setRegistry.RegisterEvents(game);
            game.AddConsoleCommand((cmd, g, player, player2, output) =>
            {
                string[] args = cmd.Split(' ');
                Item item = itemDictionary.Get(args[1]);
                string idstring = Globals1.ItemData[(int)item].IDString;
                output.WriteLine($"{item}, {idstring}");
                output.WriteLine($"[{idstring.Equals(args[1], StringComparison.InvariantCultureIgnoreCase)}] {args[1]} == {idstring}");
            }, "regtest", "", "");
        }

        public void PlayerJoined(ITMPlayer player)
        {
        }

        public void PlayerLeft(ITMPlayer player)
        {
        }

        public void Update()
        {
        }

        public void Update(ITMPlayer player)
        {
        }

        public void WorldSaved(int version)
        {
        }
    }
}
