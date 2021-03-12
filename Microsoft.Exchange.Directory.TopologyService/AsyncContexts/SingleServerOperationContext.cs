using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Directory.TopologyService.AsyncContexts
{
	// Token: 0x02000024 RID: 36
	[DebuggerDisplay("{PartitionFqdn} - {ServerFqdn} - {Operation}")]
	internal class SingleServerOperationContext
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000A80F File Offset: 0x00008A0F
		public SingleServerOperationContext(string partitionFqdn, string serverFqdn, string operation)
		{
			this.PartitionFqdn = partitionFqdn;
			this.ServerFqdn = serverFqdn;
			this.Operation = operation;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000A82C File Offset: 0x00008A2C
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000A834 File Offset: 0x00008A34
		public string PartitionFqdn { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000A83D File Offset: 0x00008A3D
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000A845 File Offset: 0x00008A45
		public string ServerFqdn { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000A84E File Offset: 0x00008A4E
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000A856 File Offset: 0x00008A56
		public string Operation { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000A85F File Offset: 0x00008A5F
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000A867 File Offset: 0x00008A67
		public IAsyncResult AsyncResult { get; set; }
	}
}
