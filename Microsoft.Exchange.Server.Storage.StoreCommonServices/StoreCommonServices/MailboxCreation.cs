using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000BB RID: 187
	public struct MailboxCreation
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x00024F85 File Offset: 0x00023185
		private MailboxCreation(bool allowCreation, Guid? unifiedMailboxGuid, bool allowPartitionCreation)
		{
			this.allowCreation = allowCreation;
			this.unifiedMailboxGuid = unifiedMailboxGuid;
			this.allowPartitionCreation = allowPartitionCreation;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00024F9C File Offset: 0x0002319C
		public static MailboxCreation DontAllow
		{
			get
			{
				return new MailboxCreation(false, null, false);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00024FB9 File Offset: 0x000231B9
		public bool IsAllowed
		{
			get
			{
				return this.allowCreation;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00024FC1 File Offset: 0x000231C1
		public Guid? UnifiedMailboxGuid
		{
			get
			{
				return this.unifiedMailboxGuid;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00024FC9 File Offset: 0x000231C9
		public bool AllowPartitionCreation
		{
			get
			{
				return this.allowPartitionCreation;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00024FD1 File Offset: 0x000231D1
		public static MailboxCreation Allow(Guid? unifiedMailboxGuid)
		{
			return MailboxCreation.Allow(unifiedMailboxGuid, true);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00024FDA File Offset: 0x000231DA
		public static MailboxCreation Allow(Guid? unifiedMailboxGuid, bool allowPartitionCreation)
		{
			return new MailboxCreation(true, unifiedMailboxGuid, allowPartitionCreation);
		}

		// Token: 0x04000462 RID: 1122
		private bool allowCreation;

		// Token: 0x04000463 RID: 1123
		private Guid? unifiedMailboxGuid;

		// Token: 0x04000464 RID: 1124
		private bool allowPartitionCreation;
	}
}
