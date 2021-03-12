using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000225 RID: 549
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class OccurrenceInfoType
	{
		// Token: 0x04000E1F RID: 3615
		public ItemIdType ItemId;

		// Token: 0x04000E20 RID: 3616
		public DateTime Start;

		// Token: 0x04000E21 RID: 3617
		public DateTime End;

		// Token: 0x04000E22 RID: 3618
		public DateTime OriginalStart;
	}
}
