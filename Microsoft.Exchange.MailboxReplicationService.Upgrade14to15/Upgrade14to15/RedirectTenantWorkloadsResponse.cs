using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200001D RID: 29
	[DataContract(Name = "RedirectTenantWorkloadsResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class RedirectTenantWorkloadsResponse : IExtensibleDataObject
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002CDB File Offset: 0x00000EDB
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002CE3 File Offset: 0x00000EE3
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

		// Token: 0x04000062 RID: 98
		private ExtensionDataObject extensionDataField;
	}
}
