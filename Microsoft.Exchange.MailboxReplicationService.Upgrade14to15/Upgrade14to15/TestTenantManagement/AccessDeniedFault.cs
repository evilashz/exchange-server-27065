using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000B2 RID: 178
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
	public class AccessDeniedFault : IExtensibleDataObject
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000905D File Offset: 0x0000725D
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00009065 File Offset: 0x00007265
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

		// Token: 0x0400028A RID: 650
		private ExtensionDataObject extensionDataField;
	}
}
