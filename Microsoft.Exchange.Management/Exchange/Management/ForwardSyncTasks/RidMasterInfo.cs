using System;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000375 RID: 885
	internal class RidMasterInfo
	{
		// Token: 0x06001F0E RID: 7950 RVA: 0x00086224 File Offset: 0x00084424
		public RidMasterInfo(string ridMasterServer, int ridMasterVersionFromAD)
		{
			this.RidMasterServer = ridMasterServer;
			this.RidMasterVersionFromAD = ridMasterVersionFromAD;
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0008623A File Offset: 0x0008443A
		// (set) Token: 0x06001F10 RID: 7952 RVA: 0x00086242 File Offset: 0x00084442
		public string RidMasterServer { get; private set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0008624B File Offset: 0x0008444B
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x00086253 File Offset: 0x00084453
		public int RidMasterVersionFromAD { get; private set; }
	}
}
