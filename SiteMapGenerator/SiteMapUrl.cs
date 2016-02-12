using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SiteMapGenerator
{
    public class SiteMapUrl
    {
        [XmlElement("loc")]
        public string Location { get; set; }

        [XmlElement("lastmod")]        
        public DateTime LastModification { get; set; }

        [XmlElement("changefreq")]
        public ChangeFrequencyTypes ChangeFrequency { get; set; }

        [XmlElement("priority")]
        public double Priority { get; set; }

        private const double DEFAULT_PRIORITY = 0.8;

        public SiteMapUrl()
        {
            Location = string.Empty;
            LastModification = DateTime.Now;
            ChangeFrequency = ChangeFrequencyTypes.Monthly;
            Priority = DEFAULT_PRIORITY;
        }

        public SiteMapUrl(string location) : this()
        {
            Location = location;
        }

        public SiteMapUrl(string location, DateTime lastModification) : this(location)
        {
            LastModification = lastModification;
        }

        public SiteMapUrl(string location, DateTime lastModification, ChangeFrequencyTypes changeFrequency) : this(location, lastModification)
        {
            ChangeFrequency = changeFrequency;
        }

        public SiteMapUrl(string location, DateTime lastModification, ChangeFrequencyTypes changeFrequency, double priority) : this(location, lastModification, changeFrequency)
        {
            Priority = priority;
        }

    }
}