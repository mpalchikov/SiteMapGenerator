using SiteMapGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            new SiteMapBuilder().AddGenerator(new StaticLinks())
                 .AddGenerator(new ProductLinks())
                 .Build()
                 .Package.Save(Environment.CurrentDirectory);

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }

    class StaticLinks : ISiteMapGenerator
    {
        public IEnumerable<SiteMapUrl> Generate()
        {
            var links = new List<SiteMapUrl>();
            
            links.Add(new SiteMapUrl("http://site.com/contacts", DateTime.Now.AddDays(-1), ChangeFrequencyTypes.Yearly));
            links.Add(new SiteMapUrl("http://site.com/home"));            

            return links;
        }
    }

    class ProductLinks : ISiteMapGenerator
    {
        public IEnumerable<SiteMapUrl> Generate()
        {
            var links = new List<SiteMapUrl>();
            for (var i = 1; i < 1001; i++)
            {
                links.Add(new SiteMapUrl(string.Format("http://site.com/products/{0}", i), DateTime.Now, ChangeFrequencyTypes.Monthly, 1));
            }
            return links;
        }
    }
}
