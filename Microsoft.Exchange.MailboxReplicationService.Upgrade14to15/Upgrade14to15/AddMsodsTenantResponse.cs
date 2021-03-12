using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000047 RID: 71
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AddMsodsTenantResponse", Namespace = "http://tempuri.org/")]
	public class AddMsodsTenantResponse : IExtensibleDataObject
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000035AC File Offset: 0x000017AC
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000035B4 File Offset: 0x000017B4
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000035BD File Offset: 0x000017BD
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000035C5 File Offset: 0x000017C5
		[DataMember]
		public Guid AddMsodsTenantResult
		{
			get
			{
				return this.AddMsodsTenantResultField;
			}
			set
			{
				this.AddMsodsTenantResultField = value;
			}
		}

		// Token: 0x040000D3 RID: 211
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000D4 RID: 212
		private Guid AddMsodsTenantResultField;
	}
}
