using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002EC RID: 748
	public interface IPagePatchReply
	{
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001E14 RID: 7700
		int PageNumber { get; }

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001E15 RID: 7701
		byte[] Token { get; }

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001E16 RID: 7702
		byte[] Data { get; }
	}
}
