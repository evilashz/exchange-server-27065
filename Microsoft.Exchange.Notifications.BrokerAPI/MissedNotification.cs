using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000019 RID: 25
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class MissedNotification : SequenceIssueNotification
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000334C File Offset: 0x0000154C
		public MissedNotification() : base(NotificationType.Missed)
		{
		}
	}
}
