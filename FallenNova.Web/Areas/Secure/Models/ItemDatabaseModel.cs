using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FallenNova.Web.Areas.Secure.Models.ItemDatabaseModel
{
    public class ItemDatabaseModel
    {
        private string _keywords;

        [AllowHtml]
        [Display(Name = "Name")]
        public string Keywords 
        { 
            get { return (!string.IsNullOrWhiteSpace(_keywords) ? _keywords.Trim() : string.Empty); }
            set { _keywords = value; } 
        }

        [Display(Name = "Include Items")]
        public bool IncludeItems { get; set; }

        [Display(Name = "Include Galactic Stuff")]
        public bool IncludeGalacticObjects { get; set; }

        public IEnumerable<Item> Items { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }

            public string Action 
            {
                get { return Type.Replace(" ", string.Empty).ToLower(); }
            }
        }
    }

    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ConstellationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public IEnumerable<SolarSystem> SolarSystems { get; set; }

        public class SolarSystem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Security { get; set; }
        }
    }

    public class RegionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Constellation> Constellations { get; set; }

        public class Constellation
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public IEnumerable<SolarSystem> SolarSystems { get; set; }

            public class SolarSystem
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public double Security { get; set; }
            }
        }
    }

    public class SolarSystemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Security { get; set; }

        public int ConstellationId { get; set; }
        public string ConstellationName { get; set; }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
}