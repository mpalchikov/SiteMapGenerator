using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SiteMapGenerator
{
    public class SiteMapBuilder
    {
        private List<ISiteMapGenerator> _generators;
        private SiteMapSerializer _siteMapSerializer;

        private List<SiteMapUrl> _urls;
        private SiteMapPackage _package;

        //TODO: up limit
        //Sitemap file must have no more than 50,000 URLs and must be no larger than 10MB (10,485,760 bytes)
        //when use 50000 items the file size is greater then 10Mb
        private const int SITE_MAP_URLS_MAX = 10000;

        private SiteMapBuilder() { }

        public SiteMapBuilder(string siteMapLocation = "")
        {
            _generators = new List<ISiteMapGenerator>();
            _siteMapSerializer = new SiteMapSerializer();
            _urls = new List<SiteMapUrl>();
            _package = new SiteMapPackage(siteMapLocation);
        }

        public IEnumerable<SiteMapUrl> Items
        {
            get
            {
                return _urls;
            }
        }

        public SiteMapPackage Package
        {
            get
            {
                return _package;
            }
        }

        public SiteMapBuilder AddGenerator(ISiteMapGenerator generator)
        {
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }

            _generators.Add(generator);

            return this;
        }

        public SiteMapBuilder Build(string siteMapIndexPath = "")
        {
            foreach (ISiteMapGenerator generator in _generators)
            {
                IEnumerable<SiteMapUrl> generatedItems = generator.Generate();
                if (generatedItems != null)
                {
                    _urls.AddRange(generatedItems.Where(u => !string.IsNullOrEmpty(u.Location)));
                }
            }

            if (_urls.Count > 0)
            {
                int i = 1;
                var urlsPortion = new List<SiteMapUrl>();
                foreach (SiteMapUrl url in _urls)
                {
                    urlsPortion.Add(url);
                    i++;

                    if (i == SITE_MAP_URLS_MAX)
                    {
                        _package.AddDocument(_siteMapSerializer.GetXml(urlsPortion));

                        urlsPortion.Clear();
                        i = 1;
                    }
                }

                if (urlsPortion.Count > 0)
                {
                    _package.AddDocument(_siteMapSerializer.GetXml(urlsPortion));
                }
            }

            return this;
        }
    }
}