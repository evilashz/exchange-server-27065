using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000081 RID: 129
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "TenantInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.WorkloadService")]
	[DebuggerStepThrough]
	public class TenantInfo : IExtensibleDataObject
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000332 RID: 818 RVA: 0x000041B1 File Offset: 0x000023B1
		// (set) Token: 0x06000333 RID: 819 RVA: 0x000041B9 File Offset: 0x000023B9
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000334 RID: 820 RVA: 0x000041C2 File Offset: 0x000023C2
		// (set) Token: 0x06000335 RID: 821 RVA: 0x000041CA File Offset: 0x000023CA
		[DataMember]
		public string InitialDomain
		{
			get
			{
				return this.InitialDomainField;
			}
			set
			{
				this.InitialDomainField = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000336 RID: 822 RVA: 0x000041D3 File Offset: 0x000023D3
		// (set) Token: 0x06000337 RID: 823 RVA: 0x000041DB File Offset: 0x000023DB
		[DataMember]
		public string PrimaryDomain
		{
			get
			{
				return this.PrimaryDomainField;
			}
			set
			{
				this.PrimaryDomainField = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000041E4 File Offset: 0x000023E4
		// (set) Token: 0x06000339 RID: 825 RVA: 0x000041EC File Offset: 0x000023EC
		[DataMember]
		public DateTime? ScheduledUpgradeDate
		{
			get
			{
				return this.ScheduledUpgradeDateField;
			}
			set
			{
				this.ScheduledUpgradeDateField = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600033A RID: 826 RVA: 0x000041F5 File Offset: 0x000023F5
		// (set) Token: 0x0600033B RID: 827 RVA: 0x000041FD File Offset: 0x000023FD
		[DataMember]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00004206 File Offset: 0x00002406
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000420E File Offset: 0x0000240E
		[DataMember]
		public string Tier
		{
			get
			{
				return this.TierField;
			}
			set
			{
				this.TierField = value;
			}
		}

		// Token: 0x0400017A RID: 378
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400017B RID: 379
		private string InitialDomainField;

		// Token: 0x0400017C RID: 380
		private string PrimaryDomainField;

		// Token: 0x0400017D RID: 381
		private DateTime? ScheduledUpgradeDateField;

		// Token: 0x0400017E RID: 382
		private Guid TenantIdField;

		// Token: 0x0400017F RID: 383
		private string TierField;
	}
}
