using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.EngineeringGuild
{
    public class AuxiliaryTargeterBasic: AbstractQuest
    {
        public AuxiliaryTargeterBasic()
        {
            CreateQuest(451, "Engineering Guild Task: 1x Auxiliary Targeter (Basic)", "eng_tsk_451")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "ssrang1", 1, true)

                .AddRewardGold(310)
                .AddRewardGuildPoints(GuildType.EngineeringGuild, 65);
        }
    }
}