using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000234 RID: 564
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetStreamingEventsResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000264E2 File Offset: 0x000246E2
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x000264EA File Offset: 0x000246EA
		[XmlArrayItem("Notification", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public NotificationType[] Notifications
		{
			get
			{
				return this.notificationsField;
			}
			set
			{
				this.notificationsField = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x000264F3 File Offset: 0x000246F3
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x000264FB File Offset: 0x000246FB
		[XmlArrayItem("SubscriptionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] ErrorSubscriptionIds
		{
			get
			{
				return this.errorSubscriptionIdsField;
			}
			set
			{
				this.errorSubscriptionIdsField = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00026504 File Offset: 0x00024704
		// (set) Token: 0x0600157A RID: 5498 RVA: 0x0002650C File Offset: 0x0002470C
		public ConnectionStatusType ConnectionStatus
		{
			get
			{
				return this.connectionStatusField;
			}
			set
			{
				this.connectionStatusField = value;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00026515 File Offset: 0x00024715
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x0002651D File Offset: 0x0002471D
		[XmlIgnore]
		public bool ConnectionStatusSpecified
		{
			get
			{
				return this.connectionStatusFieldSpecified;
			}
			set
			{
				this.connectionStatusFieldSpecified = value;
			}
		}

		// Token: 0x04000EC8 RID: 3784
		private NotificationType[] notificationsField;

		// Token: 0x04000EC9 RID: 3785
		private string[] errorSubscriptionIdsField;

		// Token: 0x04000ECA RID: 3786
		private ConnectionStatusType connectionStatusField;

		// Token: 0x04000ECB RID: 3787
		private bool connectionStatusFieldSpecified;
	}
}
