using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core.ModelsAggregate
{
    public abstract class Aggregate : Aggregate<long>
    {
    }

    public abstract class Aggregate<TId> : Entity, IAggregate<TId>
    {
        public long Version { get; set; } = -1;
        public TId Id { get; protected set; }
    }
}