using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002D3 RID: 723
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetDelegateResponseMessageType : BaseDelegateResponseMessageType
	{
		// Token: 0x04001243 RID: 4675
		public DeliverMeetingRequestsType DeliverMeetingRequests;

		// Token: 0x04001244 RID: 4676
		[XmlIgnore]
		public bool DeliverMeetingRequestsSpecified;
	}
}
