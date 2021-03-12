using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200002A RID: 42
	[DataContract(Name = "GetWorkItemsByTenant", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetWorkItemsByTenant : IExtensibleDataObject
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00002F96 File Offset: 0x00001196
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00002F9E File Offset: 0x0000119E
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00002FA7 File Offset: 0x000011A7
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00002FAF File Offset: 0x000011AF
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

		// Token: 0x04000085 RID: 133
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000086 RID: 134
		private Guid tenantIdField;
	}
}
