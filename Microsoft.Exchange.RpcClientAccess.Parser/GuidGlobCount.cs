using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000057 RID: 87
	public struct GuidGlobCount
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000955F File Offset: 0x0000775F
		public GuidGlobCount(Guid guid, ulong globCount)
		{
			GlobCountSet.VerifyGlobCountArgument(globCount, "globCount");
			this.guid = guid;
			this.globCount = globCount;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000957A File Offset: 0x0000777A
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009582 File Offset: 0x00007782
		public ulong GlobCount
		{
			get
			{
				return this.globCount;
			}
		}

		// Token: 0x04000115 RID: 277
		private Guid guid;

		// Token: 0x04000116 RID: 278
		private ulong globCount;
	}
}
