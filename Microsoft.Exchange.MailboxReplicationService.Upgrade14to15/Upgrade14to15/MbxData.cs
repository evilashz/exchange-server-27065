using System;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A0 RID: 160
	public struct MbxData
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x000065EC File Offset: 0x000047EC
		public void Add(int count, double size)
		{
			this.Count += count;
			this.Size += size;
		}

		// Token: 0x040001D0 RID: 464
		public int Count;

		// Token: 0x040001D1 RID: 465
		public double Size;
	}
}
