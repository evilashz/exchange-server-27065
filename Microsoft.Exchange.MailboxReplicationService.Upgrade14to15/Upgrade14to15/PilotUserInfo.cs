using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000072 RID: 114
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "PilotUserInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.WorkloadService")]
	[DebuggerStepThrough]
	public class PilotUserInfo : IExtensibleDataObject
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00003D5C File Offset: 0x00001F5C
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00003D64 File Offset: 0x00001F64
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

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00003D6D File Offset: 0x00001F6D
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00003D75 File Offset: 0x00001F75
		[DataMember]
		public Guid PilotUserId
		{
			get
			{
				return this.PilotUserIdField;
			}
			set
			{
				this.PilotUserIdField = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00003D7E File Offset: 0x00001F7E
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00003D86 File Offset: 0x00001F86
		[DataMember]
		public string Upn
		{
			get
			{
				return this.UpnField;
			}
			set
			{
				this.UpnField = value;
			}
		}

		// Token: 0x0400013B RID: 315
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400013C RID: 316
		private Guid PilotUserIdField;

		// Token: 0x0400013D RID: 317
		private string UpnField;
	}
}
