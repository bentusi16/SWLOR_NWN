﻿using System;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;

namespace SWLOR.Game.Server.Legacy.Scripts.Placeable.CraftingDevice
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
            NWPlayer player = (NWScript.GetLastUsedBy());
            NWPlaceable device = (NWScript.OBJECT_SELF);

            // If a structure ID is defined, we need to make sure the building is set to Workshop mode.
            var structureID = device.GetLocalString("PC_BASE_STRUCTURE_ID");
            if (!string.IsNullOrWhiteSpace(structureID))
            {
                var structureGuid = new Guid(structureID);
                var structure = DataService.PCBaseStructure.GetByID(structureGuid);

                // Workbenches and crafting devices can only be used inside 
                // buildings set to "Workshop" mode.
                if(structure.ParentPCBaseStructureID != null)
                {
                    var buildingStructure = DataService.PCBaseStructure.GetByID((Guid)structure.ParentPCBaseStructureID);
                    var modeType = (StructureModeType) buildingStructure.StructureModeID;

                    if (modeType != StructureModeType.Workshop)
                    {
                        player.FloatingText("Workbenches and crafting devices may only be used when the building is set to 'Workshop' mode.");
                        return;
                    }
                }

            }

            if (player.IsBusy)
            {
                player.SendMessage("You are too busy to do that right now.");
                return;
            }
            DialogService.StartConversation(player, device, "CraftingDevice");
        }
    }
}