using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000013 RID: 19
	[KnownType(typeof(ConversationNotification))]
	[KnownType(typeof(PeopleIKnowNotification))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[KnownType(typeof(MessageItemNotification))]
	[KnownType(typeof(CalendarItemNotification))]
	[KnownType(typeof(ItemType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public abstract class RowNotification : ApplicationNotification
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000032D2 File Offset: 0x000014D2
		protected RowNotification(NotificationType notificationType) : base(notificationType)
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000032DB File Offset: 0x000014DB
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000032E3 File Offset: 0x000014E3
		[DataMember(EmitDefaultValue = false)]
		public ItemType Item { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000032EC File Offset: 0x000014EC
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000032F4 File Offset: 0x000014F4
		[DataMember(EmitDefaultValue = false)]
		public string Prior { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000032FD File Offset: 0x000014FD
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003305 File Offset: 0x00001505
		[DataMember]
		public string FolderId { get; set; }
	}
}
