using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000068 RID: 104
	[DataContract(Name = "GetTenantPhase", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetTenantPhase : IExtensibleDataObject
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00003B1F File Offset: 0x00001D1F
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00003B27 File Offset: 0x00001D27
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

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00003B30 File Offset: 0x00001D30
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00003B38 File Offset: 0x00001D38
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

		// Token: 0x0400011E RID: 286
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400011F RID: 287
		private Guid tenantIdField;
	}
}
