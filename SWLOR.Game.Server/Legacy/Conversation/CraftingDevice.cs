﻿using System;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Legacy.Data.Entity;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;
using SWLOR.Game.Server.Legacy.ValueObject.Dialog;

namespace SWLOR.Game.Server.Legacy.Conversation
{
    public class CraftingDevice: ConversationBase
    {
        public override PlayerDialog SetUp(NWPlayer player)
        {
            var dialog = new PlayerDialog("MainPage");
            var mainPage = new DialogPage(
                "Please select a blueprint. Only blueprints you've learned will be displayed here. Learn more blueprints by purchasing crafting perks!"
            );
            var blueprintListPage = new DialogPage(
                "Please select a blueprint. Only blueprints you've learned will be displayed here. Learn more blueprints by purchasing crafting perks!"
            );

            dialog.AddPage("MainPage", mainPage);
            dialog.AddPage("BlueprintListPage", blueprintListPage);
            return dialog;
        }

        public override void Initialize()
        {
            LoadCategoryResponses();
        }

        public override void DoAction(NWPlayer player, string pageName, int responseID)
        {
            switch (pageName)
            {
                case "MainPage":
                    HandleCategoryResponse(responseID);
                    break;
                case "BlueprintListPage":
                    HandleBlueprintListResponse(responseID);
                    break;
            }
        }

        public override void Back(NWPlayer player, string beforeMovePage, string afterMovePage)
        {
        }

        public override void EndDialog()
        {
        }


        private void LoadCategoryResponses()
        {
            var device = (NWPlaceable)GetDialogTarget();
            var deviceID = device.GetLocalInt("CRAFT_DEVICE_ID");
            var categories = CraftService.GetCategoriesAvailableToPCByDeviceID(GetPC().GlobalID, deviceID);

            ClearPageResponses("MainPage");

            var lastBlueprintId = GetPC().GetLocalInt("LAST_CRAFTED_BLUEPRINT_ID_" + deviceID);
            var bp = CraftService.GetBlueprintByID(lastBlueprintId);

            if(bp != null)
                AddResponseToPage("MainPage", bp.Quantity + "x " + bp.ItemName, bp.IsActive, new Tuple<int, Type>(bp.ID, typeof(CraftBlueprint)));

            AddResponseToPage("MainPage", "Scrap Item");

            foreach (var category in categories)
            {
                AddResponseToPage("MainPage", category.Name, category.IsActive, new Tuple<int, Type>(category.ID, typeof(CraftBlueprintCategory)));
            }
        }

        private void LoadBlueprintListPage(int categoryID)
        {
            var device = GetDialogTarget();
            var deviceID = device.GetLocalInt("CRAFT_DEVICE_ID");

            var blueprints = CraftService.GetPCBlueprintsByDeviceAndCategoryID(GetPC().GlobalID, deviceID, categoryID);

            ClearPageResponses("BlueprintListPage");
            foreach (var bp in blueprints)
            {
                AddResponseToPage("BlueprintListPage", bp.Quantity + "x " + bp.ItemName, bp.IsActive, bp.ID);
            }
        }
        
        private void HandleCategoryResponse(int responseID)
        {
            var response = GetResponseByID("MainPage", responseID);
            
            if (response.CustomData == null) // 2 = Scrap Item
            {
                OpenScrapperInventory();
                return;
            }

            var customData = (Tuple<int,Type>)response.CustomData;

            if (customData.Item2 == typeof(CraftBlueprint)) // Craft last item
            {
                LoadCraftPage(customData.Item1);
            }
            else // Blueprint List
            {
                LoadBlueprintListPage(customData.Item1);
                ChangePage("BlueprintListPage");
            }
        }

        private void HandleBlueprintListResponse(int responseID)
        {
            var response = GetResponseByID("BlueprintListPage", responseID);
            var blueprintID = (int)response.CustomData;
            LoadCraftPage(blueprintID);
        }

        private void LoadCraftPage(int blueprintID)
        {
            var model = CraftService.GetPlayerCraftingData(GetPC());
            model.BlueprintID = blueprintID;
            SwitchConversation("CraftItem");
        }


        private void OpenScrapperInventory()
        {
            var model = CraftService.GetPlayerCraftingData(GetPC());
            NWPlaceable container = NWScript.CreateObject(ObjectType.Placeable, "cft_scrapper", GetPC().Location);
            container.IsLocked = false;
            model.IsAccessingStorage = true;
            
            GetPC().AssignCommand(() => NWScript.ActionInteractObject(container.Object));
            EndConversation();
        }


    }
}