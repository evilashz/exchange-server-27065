using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000318 RID: 792
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SubscribeResponseMessageType : ResponseMessageType
	{
		// Token: 0x04001322 RID: 4898
		public string SubscriptionId;

		// Token: 0x04001323 RID: 4899
		public string Watermark;
	}
}
