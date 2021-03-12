using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000086 RID: 134
	[DataContract(Name = "UpdateTenantReadiness", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class UpdateTenantReadiness : IExtensibleDataObject
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000431C File Offset: 0x0000251C
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00004324 File Offset: 0x00002524
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

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000432D File Offset: 0x0000252D
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00004335 File Offset: 0x00002535
		[DataMember]
		public TenantReadiness[] tenantReadiness
		{
			get
			{
				return this.tenantReadinessField;
			}
			set
			{
				this.tenantReadinessField = value;
			}
		}

		// Token: 0x0400018D RID: 397
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400018E RID: 398
		private TenantReadiness[] tenantReadinessField;
	}
}
