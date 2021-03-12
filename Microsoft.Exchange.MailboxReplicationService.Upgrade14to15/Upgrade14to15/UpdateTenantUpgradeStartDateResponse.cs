using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200001F RID: 31
	[DataContract(Name = "UpdateTenantUpgradeStartDateResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class UpdateTenantUpgradeStartDateResponse : IExtensibleDataObject
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002D2F File Offset: 0x00000F2F
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002D37 File Offset: 0x00000F37
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

		// Token: 0x04000066 RID: 102
		private ExtensionDataObject extensionDataField;
	}
}
