using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200043F RID: 1087
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UpdateDelegateType : BaseDelegateType
	{
		// Token: 0x040016C1 RID: 5825
		[XmlArrayItem("DelegateUser", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public DelegateUserType[] DelegateUsers;

		// Token: 0x040016C2 RID: 5826
		public DeliverMeetingRequestsType DeliverMeetingRequests;

		// Token: 0x040016C3 RID: 5827
		[XmlIgnore]
		public bool DeliverMeetingRequestsSpecified;
	}
}
