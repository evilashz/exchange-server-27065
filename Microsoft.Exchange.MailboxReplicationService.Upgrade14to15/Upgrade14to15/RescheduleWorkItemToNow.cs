using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000066 RID: 102
	[DataContract(Name = "RescheduleWorkItemToNow", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RescheduleWorkItemToNow : IExtensibleDataObject
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00003ABA File Offset: 0x00001CBA
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00003AC2 File Offset: 0x00001CC2
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

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00003ACB File Offset: 0x00001CCB
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00003AD3 File Offset: 0x00001CD3
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

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00003ADC File Offset: 0x00001CDC
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00003AE4 File Offset: 0x00001CE4
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

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00003AED File Offset: 0x00001CED
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00003AF5 File Offset: 0x00001CF5
		[DataMember(Order = 2)]
		public string workItemType
		{
			get
			{
				return this.workItemTypeField;
			}
			set
			{
				this.workItemTypeField = value;
			}
		}

		// Token: 0x04000119 RID: 281
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400011A RID: 282
		private Guid tenantIdField;

		// Token: 0x0400011B RID: 283
		private string workloadNameField;

		// Token: 0x0400011C RID: 284
		private string workItemTypeField;
	}
}
