using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200004F RID: 79
	[DataContract(Name = "UpdateBlackoutResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UpdateBlackoutResponse : IExtensibleDataObject
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000370D File Offset: 0x0000190D
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00003715 File Offset: 0x00001915
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

		// Token: 0x040000E4 RID: 228
		private ExtensionDataObject extensionDataField;
	}
}
