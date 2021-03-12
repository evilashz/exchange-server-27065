using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000051 RID: 81
	[DataContract(Name = "DeleteConstraintResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class DeleteConstraintResponse : IExtensibleDataObject
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00003761 File Offset: 0x00001961
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00003769 File Offset: 0x00001969
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

		// Token: 0x040000E8 RID: 232
		private ExtensionDataObject extensionDataField;
	}
}
