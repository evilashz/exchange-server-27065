using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200006D RID: 109
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "WorkloadStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.SuiteService")]
	public class WorkloadStatus : IExtensibleDataObject
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00003C02 File Offset: 0x00001E02
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00003C0A File Offset: 0x00001E0A
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00003C13 File Offset: 0x00001E13
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00003C1B File Offset: 0x00001E1B
		[DataMember]
		public WorkItemStatus Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00003C24 File Offset: 0x00001E24
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00003C2C File Offset: 0x00001E2C
		[DataMember]
		public string Workload
		{
			get
			{
				return this.WorkloadField;
			}
			set
			{
				this.WorkloadField = value;
			}
		}

		// Token: 0x04000129 RID: 297
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400012A RID: 298
		private WorkItemStatus StatusField;

		// Token: 0x0400012B RID: 299
		private string WorkloadField;
	}
}
