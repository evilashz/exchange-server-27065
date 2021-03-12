using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000196 RID: 406
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class OccurrenceItemIdType : BaseItemIdType
	{
		// Token: 0x040009A6 RID: 2470
		[XmlAttribute]
		public string RecurringMasterId;

		// Token: 0x040009A7 RID: 2471
		[XmlAttribute]
		public string ChangeKey;

		// Token: 0x040009A8 RID: 2472
		[XmlAttribute]
		public int InstanceIndex;
	}
}
