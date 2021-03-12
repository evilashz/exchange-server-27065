using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200001A RID: 26
	[DataContract(Name = "GetTenant", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetTenant : IExtensibleDataObject
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002C3B File Offset: 0x00000E3B
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002C43 File Offset: 0x00000E43
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002C4C File Offset: 0x00000E4C
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002C54 File Offset: 0x00000E54
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

		// Token: 0x0400005A RID: 90
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400005B RID: 91
		private Guid tenantIdField;
	}
}
