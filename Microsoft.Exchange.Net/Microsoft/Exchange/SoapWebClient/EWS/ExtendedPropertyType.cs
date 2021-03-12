using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001BD RID: 445
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ExtendedPropertyType
	{
		// Token: 0x04000A37 RID: 2615
		public PathToExtendedFieldType ExtendedFieldURI;

		// Token: 0x04000A38 RID: 2616
		[XmlElement("Values", typeof(NonEmptyArrayOfPropertyValuesType))]
		[XmlElement("Value", typeof(string))]
		public object Item;
	}
}
