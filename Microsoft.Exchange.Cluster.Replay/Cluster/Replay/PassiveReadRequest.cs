using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000373 RID: 883
	internal class PassiveReadRequest
	{
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x000A6A97 File Offset: 0x000A4C97
		// (set) Token: 0x06002375 RID: 9077 RVA: 0x000A6A9F File Offset: 0x000A4C9F
		public NetworkChannel Channel { get; set; }

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x000A6AA8 File Offset: 0x000A4CA8
		// (set) Token: 0x06002377 RID: 9079 RVA: 0x000A6AB0 File Offset: 0x000A4CB0
		public PassiveBlockMode Manager { get; set; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x000A6AB9 File Offset: 0x000A4CB9
		// (set) Token: 0x06002379 RID: 9081 RVA: 0x000A6AC1 File Offset: 0x000A4CC1
		public bool CompletedSynchronously { get; set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x000A6ACA File Offset: 0x000A4CCA
		// (set) Token: 0x0600237B RID: 9083 RVA: 0x000A6AD2 File Offset: 0x000A4CD2
		public bool CompletionWasProcessed { get; set; }

		// Token: 0x0600237C RID: 9084 RVA: 0x000A6ADB File Offset: 0x000A4CDB
		public PassiveReadRequest(PassiveBlockMode mgr, NetworkChannel channel)
		{
			this.Channel = channel;
			this.Manager = mgr;
		}
	}
}
