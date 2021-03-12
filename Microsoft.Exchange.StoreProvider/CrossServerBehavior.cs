using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CrossServerBehavior
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002BF2 File Offset: 0x00000DF2
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002BFA File Offset: 0x00000DFA
		public string ClientId { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C03 File Offset: 0x00000E03
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002C0B File Offset: 0x00000E0B
		public bool PreExchange15 { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C14 File Offset: 0x00000E14
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002C1C File Offset: 0x00000E1C
		public bool ShouldTrace { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C25 File Offset: 0x00000E25
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002C2D File Offset: 0x00000E2D
		public bool ShouldLogInfoWatson { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002C36 File Offset: 0x00000E36
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002C3E File Offset: 0x00000E3E
		public bool ShouldBlock { get; private set; }

		// Token: 0x0600004B RID: 75 RVA: 0x00002C47 File Offset: 0x00000E47
		public CrossServerBehavior(string clientId, bool preExchange15, bool shouldTrace, bool shouldLogInfoWatson, bool shouldBlock)
		{
			this.ClientId = clientId;
			this.PreExchange15 = preExchange15;
			this.ShouldTrace = shouldTrace;
			this.ShouldLogInfoWatson = shouldLogInfoWatson;
			this.ShouldBlock = shouldBlock;
		}
	}
}
