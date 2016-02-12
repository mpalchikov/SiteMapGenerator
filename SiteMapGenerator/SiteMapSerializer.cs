using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SiteMapGenerator
{
    class SiteMapSerializer
    {
        public XmlDocument GetXml(IEnumerable<SiteMapUrl> urls)
        {
            var doc = new XmlDocument();
            var siteMap = new SiteMap { Urls = new List<SiteMapUrl>(urls) };
            var xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8
            };

            using (var memoryStream = new MemoryStream())
            using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
            {
                var serializer = new XmlSerializer(typeof(SiteMap));
                serializer.Serialize(xmlWriter, siteMap);

                memoryStream.Seek(0, SeekOrigin.Begin);
                doc.Load(memoryStream);
            }
            return doc;
        }
    }
}