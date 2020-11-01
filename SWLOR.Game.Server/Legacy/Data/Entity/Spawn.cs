using Dapper.Contrib.Extensions;
using SWLOR.Game.Server.Legacy.Data.Contracts;

namespace SWLOR.Game.Server.Legacy.Data.Entity
{
    [Table("Spawn")]
    public class Spawn: IEntity
    {
        [ExplicitKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public int SpawnObjectTypeID { get; set; }

        public IEntity Clone()
        {
            return new Spawn
            {
                ID = ID,
                Name = Name,
                SpawnObjectTypeID = SpawnObjectTypeID
            };
        }
    }
}