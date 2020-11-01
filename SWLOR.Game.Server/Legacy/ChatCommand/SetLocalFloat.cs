﻿using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Legacy.ChatCommand.Contracts;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;

namespace SWLOR.Game.Server.Legacy.ChatCommand
{
    [CommandDetails("Sets a local float on a target.", CommandPermissionType.DM | CommandPermissionType.Admin)]
    public class SetLocalFloat : IChatCommand
    {
        public void DoAction(NWPlayer user, NWObject target, NWLocation targetLocation, params string[] args)
        {
            if (!target.IsValid)
            {
                user.SendMessage("Target is invalid. Targeting area instead.");
                target = user.Area;
            }

            var variableName = args[0];
            var value = float.Parse(args[1]);

            NWScript.SetLocalFloat(target, variableName, value);

            user.SendMessage("Local float set: " + variableName + " = " + value);
        }

        public string ValidateArguments(NWPlayer user, params string[] args)
        {
            if (args.Length < 2)
            {
                return "Missing arguments. Format should be: /SetLocalFloat Variable_Name <VALUE>. Example: /SetLocalFloat MY_VARIABLE 6.9";
            }
            
            if (!float.TryParse(args[1], out var value))
            {
                return "Invalid value entered. Please try again.";
            }
            return string.Empty;
        }

        public bool RequiresTarget => true;
    }
}