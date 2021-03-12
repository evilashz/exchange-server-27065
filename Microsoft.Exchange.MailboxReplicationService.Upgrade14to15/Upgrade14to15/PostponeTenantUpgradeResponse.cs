using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200005F RID: 95
	[DataContract(Name = "PostponeTenantUpgradeResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class PostponeTenantUpgradeResponse : IExtensibleDataObject
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00003972 File Offset: 0x00001B72
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000397A File Offset: 0x00001B7A
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

		// Token: 0x04000109 RID: 265
		private ExtensionDataObject extensionDataField;
	}
}
