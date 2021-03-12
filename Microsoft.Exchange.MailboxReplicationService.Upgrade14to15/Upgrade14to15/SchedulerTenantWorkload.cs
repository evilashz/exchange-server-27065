using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000034 RID: 52
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "SchedulerTenantWorkload", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.ManagementService")]
	public class SchedulerTenantWorkload : IExtensibleDataObject
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000325B File Offset: 0x0000145B
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00003263 File Offset: 0x00001463
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000326C File Offset: 0x0000146C
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00003274 File Offset: 0x00001474
		[DataMember]
		public int? ConsumedUnits
		{
			get
			{
				return this.ConsumedUnitsField;
			}
			set
			{
				this.ConsumedUnitsField = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000327D File Offset: 0x0000147D
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00003285 File Offset: 0x00001485
		[DataMember]
		public DateTime? ExpirationDate
		{
			get
			{
				return this.ExpirationDateField;
			}
			set
			{
				this.ExpirationDateField = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000328E File Offset: 0x0000148E
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00003296 File Offset: 0x00001496
		[DataMember]
		public string WorkloadName
		{
			get
			{
				return this.WorkloadNameField;
			}
			set
			{
				this.WorkloadNameField = value;
			}
		}

		// Token: 0x040000AA RID: 170
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000AB RID: 171
		private int? ConsumedUnitsField;

		// Token: 0x040000AC RID: 172
		private DateTime? ExpirationDateField;

		// Token: 0x040000AD RID: 173
		private string WorkloadNameField;
	}
}
