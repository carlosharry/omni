using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Service
{
    public interface IDataService<TItem>
    {
        IEnumerable<TItem> Read(IEnumerable<KeyValuePair<string, string>> items);

        TItem Update(TItem editedItem);

        TItem Create(TItem newItem);

        bool Delete(TItem newItem);
    }
}
