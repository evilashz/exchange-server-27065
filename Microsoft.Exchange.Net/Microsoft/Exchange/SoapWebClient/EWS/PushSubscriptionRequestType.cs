using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B6 RID: 950
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PushSubscriptionRequestType : BaseSubscriptionRequestType
	{
		// Token: 0x040014F5 RID: 5365
		public int StatusFrequency;

		// Token: 0x040014F6 RID: 5366
		public string URL;

		// Token: 0x040014F7 RID: 5367
		public string CallerData;
	}
}
