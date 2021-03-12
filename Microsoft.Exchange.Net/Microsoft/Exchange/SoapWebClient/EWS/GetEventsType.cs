using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200044B RID: 1099
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetEventsType : BaseRequestType
	{
		// Token: 0x040016E2 RID: 5858
		public string SubscriptionId;

		// Token: 0x040016E3 RID: 5859
		public string Watermark;
	}
}
