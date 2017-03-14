using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Service
{
    public interface IDataService<TItem>: IDisposable where TItem : class
    {
        IDataServiceRepository<T1> Repository<T1>() where T1 : class;

        int SaveChanges();
    }
}
