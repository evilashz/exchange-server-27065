using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000B1 RID: 177
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
	public class ArgumentFault : IExtensibleDataObject
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00009033 File Offset: 0x00007233
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0000903B File Offset: 0x0000723B
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

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00009044 File Offset: 0x00007244
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0000904C File Offset: 0x0000724C
		[DataMember]
		public string ArgumentName
		{
			get
			{
				return this.ArgumentNameField;
			}
			set
			{
				this.ArgumentNameField = value;
			}
		}

		// Token: 0x04000288 RID: 648
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000289 RID: 649
		private string ArgumentNameField;
	}
}
