using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000194 RID: 404
	[XmlInclude(typeof(RecurringMasterItemIdRangesType))]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ItemIdType : BaseItemIdType
	{
		// Token: 0x040009A2 RID: 2466
		[XmlAttribute]
		public string Id;

		// Token: 0x040009A3 RID: 2467
		[XmlAttribute]
		public string ChangeKey;
	}
}
