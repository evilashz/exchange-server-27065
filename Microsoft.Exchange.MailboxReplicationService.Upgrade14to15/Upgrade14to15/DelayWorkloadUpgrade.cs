using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200006A RID: 106
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DelayWorkloadUpgrade", Namespace = "http://tempuri.org/")]
	public class DelayWorkloadUpgrade : IExtensibleDataObject
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00003B73 File Offset: 0x00001D73
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00003B7B File Offset: 0x00001D7B
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00003B84 File Offset: 0x00001D84
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00003B8C File Offset: 0x00001D8C
		[DataMember]
		public Guid tenantId
		{
			get
			{
				return this.tenantIdField;
			}
			set
			{
				this.tenantIdField = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00003B95 File Offset: 0x00001D95
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00003B9D File Offset: 0x00001D9D
		[DataMember]
		public string[] workloadsToDelay
		{
			get
			{
				return this.workloadsToDelayField;
			}
			set
			{
				this.workloadsToDelayField = value;
			}
		}

		// Token: 0x04000122 RID: 290
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000123 RID: 291
		private Guid tenantIdField;

		// Token: 0x04000124 RID: 292
		private string[] workloadsToDelayField;
	}
}
