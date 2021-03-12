using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000011 RID: 17
	[KnownType(typeof(NewMailNotification))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[KnownType(typeof(RowNotification))]
	[KnownType(typeof(UnseenCountNotification))]
	[Serializable]
	public abstract class ApplicationNotification : BaseNotification
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000031BB File Offset: 0x000013BB
		protected ApplicationNotification(NotificationType notificationType) : base(notificationType)
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000031C4 File Offset: 0x000013C4
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000031CC File Offset: 0x000013CC
		[DataMember(EmitDefaultValue = false)]
		public string ConsumerSubscriptionId { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000031D5 File Offset: 0x000013D5
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000031E7 File Offset: 0x000013E7
		[DataMember(Name = "EventType", EmitDefaultValue = false)]
		public string EventTypeString
		{
			get
			{
				return this.EventType.ToString();
			}
			set
			{
				this.EventType = (QueryNotificationType)Enum.Parse(typeof(QueryNotificationType), value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003204 File Offset: 0x00001404
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000320C File Offset: 0x0000140C
		[IgnoreDataMember]
		internal QueryNotificationType EventType { get; set; }
	}
}
