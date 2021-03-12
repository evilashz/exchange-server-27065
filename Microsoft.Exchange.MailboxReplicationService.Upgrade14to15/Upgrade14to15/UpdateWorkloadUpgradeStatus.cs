using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000060 RID: 96
	[DebuggerStepThrough]
	[DataContract(Name = "UpdateWorkloadUpgradeStatus", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UpdateWorkloadUpgradeStatus : IExtensibleDataObject
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000398B File Offset: 0x00001B8B
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00003993 File Offset: 0x00001B93
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000399C File Offset: 0x00001B9C
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000039A4 File Offset: 0x00001BA4
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000039AD File Offset: 0x00001BAD
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000039B5 File Offset: 0x00001BB5
		[DataMember]
		public string workloadName
		{
			get
			{
				return this.workloadNameField;
			}
			set
			{
				this.workloadNameField = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000039BE File Offset: 0x00001BBE
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000039C6 File Offset: 0x00001BC6
		[DataMember(Order = 2)]
		public WorkItemStatus status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x0400010A RID: 266
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400010B RID: 267
		private Guid tenantIdField;

		// Token: 0x0400010C RID: 268
		private string workloadNameField;

		// Token: 0x0400010D RID: 269
		private WorkItemStatus statusField;
	}
}
