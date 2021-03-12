using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000315 RID: 789
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetStreamingEventsResponseMessageType : ResponseMessageType
	{
		// Token: 0x0400131A RID: 4890
		[XmlArrayItem("Notification", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public NotificationType[] Notifications;

		// Token: 0x0400131B RID: 4891
		[XmlArrayItem("SubscriptionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] ErrorSubscriptionIds;

		// Token: 0x0400131C RID: 4892
		public ConnectionStatusType ConnectionStatus;

		// Token: 0x0400131D RID: 4893
		[XmlIgnore]
		public bool ConnectionStatusSpecified;
	}
}
