using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Service
{
    public interface IDataService<TItem>
    {
        IEnumerable<TItem> Get();
        IEnumerable<TItem> Get(Func<TItem> getPredecate);

        IEnumerable<TItem> Get(IEnumerable<KeyValuePair<string, string>> items);

        TItem Edit(Func<TItem, bool> getPredicatiate, Action<TItem> editFunction);

        TItem Edit(TItem editedItem);

        TItem Add(TItem newItem);

        bool Delete(TItem newItem);
        bool Delete(Func<bool> deletePredecate);
    }
}
