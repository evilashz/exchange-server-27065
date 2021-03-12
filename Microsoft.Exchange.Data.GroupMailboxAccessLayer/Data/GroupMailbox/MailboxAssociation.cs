using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAssociation
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000652F File Offset: 0x0000472F
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00006537 File Offset: 0x00004737
		public GroupMailboxLocator Group { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006540 File Offset: 0x00004740
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00006548 File Offset: 0x00004748
		public UserMailboxLocator User { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00006551 File Offset: 0x00004751
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00006559 File Offset: 0x00004759
		public SmtpAddress GroupSmtpAddress { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00006562 File Offset: 0x00004762
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000656A File Offset: 0x0000476A
		public SmtpAddress UserSmtpAddress { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00006573 File Offset: 0x00004773
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000657B File Offset: 0x0000477B
		public bool IsMember { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00006584 File Offset: 0x00004784
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000658C File Offset: 0x0000478C
		public string JoinedBy { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00006595 File Offset: 0x00004795
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000659D File Offset: 0x0000479D
		public bool IsPin { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000065A6 File Offset: 0x000047A6
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000065AE File Offset: 0x000047AE
		public bool ShouldEscalate { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000065B7 File Offset: 0x000047B7
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000065BF File Offset: 0x000047BF
		public bool IsAutoSubscribed { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000065C8 File Offset: 0x000047C8
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000065D0 File Offset: 0x000047D0
		public ExDateTime JoinDate { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000065D9 File Offset: 0x000047D9
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000065E1 File Offset: 0x000047E1
		public ExDateTime LastVisitedDate { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000065EA File Offset: 0x000047EA
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000065F2 File Offset: 0x000047F2
		public ExDateTime PinDate { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000065FB File Offset: 0x000047FB
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00006603 File Offset: 0x00004803
		public ExDateTime LastModified { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000660C File Offset: 0x0000480C
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00006614 File Offset: 0x00004814
		public string SyncedIdentityHash { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000661D File Offset: 0x0000481D
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00006625 File Offset: 0x00004825
		public int CurrentVersion { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000662E File Offset: 0x0000482E
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00006636 File Offset: 0x00004836
		public int SyncedVersion { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000663F File Offset: 0x0000483F
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00006647 File Offset: 0x00004847
		public string LastSyncError { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00006650 File Offset: 0x00004850
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00006658 File Offset: 0x00004858
		public int SyncAttempts { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00006661 File Offset: 0x00004861
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00006669 File Offset: 0x00004869
		public string SyncedSchemaVersion { get; set; }

		// Token: 0x060000B4 RID: 180 RVA: 0x00006674 File Offset: 0x00004874
		public bool IsOutOfSync(string expectedIdentityHash)
		{
			if (this.CurrentVersion > this.SyncedVersion)
			{
				MailboxAssociation.Tracer.TraceDebug((long)this.GetHashCode(), "MailboxAssociation::IsOutOfSync. Association {0}/{1} is out of sync because current version ({2}) is greater than synced version ({3})", new object[]
				{
					this.User,
					this.Group,
					this.CurrentVersion,
					this.SyncedVersion
				});
				return true;
			}
			if (!StringComparer.OrdinalIgnoreCase.Equals(this.SyncedIdentityHash, expectedIdentityHash))
			{
				MailboxAssociation.Tracer.TraceDebug((long)this.GetHashCode(), "MailboxAssociation::IsOutOfSync. Association {0}/{1} is out of sync because current identity hash of mailbox ({2}) is different than the one synced ({3})", new object[]
				{
					this.User,
					this.Group,
					expectedIdentityHash,
					this.SyncedIdentityHash
				});
				return true;
			}
			return false;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006730 File Offset: 0x00004930
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("Group={");
			stringBuilder.Append(this.Group);
			stringBuilder.Append("}, User={");
			stringBuilder.Append(this.User);
			stringBuilder.Append("}, GroupSmtpAddress=");
			stringBuilder.Append(this.GroupSmtpAddress);
			stringBuilder.Append(", UserSmtpAddress=");
			stringBuilder.Append(this.UserSmtpAddress);
			stringBuilder.Append(", IsMember=");
			stringBuilder.Append(this.IsMember);
			stringBuilder.Append(", JoinedBy=");
			stringBuilder.Append(this.JoinedBy);
			stringBuilder.Append(", JoinDate=");
			stringBuilder.Append(this.JoinDate);
			stringBuilder.Append(", IsPin=");
			stringBuilder.Append(this.IsPin);
			stringBuilder.Append(", ShouldEscalate=");
			stringBuilder.Append(this.ShouldEscalate);
			stringBuilder.Append(", IsAutoSubscribed=");
			stringBuilder.Append(this.IsAutoSubscribed);
			stringBuilder.Append(", LastVisitedDate=");
			stringBuilder.Append(this.LastVisitedDate);
			stringBuilder.Append(", PinDate=");
			stringBuilder.Append(this.PinDate);
			stringBuilder.Append(", SyncedIdentityHash=");
			stringBuilder.Append(this.SyncedIdentityHash);
			stringBuilder.Append(", CurrentVersion=");
			stringBuilder.Append(this.CurrentVersion);
			stringBuilder.Append(", SyncedVersion=");
			stringBuilder.Append(this.SyncedVersion);
			stringBuilder.Append(", LastSyncError=");
			stringBuilder.Append(this.LastSyncError);
			stringBuilder.Append(", SyncAttempts =");
			stringBuilder.Append(this.SyncAttempts);
			stringBuilder.Append(", SyncedSchemaVersion=");
			stringBuilder.Append(this.SyncedSchemaVersion);
			stringBuilder.Append(", LastModified=");
			stringBuilder.Append(this.LastModified);
			return stringBuilder.ToString();
		}

		// Token: 0x04000035 RID: 53
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;
	}
}
