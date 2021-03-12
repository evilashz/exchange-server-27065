using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000304 RID: 772
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FieldURIOrConstantType
	{
		// Token: 0x040012F9 RID: 4857
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		[XmlElement("Path", typeof(BasePathToElementType))]
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("Constant", typeof(ConstantValueType))]
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		public object Item;
	}
}
