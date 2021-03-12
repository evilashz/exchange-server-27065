using System;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors
{
	// Token: 0x020000BC RID: 188
	internal class MoveInfo
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x00010014 File Offset: 0x0000E214
		public MoveInfo(MoveStatus status, Guid moveRequestGuid)
		{
			this.Status = status;
			this.MoveRequestGuid = moveRequestGuid;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001002A File Offset: 0x0000E22A
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00010032 File Offset: 0x0000E232
		public MoveStatus Status { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0001003B File Offset: 0x0000E23B
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00010043 File Offset: 0x0000E243
		public Guid MoveRequestGuid { get; private set; }
	}
}
