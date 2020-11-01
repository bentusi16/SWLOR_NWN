using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.WeaponsmithGuild
{
    public class HeavyVibrobladeGA3: AbstractQuest
    {
        public HeavyVibrobladeGA3()
        {
            CreateQuest(308, "Weaponsmith Guild Task: 1x Heavy Vibroblade GA3", "wpn_tsk_308")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "greataxe_3", 1, true)

                .AddRewardGold(285)
                .AddRewardGuildPoints(GuildType.WeaponsmithGuild, 59);
        }
    }
}