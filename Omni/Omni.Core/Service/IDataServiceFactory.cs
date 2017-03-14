using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Service
{
    public interface IDataServiceFactory
    {
        IDataService<T> Create<T>() where T : class;
    }
}
