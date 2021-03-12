using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SyncStateCounter
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006E28 File Offset: 0x00005028
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00006E30 File Offset: 0x00005030
		public long BytesSent { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006E39 File Offset: 0x00005039
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00006E41 File Offset: 0x00005041
		public long BytesReceived { get; set; }

		// Token: 0x060000E9 RID: 233 RVA: 0x00006E4C File Offset: 0x0000504C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"BytesSent=",
				this.BytesSent,
				",BytesReceived=",
				this.BytesReceived
			});
		}
	}
}
