using System;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x0200008E RID: 142
	internal sealed class MonitoringOptions
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00015ECE File Offset: 0x000140CE
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00015ED6 File Offset: 0x000140D6
		public int AgentExecutionLimitInMilliseconds
		{
			get
			{
				return this.agentExecutionLimitInMilliseconds;
			}
			set
			{
				this.agentExecutionLimitInMilliseconds = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00015EDF File Offset: 0x000140DF
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00015EE7 File Offset: 0x000140E7
		public bool MessageSnapshotEnabled
		{
			get
			{
				return this.messageSnapshotEnabled;
			}
			set
			{
				this.messageSnapshotEnabled = value;
			}
		}

		// Token: 0x040004DE RID: 1246
		private int agentExecutionLimitInMilliseconds = 90000;

		// Token: 0x040004DF RID: 1247
		private bool messageSnapshotEnabled = true;
	}
}
