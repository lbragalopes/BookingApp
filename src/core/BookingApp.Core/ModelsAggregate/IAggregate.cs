using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core.ModelsAggregate;

public interface IAggregate : IEntity
{
  long Version { get; set; }
}

public interface IAggregate<out T> : IAggregate
{
    T Id { get; }
}
