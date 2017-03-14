using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Model
{
    public class DynamicSearchModel
    {
        public PropertyInfo objectProperty { get; set; }

        public IList<string> Values { get; set; }
    }
}
