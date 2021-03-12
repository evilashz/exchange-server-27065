using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200002C RID: 44
	internal class AmAcllArgs
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000C836 File Offset: 0x0000AA36
		public AmAcllArgs()
		{
			this.MountDialOverride = DatabaseMountDialOverride.Lossless;
			this.SkipValidationChecks = AmBcsSkipFlags.None;
			this.MountPending = true;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000C853 File Offset: 0x0000AA53
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000C85B File Offset: 0x0000AA5B
		public int NumRetries { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000C864 File Offset: 0x0000AA64
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000C86C File Offset: 0x0000AA6C
		public int E00TimeoutMs { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000C875 File Offset: 0x0000AA75
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000C87D File Offset: 0x0000AA7D
		public int NetworkIOTimeoutMs { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000C886 File Offset: 0x0000AA86
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000C88E File Offset: 0x0000AA8E
		public int NetworkConnectTimeoutMs { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000C897 File Offset: 0x0000AA97
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000C89F File Offset: 0x0000AA9F
		public bool MountPending { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		public AmServerName SourceServer { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000C8B9 File Offset: 0x0000AAB9
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000C8C1 File Offset: 0x0000AAC1
		public AmDbActionCode ActionCode { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000C8CA File Offset: 0x0000AACA
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000C8D2 File Offset: 0x0000AAD2
		public AmBcsSkipFlags SkipValidationChecks { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000C8DB File Offset: 0x0000AADB
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000C8E3 File Offset: 0x0000AAE3
		public DatabaseMountDialOverride MountDialOverride { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public string UniqueOperationId { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000C905 File Offset: 0x0000AB05
		public int SubactionAttemptNumber { get; set; }
	}
}
