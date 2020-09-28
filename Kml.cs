using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XUnitTestMapping
{
    [XmlRoot("kml", Namespace = "http://www.opengis.net/kml/2.2")]
    public class Kml
    {
        //Checking github
        public Document Document { get; set; }
    }

    public class Document
    {
        public Folder Folder { get; set; }
    }

    public class Folder
    {
        public string name { get; set; }
        [XmlElement("Placemark")]
        public Placemark[] Placemark { get; set; }
    }

    public class Placemark
    {
        public string name { get; set; }
        public string styleUrl { get; set; }

        public Polygon Polygon { get; set; }
    }

    public class Polygon
    {
        public OuterBoundaryIs outerBoundaryIs { get; set; }
    }

    public class OuterBoundaryIs
    {
        public LinearRing LinearRing { get; set; }
    }

    public class LinearRing
    {
        public string coordinates { get; set; }
    }
}
