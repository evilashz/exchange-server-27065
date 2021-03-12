using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	internal abstract class SubmissionInfo
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003FC3 File Offset: 0x000021C3
		protected SubmissionInfo(string serverDN, string serverFqdn, IPAddress networkAddress, Guid mdbGuid, bool isShadowSupported, DateTime originalCreateTime, string mailboxHopLatency)
		{
			this.serverDN = serverDN;
			this.serverFqdn = serverFqdn;
			this.networkAddress = networkAddress;
			this.mdbGuid = mdbGuid;
			this.isShadowSupported = isShadowSupported;
			this.originalCreateTime = originalCreateTime;
			this.mailboxHopLatency = mailboxHopLatency;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004000 File Offset: 0x00002200
		public string MailboxServerDN
		{
			get
			{
				return this.serverDN;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004008 File Offset: 0x00002208
		public string MailboxFqdn
		{
			get
			{
				return this.serverFqdn;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004010 File Offset: 0x00002210
		public IPAddress NetworkAddress
		{
			get
			{
				return this.networkAddress;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004018 File Offset: 0x00002218
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004020 File Offset: 0x00002220
		public bool IsShadowSupported
		{
			get
			{
				return this.isShadowSupported;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004028 File Offset: 0x00002228
		public DateTime OriginalCreateTime
		{
			get
			{
				return this.originalCreateTime;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004030 File Offset: 0x00002230
		public string MailboxHopLatency
		{
			get
			{
				return this.mailboxHopLatency;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004038 File Offset: 0x00002238
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004040 File Offset: 0x00002240
		public long ContentHash
		{
			get
			{
				return this.contentHash;
			}
			set
			{
				this.contentHash = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000091 RID: 145
		public abstract bool IsShadowSubmission { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004049 File Offset: 0x00002249
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004051 File Offset: 0x00002251
		internal string DatabaseName { get; set; }

		// Token: 0x06000094 RID: 148
		public abstract SubmissionItem CreateSubmissionItem(MailItemSubmitter context);

		// Token: 0x06000095 RID: 149
		public abstract OrganizationId GetOrganizationId();

		// Token: 0x06000096 RID: 150
		public abstract SenderGuidTraceFilter GetTraceFilter();

		// Token: 0x06000097 RID: 151
		public abstract string GetPoisonId();

		// Token: 0x04000054 RID: 84
		protected static readonly Trace Diag = ExTraceGlobals.MapiSubmitTracer;

		// Token: 0x04000055 RID: 85
		private readonly string serverDN;

		// Token: 0x04000056 RID: 86
		private readonly string serverFqdn;

		// Token: 0x04000057 RID: 87
		private readonly IPAddress networkAddress;

		// Token: 0x04000058 RID: 88
		private readonly Guid mdbGuid;

		// Token: 0x04000059 RID: 89
		private readonly bool isShadowSupported;

		// Token: 0x0400005A RID: 90
		private long contentHash;

		// Token: 0x0400005B RID: 91
		private DateTime originalCreateTime;

		// Token: 0x0400005C RID: 92
		private string mailboxHopLatency;

		// Token: 0x02000012 RID: 18
		internal enum Event
		{
			// Token: 0x0400005F RID: 95
			StoreDriverPoisonMessage,
			// Token: 0x04000060 RID: 96
			StoreDriverPoisonMessageInSubmission,
			// Token: 0x04000061 RID: 97
			FailedToGenerateNdrInSubmission,
			// Token: 0x04000062 RID: 98
			InvalidSender
		}
	}
}
