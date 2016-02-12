using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SiteMapGenerator
{    
    public enum ChangeFrequencyTypes
    {
        [XmlEnum("always")]
        Always,
        [XmlEnum("hourly")]
        Hourly,
        [XmlEnum("daily")]
        Daily,
        [XmlEnum("weekly")]
        Weekly,
        [XmlEnum("monthly")]
        Monthly,
        [XmlEnum("yearly")]
        Yearly,
        [XmlEnum("never")]
        Never
    }
}