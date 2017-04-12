// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public interface IXmlIdentifiable
{
    string id { get; set; }
}

public partial class dataChemical : IXmlIdentifiable { }

public partial class dataBiologicalobject : IXmlIdentifiable { }

public partial class dataBiologicalprocess : IXmlIdentifiable { }

public partial class dataBiologicalaction : IXmlIdentifiable { }

public partial class dataTaxonomy : IXmlIdentifiable { }

public partial class dataKeyevent : IXmlIdentifiable
{
    // Extensions used for MIE and AO.
    public string evidencesupportingchemicalinitiation { get; set; }
    public string examplesusingadverseoutcome { get; set; }
}

public partial class dataKeyeventrelationship : IXmlIdentifiable { }

public partial class dataStressor : IXmlIdentifiable { }

public partial class dataAop : IXmlIdentifiable { }
