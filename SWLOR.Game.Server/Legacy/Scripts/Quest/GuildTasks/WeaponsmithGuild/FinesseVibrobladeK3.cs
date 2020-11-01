using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.WeaponsmithGuild
{
    public class FinesseVibrobladeK3: AbstractQuest
    {
        public FinesseVibrobladeK3()
        {
            CreateQuest(304, "Weaponsmith Guild Task: 1x Finesse Vibroblade K3", "wpn_tsk_304")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "kukri_3", 1, true)

                .AddRewardGold(285)
                .AddRewardGuildPoints(GuildType.WeaponsmithGuild, 59);
        }
    }
}