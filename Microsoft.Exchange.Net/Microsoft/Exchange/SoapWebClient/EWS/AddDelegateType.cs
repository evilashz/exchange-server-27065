using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000441 RID: 1089
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class AddDelegateType : BaseDelegateType
	{
		// Token: 0x040016C5 RID: 5829
		[XmlArrayItem("DelegateUser", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public DelegateUserType[] DelegateUsers;

		// Token: 0x040016C6 RID: 5830
		public DeliverMeetingRequestsType DeliverMeetingRequests;

		// Token: 0x040016C7 RID: 5831
		[XmlIgnore]
		public bool DeliverMeetingRequestsSpecified;
	}
}
