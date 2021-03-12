using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000086 RID: 134
	[KnownType(typeof(CreateCalendarEventActionResult))]
	[DataContract]
	[KnownType(typeof(MoveActionResult))]
	internal abstract class ReplayActionResult
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0000AA4B File Offset: 0x00008C4B
		public ReplayActionResult()
		{
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000AA53 File Offset: 0x00008C53
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x0400036E RID: 878
		internal const ReplayActionResult Void = null;
	}
}
