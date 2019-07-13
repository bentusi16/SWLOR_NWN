﻿using NWN;
using SWLOR.Game.Server.Event;
using SWLOR.Game.Server.Scripting.Contracts;
using SWLOR.Game.Server.Service;


namespace SWLOR.Game.Server.Placeable.QuestSystem
{
    public class OnUsed: IScript
    {
        public void SubscribeEvents()
        {
        }

        public void UnsubscribeEvents()
        {
        }

        public void Main()
        {
            QuestService.OnQuestPlaceableUsed(NWGameObject.OBJECT_SELF);
        }
    }
}