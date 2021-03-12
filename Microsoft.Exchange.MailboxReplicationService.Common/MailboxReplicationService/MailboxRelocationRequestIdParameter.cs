using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	public sealed class MailboxRelocationRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00026E17 File Offset: 0x00025017
		public MailboxRelocationRequestIdParameter()
		{
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00026E1F File Offset: 0x0002501F
		public MailboxRelocationRequestIdParameter(MailboxRelocationRequest request) : base(request)
		{
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00026E28 File Offset: 0x00025028
		public MailboxRelocationRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00026E31 File Offset: 0x00025031
		public MailboxRelocationRequestIdParameter(MailboxRelocationRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00026E3A File Offset: 0x0002503A
		public MailboxRelocationRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00026E43 File Offset: 0x00025043
		public MailboxRelocationRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00026E4C File Offset: 0x0002504C
		public MailboxRelocationRequestIdParameter(string request) : base(request)
		{
		}
	}
}
