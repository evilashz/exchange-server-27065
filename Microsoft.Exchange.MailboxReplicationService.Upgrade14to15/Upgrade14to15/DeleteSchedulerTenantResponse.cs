using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000037 RID: 55
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DeleteSchedulerTenantResponse", Namespace = "http://tempuri.org/")]
	public class DeleteSchedulerTenantResponse : IExtensibleDataObject
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000032EA File Offset: 0x000014EA
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000032F2 File Offset: 0x000014F2
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

		// Token: 0x040000B1 RID: 177
		private ExtensionDataObject extensionDataField;
	}
}
