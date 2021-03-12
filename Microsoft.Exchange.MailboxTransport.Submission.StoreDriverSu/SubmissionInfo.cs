using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	internal abstract class SubmissionInfo
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00008748 File Offset: 0x00006948
		protected SubmissionInfo(string serverDN, string serverFqdn, IPAddress networkAddress, Guid mdbGuid, string databaseName, DateTime originalCreateTime, TenantPartitionHint tenantHint, string mailboxHopLatency, LatencyTracker latencyTracker, bool shouldDeprioritize, bool shouldThrottle)
		{
			this.serverDN = serverDN;
			this.serverFqdn = serverFqdn;
			this.networkAddress = networkAddress;
			this.mdbGuid = mdbGuid;
			this.databaseName = databaseName;
			this.originalCreateTime = originalCreateTime;
			this.tenantHint = tenantHint;
			this.mailboxHopLatency = mailboxHopLatency;
			this.latencyTracker = latencyTracker;
			this.shouldDeprioritize = shouldDeprioritize;
			this.shouldThrottle = shouldThrottle;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000087B0 File Offset: 0x000069B0
		public string MailboxServerDN
		{
			get
			{
				return this.serverDN;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000087B8 File Offset: 0x000069B8
		public string MailboxFqdn
		{
			get
			{
				return this.serverFqdn;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000087C0 File Offset: 0x000069C0
		public IPAddress NetworkAddress
		{
			get
			{
				return this.networkAddress;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000087C8 File Offset: 0x000069C8
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000087D0 File Offset: 0x000069D0
		public DateTime OriginalCreateTime
		{
			get
			{
				return this.originalCreateTime;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000087D8 File Offset: 0x000069D8
		public TenantPartitionHint TenantHint
		{
			get
			{
				return this.tenantHint;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000087E0 File Offset: 0x000069E0
		public string MailboxHopLatency
		{
			get
			{
				return this.mailboxHopLatency;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000087E8 File Offset: 0x000069E8
		public LatencyTracker LatencyTracker
		{
			get
			{
				return this.latencyTracker;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000087F0 File Offset: 0x000069F0
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000087F8 File Offset: 0x000069F8
		public bool ShouldDeprioritize
		{
			get
			{
				return this.shouldDeprioritize;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00008800 File Offset: 0x00006A00
		public bool ShouldThrottle
		{
			get
			{
				return this.shouldThrottle;
			}
		}

		// Token: 0x060000F6 RID: 246
		public abstract SubmissionItem CreateSubmissionItem(MailItemSubmitter context);

		// Token: 0x060000F7 RID: 247
		public abstract OrganizationId GetOrganizationId();

		// Token: 0x060000F8 RID: 248
		public abstract SenderGuidTraceFilter GetTraceFilter();

		// Token: 0x060000F9 RID: 249
		public abstract SubmissionPoisonContext GetPoisonContext();

		// Token: 0x060000FA RID: 250
		public abstract void LogEvent(SubmissionInfo.Event submissionInfoEvent);

		// Token: 0x060000FB RID: 251
		public abstract void LogEvent(SubmissionInfo.Event submissionInfoEvent, Exception exception);

		// Token: 0x04000073 RID: 115
		private readonly string serverDN;

		// Token: 0x04000074 RID: 116
		private readonly string serverFqdn;

		// Token: 0x04000075 RID: 117
		private readonly IPAddress networkAddress;

		// Token: 0x04000076 RID: 118
		private readonly Guid mdbGuid;

		// Token: 0x04000077 RID: 119
		private readonly string databaseName;

		// Token: 0x04000078 RID: 120
		private readonly DateTime originalCreateTime;

		// Token: 0x04000079 RID: 121
		private readonly TenantPartitionHint tenantHint;

		// Token: 0x0400007A RID: 122
		private readonly string mailboxHopLatency;

		// Token: 0x0400007B RID: 123
		private readonly LatencyTracker latencyTracker;

		// Token: 0x0400007C RID: 124
		private readonly bool shouldDeprioritize;

		// Token: 0x0400007D RID: 125
		private readonly bool shouldThrottle;

		// Token: 0x02000018 RID: 24
		internal enum Event
		{
			// Token: 0x0400007F RID: 127
			StoreDriverSubmissionPoisonMessage,
			// Token: 0x04000080 RID: 128
			StoreDriverSubmissionPoisonMessageInSubmission,
			// Token: 0x04000081 RID: 129
			FailedToGenerateNdrInSubmission,
			// Token: 0x04000082 RID: 130
			InvalidSender
		}
	}
}
