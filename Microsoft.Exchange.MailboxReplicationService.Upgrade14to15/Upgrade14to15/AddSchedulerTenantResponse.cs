using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000035 RID: 53
	[DebuggerStepThrough]
	[DataContract(Name = "AddSchedulerTenantResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddSchedulerTenantResponse : IExtensibleDataObject
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000032A7 File Offset: 0x000014A7
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000032AF File Offset: 0x000014AF
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

		// Token: 0x040000AE RID: 174
		private ExtensionDataObject extensionDataField;
	}
}
