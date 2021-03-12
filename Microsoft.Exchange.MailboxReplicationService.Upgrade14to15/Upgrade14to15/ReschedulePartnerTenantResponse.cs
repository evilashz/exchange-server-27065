using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000065 RID: 101
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ReschedulePartnerTenantResponse", Namespace = "http://tempuri.org/")]
	public class ReschedulePartnerTenantResponse : IExtensibleDataObject
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00003AA1 File Offset: 0x00001CA1
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00003AA9 File Offset: 0x00001CA9
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

		// Token: 0x04000118 RID: 280
		private ExtensionDataObject extensionDataField;
	}
}
