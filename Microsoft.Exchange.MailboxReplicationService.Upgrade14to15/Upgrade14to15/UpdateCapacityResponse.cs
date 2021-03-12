using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200004D RID: 77
	[DataContract(Name = "UpdateCapacityResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UpdateCapacityResponse : IExtensibleDataObject
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000036B9 File Offset: 0x000018B9
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x000036C1 File Offset: 0x000018C1
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

		// Token: 0x040000E0 RID: 224
		private ExtensionDataObject extensionDataField;
	}
}
