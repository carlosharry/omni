using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Service
{
    public interface IDataService: IDisposable
    {
        IDataServiceRepository<TItem> Repository<TItem>() where TItem : class;

        int SaveChanges();
    }
}
