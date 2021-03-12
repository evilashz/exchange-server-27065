using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200004E RID: 78
	[DebuggerStepThrough]
	[DataContract(Name = "UpdateBlackout", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UpdateBlackout : IExtensibleDataObject
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000036D2 File Offset: 0x000018D2
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000036DA File Offset: 0x000018DA
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000036E3 File Offset: 0x000018E3
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x000036EB File Offset: 0x000018EB
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000036F4 File Offset: 0x000018F4
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x000036FC File Offset: 0x000018FC
		[DataMember(Order = 1)]
		public GroupBlackout blackout
		{
			get
			{
				return this.blackoutField;
			}
			set
			{
				this.blackoutField = value;
			}
		}

		// Token: 0x040000E1 RID: 225
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000E2 RID: 226
		private string workloadNameField;

		// Token: 0x040000E3 RID: 227
		private GroupBlackout blackoutField;
	}
}
