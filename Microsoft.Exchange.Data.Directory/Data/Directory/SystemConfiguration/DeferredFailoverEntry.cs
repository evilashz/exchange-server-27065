using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003E0 RID: 992
	[Serializable]
	public class DeferredFailoverEntry
	{
		// Token: 0x06002D59 RID: 11609 RVA: 0x000BA79D File Offset: 0x000B899D
		public DeferredFailoverEntry(ADObjectId server, DateTime recoveryDueAt)
		{
			this.m_server = server;
			this.m_recoveryDueAt = recoveryDueAt;
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000BA7B3 File Offset: 0x000B89B3
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x000BA7BB File Offset: 0x000B89BB
		public ADObjectId Server
		{
			get
			{
				return this.m_server;
			}
			internal set
			{
				this.m_server = value;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000BA7C4 File Offset: 0x000B89C4
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x000BA7CC File Offset: 0x000B89CC
		public DateTime RecoveryDueAt
		{
			get
			{
				return this.m_recoveryDueAt;
			}
			internal set
			{
				this.m_recoveryDueAt = value;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000BA7D5 File Offset: 0x000B89D5
		public override string ToString()
		{
			return string.Format(DirectoryStrings.DeferredFailoverEntryString, this.Server.Name, this.RecoveryDueAt);
		}

		// Token: 0x04001E9C RID: 7836
		private ADObjectId m_server;

		// Token: 0x04001E9D RID: 7837
		private DateTime m_recoveryDueAt;
	}
}
