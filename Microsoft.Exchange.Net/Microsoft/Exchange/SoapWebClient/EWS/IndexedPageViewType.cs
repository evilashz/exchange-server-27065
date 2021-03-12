using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003EA RID: 1002
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class IndexedPageViewType : BasePagingType
	{
		// Token: 0x040015A0 RID: 5536
		[XmlAttribute]
		public int Offset;

		// Token: 0x040015A1 RID: 5537
		[XmlAttribute]
		public IndexBasePointType BasePoint;
	}
}
