using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x02000326 RID: 806
	[DataContract(Name = "DatabaseServerInformation", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public class DatabaseServerInformation
	{
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x00099BF3 File Offset: 0x00097DF3
		// (set) Token: 0x06002117 RID: 8471 RVA: 0x00099BFB File Offset: 0x00097DFB
		[DataMember(Name = "DatabaseGuid", IsRequired = false, Order = 0)]
		public Guid DatabaseGuid
		{
			get
			{
				return this.m_databaseGuid;
			}
			set
			{
				this.m_databaseGuid = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x00099C04 File Offset: 0x00097E04
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x00099C0C File Offset: 0x00097E0C
		[DataMember(Name = "ServerFqdn", IsRequired = false, Order = 1)]
		public string ServerFqdn
		{
			get
			{
				return this.m_serverFqdn;
			}
			set
			{
				this.m_serverFqdn = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00099C15 File Offset: 0x00097E15
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x00099C1D File Offset: 0x00097E1D
		[DataMember(Name = "RequestSentUtc", IsRequired = false, Order = 2)]
		public DateTime RequestSentUtc
		{
			get
			{
				return this.m_requestSentUtc;
			}
			set
			{
				this.m_requestSentUtc = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x00099C26 File Offset: 0x00097E26
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00099C2E File Offset: 0x00097E2E
		[DataMember(Name = "RequestReceivedUtc", IsRequired = false, Order = 3)]
		public DateTime RequestReceivedUtc
		{
			get
			{
				return this.m_requestReceivedUtc;
			}
			set
			{
				this.m_requestReceivedUtc = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x00099C37 File Offset: 0x00097E37
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x00099C3F File Offset: 0x00097E3F
		[DataMember(Name = "ReplySentUtc", IsRequired = false, Order = 4)]
		public DateTime ReplySentUtc
		{
			get
			{
				return this.m_replySentUtc;
			}
			set
			{
				this.m_replySentUtc = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x00099C48 File Offset: 0x00097E48
		// (set) Token: 0x06002121 RID: 8481 RVA: 0x00099C50 File Offset: 0x00097E50
		[DataMember(Name = "ServerVersion", IsRequired = false, Order = 5)]
		public int ServerVersion
		{
			get
			{
				return this.m_serverVersion;
			}
			set
			{
				this.m_serverVersion = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x00099C59 File Offset: 0x00097E59
		// (set) Token: 0x06002123 RID: 8483 RVA: 0x00099C61 File Offset: 0x00097E61
		[DataMember(Name = "MountedTimeUtc", IsRequired = false, Order = 6)]
		public DateTime MountedTimeUtc
		{
			get
			{
				return this.m_mountedTimeUtc;
			}
			set
			{
				this.m_mountedTimeUtc = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x00099C6A File Offset: 0x00097E6A
		// (set) Token: 0x06002125 RID: 8485 RVA: 0x00099C72 File Offset: 0x00097E72
		[DataMember(Name = "LastMountedServerFqdn", IsRequired = false, Order = 7)]
		public string LastMountedServerFqdn
		{
			get
			{
				return this.m_lastMountedServerFqdn;
			}
			set
			{
				this.m_lastMountedServerFqdn = value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x00099C7B File Offset: 0x00097E7B
		// (set) Token: 0x06002127 RID: 8487 RVA: 0x00099C83 File Offset: 0x00097E83
		[DataMember(Name = "FailoverSequenceNumber", IsRequired = false, Order = 8)]
		public long FailoverSequenceNumber
		{
			get
			{
				return this.m_failoverSequenceNumber;
			}
			set
			{
				this.m_failoverSequenceNumber = value;
			}
		}

		// Token: 0x04000D5E RID: 3422
		private Guid m_databaseGuid;

		// Token: 0x04000D5F RID: 3423
		private string m_serverFqdn;

		// Token: 0x04000D60 RID: 3424
		private DateTime m_requestSentUtc;

		// Token: 0x04000D61 RID: 3425
		private DateTime m_requestReceivedUtc;

		// Token: 0x04000D62 RID: 3426
		private DateTime m_replySentUtc;

		// Token: 0x04000D63 RID: 3427
		private int m_serverVersion;

		// Token: 0x04000D64 RID: 3428
		private DateTime m_mountedTimeUtc;

		// Token: 0x04000D65 RID: 3429
		private string m_lastMountedServerFqdn;

		// Token: 0x04000D66 RID: 3430
		private long m_failoverSequenceNumber;
	}
}
