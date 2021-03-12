using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000078 RID: 120
	[DataContract(Name = "CancelTenantUpgrade", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class CancelTenantUpgrade : IExtensibleDataObject
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00003ECF File Offset: 0x000020CF
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00003ED7 File Offset: 0x000020D7
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00003EE0 File Offset: 0x000020E0
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00003EE8 File Offset: 0x000020E8
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

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00003EF1 File Offset: 0x000020F1
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00003EF9 File Offset: 0x000020F9
		[DataMember(Order = 1)]
		public string reason
		{
			get
			{
				return this.reasonField;
			}
			set
			{
				this.reasonField = value;
			}
		}

		// Token: 0x0400014E RID: 334
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400014F RID: 335
		private Guid tenantIdField;

		// Token: 0x04000150 RID: 336
		private string reasonField;
	}
}
