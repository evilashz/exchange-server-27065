using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000519 RID: 1305
	[XmlType("GetStreamingEventsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetStreamingEventsResponseMessage : ResponseMessage
	{
		// Token: 0x06002575 RID: 9589 RVA: 0x000A5ADD File Offset: 0x000A3CDD
		public GetStreamingEventsResponseMessage()
		{
			this.ConnectionStatusSpecified = false;
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000A5AEC File Offset: 0x000A3CEC
		internal GetStreamingEventsResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000A5AF6 File Offset: 0x000A3CF6
		// (set) Token: 0x06002578 RID: 9592 RVA: 0x000A5AFE File Offset: 0x000A3CFE
		[DataMember(Name = "Notifications", IsRequired = true)]
		[XmlArrayItem(ElementName = "Notification", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArray(ElementName = "Notifications", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public EwsNotificationType[] Notifications { get; set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x000A5B07 File Offset: 0x000A3D07
		// (set) Token: 0x0600257A RID: 9594 RVA: 0x000A5B0F File Offset: 0x000A3D0F
		[XmlArrayItem(ElementName = "SubscriptionId", Type = typeof(string), Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArray(ElementName = "ErrorSubscriptionIds", IsNullable = false, Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string[] ErrorSubscriptionIds { get; set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x000A5B18 File Offset: 0x000A3D18
		// (set) Token: 0x0600257C RID: 9596 RVA: 0x000A5B20 File Offset: 0x000A3D20
		[XmlElement(ElementName = "ConnectionStatus", IsNullable = false, Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Type = typeof(ConnectionStatus))]
		public ConnectionStatus ConnectionStatus { get; set; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x000A5B29 File Offset: 0x000A3D29
		// (set) Token: 0x0600257E RID: 9598 RVA: 0x000A5B31 File Offset: 0x000A3D31
		[XmlIgnore]
		public bool ConnectionStatusSpecified { get; set; }

		// Token: 0x0600257F RID: 9599 RVA: 0x000A5B3A File Offset: 0x000A3D3A
		internal void AddNotifications(List<EwsNotificationType> notifications)
		{
			if (notifications.Count > 0)
			{
				this.Notifications = notifications.ToArray();
				return;
			}
			this.Notifications = null;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000A5B59 File Offset: 0x000A3D59
		internal void AddErrorSubscriptionIds(IEnumerable<string> ids)
		{
			if (ids != null)
			{
				this.ErrorSubscriptionIds = ids.ToArray<string>();
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000A5B6A File Offset: 0x000A3D6A
		internal void SetConnectionStatus(ConnectionStatus status)
		{
			this.ConnectionStatus = status;
			this.ConnectionStatusSpecified = true;
		}
	}
}
