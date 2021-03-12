using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200044A RID: 1098
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetStreamingEventsType : BaseRequestType
	{
		// Token: 0x040016E0 RID: 5856
		[XmlArrayItem("SubscriptionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] SubscriptionIds;

		// Token: 0x040016E1 RID: 5857
		public int ConnectionTimeout;
	}
}
