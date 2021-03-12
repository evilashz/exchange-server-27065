using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000026 RID: 38
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public sealed class MessageItemSubscription : RowSubscription
	{
		// Token: 0x060000DE RID: 222 RVA: 0x0000391D File Offset: 0x00001B1D
		public MessageItemSubscription() : base(NotificationType.MessageItem)
		{
		}
	}
}
