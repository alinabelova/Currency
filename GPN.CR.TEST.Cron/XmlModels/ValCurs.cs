using System.Collections.Generic;
using System.Xml.Serialization;

namespace GPN.CR.TEST.Cron.XmlModels
{
    [XmlRoot("ValCurs")]
    public class ValCurs
    {
        [XmlAttribute("Date")]
        public string Date { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Valute")]
        public List<Valute> Valute { get; set; }
    }
}
