using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000016 RID: 22
	[KnownType(typeof(DroppedNotification))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public abstract class SequenceIssueNotification : BaseNotification
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003331 File Offset: 0x00001531
		public SequenceIssueNotification(NotificationType notificationType) : base(notificationType)
		{
		}
	}
}
