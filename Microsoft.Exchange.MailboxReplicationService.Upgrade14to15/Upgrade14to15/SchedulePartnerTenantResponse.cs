using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000059 RID: 89
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SchedulePartnerTenantResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class SchedulePartnerTenantResponse : IExtensibleDataObject
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00003865 File Offset: 0x00001A65
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000386D File Offset: 0x00001A6D
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

		// Token: 0x040000FC RID: 252
		private ExtensionDataObject extensionDataField;
	}
}
