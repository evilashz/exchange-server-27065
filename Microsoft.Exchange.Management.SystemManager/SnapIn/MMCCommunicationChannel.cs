using System;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000285 RID: 645
	public class MMCCommunicationChannel
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00078204 File Offset: 0x00076404
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0007820C File Offset: 0x0007640C
		public WritableSharedDataItem Channel { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00078215 File Offset: 0x00076415
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x0007821D File Offset: 0x0007641D
		public bool Initiated { get; set; }
	}
}
