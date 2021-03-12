using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000022 RID: 34
	[DataContract(Name = "PilotUser", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.ManagementService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class PilotUser : IExtensibleDataObject
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002E57 File Offset: 0x00001057
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002E5F File Offset: 0x0000105F
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002E68 File Offset: 0x00001068
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002E70 File Offset: 0x00001070
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002E79 File Offset: 0x00001079
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002E81 File Offset: 0x00001081
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

		// Token: 0x04000076 RID: 118
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000077 RID: 119
		private Guid PilotUserIdField;

		// Token: 0x04000078 RID: 120
		private string UpnField;
	}
}
