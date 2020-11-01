using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.HuntersGuild
{
    public class MandalorianDogTags: AbstractQuest
    {
        public MandalorianDogTags()
        {
            CreateQuest(592, "Hunter's Guild Task: 6x Mandalorian Dog Tags", "hun_tsk_592")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "man_tags", 6, false)

                .AddRewardGold(80)
                .AddRewardGuildPoints(GuildType.HuntersGuild, 20);
        }
    }
}