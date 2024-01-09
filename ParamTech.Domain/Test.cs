using ParamTech.Core.Persistence.Repositoires.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamTech.Domain
{
    public class Test : Entity
    {
        public Guid TestId { get; set; }
        public string Name { get; set; }
    }
}
