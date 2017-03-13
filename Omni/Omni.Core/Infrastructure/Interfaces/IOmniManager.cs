using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Infrastructure.Interfaces
{
    public interface IOmniManager
    {
        T GetService<T>() where T : class;
        object GetService(Type theType);
    }
}
