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
        private ITMGame _game;
        private ItemDictionary itemDictionary;

        public void Draw(ITMPlayer player, ITMPlayer virtualPlayer)
        {
        }

        public bool HandleInput(ITMPlayer player)
        {
            return false;
        }

        public void Initialize(ITMPluginManager mgr, string path)
        {
            itemDictionary = new ItemDictionary(path, (Item)mgr.Offsets.ItemID);
        }

        public void InitializeGame(ITMGame game)
        {
            _game = game;
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
