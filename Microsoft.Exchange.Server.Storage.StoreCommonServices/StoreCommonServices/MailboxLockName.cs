using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000BA RID: 186
	public class MailboxLockName : MailboxLockNameBase
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x00024F62 File Offset: 0x00023162
		public MailboxLockName(Guid databaseGuid, int mailboxPartitionNumber) : base(databaseGuid, mailboxPartitionNumber)
		{
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00024F73 File Offset: 0x00023173
		public override Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00024F7B File Offset: 0x0002317B
		public override ILockName GetLockNameToCache()
		{
			return this;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00024F7E File Offset: 0x0002317E
		public override string GetFriendlyNameForLogging()
		{
			return string.Empty;
		}

		// Token: 0x04000461 RID: 1121
		private readonly Guid databaseGuid;
	}
}
