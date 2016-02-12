using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteMapGenerator
{
    public interface ISiteMapGenerator
    {
        IEnumerable<SiteMapUrl> Generate();
    }
}