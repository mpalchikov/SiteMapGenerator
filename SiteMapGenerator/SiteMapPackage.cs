using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SiteMapGenerator
{
    public class SiteMapPackage
    {
        private string _siteMapLocation;
        private List<XmlDocument> _siteMapDocuments;
        private SiteMapIndexWriter _siteMapIndexWriter;

        public SiteMapPackage(string siteMapLocation = "")
        {
            _siteMapIndexWriter = new SiteMapIndexWriter();
            _siteMapDocuments = new List<XmlDocument>();
            _siteMapLocation = siteMapLocation;
        }

        public IEnumerable<XmlDocument> Documents
        {
            get
            {
                return _siteMapDocuments;
            }
        }

        public void Save(string directoryPath)
        {
            var directory = new DirectoryInfo(directoryPath);
            if (directory.Exists && _siteMapDocuments.Count != 0)
            {
                if (_siteMapDocuments.Count > 1)
                {
                    if (!string.IsNullOrEmpty(_siteMapLocation))
                    {
                        //GetSiteMapIndex(_siteMapLocation).Save(string.Format("{0}/sitemap_index.xml", directory.FullName));
                    }

                    for (var i = 0; i < _siteMapDocuments.Count; i++)
                    {
                        _siteMapDocuments[i].Save(string.Format("{0}/sitemap{1}.xml", directory.FullName, i + 1));
                    }
                }
                else
                {
                    _siteMapDocuments.First().Save(string.Format("{0}/sitemap.xml", directory.FullName));
                }
            }
        }

        public void AddDocument(XmlDocument document)
        {
            _siteMapDocuments.Add(document);
        }

        private XmlDocument GetSiteMapIndex(string siteMapLocation)
        {
            var siteMapIndexData = new List<SiteMapIndexItem>();
            for (var i = 0; i < _siteMapDocuments.Count; i++)
            {
                siteMapIndexData.Add(new SiteMapIndexItem
                {
                    Location = string.Concat(siteMapLocation, string.Format("/sitemap{0}.xml", i + 1)),
                    LastModification = DateTime.Now
                });
            }
            return _siteMapIndexWriter.GetXml(siteMapIndexData);
        }
    }
}