using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003C8 RID: 968
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FieldOrderType
	{
		// Token: 0x04001557 RID: 5463
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		public BasePathToElementType Item;

		// Token: 0x04001558 RID: 5464
		[XmlAttribute]
		public SortDirectionType Order;
	}
}
