using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000017 RID: 23
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "AddTenantResponse", Namespace = "http://tempuri.org/")]
	public class AddTenantResponse : IExtensibleDataObject
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002BDF File Offset: 0x00000DDF
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002BE7 File Offset: 0x00000DE7
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

		// Token: 0x04000056 RID: 86
		private ExtensionDataObject extensionDataField;
	}
}
