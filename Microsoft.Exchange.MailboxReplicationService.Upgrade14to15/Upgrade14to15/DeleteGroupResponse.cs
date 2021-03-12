using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200004B RID: 75
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DeleteGroupResponse", Namespace = "http://tempuri.org/")]
	public class DeleteGroupResponse : IExtensibleDataObject
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00003665 File Offset: 0x00001865
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000366D File Offset: 0x0000186D
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

		// Token: 0x040000DC RID: 220
		private ExtensionDataObject extensionDataField;
	}
}
