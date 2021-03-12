using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000019 RID: 25
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DeleteTenantResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class DeleteTenantResponse : IExtensibleDataObject
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002C22 File Offset: 0x00000E22
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00002C2A File Offset: 0x00000E2A
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

		// Token: 0x04000059 RID: 89
		private ExtensionDataObject extensionDataField;
	}
}
