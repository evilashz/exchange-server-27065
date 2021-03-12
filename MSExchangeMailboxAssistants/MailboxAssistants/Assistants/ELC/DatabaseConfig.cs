using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A2 RID: 162
	internal class DatabaseConfig
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x0002FBB1 File Offset: 0x0002DDB1
		internal DatabaseConfig()
		{
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0002FBC4 File Offset: 0x0002DDC4
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0002FBCC File Offset: 0x0002DDCC
		internal EnhancedTimeSpan? DumpsterRetentionPeriod
		{
			get
			{
				return this.dumpsterRetentionPeriod;
			}
			set
			{
				this.dumpsterRetentionPeriod = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0002FBD5 File Offset: 0x0002DDD5
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0002FBDD File Offset: 0x0002DDDD
		internal Unlimited<ByteQuantifiedSize>? DatabaseDumpsterWarningQuota
		{
			get
			{
				return this.databaseDumpsterWarningQuota;
			}
			set
			{
				this.databaseDumpsterWarningQuota = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0002FBE6 File Offset: 0x0002DDE6
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0002FBEE File Offset: 0x0002DDEE
		internal Unlimited<ByteQuantifiedSize>? DatabaseIssueWarningQuota
		{
			get
			{
				return this.databaseIssueWarningQuota;
			}
			set
			{
				this.databaseIssueWarningQuota = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0002FBF7 File Offset: 0x0002DDF7
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0002FBFF File Offset: 0x0002DDFF
		internal bool RetainDeletedItemsUntilBackup
		{
			get
			{
				return this.retainDeletedItemsUntilBackup;
			}
			set
			{
				this.retainDeletedItemsUntilBackup = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0002FC08 File Offset: 0x0002DE08
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0002FC10 File Offset: 0x0002DE10
		internal DateTime LastFullBackup
		{
			get
			{
				return this.lastFullBackup;
			}
			set
			{
				this.lastFullBackup = value;
			}
		}

		// Token: 0x04000492 RID: 1170
		private EnhancedTimeSpan? dumpsterRetentionPeriod;

		// Token: 0x04000493 RID: 1171
		private Unlimited<ByteQuantifiedSize>? databaseDumpsterWarningQuota;

		// Token: 0x04000494 RID: 1172
		private Unlimited<ByteQuantifiedSize>? databaseIssueWarningQuota;

		// Token: 0x04000495 RID: 1173
		private bool retainDeletedItemsUntilBackup;

		// Token: 0x04000496 RID: 1174
		private DateTime lastFullBackup = DateTime.MinValue;
	}
}
