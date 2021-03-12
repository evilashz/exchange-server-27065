using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000156 RID: 342
	public class PrequarantinedMailbox
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000430E9 File Offset: 0x000412E9
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000430F1 File Offset: 0x000412F1
		public int CrashCount
		{
			get
			{
				return this.crashCount;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000430F9 File Offset: 0x000412F9
		public DateTime LastCrashTime
		{
			get
			{
				return this.lastCrashTime;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00043101 File Offset: 0x00041301
		public TimeSpan QuarantineDuration
		{
			get
			{
				return this.quarantineDuration;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00043109 File Offset: 0x00041309
		public string QuarantineReason
		{
			get
			{
				return this.quarantineReason;
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00043111 File Offset: 0x00041311
		internal static string TruncateQuarantineReason(string quarantineReason)
		{
			if (quarantineReason == null)
			{
				return null;
			}
			if (quarantineReason.Length <= PrequarantinedMailbox.QuarantineReasonLengthLimit)
			{
				return quarantineReason;
			}
			return quarantineReason.Substring(0, PrequarantinedMailbox.QuarantineReasonLengthLimit);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00043133 File Offset: 0x00041333
		public PrequarantinedMailbox(Guid mailboxGuid, int crashCount, DateTime lastCrashTime, TimeSpan quarantineDuration, string quarantineReason)
		{
			this.mailboxGuid = mailboxGuid;
			this.crashCount = crashCount;
			this.lastCrashTime = lastCrashTime;
			this.quarantineDuration = quarantineDuration;
			this.quarantineReason = PrequarantinedMailbox.TruncateQuarantineReason(quarantineReason);
		}

		// Token: 0x04000769 RID: 1897
		internal static readonly int QuarantineReasonLengthLimit = 8192;

		// Token: 0x0400076A RID: 1898
		private readonly Guid mailboxGuid;

		// Token: 0x0400076B RID: 1899
		private readonly int crashCount;

		// Token: 0x0400076C RID: 1900
		private readonly DateTime lastCrashTime;

		// Token: 0x0400076D RID: 1901
		private readonly TimeSpan quarantineDuration;

		// Token: 0x0400076E RID: 1902
		private readonly string quarantineReason;
	}
}
