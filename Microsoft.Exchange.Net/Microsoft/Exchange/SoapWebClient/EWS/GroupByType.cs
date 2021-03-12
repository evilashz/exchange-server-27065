using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003CF RID: 975
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GroupByType : BaseGroupByType
	{
		// Token: 0x04001565 RID: 5477
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		public BasePathToElementType Item;

		// Token: 0x04001566 RID: 5478
		public AggregateOnType AggregateOn;
	}
}
