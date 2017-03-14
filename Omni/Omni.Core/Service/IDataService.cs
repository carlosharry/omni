using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Service
{
    public interface IDataService<TItem>: IDisposable where TItem : class
    {
        IEnumerable<TItem> Read(IEnumerable<KeyValuePair<string, string>> items);

        TItem Update(TItem editedItem);

        TItem Create(TItem newItem);

        void Delete(TItem newItem);

        int SaveChanges();
    }
}
