using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000274 RID: 628
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class NonIndexableItemStatisticType
	{
		// Token: 0x0400103D RID: 4157
		public string Mailbox;

		// Token: 0x0400103E RID: 4158
		public long ItemCount;

		// Token: 0x0400103F RID: 4159
		public string ErrorMessage;
	}
}
