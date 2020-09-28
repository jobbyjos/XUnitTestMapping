using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Xunit;


namespace XUnitTestMapping
{
    public class TestMapping
    {
        [Fact]
        public void FindWrongCoordinates()
        {
            XmlDocument document = new XmlDocument();
            document.Load("polygons.kml");

            XmlNamespaceManager m = new XmlNamespaceManager(document.NameTable);
            m.AddNamespace("ns", "http://www.opengis.net/kml/2.2");

            var name = document.SelectSingleNode("ns:kml/ns:Document/ns:Folder/ns:Placemark/ns:name", m).InnerText;
        }
        [Fact]
        public void FindWrongCoordinatesUsingSerielizer()
        {
            using (var fileStream=File.Open("polygons.kml", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Kml));
                var myDocument = (Kml)serializer.Deserialize(fileStream);
                Dictionary<string, string[]> listOfCoordinates = new Dictionary<string, string[]>();
                int index = 1;
                foreach (var item in myDocument.Document.Folder.Placemark)
                {
                    var name = item.name;
                    var coordinates = item.Polygon.outerBoundaryIs.LinearRing.coordinates;
                    var splitCoordinates = coordinates.Split("\n");
                    //Name is not a valid key to use here. So just adding an index to the key value to make sure all locations are captured
                    listOfCoordinates.Add(name + '-' + index.ToString(), splitCoordinates);
                    index++;
                }
                Assert.Equal(599, listOfCoordinates.Count);
            }
        }
    }
}
