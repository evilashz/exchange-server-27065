using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public sealed class MessageItemNotification : RowNotification
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003343 File Offset: 0x00001543
		public MessageItemNotification() : base(NotificationType.MessageItem)
		{
		}
	}
}
