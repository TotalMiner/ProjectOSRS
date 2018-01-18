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
            this.itemDictionary = new ItemDictionary(path, (Item)mgr.Offsets.ItemID);
            this.setRegistry = new SetRegistry();
            setRegistry.Register(new PartyhatSet());
        }

        public void InitializeGame(ITMGame game)
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
            Logger.Log($"First item id from Globals1 is {Globals1.ItemData[(int)ItemDictionary._firstItem].IDString}");
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
