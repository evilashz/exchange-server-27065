using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200006C RID: 108
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UserWorkloadStatusInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.SuiteService")]
	[DebuggerStepThrough]
	public class UserWorkloadStatusInfo : IExtensibleDataObject
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00003BC7 File Offset: 0x00001DC7
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00003BCF File Offset: 0x00001DCF
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00003BD8 File Offset: 0x00001DD8
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00003BE0 File Offset: 0x00001DE0
		[DataMember]
		public WorkloadStatus[] Status
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00003BE9 File Offset: 0x00001DE9
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00003BF1 File Offset: 0x00001DF1
		[DataMember]
		public UserId User
		{
			get
			{
				return this.UserField;
			}
			set
			{
				this.UserField = value;
			}
		}

		// Token: 0x04000126 RID: 294
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000127 RID: 295
		private WorkloadStatus[] StatusField;

		// Token: 0x04000128 RID: 296
		private UserId UserField;
	}
}
