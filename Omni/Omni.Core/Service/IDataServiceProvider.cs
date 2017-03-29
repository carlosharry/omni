using Omni.Core.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Service
{
    public interface IDataServiceProvider
    {
        void Setup(IOmniManager manager);

        IDataService Create();
    }
}
