using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000036 RID: 54
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "DeleteSchedulerTenant", Namespace = "http://tempuri.org/")]
	public class DeleteSchedulerTenant : IExtensibleDataObject
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000032C0 File Offset: 0x000014C0
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000032C8 File Offset: 0x000014C8
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000032D1 File Offset: 0x000014D1
		// (set) Token: 0x0600017B RID: 379 RVA: 0x000032D9 File Offset: 0x000014D9
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

		// Token: 0x040000AF RID: 175
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000B0 RID: 176
		private Guid tenantIdField;
	}
}
