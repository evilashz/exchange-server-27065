using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000039 RID: 57
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "GetSchedulerTenantResponse", Namespace = "http://tempuri.org/")]
	public class GetSchedulerTenantResponse : IExtensibleDataObject
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000332D File Offset: 0x0000152D
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00003335 File Offset: 0x00001535
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000333E File Offset: 0x0000153E
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00003346 File Offset: 0x00001546
		[DataMember]
		public SchedulerTenant GetSchedulerTenantResult
		{
			get
			{
				return this.GetSchedulerTenantResultField;
			}
			set
			{
				this.GetSchedulerTenantResultField = value;
			}
		}

		// Token: 0x040000B4 RID: 180
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000B5 RID: 181
		private SchedulerTenant GetSchedulerTenantResultField;
	}
}
