using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200002C RID: 44
	public class SessionsInfo
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000A504 File Offset: 0x00008704
		internal SessionsInfo()
		{
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000A50C File Offset: 0x0000870C
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000A514 File Offset: 0x00008714
		public int Count { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A51D File Offset: 0x0000871D
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000A525 File Offset: 0x00008725
		public bool Stopping { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A52E File Offset: 0x0000872E
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000A536 File Offset: 0x00008736
		public SessionInfo[] Sessions { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A53F File Offset: 0x0000873F
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000A547 File Offset: 0x00008747
		public UserInfo[] Users { get; set; }
	}
}
