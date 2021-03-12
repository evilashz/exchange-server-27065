using System;
using Microsoft.Exchange.Data.Serialization;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000277 RID: 631
	[Serializable]
	internal class TestNetworkParms
	{
		// Token: 0x06001895 RID: 6293 RVA: 0x000651F0 File Offset: 0x000633F0
		public static TestNetworkParms FromBytes(byte[] bytes)
		{
			return (TestNetworkParms)Serialization.BytesToObject(bytes);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0006520A File Offset: 0x0006340A
		public byte[] ToBytes()
		{
			return Serialization.ObjectToBytes(this);
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x00065212 File Offset: 0x00063412
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0006521A File Offset: 0x0006341A
		public bool Compression { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00065223 File Offset: 0x00063423
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x0006522B File Offset: 0x0006342B
		public bool Encryption { get; set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00065234 File Offset: 0x00063434
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x0006523C File Offset: 0x0006343C
		public int TcpWindowSize { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00065245 File Offset: 0x00063445
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x0006524D File Offset: 0x0006344D
		public long ReplyCount { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00065256 File Offset: 0x00063456
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x0006525E File Offset: 0x0006345E
		public int TransferSize { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00065267 File Offset: 0x00063467
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x0006526F File Offset: 0x0006346F
		public int TimeoutInSec { get; set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00065278 File Offset: 0x00063478
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x00065280 File Offset: 0x00063480
		public long TransferCount { get; set; }
	}
}
