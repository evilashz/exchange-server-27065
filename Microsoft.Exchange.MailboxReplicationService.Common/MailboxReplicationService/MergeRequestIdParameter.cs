using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	public sealed class MergeRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x0600108C RID: 4236 RVA: 0x00026E9B File Offset: 0x0002509B
		public MergeRequestIdParameter()
		{
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00026EA3 File Offset: 0x000250A3
		public MergeRequestIdParameter(MergeRequest request) : base(request)
		{
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00026EAC File Offset: 0x000250AC
		public MergeRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00026EB5 File Offset: 0x000250B5
		public MergeRequestIdParameter(MergeRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00026EBE File Offset: 0x000250BE
		public MergeRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00026EC7 File Offset: 0x000250C7
		public MergeRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00026ED0 File Offset: 0x000250D0
		public MergeRequestIdParameter(string request) : base(request)
		{
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00026ED9 File Offset: 0x000250D9
		public static MergeRequestIdParameter Parse(string request)
		{
			return new MergeRequestIdParameter(request);
		}
	}
}
