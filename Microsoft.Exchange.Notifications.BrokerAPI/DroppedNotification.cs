using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class DroppedNotification : SequenceIssueNotification
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000333A File Offset: 0x0000153A
		public DroppedNotification() : base(NotificationType.Dropped)
		{
		}
	}
}
