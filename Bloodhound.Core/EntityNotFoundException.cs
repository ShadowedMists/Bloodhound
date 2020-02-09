using System;
using System.Collections.Generic;
using System.Text;

namespace Bloodhound.Core
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string objectName, object idValue) : base(string.Format($"Unable to locate {objectName} by id {idValue}.")) { }
    }
}
