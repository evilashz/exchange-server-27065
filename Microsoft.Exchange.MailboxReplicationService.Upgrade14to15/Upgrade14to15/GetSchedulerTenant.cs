using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000038 RID: 56
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetSchedulerTenant", Namespace = "http://tempuri.org/")]
	public class GetSchedulerTenant : IExtensibleDataObject
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003303 File Offset: 0x00001503
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000330B File Offset: 0x0000150B
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00003314 File Offset: 0x00001514
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000331C File Offset: 0x0000151C
		[DataMember]
		public Guid tenantId
		{
			get
			{
				return this.tenantIdField;
			}
			set
			{
				this.tenantIdField = value;
			}
		}

		// Token: 0x040000B2 RID: 178
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000B3 RID: 179
		private Guid tenantIdField;
	}
}
